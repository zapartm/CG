using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Windows.Media.Media3D;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using static lab4.Commons;

namespace lab4
{
    public partial class MainForm : Form
    {
        Bitmap bmp;
        readonly Color BackgroundColor = System.Drawing.Color.FromArgb(30, 30, 30);
        readonly Color[] colorsOfNewPrimitives = { Color.BlueViolet, Color.DeepPink, Color.LightSalmon, Color.DarkRed, Color.OliveDrab, Color.AliceBlue };

        List<Primitive> objects;
        List<Camera> cameras;
        Primitive activeObject;
        Camera activeCamera;
        PhongModel lightModel;
        Settings settings = Settings.GetInstance();
        ObjectProperties properties = new ObjectProperties();

        private Dictionary<ObjectType, BaseControl> UserControls = new Dictionary<ObjectType, BaseControl>();

        public MainForm()
        {
            InitializeComponent();
            ContextMenu cm = new ContextMenu();
            cm.MenuItems.Add("Add Box", new EventHandler((sender, args) => AddPrimitive(ObjectType.Box)));
            cm.MenuItems.Add("Add Sphere", new EventHandler((sender, args) => AddPrimitive(ObjectType.Sphere)));
            cm.MenuItems.Add("Add Cone", new EventHandler((sender, args) => AddPrimitive(ObjectType.Cone)));
            cm.MenuItems.Add("Add Cylinder", new EventHandler((sender, args) => AddPrimitive(ObjectType.Cylinder)));
            listBox1.ContextMenu = cm;

            bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            this.pictureBox1.Image = bmp;
            objects = new List<Primitive>();
            cameras = new List<Camera>();
            activeCamera = new Camera(new Vector3D(2, 4, 5), new Vector3D(0, 0, 0), 30);
            lightModel = new PhongModel(0, 4, 4, LightType.Gouraud); // LIGHT
            cameras.Add(activeCamera);
            listBox2.Items.Add(activeCamera);

            RenderScene();
        }

