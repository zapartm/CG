﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;
using System.Drawing;
using static lab4.Commons;

namespace lab4
{

    class SimpleTriangle
    {
        public Vector3D[] vertices;
        public SimpleTriangle(Vector3D v1, Vector3D v2, Vector3D v3)
        {
            vertices = new Vector3D[3];
            vertices[0] = v1;
            vertices[1] = v2;
            vertices[2] = v3;
        }

        public double GetArea()
        {
            double result = 0;
            double a = Math.Sqrt((vertices[0].X - vertices[1].X) * (vertices[0].X - vertices[1].X) + (vertices[0].Y - vertices[1].Y) * (vertices[0].Y - vertices[1].Y));
            double b = Math.Sqrt((vertices[0].X - vertices[2].X) * (vertices[0].X - vertices[2].X) + (vertices[0].Y - vertices[2].Y) * (vertices[0].Y - vertices[2].Y));
            double c = Math.Sqrt((vertices[1].X - vertices[2].X) * (vertices[1].X - vertices[2].X) + (vertices[1].Y - vertices[2].Y) * (vertices[1].Y - vertices[2].Y));
            result = Math.Sqrt((a + b + c) * (b + c - a) * (c + a - b) * (a + b - c)) / 4;

            return result;
        }
    }


    static class AuxiliaryMethods
    {

