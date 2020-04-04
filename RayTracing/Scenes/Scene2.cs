using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Media3D;
using System.Drawing;

namespace RayTracing
{
    public class Scene2 : Scene
    {
        public Scene2()
        {
            frustum = new Frustum(new Point3D(800, 200, 600), new Vector3D(-4, -1, -3), 1000, 0, 1600, 900, 4);
            ambient = Color.FromArgb(30, 30, 30);

            lights = new List<Light>();
            PointLight pLight = new PointLight(Color.FromArgb(255, 255, 255), new Point3D(100, 100, 100), 100);
            lights.Add(pLight);

            objects = new List<Traceable>();
            Plane4 plane = new Plane4(new Point3D(-1000, 0, -1000), new Point3D(-1000, 0, 1000), new Point3D(1000, 0, -1000), new Point3D(1000, 0, 1000), Color.FromArgb(135, 112, 72), Color.FromArgb(130, 208, 166), Color.White, 50);
            objects.Add(plane);

            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    if (i != 3 || j != 3)
                    {
                        Sphere sphere = new Sphere(new Point3D(80 * (i - 2), 20, 80 * (j - 2)), 20, Color.FromArgb(62, 81, 163), Color.FromArgb(13, 32, 210), Color.FromArgb(221, 255, 198), Color.White, 50, 0);
                        objects.Add(sphere);
                    }
                }
            }
        }
    }
}
