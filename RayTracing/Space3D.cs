using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Media3D;
using System.Drawing;

namespace RayTracing
{
    public class Space3D
    {
        public Frustum frustum;
        public Color ambient;
        public List<Light> lights = new List<Light>();
        public List<Traceable> objects = new List<Traceable>();
        public int MaxDepth = 6;

        public Space3D(Scene scene, Action<Bitmap, int, int, Color> updatebitmap, Action<Bitmap> updateimage, Action rendercomplete)
        {
            frustum = scene.frustum;
            frustum.UpdateBitmap += updatebitmap;
            frustum.UpdateImage += updateimage;
            frustum.RenderComplete += rendercomplete;

            double r = scene.ambient.R;
            double g = scene.ambient.G;
            double b = scene.ambient.B;

            foreach (Light light in scene.lights)
            {
                r += light._color.R;
                g += light._color.G;
                b += light._color.B;
            }

            r = Math.Max(r, 255);
            g = Math.Max(g, 255);
            b = Math.Max(b, 255);

            ambient = Color.FromArgb((int)(scene.ambient.R * 255 / r), (int)(scene.ambient.G * 255 / g), (int)(scene.ambient.B * 255 / b));
            foreach (Light light in scene.lights)
            {
                light._color = Color.FromArgb((int)(light._color.R * 255 / r), (int)(light._color.G * 255 / g), (int)(light._color.B * 255 / b));
                lights.Add(light);
            }

            objects = scene.objects;
        }

        public void Render()
        {
            if (frustum != null)
            {
                frustum.Render(this);
            }
        }

        public void SaveImage()
        {
            frustum.SaveImage();
        }
    }
}
