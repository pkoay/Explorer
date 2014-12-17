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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BasicGridTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly BASICGRIDDATAEntities context;

        public MainWindow()
        {
            InitializeComponent();
            context=((MyViewModel)grd.DataContext).context;

        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            tblMain m= new tblMain();
            m.Title="BRAND NEW";
            m.SortOrder = 99999;
            m.TypeID = 1;
            context.tblMains.Add(m);
            context.SaveChanges();
            grd.Rebind();
            //((MyViewModel)grd.DataContext).refreshMainView();
        }

        private void grd_RowEditEnded(object sender, Telerik.Windows.Controls.GridViewRowEditEndedEventArgs e)
        {
           
            tblMain m = (tblMain)e.EditedItem;
            if (!(context.tblMains.Local.Contains(m)))
            {
                m = context.tblMains.Add(m);
            }



            grd.CommitRowEdit(e.Row);
            
            context.SaveChanges();
            //grd.new
        }

        private void grd_AddingNewDataItem(object sender, Telerik.Windows.Controls.GridView.GridViewAddingNewEventArgs e)
        {
            
           
            
        }
        
    }
}
