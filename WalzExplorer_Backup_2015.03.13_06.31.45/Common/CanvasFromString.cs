using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace WalzExplorer.Common
{
    public class CanvasFromString : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo language)
        {
            if (value != null)
            {
                ResourceDictionary rdIcon = new ResourceDictionary();
                rdIcon.Source = new Uri("/WalzExplorer;component/Resources/Icons.xaml", UriKind.RelativeOrAbsolute);
                return rdIcon[value as string] as Canvas;
            }
            else
            {
                return null;
            }

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo language)
        {
            throw new NotImplementedException();

        }
    }
}
