using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Media3D;
using System.Drawing;

namespace RayTracing
{
    public class Box : Figure
    {
        public Point3D _center;
        public double _length;
        public double _width;
        public double _height;
        public double _rotateX;
        public double _rotateY;
        public double _rotateZ;

        List<Plane> planes = new List<Plane>();

        public Box(Point3D center, double length, double width, double height, double rotateX, double rotateY, double rotateZ, Color ambient, Color diffuse, Color specular, Color material, double specularConst, double refractConst)
        {
            _center = center;
            _length = length;
            _width = width;
            _height = height;
            _rotateX = rotateX / 180 * Math.PI;
            _rotateY = rotateY / 180 * Math.PI;
            _rotateZ = rotateZ / 180 * Math.PI;

            _ambient = ambient;
            _diffuse = diffuse;
            _specular = specular;
            _material = material;
            _specularConst = specularConst;
            _refractConst = refractConst;

            CreatePlanes();
        }

        void CreatePlanes()
        {
            Vector3D x = new Vector3D(1, 0, 0);
            Vector3D y = new Vector3D(0, 1, 0);
            Vector3D z = new Vector3D(0, 0, 1);
            Matrix3D mrX = new Matrix3D(1, 0, 0, 0, 0, Math.Cos(_rotateX), -Math.Sin(_rotateX), 0, 0, Math.Sin(_rotateX), Math.Cos(_rotateX), 0, 0, 0, 0, 0);
            Matrix3D mrY = new Matrix3D(Math.Cos(_rotateY), 0, Math.Sin(_rotateY), 0, 0, 1, 0, 0, -Math.Sin(_rotateX), 0, Math.Cos(_rotateX), 0, 0, 0, 0, 0);
            Matrix3D mrZ = new Matrix3D(Math.Cos(_rotateZ), -Math.Sin(_rotateZ), 0, 0, Math.Sin(_rotateZ), Math.Cos(_rotateZ), 0, 0, 0, 0, 1, 0, 0, 0, 0, 0);

            Point3D[] p = new Point3D[8];

            for (int i = 0; i < 8; i++)
            {
                Vector3D v;
                switch (i)
                {
                    case 0:
                        v = (Vector3D)(-_length / 2 * x - _width / 2 * z - _height / 2 * y);
                        break;
                    case 1:
                        v = (Vector3D)(-_length / 2 * x + _width / 2 * z - _height / 2 * y);
                        break;
                    case 2:
                        v = (Vector3D)(-_length / 2 * x - _width / 2 * z + _height / 2 * y);
                        break;
                    case 3:
                        v = (Vector3D)(-_length / 2 * x + _width / 2 * z + _height / 2 * y);
                        break;
                    case 4:
                        v = (Vector3D)(_length / 2 * x - _width / 2 * z - _height / 2 * y);
                        break;
                    case 5:
                        v = (Vector3D)(_length / 2 * x + _width / 2 * z - _height / 2 * y);
                        break;
                    case 6:
                        v = (Vector3D)(_length / 2 * x - _width / 2 * z + _height / 2 * y);
                        break;
                    default:
                        v = (Vector3D)(_length / 2 * x + _width / 2 * z + _height / 2 * y);
                        break;
                }
                v = Vector3D.Multiply(v, mrX);
                v = Vector3D.Multiply(v, mrY);
                v = Vector3D.Multiply(v, mrZ);
                p[i] = _center + v;
            }

            planes.Add(new Plane4(p[0], p[1], p[2], p[3], _ambient, _diffuse, _specular, _specularConst));
            planes.Add(new Plane4(p[4], p[6], p[5], p[7], _ambient, _diffuse, _specular, _specularConst));
            planes.Add(new Plane4(p[2], p[3], p[6], p[7], _ambient, _diffuse, _specular, _specularConst));
            planes.Add(new Plane4(p[0], p[4], p[1], p[5], _ambient, _diffuse, _specular, _specularConst));
            planes.Add(new Plane4(p[0], p[2], p[4], p[6], _ambient, _diffuse, _specular, _specularConst));
            planes.Add(new Plane4(p[1], p[5], p[3], p[7], _ambient, _diffuse, _specular, _specularConst));
        }

        public override Intersection FindIntersect(Ray ray)
        {
            List<Intersection> intersections = new List<Intersection>();
            foreach (Plane p in planes)
            {
                if (p.FindIntersect(ray) != null)
                {
                    intersections.Add(p.FindIntersect(ray));
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

            if (index > -1)
            {
                intersections[index].material = _material;
                intersections[index].refractConst = _refractConst;
                return intersections[index];
            }
            else
                return null;
        }
    }
}
