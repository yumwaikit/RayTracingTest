using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Media3D;
using System.Drawing;

namespace RayTracing
{
    public class Intersection
    {
        public double distance;
        public Vector3D normal;
        public Color ambient;
        public Color diffuse;
        public Color specular;
        public Color material;
        public double specularConst;
        public double refractConst;

        public Intersection(double _distance, Vector3D _normal, Color _ambient, Color _diffuse, Color _specular, Color _material, double _specularConst, double _refractConst)
        {
            distance = _distance;
            normal = _normal;
            ambient = _ambient;
            diffuse = _diffuse;
            specular = _specular;
            material = _material;
            specularConst = _specularConst;
            refractConst = _refractConst;
        }
    }
}
