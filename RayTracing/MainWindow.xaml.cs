using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Animation;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Drawing;
using System.IO;

namespace RayTracing
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Space3D space;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            space = new Space3D(new Scene4(), UpdateBitmap, UpdateImage, RenderComplete);

            space.Render();

            this.SizeToContent = SizeToContent.WidthAndHeight;
        }

        private void UpdateBitmap(Bitmap bitmap, int x, int y, System.Drawing.Color color)
        {
            this.Dispatcher.Invoke(new Action(() =>
            {
                bitmap.SetPixel(x, y, color);
            }));
        }
        private void UpdateImage(Bitmap bitmap)
        {
            this.Dispatcher.Invoke(new Action(() =>
            {
                Im.Source = ConvertFromBitmap(bitmap);
            }));
        }

        public BitmapImage ConvertFromBitmap(Bitmap bitmap)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
                memory.Position = 0;
                BitmapImage bitmapimage = new BitmapImage();
                bitmapimage.BeginInit();
                bitmapimage.StreamSource = memory;
                bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapimage.EndInit();

                return bitmapimage;
            }
        }

        private void RenderComplete()
        {
            this.Dispatcher.Invoke(new Action(() =>
            {
                BdSave.Visibility = Visibility.Visible;
                DoubleAnimation da = new DoubleAnimation(0, 1, TimeSpan.FromMilliseconds(500));
                BdSave.BeginAnimation(OpacityProperty, da);
            }));
        }

        private void BdSave_MouseUp(object sender, MouseButtonEventArgs e)
        {
            space.SaveImage();
        }
    }
}
