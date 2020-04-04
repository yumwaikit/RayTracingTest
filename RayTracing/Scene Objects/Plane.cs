using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Media3D;
using System.Drawing;

namespace RayTracing
{
    public class Plane3 : Plane
    {
        public Plane3(Point3D p1, Point3D p2, Point3D p3, Color ambient, Color diffuse, Color specular, double specularConst)
        {
            _p1 = p1;
            _p2 = p2;
            _p3 = p3;

            _ambient = ambient;
            _diffuse = diffuse;
            _specular = specular;
            _specularConst = specularConst;
        }

        public override Vector3D GetNormal()
        {
            Vector3D v = Vector3D.CrossProduct((Vector3D)_p2 - (Vector3D)_p1, (Vector3D)_p3 - (Vector3D)_p1);
            v /= v.Length;
            return v;
        }

        public override Intersection FindIntersect(Ray ray)
        {
            //parallel
            if (Vector3D.DotProduct(ray.vector, GetNormal()) == 0)
            {
                return null;
            }

            //find intersection point
            Point3D p1 = _p1;
            Vector3D normal = GetNormal();
            // Vector3D.DotProduct(((ray.origin + ray.vector * distance) - p1), normal) = 0;
            // (ray.origin.X + ray.vector.X * distance - p1.X) * normal.X + (ray.origin.Y + ray.vector.Y * distance - p1.Y) * normal.Y + (ray.origin.Z + ray.vector.Z * distance - p1.Z) * normal.Z = 0;
            // ray.origin.X * normal.X + ray.vector.X * normal.X * distance - p1.X * normal.X + ray.origin.Y * normal.Y + ray.vector.Y * normal.Y * distance - p1.Y * normal.Y + ray.origin.Z * normal.Z + ray.vector.Z * normal.Z * distance - p1.Z * normal.Z = 0
            // ray.vector.X * normal.X * distance + ray.vector.Y * normal.Y * distance + ray.vector.Z * normal.Z * distance = p1.X * normal.X - ray.origin.X * normal.X + p.Y * normal.Y - ray.origin.Y * normal.Y + p1.Z * normal.Z - ray.origin.Z * normal.Z
            // distance * (ray.vector.X * normal.X + ray.vector.Y * normal.Y + ray.vector.Z * normal.Z) = p1.X * normal.X - ray.origin.X * normal.X + p1.Y * normal.Y - ray.origin.Y * normal.Y + p1.Z * normal.Z - ray.origin.Z * normal.Z
            double distance = (p1.X * normal.X - ray.origin.X * normal.X + p1.Y * normal.Y - ray.origin.Y * normal.Y + p1.Z * normal.Z - ray.origin.Z * normal.Z) / (ray.vector.X * normal.X + ray.vector.Y * normal.Y + ray.vector.Z * normal.Z);
            Point3D point = ray.origin + ray.vector * distance;

            //check boundary
            double angleA = Vector3D.AngleBetween((Vector3D)(_p1 - point), (Vector3D)(_p2 - point));
            double angleB = Vector3D.AngleBetween((Vector3D)(_p2 - point), (Vector3D)(_p3 - point));
            double angleC = Vector3D.AngleBetween((Vector3D)(_p3 - point), (Vector3D)(_p1 - point));
            if (angleA + angleB < 180 || angleB + angleC < 180 || angleC + angleA < 180)
            {
                return null;
            }

            return new Intersection(distance, normal, _ambient, _diffuse, _specular, Color.Black, _specularConst, 0);
        }

    }

    public class Plane4 : Plane
    {
        public Point3D _p4;

        public Plane4(Point3D p1, Point3D p2, Point3D p3, Point3D p4, Color ambient, Color diffuse, Color specular, double specularConst)
        {
            _p1 = p1;
            _p2 = p2;
            _p3 = p3;
            _p4 = p4;

            _ambient = ambient;
            _diffuse = diffuse;
            _specular = specular;
            _specularConst = specularConst;
        }

        public override Vector3D GetNormal()
        {
            Vector3D v = Vector3D.CrossProduct((Vector3D)_p2 - (Vector3D)_p1, (Vector3D)_p3 - (Vector3D)_p1);
            v /= v.Length;
            return v;
        }

        public override Intersection FindIntersect(Ray ray)
        {
            //parallel
            if (Vector3D.DotProduct(ray.vector, GetNormal()) == 0)
            {
                return null;
            }

            //find intersection point
            Point3D p1 = _p1;
            Vector3D normal = GetNormal();
            // Vector3D.DotProduct(((ray.origin + ray.vector * distance) - p1), normal) = 0;
            // (ray.origin.X + ray.vector.X * distance - p1.X) * normal.X + (ray.origin.Y + ray.vector.Y * distance - p1.Y) * normal.Y + (ray.origin.Z + ray.vector.Z * distance - p1.Z) * normal.Z = 0;
            // ray.origin.X * normal.X + ray.vector.X * normal.X * distance - p1.X * normal.X + ray.origin.Y * normal.Y + ray.vector.Y * normal.Y * distance - p1.Y * normal.Y + ray.origin.Z * normal.Z + ray.vector.Z * normal.Z * distance - p1.Z * normal.Z = 0
            // ray.vector.X * normal.X * distance + ray.vector.Y * normal.Y * distance + ray.vector.Z * normal.Z * distance = p1.X * normal.X - ray.origin.X * normal.X + p.Y * normal.Y - ray.origin.Y * normal.Y + p1.Z * normal.Z - ray.origin.Z * normal.Z
            // distance * (ray.vector.X * normal.X + ray.vector.Y * normal.Y + ray.vector.Z * normal.Z) = p1.X * normal.X - ray.origin.X * normal.X + p1.Y * normal.Y - ray.origin.Y * normal.Y + p1.Z * normal.Z - ray.origin.Z * normal.Z
            double distance = (p1.X * normal.X - ray.origin.X * normal.X + p1.Y * normal.Y - ray.origin.Y * normal.Y + p1.Z * normal.Z - ray.origin.Z * normal.Z) / (ray.vector.X * normal.X + ray.vector.Y * normal.Y + ray.vector.Z * normal.Z);
            Point3D point = ray.origin + ray.vector * distance;

            //check boundary
            double angle12 = Vector3D.AngleBetween((Vector3D)(_p1 - point), (Vector3D)(_p2 - point));
            double angle13 = Vector3D.AngleBetween((Vector3D)(_p1 - point), (Vector3D)(_p3 - point));
            double angle42 = Vector3D.AngleBetween((Vector3D)(_p4 - point), (Vector3D)(_p2 - point));
            double angle43 = Vector3D.AngleBetween((Vector3D)(_p4 - point), (Vector3D)(_p3 - point));

            if (angle12 + angle13 + angle42 < 180 ||
                angle12 + angle13 + angle43 < 180 ||
                angle12 + angle42 + angle43 < 180 ||
                angle13 + angle42 + angle43 < 180)
            {
                return null;
            }

            return new Intersection(distance, normal, _ambient, _diffuse, _specular, Color.Black, _specularConst, 0);
        }

    }
}
