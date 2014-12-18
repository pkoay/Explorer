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
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;

namespace BasicGridTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly BASICGRIDDATAEntities context;
        private readonly MyViewModel viewModel;
        public MainWindow()
        {
            InitializeComponent();
            viewModel = ((MyViewModel)grd.DataContext);
            context = viewModel.context;
            

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
            //this is the function that saves the data back to the database
            //it should contain all error handling 

            tblMain m = (tblMain)e.EditedItem;

            // If item not in database 
            if (context.Entry(m).State == System.Data.Entity.EntityState.Detached)
            {
                //Add item to dataabse 
                m = context.tblMains.Add(m);
            }
            context.SaveChanges();

            //also update view model with database generated data e.g. Identity auto increment, Modified by, Modified date, etc.
            if (context.Entry(m).State == System.Data.Entity.EntityState.Added)
                context.Entry(m).Reload();

            //redisplay new values such as ID
            grd.Rebind();
        }

        private void cmGrid_Click(object sender, RoutedEventArgs e)
        {
            MenuItem mi = (MenuItem)sender;
            switch (mi.Name)
            {
                case "miInsert":

                    tblMain m = context.tblMains.Create();

                    m.SortOrder = viewModel.StortOrderNumber((tblMain)grd.SelectedItem);
                    m.TypeID = 1;

                    // insert in the model and the "RowEditEnded" handles write to database
                    m = viewModel.Insert(m, (tblMain)grd.SelectedItem);
                    grd.CurrentItem = m;
                    grd.BeginEdit();

                    break;

                case "miInsertPaste":
                    

                    break;
                default:
                    MessageBox.Show(mi.Header.ToString(), "Configuration menu", MessageBoxButton.OK, MessageBoxImage.Information);
                    break;
            }
        }
        
    }
}
