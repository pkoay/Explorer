using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WalzExplorer.Common
{
    public static class BindingLibrary
    {
        public static Binding NewBinding(string name, string format)
        {
            Binding binding = new Binding(name)
            {
                StringFormat = format
            };
            return binding;
        }
    }
}
