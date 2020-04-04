using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Media3D;
using System.Drawing;

namespace RayTracing
{
    public class Rod : Figure
    {
        public Point3D _point;
        public Vector3D _direction;
        public double _radius;

        public Rod(Point3D point, Vector3D direction, double radius, Color ambient, Color diffuse, Color specular, Color material, double specularConst, double refractConst)
        {
            _point = point;
            _direction = direction / direction.Length;
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
            return null;
        }
    }
}
