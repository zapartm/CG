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
    class Cone : Primitive, ISerializable
    {
        private static int counter = 0;
        public double Radius;
        public double Height;
        public int Segments;

        public Cone(Color color) : base()
        {
            base.Color = color;
            base.GetType = ObjectType.Cone;

            Segments = 3;
            Radius = 1;
            Height = 2;
            points = new List<Vector3D>();
            pointsForNormals = new List<Vector3D>();
            for (int i = 0; i < Segments; i++)
            {
                points.Add(new Vector3D(Radius * Math.Sin(Math.PI * 2 * i / Segments), 0, Radius * Math.Cos(Math.PI * 2 * i / Segments)));
            }
            points.Add(new Vector3D(0, 0, 0));
            points.Add(new Vector3D(0, Height, 0));

            for (int i = 0; i < Segments; i++)
            {
                var normalVector = AuxiliaryMethods.CreateNormalVectorToSurface(points[Segments + 1], points[(i + 1) % Segments], points[i]);
                pointsForNormals.Add(Vector3D.Add(points[Segments + 1], normalVector));
                pointsForNormals.Add(Vector3D.Add(points[(i + 1) % Segments], normalVector));
                pointsForNormals.Add(Vector3D.Add(points[i], normalVector));
            }
            for (int i = 0; i < Segments; i++)
            {
                pointsForNormals.Add(Vector3D.Add(points[i], new Vector3D(0, -1, 0)));
            }
            pointsForNormals.Add(new Vector3D(0, -1, 0));

            id = counter++;
            base.SaveOriginalState();
        }

        public Cone(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            Segments = (int)info.GetValue("N", typeof(int));
            Radius = (double)info.GetValue("R", typeof(double));
            Height = (double)info.GetValue("H", typeof(double));
        }

        public override List<Triangle> GetTriangles()
        {
            ApplyTrasformations();
            List<Triangle> result = new List<Triangle>();

            for (int i = 0; i < Segments; i++)
            {
                Vector3D p1, p2, p3, pn1, pn2, pn3;
                p1 = points[Segments];
                p2 = points[i];
                p3 = points[(i + 1) % Segments];
                pn1 = pointsForNormals[4 * Segments];
                pn2 = pointsForNormals[3 * Segments + i];
                pn3 = pointsForNormals[3 * Segments + (i + 1) % Segments];
                result.Add(new Triangle(p1, p2, p3, pn1, pn2, pn3, p1, p2, p3, Color));

                p1 = points[Segments + 1];
                p2 = points[(i + 1) % Segments];
                p3 = points[i];
                pn1 = pointsForNormals[3 * i];
                pn2 = pointsForNormals[3 * i + 1];
                pn3 = pointsForNormals[3 * i + 2];
                result.Add(new Triangle(p1, p2, p3, pn1, pn2, pn3, p1, p2, p3, Color));
            }

            return result;
        }

        public new void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("N", Segments);
            info.AddValue("R", Radius);
            info.AddValue("H", Height);
        }

        public override string ToString()
        {
            return "Cone " + counter;
        }

        public override void ApplyProperties(ObjectProperties properties)
        {
            this.Segments = properties.segments ?? this.Segments;
            this.Radius = properties.radius ?? this.Radius;
            this.Height = properties.height ?? this.Height;

            points = new List<Vector3D>();
            pointsForNormals = new List<Vector3D>();
            for (int i = 0; i < Segments; i++)
            {
                points.Add(new Vector3D(Radius * Math.Sin(Math.PI * 2 * i / Segments), 0, Radius * Math.Cos(Math.PI * 2 * i / Segments)));
            }
            points.Add(new Vector3D(0, 0, 0));
            points.Add(new Vector3D(0, Height, 0));

            for (int i = 0; i < Segments; i++)
            {
                var normalVector = AuxiliaryMethods.CreateNormalVectorToSurface(points[Segments + 1], points[(i + 1) % Segments], points[i]);
                pointsForNormals.Add(Vector3D.Add(points[Segments + 1], normalVector));
                pointsForNormals.Add(Vector3D.Add(points[(i + 1) % Segments], normalVector));
                pointsForNormals.Add(Vector3D.Add(points[i], normalVector));
            }
            for (int i = 0; i < Segments; i++)
            {
                pointsForNormals.Add(Vector3D.Add(points[i], new Vector3D(0, -1, 0)));
            }
            pointsForNormals.Add(new Vector3D(0, -1, 0));

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
