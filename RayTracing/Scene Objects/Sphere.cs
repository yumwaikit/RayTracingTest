using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Media3D;
using System.Drawing;

namespace RayTracing
{
    class Sphere : Figure
    {
        public Point3D _center;
        public double _radius;

        public Sphere(Point3D center, double radius, Color ambient, Color diffuse, Color specular, Color material, double specularConst, double refractConst)
        {
            _center = center;
            _radius = radius;

            _ambient = ambient;
            _diffuse = diffuse;
            _specular = specular;
            _material = material;
            _specularConst = specularConst;
            _refractConst = refractConst;
        }

        public override Intersection FindIntersect(Ray ray)
        {
            // Math.Pow((ray.origin + distance * ray.vector - (Vector3D)_center).Length, 2) = Math.Pow(_radius, 2);
            // Vector3D.DotProduct(ray.origin + distance * ray.vector - (Vector3D)_center, ray.origin + distance * ray.vector - (Vector3D)_center) = Math.Pow(_radius, 2);
            // Math.Pow(distance, 2) * Vector3D.DotProduct(ray.vector, ray.vector) + 2 * distance * Vector3D.DotProduct(ray.vector, ray.origin - _center) + Vector3D.DotProduct(ray.origin - _center, ray.origin - _center) - Math.Pow(_radius, 2) = 0;
            // double a = Vector3D.DotProduct(ray.vector, ray.vector); // = 1
            double b = 2 * Vector3D.DotProduct(ray.vector, ray.origin - _center);
            double c = Vector3D.DotProduct(ray.origin - _center, ray.origin - _center) - Math.Pow(_radius, 2);
            double delta = Math.Pow(b, 2) - 4 * c;

            if (delta <= 0)
            {
                return null;
            }
            else
            {
                double distance1 = (-b + Math.Sqrt(delta)) / 2;
                double distance2 = (-b - Math.Sqrt(delta)) / 2;
                double distance = (distance1 - 1) * (distance2 - 1) > 0 ? Math.Min(distance1, distance2) : Math.Max(distance1, distance2);
                Vector3D normal = ray.origin + distance * ray.vector - _center;
                normal /= normal.Length;
                return new Intersection(distance, normal, _ambient, _diffuse, _specular, _material, _specularConst, _refractConst);
            }
        }
    }
}