        public static double[,] CreateZbufferArray(List<Triangle> li, out Color?[,] colors, int h, int w, PhongModel PM, Camera c)
        {
            colors = new Color?[h, w];
            double[,] result = new double[h, w];
            double eps = 0.001;

            for (int i = 0; i < h; i++)
                for (int j = 0; j < w; j++)
                    result[i, j] = 1;

            foreach (var t in li)
            {
                Tuple<Triangle, Triangle> tt = t.SplitHorizontal();
                double intensityFlat = (PM.calculateIntensity(c, t.WorldCoordinates[0], t.normalVectors[0]) +
                                        PM.calculateIntensity(c, t.WorldCoordinates[1], t.normalVectors[1]) +
                                        PM.calculateIntensity(c, t.WorldCoordinates[2], t.normalVectors[2])) / 3;

                if (tt.Item1 != null)
                {
                    double x1 = tt.Item1.vertices[0].values[0];
                    double x2 = tt.Item1.vertices[1].values[0];
                    double x3 = tt.Item1.vertices[2].values[0];
                    double y1 = tt.Item1.vertices[0].values[1];
                    double y2 = tt.Item1.vertices[1].values[1];
                    double y3 = tt.Item1.vertices[2].values[1];
                    double z1 = tt.Item1.vertices[0].values[2];
                    double z2 = tt.Item1.vertices[1].values[2];
                    double z3 = tt.Item1.vertices[2].values[2];
                    Vector3D v1 = new Vector3D(x1, y1, 0);
                    Vector3D v2 = new Vector3D(x2, y2, 0);
                    Vector3D v3 = new Vector3D(x3, y3, 0);
                    SimpleTriangle tmpTriagnle = new SimpleTriangle(v1, v2, v3);
                    var a = tmpTriagnle.GetArea();

                    double invSlope1 = (x3 - x1) / (y1 - y3);
                    double invSlope2 = (x2 - x1) / (y1 - y2);
                    double invSlope3 = (z3 - z1) / (y1 - y3);
                    double invSlope4 = (z2 - z1) / (y1 - y2);
                    double ymin = y2;
                    double currx1 = x1;
                    double currx2 = x1;
                    double currz1 = z1;
                    double currz2 = z1;

                    if (Math.Abs(y1 - y2) < 1.0)
                    {
                        if (currx1 < 0 || currx2 < 0) continue;
                        double d = Math.Abs(currx1 - currx2);
                        for (int i = (int)x2; i <= x3; i++)
                        {
                            if (d == 0) break;
                            double z = currz2 * ((double)i / d) + currz1 * (1 - (double)i / d);
                            if (result[(int)y1, i] < z)
                            {
                                result[(int)y1, i] = z;
                                double I = PM.calculateIntensity(c, tt.Item1.WorldCoordinates[0], tt.Item1.normalVectors[1]);
                                colors[(int)y1, i] = Color.FromArgb((int)(tt.Item1.color.R * I) % 256, (int)(tt.Item1.color.G * I) % 256, (int)(tt.Item1.color.B * I) % 256);
                            }
                        }
                    }
                    else
                    {
                        double I1 = PM.calculateIntensity(c, tt.Item1.WorldCoordinates[0], tt.Item1.normalVectors[0]);
                        double I2 = PM.calculateIntensity(c, tt.Item1.WorldCoordinates[1], tt.Item1.normalVectors[1]);
                        double I3 = PM.calculateIntensity(c, tt.Item1.WorldCoordinates[2], tt.Item1.normalVectors[2]);
                        double I = 0;

                        int resultDim1 = result.GetLength(0);
                        int resultDim2 = result.GetLength(1);

                        for (int scanLine = (int)y1; (scanLine >= ymin && scanLine >= 0); scanLine--)
                        {
                            if (currx1 < 0) currx1 = 0;
                            if (currx2 < 0) currx2 = 0;
                            double d = Math.Abs(currx1 - currx2);
                            for (int i = 0; i <= d; i++)
                            {
                                if (d == 0) break;
                                double z = currz2 * ((double)i / d) + currz1 * (1 - (double)i / d);

                                if ((int)currx1 + i < resultDim2 && scanLine < resultDim1 && result[scanLine, (int)currx1 + i] > z + eps)
                                {
                                    result[scanLine, (int)currx1 + i] = z;

                                    Vector3D vs = new Vector3D((int)currx1 + i, scanLine, 0);
                                    SimpleTriangle a1 = new SimpleTriangle(v2, v3, vs);
                                    SimpleTriangle a2 = new SimpleTriangle(v1, v3, vs);
                                    SimpleTriangle a3 = new SimpleTriangle(v2, v1, vs);
                                    var p1 = a1.GetArea();
                                    var p2 = a2.GetArea();
                                    var p3 = a3.GetArea();

                                    if (PM.lightType == LightType.Gouraud)
                                    {
                                        I = I1 * p1 / a + I2 * p2 / a + I3 * p3 / a;
                                    }
                                    else
                                    {
                                        I = intensityFlat;
                                    }

                                    int R = (int)(tt.Item1.color.R * I) % 256;
                                    int G = (int)(tt.Item1.color.G * I) % 256;
                                    int B = (int)(tt.Item1.color.B * I) % 256;
                                    colors[scanLine, (int)currx1 + i] = Color.FromArgb(R, G, B);
                                }
                            }
                            currx1 += invSlope1;
                            currx2 += invSlope2;
                            currz1 += invSlope3;
                            currz2 += invSlope4;
                        }
                    }
                }

                // bottom triangle
                if (tt.Item2 != null)
                {
                    double x1 = tt.Item2.vertices[2].values[0];
                    double x2 = tt.Item2.vertices[1].values[0];
                    double x3 = tt.Item2.vertices[0].values[0];
                    double y1 = tt.Item2.vertices[2].values[1];
                    double y2 = tt.Item2.vertices[1].values[1];
                    double y3 = tt.Item2.vertices[0].values[1];
                    double z1 = tt.Item2.vertices[2].values[2];
                    double z2 = tt.Item2.vertices[1].values[2];
                    double z3 = tt.Item2.vertices[0].values[2];
                    Vector3D v1 = new Vector3D(x1, y1, 0);
                    Vector3D v2 = new Vector3D(x2, y2, 0);
                    Vector3D v3 = new Vector3D(x3, y3, 0);
                    SimpleTriangle tmpTriagnle = new SimpleTriangle(v1, v2, v3);
                    var a = tmpTriagnle.GetArea();

                    double invSlope1 = -(x1 - x3) / (y3 - y1);
                    double invSlope2 = -(x1 - x2) / (y2 - y1);
                    double invSlope3 = -(z1 - z3) / (y3 - y1);
                    double invSlope4 = -(z1 - z2) / (y2 - y1);
                    double ymin = y2;
                    double currx1 = x1;
                    double currx2 = x1;
                    double currz1 = z1;
                    double currz2 = z1;

                    int viewPortHeight = result.GetLength(0);
                    int viewPortLength = result.GetLength(1);

                    if (Math.Abs(y1 - y2) < 1.0)
                    {
                        if (currx1 < 0 || currx2 < 0) continue;
                        double d = Math.Abs(currx1 - currx2);
                        for (int i = (int)x2; i <= x3; i++)
                        {
                            if (d == 0) break;
                            double z = currz2 * ((double)i / d) + currz1 * (1 - (double)i / d);
                            if (y1 >= 0 && i >= 0 && y1 < viewPortHeight && i < viewPortLength && result[(int)y1, i] < z - eps)
                            {
                                result[(int)y1, i] = z;
                                double I = PM.calculateIntensity(c, tt.Item2.WorldCoordinates[0], tt.Item2.normalVectors[1]);
                                colors[(int)y1, i] = Color.FromArgb((int)(tt.Item2.color.R * I) % 256, (int)(tt.Item2.color.G * I) % 256, (int)(tt.Item2.color.B * I) % 256);
                            }
                        }
                    }
                    else
                    {
                        double I1 = PM.calculateIntensity(c, tt.Item2.WorldCoordinates[0], tt.Item2.normalVectors[0]);
                        double I2 = PM.calculateIntensity(c, tt.Item2.WorldCoordinates[1], tt.Item2.normalVectors[1]);
                        double I3 = PM.calculateIntensity(c, tt.Item2.WorldCoordinates[2], tt.Item2.normalVectors[2]);
                        double I = 0;

                        for (int scanLine = (int)y1; (scanLine <= ymin && scanLine < result.GetLength(0)); scanLine++)
                        {
                            if (currx1 < 0) currx1 = 0;
                            double d = Math.Abs(currx1 - currx2);
                            for (int i = 0; i <= d; i++)
                            {
                                if (d == 0) break;
                                double z = currz2 * ((double)i / d) + currz1 * (1 - (double)i / d);

                                if ((int)currx1 + i < viewPortLength &&
                                    scanLine < viewPortHeight &&
                                    scanLine >= 0 &&
                                    (int)currx1 + i >= 0 &&
                                    result[scanLine, (int)currx1 + i] > z)
                                {
                                    result[scanLine, (int)currx1 + i] = z;

                                    Vector3D vs = new Vector3D((int)currx1 + i, scanLine, 0);
                                    SimpleTriangle a1 = new SimpleTriangle(v2, v3, vs);
                                    SimpleTriangle a2 = new SimpleTriangle(v1, v3, vs);
                                    SimpleTriangle a3 = new SimpleTriangle(v2, v1, vs);
                                    var p1 = a1.GetArea();
                                    var p2 = a2.GetArea();
                                    var p3 = a3.GetArea();

                                    if (PM.lightType == LightType.Gouraud)
                                    {
                                        I = I3 * p1 / a + I2 * p2 / a + I1 * p3 / a;
                                    }
                                    else
                                    {
                                        I = intensityFlat;
                                    }

                                    colors[scanLine, (int)currx1 + i] = Color.FromArgb((int)(tt.Item2.color.R * I) % 256, (int)(tt.Item2.color.G * I) % 256, (int)(tt.Item2.color.B * I) % 256);
                                }
                            }
                            currx1 += invSlope1;
                            currx2 += invSlope2;
                            currz1 += invSlope3;
                            currz2 += invSlope4;
                        }
                    }
                }
            }

            return result;
        }