        void RenderScene()
        {
            lightModel.lightType = settings.Light;
            System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
            stopwatch.Start();

            List<Triangle> triangles = new List<Triangle>();
            if (objects != null)
            {
                foreach (var v in objects)
                {
                    triangles.AddRange(v.GetTriangles());
                }
            }

            Matrix4x4 viewMatrix = AuxiliaryMethods.CreateViewMatrix(activeCamera.position, activeCamera.target, new Vector3D(0, 1, 0));
            Matrix4x4 projectionMatrix = AuxiliaryMethods.CreateProjectionMatrix(activeCamera.fov, 1, 10, pictureBox1.Width, pictureBox1.Height);
            Matrix4x4 PVM = projectionMatrix * viewMatrix;

            #region show axis 
            Vector4D center = new Vector4D(0, 0, 0, 1);
            Tuple<Vector4D, Vector4D, Vector4D> axis = new Tuple<Vector4D, Vector4D, Vector4D>(new Vector4D(3, 0, 0, 1), new Vector4D(0, 3, 0, 1), new Vector4D(0, 0, 3, 1));
            axis = new Tuple<Vector4D, Vector4D, Vector4D>(PVM * axis.Item1, PVM * axis.Item2, PVM * axis.Item3);
            center = PVM * center;
            #endregion

            int n = triangles.Count;
            for (int i = 0; i < n; i++)
            {
                triangles[i].ApplyTransformationMatrix(viewMatrix);
                triangles[i].ApplyTransformationMatrix(projectionMatrix);
                triangles[i].TransformToNDC();
            }

            // backface culling
            if (settings.BackfaceCulling)
            {
                triangles.RemoveAll(t => { return !checkIsClockWise(t); });
            }

            int VIEWPORT_WIDTH = pictureBox1.Width;
            int VIEWPORT_HEIGHT = pictureBox1.Height;
            for (int i = 0; i < triangles.Count; i++)
            {
                triangles[i].TransformToScreen(VIEWPORT_WIDTH, VIEWPORT_HEIGHT);
            }

            // remove all triangles that are entirely outside the viewport
            triangles.RemoveAll(t =>
            {
                return (t.vertices[0].values[0] < 0 && t.vertices[1].values[0] < 0 && t.vertices[2].values[0] < 0) ||
                       (t.vertices[0].values[0] > VIEWPORT_WIDTH && t.vertices[1].values[0] > VIEWPORT_WIDTH && t.vertices[2].values[0] > VIEWPORT_WIDTH) ||
                       (t.vertices[0].values[1] < 0 && t.vertices[1].values[1] < 0 && t.vertices[2].values[1] < 0) ||
                       (t.vertices[0].values[1] > VIEWPORT_HEIGHT && t.vertices[1].values[1] > VIEWPORT_HEIGHT && t.vertices[2].values[1] > VIEWPORT_HEIGHT);
            });

            Color?[,] pixelColors;
            double[,] zbuff = AuxiliaryMethods.CreateZbufferArray(triangles, out pixelColors, pictureBox1.Height, pictureBox1.Width, lightModel, activeCamera);

            double min = 2, max = -1;
            for (int i = 0; i < zbuff.GetLength(0); i++)
            {
                for (int j = 0; j < zbuff.GetLength(1); j++)
                {
                    if (zbuff[i, j] < min)
                        min = zbuff[i, j];
                    if (zbuff[i, j] < 1 && zbuff[i, j] > max)
                        max = zbuff[i, j];
                }
            }

            using (Graphics g = Graphics.FromImage(pictureBox1.Image))
            {
                g.Clear(BackgroundColor);
                g.DrawLine(Pens.Red, Get2DPointWithScaling(center), Get2DPointWithScaling(axis.Item1));
                g.DrawLine(Pens.Green, Get2DPointWithScaling(center), Get2DPointWithScaling(axis.Item2));
                g.DrawLine(Pens.Yellow, Get2DPointWithScaling(center), Get2DPointWithScaling(axis.Item3));

                unsafe
                {
                    BitmapData bitmapData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite, bmp.PixelFormat);
                    int bytesPerPixel = Image.GetPixelFormatSize(bmp.PixelFormat) / 8;
                    int heightInPixels = bitmapData.Height;
                    int widthInBytes = bitmapData.Width * bytesPerPixel;
                    byte* ptrFirstPixel = (byte*)bitmapData.Scan0;

                    int i = 0, j = 0;
                    for (int y = 0; y < heightInPixels; y++, j = 0, i++)
                    {
                        byte* currentLine = ptrFirstPixel + (y * bitmapData.Stride);
                        for (int x = 0; x < widthInBytes; x = x + bytesPerPixel, j++)
                        {
                            if (i < pixelColors.GetLength(0) && j < pixelColors.GetLength(1) && pixelColors[i, j] != null)
                            {
                                currentLine[x] = (byte)pixelColors[i, j].Value.B;
                                currentLine[x + 1] = (byte)pixelColors[i, j].Value.G;
                                currentLine[x + 2] = (byte)pixelColors[i, j].Value.R;
                            }
                        }
                    }

                    bmp.UnlockBits(bitmapData);
                }

                #region  normals preview
                if (settings.ShowNormals)
                {
                    var normalVectors = new List<Tuple<Vector4D, Vector4D>>();
                    foreach (var o in objects)
                    {
                        foreach (var t in o.GetTriangles())
                        {
                            for (int i = 0; i < 3; i++)
                            {
                                normalVectors.Add(new Tuple<Vector4D, Vector4D>(t.vertices[i], t.normalVectors[i]));
                            }
                        }
                    }
                    for (int i = 0; i < normalVectors.Count; i++)
                    {
                        normalVectors[i] = new Tuple<Vector4D, Vector4D>(PVM * normalVectors[i].Item1, PVM * normalVectors[i].Item2);
                    }

                    foreach (var tup in normalVectors)
                    {
                        Pen p = Pens.White;
                        g.DrawLine(p, Get2DPointWithScaling(tup.Item1), Get2DPointWithScaling(tup.Item2));
                    }
                }
                #endregion

                #region show mesh
                if (settings.ShowMesh)
                {
                    Pen p = Pens.White;
                    foreach (var v in triangles)
                    {
                        g.DrawLine(p, Get2DPoint(v.vertices[0]), Get2DPoint(v.vertices[1]));
                        g.DrawLine(p, Get2DPoint(v.vertices[1]), Get2DPoint(v.vertices[2]));
                        g.DrawLine(p, Get2DPoint(v.vertices[2]), Get2DPoint(v.vertices[0]));
                    }
                }
                #endregion

            }

            pictureBox1.Refresh();
            stopwatch.Stop();
            var elapsedTime = stopwatch.ElapsedMilliseconds;
            textBox10.Text = "FPS: " + (1000 / elapsedTime).ToString("0.0");
        }

