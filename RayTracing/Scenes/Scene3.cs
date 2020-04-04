using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Media3D;
using System.Drawing;

namespace RayTracing
{
    public class Scene3 : Scene
    {
        public Scene3()
        {
            frustum = new Frustum(new Point3D(800, 0, 10), new Vector3D(-80, 0, -1), 1000, 0, 1600, 900, 4);
            ambient = Color.FromArgb(30, 30, 30);

            lights = new List<Light>();
            PointLight pLight = new PointLight(Color.FromArgb(255, 255, 255), new Point3D(100, 100, 100), 100);
            lights.Add(pLight);
            pLight = new PointLight(Color.FromArgb(255, 255, 255), new Point3D(-100, -20, -20), 300);
            lights.Add(pLight);
            DirectionalLight dLight = new DirectionalLight(Color.FromArgb(100, 100, 100), new Vector3D(-1, -1, 0));
            lights.Add(dLight);

            objects = new List<Traceable>();
            Box box = new Box(new Point3D(0, 0, 0), 5, 5, 500, 0, 0, 0, Color.Red, Color.Red, Color.White, Color.White, 10, 0);
            objects.Add(box);
            box = new Box(new Point3D(-10, 0, 10), 5, 5, 500, 20, 0, 0, Color.Green, Color.Green, Color.White, Color.White, 10, 0);
            objects.Add(box);
            box = new Box(new Point3D(-20, 0, -5), 5, 5, 500, -20, 0, 0, Color.Blue, Color.Blue, Color.White, Color.White, 10, 0);
            objects.Add(box);

            Sphere sphere = new Sphere(new Point3D(100, 0, 0), 50, Color.FromArgb(20, 20, 20), Color.FromArgb(20, 20, 20), Color.White, Color.White, 50, 2);
            objects.Add(sphere);
        }
    }
}
