using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;
using System.Drawing;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using static lab4.MainForm;
using lab4.Primitives;

namespace lab4
{
    [Serializable]
    class Sphere : Primitive, ISerializable
    {
        public int HorizontalSegments; // number of horizontal slices (latitude)
        public int VerticalSegments; // number of vertical slices (longtitude)
        public double Radius; // radius
        private static int counter = 0;

        public Sphere(Color color) : base()
        {
            this.Color = color;
            base.GetType = PrimitiveType.Sphere;

            HorizontalSegments = 30;
            VerticalSegments = 30;
            Radius = 1;

            points = new List<Vector3D>();
            pointsForNormals = new List<Vector3D>();
            points.Add(new Vector3D(0, Radius, 0));
            pointsForNormals.Add(new Vector3D(0, Radius + 1, 0));
            for (int i = 1; i < HorizontalSegments - 1; i++)
            {
                for (int j = 0; j < VerticalSegments; j++)
                {
                    double x = Radius * Math.Cos(2 * Math.PI * j / VerticalSegments) * Math.Sin(Math.PI * i / HorizontalSegments);
                    double z = Radius * Math.Sin(2 * Math.PI * j / VerticalSegments) * Math.Sin(Math.PI * i / HorizontalSegments);
                    double y = Radius * Math.Cos(Math.PI * i / HorizontalSegments);
                    double xx = (Radius + 1) * Math.Cos(2 * Math.PI * j / VerticalSegments) * Math.Sin(Math.PI * i / HorizontalSegments);
                    double zz = (Radius + 1) * Math.Sin(2 * Math.PI * j / VerticalSegments) * Math.Sin(Math.PI * i / HorizontalSegments);
                    double yy = (Radius + 1) * Math.Cos(Math.PI * i / HorizontalSegments);
                    points.Add(new Vector3D(x, y, z));
                    pointsForNormals.Add(new Vector3D(xx, yy, zz));
                }
            }
            points.Add(new Vector3D(0, -Radius, 0));
            pointsForNormals.Add(new Vector3D(0, -(Radius + 1), 0));
            id = counter++;
            base.SaveOriginalState();
        }

        public Sphere(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            VerticalSegments = (int)info.GetValue("N", typeof(int));
            HorizontalSegments = (int)info.GetValue("M", typeof(int));
            Radius = (double)info.GetValue("R", typeof(double));
        }

        public new void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("N", VerticalSegments);
            info.AddValue("M", HorizontalSegments);
            info.AddValue("R", Radius);
        }

        public override List<Triangle> GetTriangles()
        {
            ApplyTrasformations();
            var result = new List<Triangle>();

            for (int j = 0; j < VerticalSegments - 1; j++)
            {
                result.Add(new Triangle(points[0], points[j + 1], points[j + 2],
                                pointsForNormals[0], pointsForNormals[j + 1], pointsForNormals[j + 2],
                                points[0], points[j + 1], points[j + 2],
                                Color));
            }

            for (int i = 0; i < HorizontalSegments - 3; i++)
            {
                for (int j = 0; j <= VerticalSegments; j++)
                {
                    result.Add(new Triangle(points[i * VerticalSegments + j + 1], points[i * VerticalSegments + j], points[(i + 1) * VerticalSegments + j],
                                    pointsForNormals[i * VerticalSegments + j + 1], pointsForNormals[i * VerticalSegments + j], pointsForNormals[(i + 1) * VerticalSegments + j],
                                    points[i * VerticalSegments + j + 1], points[i * VerticalSegments + j], points[(i + 1) * VerticalSegments + j],
                                    Color));
                    result.Add(new Triangle(points[i * VerticalSegments + j + 1], points[(i + 1) * VerticalSegments + j], points[(i + 1) * VerticalSegments + j + 1],
                                    pointsForNormals[i * VerticalSegments + j + 1], pointsForNormals[(i + 1) * VerticalSegments + j], pointsForNormals[(i + 1) * VerticalSegments + j + 1],
                                    points[i * VerticalSegments + j + 1], points[(i + 1) * VerticalSegments + j], points[(i + 1) * VerticalSegments + j + 1],
                                    Color));
                }
            }
            for (int j = 1; j < VerticalSegments; j++)
            {
                result.Add(new Triangle(points[points.Count - 1], points[(HorizontalSegments - 3) * VerticalSegments + j + 1], points[(HorizontalSegments - 3) * VerticalSegments + j],
                                pointsForNormals[points.Count - 1], pointsForNormals[(HorizontalSegments - 3) * VerticalSegments + j + 1], pointsForNormals[(HorizontalSegments - 3) * VerticalSegments + j],
                                points[points.Count - 1], points[(HorizontalSegments - 3) * VerticalSegments + j + 1], points[(HorizontalSegments - 3) * VerticalSegments + j],
                                Color));
            }

            return result;
        }

        public override string ToString()
        {
            return "Sphere " + counter;
        }

        public override void ApplyProperties(PrimitiveProperties properties)
        {
            this.VerticalSegments = properties.verticalSegments ?? this.VerticalSegments;
            this.HorizontalSegments = properties.horizontalSegments ?? this.HorizontalSegments;
            this.Radius = properties.radius ?? this.Radius;

            points = new List<Vector3D>();
            pointsForNormals = new List<Vector3D>();
            points.Add(new Vector3D(0, Radius, 0));
            pointsForNormals.Add(new Vector3D(0, Radius + 1, 0));
            for (int i = 1; i < HorizontalSegments - 1; i++)
            {
                for (int j = 0; j < VerticalSegments; j++)
                {
                    double x = Radius * Math.Cos(2 * Math.PI * j / VerticalSegments) * Math.Sin(Math.PI * i / HorizontalSegments);
                    double z = Radius * Math.Sin(2 * Math.PI * j / VerticalSegments) * Math.Sin(Math.PI * i / HorizontalSegments);
                    double y = Radius * Math.Cos(Math.PI * i / HorizontalSegments);
                    double xx = (Radius + 1) * Math.Cos(2 * Math.PI * j / VerticalSegments) * Math.Sin(Math.PI * i / HorizontalSegments);
                    double zz = (Radius + 1) * Math.Sin(2 * Math.PI * j / VerticalSegments) * Math.Sin(Math.PI * i / HorizontalSegments);
                    double yy = (Radius + 1) * Math.Cos(Math.PI * i / HorizontalSegments);
                    points.Add(new Vector3D(x, y, z));
                    pointsForNormals.Add(new Vector3D(xx, yy, zz));
                }
            }
            points.Add(new Vector3D(0, -Radius, 0));
            pointsForNormals.Add(new Vector3D(0, -(Radius + 1), 0));

            base.SaveOriginalState();
            ResetScale();
            ApplyTrasformations();
        }

        public override PrimitiveProperties CreateProperties()
        {
            return new PrimitiveProperties
            {
                radius = this.Radius,
                verticalSegments = this.VerticalSegments,
                horizontalSegments = this.HorizontalSegments
            };
        }
    }

}
