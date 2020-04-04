using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Media3D;
using System.Drawing;

namespace RayTracing
{
    public abstract class Light
    {
        public Color _color;
        abstract public Color getColor(Point3D point);
        abstract public Vector3D getDirection(Point3D point);
    }

    public class DirectionalLight : Light
    {
        public Vector3D _direction;

        public DirectionalLight(Color color, Vector3D direction)
        {
            _color = color;
            _direction = direction / direction.Length;
        }

        public override Color getColor(Point3D point)
        {
            return _color;
        }

        public override Vector3D getDirection(Point3D point)
        {
            return _direction;
        }
    }

    public class PointLight : Light
    {
        public Point3D _point;
        public double _intensity;

        public PointLight(Color color, Point3D point, double intensity)
        {
            _color = color;
            _point = point;
            _intensity = intensity;
        }

        public override Color getColor(Point3D point)
        {
            double strength = _intensity / ((Vector3D)point - (Vector3D)_point).Length;
            return Color.FromArgb((int)Math.Min(_color.R * strength, 255), (int)Math.Min(_color.G * strength, 255), (int)Math.Min(_color.B * strength, 255));
        }

        public override Vector3D getDirection(Point3D point)
        {
            Vector3D v = (Vector3D)point - (Vector3D)_point;
            return v / v.Length;
        }
    }

}
