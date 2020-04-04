using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Media3D;
using System.Drawing;

namespace RayTracing
{
    public class Scene1 : Scene
    {
        public Scene1()
        {
            frustum = new Frustum(new Point3D(800, 600, 400), new Vector3D(-4, -3, -2), 1000, 0, 1600, 900, 4);
            ambient = Color.FromArgb(30, 30, 30);

            lights = new List<Light>();
            PointLight pLight = new PointLight(Color.FromArgb(255, 255, 255), new Point3D(-100, 100, -300), 200);
            lights.Add(pLight);
            DirectionalLight dLight = new DirectionalLight(Color.FromArgb(100, 100, 100), new Vector3D(-1, -1, 0));
            lights.Add(dLight);

            objects = new List<Traceable>();
            Plane4 plane = new Plane4(new Point3D(-1000, 0, -1000), new Point3D(-1000, 0, 1000), new Point3D(1000, 0, -1000), new Point3D(1000, 0, 1000), Color.FromArgb(135, 112, 72), Color.FromArgb(130, 208, 166), Color.White, 5);
            objects.Add(plane);

            Box box = new Box(new Point3D(0, 25, 30), 50, 50, 50, 0, 0, 0, Color.FromArgb(211, 146, 128), Color.FromArgb(244, 158, 66), Color.White, Color.White, 10, 0);
            objects.Add(box);
            box = new Box(new Point3D(50, 15, 20), 30, 30, 30, 0, 0, 0, Color.FromArgb(52, 146, 199), Color.FromArgb(14, 158, 254), Color.White, Color.White, 6, 0);
            objects.Add(box);

            Sphere sphere = new Sphere(new Point3D(0, 40, -50), 40, Color.FromArgb(31, 93, 172), Color.FromArgb(42, 72, 255), Color.White, Color.White, 20, 0);
            objects.Add(sphere);
        }
    }
}
