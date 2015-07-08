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
using Telerik.Windows.Controls.GridView;
using WalzExplorer.Controls.RHSTabs;

namespace WalzExplorer.Windows
{
    /// <summary>
    /// Interaction logic for DrilldownView.xaml
    /// </summary>
    public partial class PaletteView : Window
    {
        PaletteViewModel vm;

        public PaletteView()
        {
            InitializeComponent();
            vm = new PaletteViewModel();

            grd.AutoGenerateColumns = false;
            //grd.AutoGeneratingColumn += grd_AutoGeneratingColumn;
            grd.GroupRenderMode = GroupRenderMode.Flat;
            grd.AlternationCount = 4;
            grd.CanUserFreezeColumns = true;
            grd.GridLinesVisibility = GridLinesVisibility.None;
            grd.SelectionMode = SelectionMode.Extended;


            grd.ShowColumnHeaders = true;
            grd.ShowGroupPanel = true;
            grd.ShowColumnFooters = true;
            

            grd.DataContext = vm;
            grd.ItemsSource = vm.data;
        }

        //void grd_AutoGeneratingColumn(object sender, Telerik.Windows.Controls.GridViewAutoGeneratingColumnEventArgs e)
        //{
        //   if (e.Column.UniqueName=="display")
        //   {
        //       Style s= grd.FindResource("{StaticResource Colour_CellStyle}") as Style;
        //       e.Column.CellStyle = s;
        //   }
        //}

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
           
           
        }
    }
}
