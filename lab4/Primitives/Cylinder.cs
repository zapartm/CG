using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Media.Media3D;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using static lab4.MainForm;
using static lab4.Commons;

namespace lab4
{
    [Serializable]
    class Cylinder : Primitive, ISerializable
    {
        public double Radius;
        public double Height;
        public int Segments;
        private static int counter = 0;

        public Cylinder(Color color) : base()
        {
            base.Color = color;
            base.GetType = ObjectType.Cylinder;

            Segments = 12;
            Radius = 1;
            Height = 1;
            points = new List<Vector3D>();
            pointsForNormals = new List<Vector3D>();
            var tmp = new List<Vector3D>();
            for (int i = 0; i < Segments; i++)
            {
                points.Add(new Vector3D(Radius * Math.Sin(Math.PI * 2 * i / Segments), 0, Radius * Math.Cos(Math.PI * 2 * i / Segments)));
                pointsForNormals.Add(new Vector3D((Radius + 1) * Math.Sin(Math.PI * 2 * i / Segments), 0, (Radius + 1) * Math.Cos(Math.PI * 2 * i / Segments)));
                tmp.Add(new Vector3D((Radius) * Math.Sin(Math.PI * 2 * i / Segments), -1, (Radius) * Math.Cos(Math.PI * 2 * i / Segments)));

                points.Add(new Vector3D(Radius * Math.Sin(Math.PI * 2 * i / Segments), Height , Radius * Math.Cos(Math.PI * 2 * i / Segments)));
                pointsForNormals.Add(new Vector3D((Radius+1) * Math.Sin(Math.PI * 2 * i / Segments), Height, (Radius + 1) * Math.Cos(Math.PI * 2 * i / Segments)));
                tmp.Add(new Vector3D((Radius) * Math.Sin(Math.PI * 2 * i / Segments), Height+1, (Radius) * Math.Cos(Math.PI * 2 * i / Segments)));
            }
            pointsForNormals.AddRange(tmp);
            points.Add(new Vector3D(0, 0, 0));
            pointsForNormals.Add(new Vector3D(0, -1, 0));
            points.Add(new Vector3D(0, Height, 0));
            pointsForNormals.Add(new Vector3D(0, Height+1, 0));
            id = counter++;
            base.SaveOriginalState();
        }

        #region serialization
        public Cylinder(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            Segments = (int)info.GetValue("N", typeof(int));
            Radius = (double)info.GetValue("R", typeof(double));
            Height = (double)info.GetValue("H", typeof(double));
        }

        public new void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("N", Segments);
            info.AddValue("R", Radius);
            info.AddValue("H", Height);
        }
        #endregion

        public override List<Triangle> GetTriangles()
        {
            ApplyTrasformations();
            List<Triangle> result = new List<Triangle>();
            int n1 = pointsForNormals.Count;
            int n = points.Count - 2;
            var tmpList = pointsForNormals.GetRange(2 * Segments, pointsForNormals.Count - 2 * Segments);

            for (int i = 0; i < Segments; i++)
            {
                Vector3D p1, p2, p3, pn1, pn2, pn3;
                // side top-flat triangles
                p1 = points[2 * i];
                p2 = points[(2 * i + 1) % n];
                p3 = points[(2 * (i + 1) + 1) % n];
                pn1 = pointsForNormals[2 * i];
                pn2 = pointsForNormals[(2 * i + 1) % n];
                pn3 = pointsForNormals[(2 * (i + 1) + 1) % n];
                result.Add(new Triangle(p1, p2, p3, pn1, pn2, pn3, p1, p2, p3, Color));

                // side bottom-flat triangles
                p1 = points[2 * i];
                p2 = points[(2 * (i + 1) + 1) % n];
                p3 = points[(2 * (i + 1)) % n];
                pn1 = pointsForNormals[2 * i];
                pn2 = pointsForNormals[(2 * (i + 1) + 1) % n];
                pn3 = pointsForNormals[(2 * (i + 1)) % n];
                result.Add(new Triangle(p1, p2, p3, pn1, pn2, pn3, p1, p2, p3, Color));

                // top
                p1 = points[(2 * i + 1) % n];
                p2 = points[n + 1];
                p3 = points[(2 * (i + 1) + 1) % n];
                pn1 = tmpList[(2 * i + 1 ) % n  ];
                pn2 = tmpList[n + 1];
                pn3 = tmpList[(2 * (i + 1) + 1 ) % n];
                result.Add(new Triangle(p1, p2, p3, pn1, pn2, pn3, p1, p2, p3, Color));

                // bottom
                p1 = points[2 * i];
                p2 = points[(2 * (i + 1)) % n];
                p3 = points[n];
                pn1 = tmpList[2 * i];
                pn2 = tmpList[(2 * (i + 1)) % n];
                pn3 = tmpList[n];
                result.Add(new Triangle(p1, p2, p3, pn1, pn2, pn3, p1, p2, p3, Color));
            }

            return result;
        }

        public override string ToString()
        {
            return "Cylinder " + counter;
        }

        public override void ApplyProperties(ObjectProperties properties)
        {
            this.Height = properties.height ?? this.Height;
            this.Radius = properties.radius ?? this.Radius;
            this.Segments = properties.segments ?? this.Segments;
            points = new List<Vector3D>();
            pointsForNormals = new List<Vector3D>();
            var tmp = new List<Vector3D>();
            for (int i = 0; i < Segments; i++)
            {
                points.Add(new Vector3D(Radius * Math.Sin(Math.PI * 2 * i / Segments), 0, Radius * Math.Cos(Math.PI * 2 * i / Segments)));
                pointsForNormals.Add(new Vector3D((Radius + 1) * Math.Sin(Math.PI * 2 * i / Segments), 0, (Radius + 1) * Math.Cos(Math.PI * 2 * i / Segments)));
                tmp.Add(new Vector3D((Radius) * Math.Sin(Math.PI * 2 * i / Segments), -1, (Radius) * Math.Cos(Math.PI * 2 * i / Segments)));

                points.Add(new Vector3D(Radius * Math.Sin(Math.PI * 2 * i / Segments), Height, Radius * Math.Cos(Math.PI * 2 * i / Segments)));
                pointsForNormals.Add(new Vector3D((Radius + 1) * Math.Sin(Math.PI * 2 * i / Segments), Height, (Radius + 1) * Math.Cos(Math.PI * 2 * i / Segments)));
                tmp.Add(new Vector3D((Radius) * Math.Sin(Math.PI * 2 * i / Segments), Height + 1, (Radius) * Math.Cos(Math.PI * 2 * i / Segments)));
            }
            pointsForNormals.AddRange(tmp);
            points.Add(new Vector3D(0, 0, 0));
            pointsForNormals.Add(new Vector3D(0, -1, 0));
            points.Add(new Vector3D(0, Height, 0));
            pointsForNormals.Add(new Vector3D(0, Height + 1, 0));

            base.SaveOriginalState();
            ResetScale();
            ApplyTrasformations();
        }

        public override ObjectProperties CreateProperties()
        {
            return new ObjectProperties
            {
                radius = this.Radius,
                height = this.Height,
                segments = this.Segments
            };
        }
    }
}
