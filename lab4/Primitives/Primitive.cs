using System;
using System.Collections.Generic;
using System.Windows.Media.Media3D;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using static lab4.MainForm;
using static lab4.Commons;

namespace lab4
{

    [Serializable]
    abstract class Primitive : ISerializable
    {
        protected static int objectsCounter = 0;
        protected int id;
        protected int idUnique;

        private ObjectType primitiveType;
        public new virtual ObjectType GetType
        {
            get { return primitiveType; }
            protected set { primitiveType = value; }
        }

        // original vertices position for object and its normals
        private List<Vector3D> pointsOriginal;
        private List<Vector3D> pointsForNormalsOriginal;

        // current vertices position = original vertices + TRS matrices
        protected List<Vector3D> points;
        protected List<Vector3D> pointsForNormals;

        // currently applied transformations
        private Matrix4x4 translationMatrix;
        private Matrix4x4 rotationMatrix;
        private Matrix4x4 scaleMatrix;
        public Color Color { get; set; }

        public double translationX = 0;
        public double translationY = 0;
        public double translationZ = 0;
        public int rotationX = 0;
        public int rotationY = 0;
        public int rotationZ = 0;
        public double ScaleX = 1;
        public double ScaleY = 1;
        public double ScaleZ = 1;

        /// <summary>
        /// Each tuple consists of object vertex and cooresponding end of normal vector
        /// </summary>
        public List<Tuple<Vector4D, Vector4D>> getPointsAndNormals()
        {
            var result = new List<Tuple<Vector4D, Vector4D>>();
            for (int i = 0; i < points.Count; i++)
            {
                result.Add(new Tuple<Vector4D, Vector4D>(Vector4D.CreatePositionVector(points[i]), Vector4D.CreatePositionVector(pointsForNormals[i])));
            }
            return result;
        }

        public int GetID()
        {
            return idUnique;
        }

        public Primitive()
        {
            idUnique = objectsCounter++;
            translationMatrix = new Matrix4x4();
            rotationMatrix = new Matrix4x4();
            scaleMatrix = new Matrix4x4();
        }

        public abstract List<Triangle> GetTriangles();

        public abstract void ApplyProperties(ObjectProperties properties);
        public abstract ObjectProperties CreateProperties();

        #region serialization
        public Primitive(SerializationInfo info, StreamingContext ctxt)
        {
            //Get the values from info and assign them to the appropriate properties
            points = (List<Vector3D>)info.GetValue("points", typeof(List<Vector3D>));
            pointsForNormals = (List<Vector3D>)info.GetValue("pointsForNormals", typeof(List<Vector3D>));
            translationMatrix = (Matrix4x4)info.GetValue("translationMatrix", typeof(Matrix4x4));
            rotationMatrix = (Matrix4x4)info.GetValue("rotationMatrix", typeof(Matrix4x4));
            scaleMatrix = (Matrix4x4)info.GetValue("scaleMatrix", typeof(Matrix4x4));
            Color = (Color)info.GetValue("Color", typeof(Color));
            translationX = (double)info.GetValue("tx", typeof(double));
            translationY = (double)info.GetValue("ty", typeof(double));
            translationZ = (double)info.GetValue("tz", typeof(double));
            rotationX = (int)info.GetValue("rx", typeof(int));
            rotationY = (int)info.GetValue("ry", typeof(int));
            rotationZ = (int)info.GetValue("rz", typeof(int));
            ScaleX = (double)info.GetValue("sx", typeof(double));
            ScaleY = (double)info.GetValue("sy", typeof(double));
            ScaleZ = (double)info.GetValue("sz", typeof(double));
            id = (int)info.GetValue("id", typeof(int));
            primitiveType = (ObjectType)info.GetValue("type", typeof(ObjectType));
            SaveOriginalState();
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("points", pointsOriginal);
            info.AddValue("pointsForNormals", pointsForNormalsOriginal);
            info.AddValue("translationMatrix", translationMatrix);
            info.AddValue("rotationMatrix", rotationMatrix);
            info.AddValue("scaleMatrix", scaleMatrix);
            info.AddValue("Color", Color);
            info.AddValue("tx", translationX);
            info.AddValue("ty", translationY);
            info.AddValue("tz", translationZ);
            info.AddValue("rx", rotationX);
            info.AddValue("ry", rotationY);
            info.AddValue("rz", rotationZ);
            info.AddValue("sx", ScaleX);
            info.AddValue("sy", ScaleY);
            info.AddValue("sz", ScaleZ);
            info.AddValue("id", id);
            info.AddValue("type", primitiveType);
        }
        #endregion

