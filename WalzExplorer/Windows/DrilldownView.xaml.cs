using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WalzExplorer.Controls.RHSTabs;

namespace WalzExplorer.Windows
{
    /// <summary>
    /// Interaction logic for DrilldownView.xaml
    /// </summary>
    public partial class DrilldownView : Window
    {
        public DrilldownView(RHSTabViewBase Drilldown)
        {
            InitializeComponent();
            const int border = 50;
            Application curApp = Application.Current;
            Window mainWindow = curApp.MainWindow;
            this.Left = mainWindow.Left + border;
            this.Top = mainWindow.Top + border;
            this.Width = mainWindow.Width - border * 2;
            this.Height = mainWindow.Height - border * 2;
            MainWindow mw = (MainWindow)mainWindow;

            System.Windows.Controls.Grid.SetRow(Drilldown, 1);
            grid.Children.Add(Drilldown);
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
           
        }
    }
}