        public static Matrix4x4 CreateProjectionMatrix(double angle, double near, double far, int w, int h)
        {
            double rad = angle * Math.PI / 180;
            double aspect = (double)w / (double)h;
            Vector4D v1 = new Vector4D(1 / (Math.Tan(rad / 2) * aspect), 0, 0, 0);
            Vector4D v2 = new Vector4D(0, 1 / Math.Tan(rad / 2), 0, 0);
            Vector4D v3 = new Vector4D(0, 0, (far + near) / -(far - near), -1);
            Vector4D v4 = new Vector4D(0, 0, (-2 * near * far) / (far - near), 0);

            return new Matrix4x4(v1, v2, v3, v4);
        }

        public static Matrix4x4 CreateViewMatrix(Vector3D position, Vector3D target, Vector3D upWorld)
        {
            Vector3D D = position - target;
            D.Normalize();
            Vector3D R = Vector3D.CrossProduct(D, upWorld);
            Vector3D U = Vector3D.CrossProduct(D, R);

            Vector4D v1 = new Vector4D(R.X, U.X, D.X, 0);
            Vector4D v2 = new Vector4D(R.Y, U.Y, D.Y, 0);
            Vector4D v3 = new Vector4D(R.Z, U.Z, D.Z, 0);
            Vector4D v4 = new Vector4D(0, 0, 0, 1);
            Matrix4x4 M = new Matrix4x4(v1, v2, v3, v4);
            Matrix4x4 M2 = new Matrix4x4();
            M2[0, 3] = -position.X;
            M2[1, 3] = -position.Y;
            M2[2, 3] = -position.Z;
            return M * M2;
        }

        public static Vector3D CreateNormalVectorToSurface(Vector3D p1, Vector3D p2, Vector3D p3)
        {
            var vector = new Vector3D();
            var v1 = new Vector3D(p2.X - p1.X, p2.Y - p1.Y, p2.Z - p1.Z);
            var v2 = new Vector3D(p3.X - p1.X, p3.Y - p1.Y, p3.Z - p1.Z);
            vector = Vector3D.CrossProduct(v2, v1);
            vector.Normalize();
            return vector;
        }
    }
}
