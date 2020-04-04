using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Media.Media3D;

namespace RayTracing
{
    public class Ray
    {
        public Vector3D vector;
        public Point3D origin;
        public double refractConst;
        public int traceDepth;

        public Ray(Vector3D v, Point3D o, double refract, int depth)
        {
            vector = v / v.Length;
            origin = o;
            refractConst = refract;
            traceDepth = depth;
        }

        public Color TraceRay(Space3D space)
        {
            List<Intersection> intersections = new List<Intersection>();
            foreach (Traceable t in space.objects)
            {
                if (t.FindIntersect(this) != null)
                {
                    intersections.Add(t.FindIntersect(this));
                }
            }

            int index = -1;
            double distance = double.MaxValue;
            for (int i = 0; i < intersections.Count; i++)
            {
                if (intersections[i].distance < distance && intersections[i].distance > 1)
                {
                    distance = intersections[i].distance;
                    index = i;
                }
            }

            double r = 0;
            double g = 0;
            double b = 0;

            if (index > -1)
            {
                //if (traceDepth > 1)
                //{
                //    System.Diagnostics.Trace.WriteLine("Hit after reflection!");
                //}
                Point3D intersectionPoint = origin + distance * vector;
                //ambient
                r += (double)space.ambient.R / 255 * (double)intersections[index].ambient.R / 255;
                g += (double)space.ambient.G / 255 * (double)intersections[index].ambient.G / 255;
                b += (double)space.ambient.B / 255 * (double)intersections[index].ambient.B / 255;

                //lights
                foreach (Light light in space.lights)
                {
                    //diffuse
                    Color lightColor = light.getColor(intersectionPoint);
                    Vector3D lightDirection = light.getDirection(intersectionPoint);
                    Vector3D normal = intersections[index].normal;
                    lightDirection /= lightDirection.Length;
                    double dotProduct = Vector3D.DotProduct(lightDirection, normal);
                    if (dotProduct < 0)
                    {
                        r += ((double)intersections[index].ambient.R / 255) * ((double)lightColor.R / 255) * -dotProduct;
                        g += ((double)intersections[index].ambient.G / 255) * ((double)lightColor.G / 255) * -dotProduct;
                        b += ((double)intersections[index].ambient.B / 255) * ((double)lightColor.B / 255) * -dotProduct;

                        //specular
                        //refract
                        double refractConstRatio = refractConst / intersections[index].refractConst;

                        Vector3D refractDirection = refractConstRatio * lightDirection + (refractConstRatio * -dotProduct - Math.Sqrt(1 - Math.Pow(refractConstRatio, 2) * (1 - Math.Pow(dotProduct, 2)))) * normal;
                        refractDirection /= refractDirection.Length;

                        dotProduct = Vector3D.DotProduct(refractDirection, normal);
                        if (dotProduct > 0)
                        {
                            r += ((double)intersections[index].material.R / 255) * ((double)lightColor.R / 255) * Math.Pow(-dotProduct, intersections[index].specularConst);
                            g += ((double)intersections[index].material.G / 255) * ((double)lightColor.G / 255) * Math.Pow(-dotProduct, intersections[index].specularConst);
                            b += ((double)intersections[index].material.B / 255) * ((double)lightColor.B / 255) * Math.Pow(-dotProduct, intersections[index].specularConst);
                        }

                        //reflect
                        Vector3D lightReflect = lightDirection - 2 * dotProduct * normal;
                        lightReflect /= lightReflect.Length;
                        dotProduct = Vector3D.DotProduct(lightReflect, vector);
                        if (dotProduct < 0)
                        {
                            //System.Diagnostics.Trace.WriteLine("dotproduct = " + dotProduct);
                            r += ((double)intersections[index].specular.R / 255) * ((double)lightColor.R / 255) * Math.Pow(-dotProduct, intersections[index].specularConst);
                            g += ((double)intersections[index].specular.G / 255) * ((double)lightColor.G / 255) * Math.Pow(-dotProduct, intersections[index].specularConst);
                            b += ((double)intersections[index].specular.B / 255) * ((double)lightColor.B / 255) * Math.Pow(-dotProduct, intersections[index].specularConst);
                        }


                    }
                }

                //reflection
                if (traceDepth < space.MaxDepth)
                {
                    Vector3D normal = intersections[index].normal;
                    double dotProduct = Vector3D.DotProduct(vector, normal);

                    if (dotProduct < 0)
                    {
                        Ray reflectRay = new Ray(vector - 2 * dotProduct * normal, intersectionPoint, refractConst, traceDepth + 1);
                        Color reflectColor = reflectRay.TraceRay(space);

                        r += (double)reflectColor.R / 255;
                        g += (double)reflectColor.G / 255;
                        b += (double)reflectColor.B / 255;
                    }
                }

                //refraction
                if (traceDepth < space.MaxDepth && intersections[index].refractConst != 0)
                {
                    Vector3D normal = intersections[index].normal;
                    double dotProduct = Vector3D.DotProduct(vector, normal);
                    double refractConstRatio = refractConst / intersections[index].refractConst;

                    if (dotProduct < 0)
                    {
                        Vector3D refractDirection = refractConstRatio * vector + (refractConstRatio * -dotProduct - Math.Sqrt(1 - Math.Pow(refractConstRatio, 2) * (1 - Math.Pow(dotProduct, 2)))) * normal;
                        refractDirection /= refractDirection.Length;

                        dotProduct = Vector3D.DotProduct(refractDirection, normal);
                        if (dotProduct < 0)
                        {
                            Ray refractRay = new Ray(refractDirection, intersectionPoint, intersections[index].refractConst, traceDepth + 1);
                            Color refractColor = refractRay.TraceRay(space);

                            r += intersections[index].material.R / 255 * (double)refractColor.R / 255;
                            g += intersections[index].material.G / 255 * (double)refractColor.G / 255;
                            b += intersections[index].material.B / 255 * (double)refractColor.B / 255;
                        }
                    }
                    if (dotProduct > 0)
                    {
                        Vector3D refractDirection = refractConstRatio * vector + (refractConstRatio * dotProduct - Math.Sqrt(1 - Math.Pow(refractConstRatio, 2) * (1 - Math.Pow(dotProduct, 2)))) * normal;
                        refractDirection /= refractDirection.Length;

                        dotProduct = Vector3D.DotProduct(refractDirection, normal);
                        if (dotProduct > 0)
                        {
                            Ray refractRay = new Ray(refractDirection, intersectionPoint, 1, traceDepth + 1);
                            Color refractColor = refractRay.TraceRay(space);

                            r += (double)refractColor.R / 255;
                            g += (double)refractColor.G / 255;
                            b += (double)refractColor.B / 255;
                        }
                    }
                }

            }

            if (r > 1)
                r = 1;
            r *= 255;
            if (g > 1)
                g = 1;
            g *= 255;
            if (b > 1)
                b = 1;
            b *= 255;

            return Color.FromArgb((int)r, (int)g, (int)b);
        }

    }
}
