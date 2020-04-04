using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Media3D;
using System.Drawing;
using System.Threading;
using System.IO;

namespace RayTracing
{
    public class Frustum
    {
        Space3D _space;
        Point3D _center;
        Vector3D _direction;
        double _distance;
        double _rotate;
        double _zoom;

        Vector3D v1;
        Vector3D v2;
        Bitmap renderer;
        int _width;
        int _height;

        int num_thread = 40;
        List<Thread> threads;
        public event Action<Bitmap, int, int, Color> UpdateBitmap;
        public event Action<Bitmap> UpdateImage;
        public event Action RenderComplete;

        public Frustum(Point3D center, Vector3D direction, double distance, double rotate, int width, int height, double zoom)
        {
            _center = center;
            _direction = direction / direction.Length;
            _distance = distance;
            _rotate = rotate;
            _zoom = zoom;

            _width = width;
            _height = height;
            renderer = new Bitmap(_width, _height);
            v1 = new Vector3D(-_direction.Z, 0, _direction.X);
            v1 /= v1.Length;
            v2 = Vector3D.CrossProduct(_direction, v1);
            v2 /= v2.Length;

            threads = new List<Thread>();
        }

        public void Render(Space3D space)
        {
            _space = space;
            int renderSize = _width / num_thread;

            for (int i = 0; i < num_thread; i++)
            {
                int start = renderSize * i - _width / 2;
                Thread t = new Thread(() => tRender(start, start + renderSize));
                threads.Add(t);
                t.Start();
            }
            Thread t1 = new Thread(tWaitRender);
            threads.Add(t1);
            t1.Start();
        }

        void tRender(int start, int end)
        {
            for (int i = start; i < end; i++)
            {
                for (int j = -_height / 2; j < _height / 2; j++)
                {
                    Point3D p = _center + _direction * _distance + v1 * i / _zoom + v2 * j / _zoom;
                    Vector3D v = (Vector3D)p - (Vector3D)_center;
                    v /= v.Length;
                    Ray ray = new Ray(v, _center, 1, 1);
                    if (UpdateBitmap != null)
                    {
                        UpdateBitmap(renderer, i + _width / 2, j + _height / 2, ray.TraceRay(_space));
                    }
                }
            }
            renderCounter++;
        }
        int renderCounter = 0;
        void tWaitRender()
        {
            while (renderCounter < num_thread)
            {
                Thread.Sleep(1000);
                if (UpdateImage != null)
                {
                    UpdateImage(renderer);
                }
            }

            if (RenderComplete != null)
            {
                RenderComplete();
            }
        }

        public void SaveImage()
        {
            string _dir = Directory.GetCurrentDirectory();
            renderer.Save(_dir + "\\renderer.bmp");
        }
    }
}
