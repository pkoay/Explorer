using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WalzExplorer.Common
{
    public static class GraphicsLibrary
    {
        public static Rectangle ResourceIconCanvasToSize(string name, int width, int height)
        {
            Rectangle r = new Rectangle();
            r.Height = height;
            r.Width = width;
            r.Stretch = Stretch.Fill;

            ResourceDictionary rdIcon = new ResourceDictionary();
            rdIcon.Source = new Uri("/WalzExplorer;component/Resources/Icons.xaml", UriKind.RelativeOrAbsolute);
            Canvas cc = rdIcon[name] as Canvas;
            VisualBrush vb = new VisualBrush(cc);
            vb.Stretch = Stretch.Fill;
            r.Fill = vb;
            return r;
        }

        public static SolidColorBrush BrushFromHex(string hex)
        {
         return   (SolidColorBrush)(new BrushConverter().ConvertFrom(hex));
        }
    }
}
