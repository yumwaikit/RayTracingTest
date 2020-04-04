using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Media3D;
using System.Drawing;

namespace RayTracing
{
    public class Scene4 : Scene
    {
        public Scene4()
        {
            frustum = new Frustum(new Point3D(800, 0, 800), new Vector3D(-1, 0, -1), 800, 0, 1080, 1920, 4);
            ambient = Color.FromArgb(30, 30, 30);

            lights = new List<Light>();
            PointLight pLight = new PointLight(Color.FromArgb(255, 255, 255), new Point3D(-400, 0, 0), 400);
            lights.Add(pLight);
            pLight = new PointLight(Color.FromArgb(255, 255, 255), new Point3D(0, 0, -400), 400);
            lights.Add(pLight);
            pLight = new PointLight(Color.FromArgb(255, 255, 255), new Point3D(0, 0, 400), 400);
            lights.Add(pLight);

            objects = new List<Traceable>();
            Plane4 plane = new Plane4(new Point3D(-1000, -120, -1000), new Point3D(-1000, -120, 1000), new Point3D(1000, -120, -1000), new Point3D(1000, -120, 1000), Color.FromArgb(135, 112, 72), Color.FromArgb(130, 208, 166), Color.White, 50);
            objects.Add(plane);
            plane = new Plane4(new Point3D(-1000, 120, -1000), new Point3D(1000, 120, -1000), new Point3D(-1000, 120, 1000), new Point3D(1000, 120, 1000), Color.FromArgb(135, 112, 72), Color.FromArgb(130, 208, 166), Color.White, 50);
            objects.Add(plane);

            Sphere sphere;
            Box box;
            for (int i = 0; i < 16; i++)
            {
                for (int j = 0; j < 16; j++)
                {
                    if ((i != 8 && i !=9) || (j != 8 && j != 9))
                    {
                        sphere = new Sphere(new Point3D(80 * (i - 7), -80, 80 * (j - 7)), 20, Color.FromArgb(62, 81, 163), Color.FromArgb(13, 32, 210), Color.FromArgb(221, 255, 198), Color.White, 50, 0);
                        objects.Add(sphere);
                        box = new Box(new Point3D(80 * (i - 7), -110, 80 * (j - 7)), 5, 5, 20, 0, 0, 0, Color.Blue, Color.Blue, Color.White, Color.White, 10, 0);
                        objects.Add(box);

                        sphere = new Sphere(new Point3D(80 * (i - 7), 80, 80 * (j - 7)), 20, Color.FromArgb(62, 81, 163), Color.FromArgb(13, 32, 210), Color.FromArgb(221, 255, 198), Color.White, 50, 0);
                        objects.Add(sphere);
                        box = new Box(new Point3D(80 * (i - 7), -110, 80 * (j - 7)), 5, 5, 20, 0, 0, 0, Color.Blue, Color.Blue, Color.White, Color.White, 10, 0);
                        objects.Add(box);
                    }
                }
            }

            sphere = new Sphere(new Point3D(0, 0, 0), 100, Color.FromArgb(20, 20, 20), Color.FromArgb(20, 20, 20), Color.White, Color.White, 50, 1.15);
            objects.Add(sphere);

            box = new Box(new Point3D(-200, 0, 0), 20, 20, 240, 0, 0, 0, Color.Red, Color.Red, Color.White, Color.White, 10, 0);
            objects.Add(box);
            box = new Box(new Point3D(200, 0, 0), 20, 20, 240, 0, 0, 0, Color.Red, Color.Red, Color.White, Color.White, 10, 0);
            objects.Add(box);
            box = new Box(new Point3D(0, 0, -200), 20, 20, 240, 0, 0, 0, Color.Red, Color.Red, Color.White, Color.White, 10, 0);
            objects.Add(box);
            box = new Box(new Point3D(0, 0, 200), 20, 20, 240, 0, 0, 0, Color.Red, Color.Red, Color.White, Color.White, 10, 0);
            objects.Add(box);

        }
    }
}
