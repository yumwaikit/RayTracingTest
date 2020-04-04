using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Media3D;
using System.Drawing;

namespace RayTracing
{
    public abstract class Traceable
    {
        public Color _ambient;
        public Color _diffuse;
        public Color _specular;
        public double _specularConst;

        public abstract Intersection FindIntersect(Ray ray);
    }

    public abstract class Plane : Traceable
    {
        public Point3D _p1;
        public Point3D _p2;
        public Point3D _p3;

        public abstract Vector3D GetNormal();
    }

    public abstract class Figure : Traceable
    {
        public Color _material;
        public double _refractConst;
    }
}