        public void SaveOriginalState()
        {
            pointsOriginal = new List<Vector3D>();
            if(points != null)
            {
                pointsOriginal.AddRange(points);
            }
            pointsForNormalsOriginal = new List<Vector3D>();
            if (pointsForNormals != null)
            {
                pointsForNormalsOriginal.AddRange(pointsForNormals);
            }
        }

        protected void ApplyTrasformations()
        {
            Matrix4x4 M = translationMatrix * rotationMatrix * scaleMatrix;
            //Matrix4x4 M = scaleMatrix* rotationMatrix * translationMatrix;

            var n = points.Count;
            for (int i = 0; i < n; i++)
            {
                Vector4D v = Vector4D.CreatePositionVector(pointsOriginal[i]);
                v = M * v;
                points[i] = new Vector3D(v.values[0], v.values[1], v.values[2]);
            }

            n = pointsForNormals.Count;
            for (int i = 0; i < n; i++)
            {
                var tmp4d = Vector4D.CreatePositionVector(pointsForNormalsOriginal[i]);
                tmp4d = M * tmp4d;
                pointsForNormals[i] = new Vector3D(tmp4d.values[0], tmp4d.values[1], tmp4d.values[2]);
            }
        }

        #region Transformations
        // translate -> rotate -> scale - order important! T*R*S       
        public void Translate(double x, double y, double z)
        {
            Matrix4x4 Mmod = new Matrix4x4();
            Mmod[0, 3] = x;
            Mmod[1, 3] = y;
            Mmod[2, 3] = z;
            translationMatrix = Mmod;
            translationX = x;
            translationY = y;
            translationZ = z;
        }

        public void Rotate(int x, int y, int z)
        {
            Matrix4x4 Mmod = new Matrix4x4();
            double ax = Math.PI * x / 180.0;
            double ay = Math.PI * y / 180.0;
            double az = Math.PI * z / 180.0;

            if (x != 0)
            {
                Mmod[1, 1] = Math.Cos(ax);
                Mmod[1, 2] = -Math.Sin(ax);
                Mmod[2, 1] = Math.Sin(ax);
                Mmod[2, 2] = Math.Cos(ax);
            }

            if (y != 0)
            {
                Mmod[0, 0] = Math.Cos(ay);
                Mmod[2, 0] = -Math.Sin(ay);
                Mmod[0, 2] = Math.Sin(ay);
                Mmod[2, 2] = Math.Cos(ay);
            }

            if (z != 0)
            {
                Mmod[0, 0] = Math.Cos(az);
                Mmod[0, 1] = -Math.Sin(az);
                Mmod[1, 0] = Math.Sin(az);
                Mmod[1, 1] = Math.Cos(az);
            }

            rotationMatrix = Mmod;
            rotationX = x;
            rotationY = y;
            rotationZ = z;
        }

        public void Scale(double x, double y, double z)
        {
            Matrix4x4 Mmod = new Matrix4x4();
            Mmod[0, 0] *= x;
            Mmod[1, 1] *= y;
            Mmod[2, 2] *= z;
            //scaleMatrix = scaleMatrix * Mmod;
            scaleMatrix = Mmod;
            ScaleX = x;
            ScaleY = y;
            ScaleZ = z;
        }

        public void ResetScale()
        {
            Matrix4x4 Mmod = new Matrix4x4();
            scaleMatrix = Mmod;
        }

        #endregion
    }
}