        private bool checkIsClockWise(Triangle t)
        {
            double ax = t.vertices[0].values[0];
            double ay = t.vertices[0].values[1];
            double bx = t.vertices[1].values[0];
            double by = t.vertices[1].values[1];
            double cx = t.vertices[2].values[0];
            double cy = t.vertices[2].values[1];

            return ((bx - ax) * (cy - ay)) - ((by - ay) * (cx - ax)) <= 0;
        }

        public Point Get2DPoint(Vector4D v)
        {
            return new Point((int)(v.values[0]), (int)(v.values[1]));
        }

        public Point Get2DPointWithScaling(Vector4D v)
        {
            if (v.values[3] > -0.000001 && v.values[3] < 0.000001) return new Point(0, 0);
            int h = pictureBox1.Height;
            int w = pictureBox1.Width;
            return new Point((int)(((v.values[0] / v.values[3]) + 1) * w / 2), (int)(((v.values[1] / v.values[3]) + 1) * h / 2));
        }

        bool isInitialRun = true;
        private void Form1_Resize(object sender, EventArgs e)
        {
            bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            this.pictureBox1.Image = bmp;
            //if (!isInitialRun)
            //{
            //    RenderScene();
            //    isInitialRun = false;
            //}
            RenderScene();
        }

        #region mouse event handlers
        private bool mouseDown = false;
        Point lastMousePosition;
        int movementCounterX = 0;
        int movementCounterY = 0;
        int speed = 3;
        double change = 0.1;
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Cursor = Cursors.Hand;
                lastMousePosition = new Point(e.Location.X, e.Location.Y);
                mouseDown = true;
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                if (Math.Abs(movementCounterX) > speed)
                {
                    if (movementCounterX < 0)
                    {
                        activeCamera.MoveTarget(-change, 0, 0);
                        RenderScene();
                    }
                    else
                    {
                        activeCamera.MoveTarget(change, 0, 0);
                        RenderScene();
                    }
                    movementCounterX = 0;
                    //movementCounterY = 0;
                }
                else
                {
                    if (lastMousePosition.X > e.Location.X)
                        movementCounterX--;
                    else
                        movementCounterX++;
                }

                if (Math.Abs(movementCounterY) > speed)
                {
                    if (movementCounterY > 0)
                    {
                        activeCamera.MoveTarget(0, change, 0);
                        RenderScene();
                    }
                    else
                    {
                        activeCamera.MoveTarget(0, -change, 0);
                        RenderScene();
                    }
                    //movementCounterX = 0;
                    movementCounterY = 0;
                }
                else
                {
                    if (lastMousePosition.Y > e.Location.Y)
                        movementCounterY--;
                    else
                        movementCounterY++;
                }

