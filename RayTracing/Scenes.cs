using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Media3D;
using System.Drawing;

namespace RayTracing
{
    public abstract class Scene
    {
        public Frustum frustum;
        public Color ambient;
        public List<Light> lights;
        public List<Traceable> objects;
    }
}