                lastMousePosition = new Point(e.Location.X, e.Location.Y);
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Cursor = Cursors.Default;
                mouseDown = false;
            }
        }
        #endregion


        public void AddPrimitive(ObjectType type)
        {
            activeObject = null;
            Primitive primitive = null;
            switch (type)
            {
                case ObjectType.Box:
                    primitive = new Box(colorsOfNewPrimitives[objects.Count % colorsOfNewPrimitives.Length]);
                    break;
                case ObjectType.Cone:
                    primitive = new Cone(colorsOfNewPrimitives[objects.Count % colorsOfNewPrimitives.Length]);
                    break;
                case ObjectType.Cylinder:
                    primitive = new Cylinder(colorsOfNewPrimitives[objects.Count % colorsOfNewPrimitives.Length]);
                    break;
                case ObjectType.Sphere:
                    primitive = new Sphere(colorsOfNewPrimitives[objects.Count % colorsOfNewPrimitives.Length]);
                    break;
            }

            objects.Add(primitive);
            listBox1.Items.Add(objects[objects.Count - 1]);
            RenderScene();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            activeObject = listBox1.SelectedItem as Primitive;
            if (activeObject == null) return;

            this.SuspendLayout();
            textBox1.Text = activeObject.translationX.ToString("0.0");
            textBox2.Text = activeObject.translationY.ToString("0.0");
            textBox3.Text = activeObject.translationZ.ToString("0.0");

            textBox4.Text = activeObject.rotationX.ToString("0");
            textBox5.Text = activeObject.rotationY.ToString("0");
            textBox6.Text = activeObject.rotationZ.ToString("0");

            textBox7.Text = activeObject.ScaleX.ToString("0.0");
            textBox8.Text = activeObject.ScaleY.ToString("0.0");
            textBox9.Text = activeObject.ScaleZ.ToString("0.0");

            this.panel3.Controls.Clear();
            var type = activeObject.GetType;
            this.properties = activeObject.CreateProperties();
            BaseControl tmpControl;
            switch (type)
            {
                case ObjectType.Box:
                    this.panel3.Controls.Add(UserControls[ObjectType.Box]);
                    break;
                case ObjectType.Sphere:
                    this.panel3.Controls.Add(UserControls[ObjectType.Sphere]);
                    break;
                case ObjectType.Cone:
                    this.panel3.Controls.Add(UserControls[ObjectType.Cone]);
                    break;
                case ObjectType.Cylinder:
                    this.panel3.Controls.Add(UserControls[ObjectType.Cylinder]);
                    break;
                default:
                    throw new ApplicationException("Unknown primitive type");
            }
            if (panel3.Controls.Count > 0)
            {
                BaseControl control = panel3.Controls[0] as BaseControl;
                control.Properties = this.properties;
                control.RefreshData();
                control.apply_Button.Click += ((_sender, _e) => activeObject.ApplyProperties(this.properties));
                control.apply_Button.Click += ((_sender, _e) => RenderScene());
            }

            this.ResumeLayout(false);
        }


        private void applyTRS_click(object sender, EventArgs e)
        {
            if (activeObject == null) return;
            textBox1.Text = textBox1.Text.Replace('.', ',');
            textBox2.Text = textBox2.Text.Replace('.', ',');
            textBox3.Text = textBox3.Text.Replace('.', ',');
            textBox4.Text = textBox4.Text.Replace('.', ',');
            textBox5.Text = textBox5.Text.Replace('.', ',');
            textBox6.Text = textBox6.Text.Replace('.', ',');
            textBox7.Text = textBox7.Text.Replace('.', ',');
            textBox8.Text = textBox8.Text.Replace('.', ',');
            textBox9.Text = textBox9.Text.Replace('.', ',');

            double dx, dy, dz;
            if (double.TryParse(textBox1.Text, out dx) && double.TryParse(textBox2.Text, out dy) && double.TryParse(textBox3.Text, out dz))
                activeObject.Translate(dx, dy, dz);

            int rx, ry, rz;
            if (int.TryParse(textBox4.Text, out rx) && int.TryParse(textBox5.Text, out ry) && int.TryParse(textBox6.Text, out rz))
                activeObject.Rotate(rx, ry, rz);

            double sx, sy, sz;
            if (double.TryParse(textBox7.Text, out sx) && double.TryParse(textBox8.Text, out sy) && double.TryParse(textBox9.Text, out sz))
                activeObject.Scale(sx, sy, sz);

            RenderScene();
        }

        #region Tool Strip handlers
        public void Clear()
        {
            activeObject = null;
            objects.Clear();
            listBox1.Items.Clear();
            RenderScene();
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stream stream = File.Open("Scene.sc", FileMode.Create);
            BinaryFormatter bformatter = new BinaryFormatter();
            bformatter.Serialize(stream, objects);
            stream.Close();
            Clear();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clear();
            try
            {
                Stream stream = File.Open("Scene.sc", FileMode.Open);
                var bformatter = new BinaryFormatter();

                objects = (List<Primitive>)bformatter.Deserialize(stream);
                foreach (var o in objects)
                    listBox1.Items.Add(o);
                stream.Close();
                RenderScene();
            }
            catch (Exception ee)
            {
                if(ee is FileNotFoundException)
                {
                    MessageBox.Show("No saved scenes found");
                }
            }

        }
        #endregion

        // Light tab
        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (activeCamera == null) return;
            activeCamera = listBox2.SelectedItem as Camera;
            this.panel3.Controls.Clear();
            this.properties = activeCamera.CreateProperties();
            this.panel3.Controls.Add(UserControls[ObjectType.Camera]);
            if (panel3.Controls.Count > 0)
            {
                BaseControl control = panel3.Controls[0] as BaseControl;
                control.Properties = this.properties;
                control.RefreshData();
                control.apply_Button.Click += ((_sender, _e) => activeCamera.ApplyProperties(this.properties));
                control.apply_Button.Click += ((_sender, _e) => RenderScene());
            }
            RenderScene();
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cameras.Add(new Camera(activeCamera.position, activeCamera.target, activeCamera.fov));
            listBox2.Items.Add(cameras[cameras.Count - 1]);
        }


        private void optionsButton_Click(object sender, EventArgs e)
        {
            var form = new OptionsForm(settings, RenderScene);
            form.Show();
        }

    }
}
