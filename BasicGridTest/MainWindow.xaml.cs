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
using Telerik.Windows.DragDrop;

namespace BasicGridTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly BASICGRIDDATAEntities context;
        private readonly MyViewModel viewModel;
        private Point startPoint;
        bool isGridEditing = false; 

        public MainWindow()
        {
            InitializeComponent();
            viewModel = ((MyViewModel)grd.DataContext);
            context = viewModel.context;

        }
        private void grd_BeginningEdit(object sender, GridViewBeginningEditRoutedEventArgs e)
        {
            this.isGridEditing = true;
        }

        private void grd_CellEditEnded(object sender, GridViewCellEditEndedEventArgs e)
        {
            this.isGridEditing = false;
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
            this.isGridEditing = false;
        }

        private void cmGrid_Click(object sender, RoutedEventArgs e)
        {
            MenuItem mi = (MenuItem)sender;
            switch (mi.Name)
            {
                case "miInsert":

                    tblMain m = context.tblMains.Create();
                    

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

        private void grd_DataLoaded(object sender, EventArgs e)
        {
            //Set some fields invisible
            foreach (Telerik.Windows.Controls.GridViewColumn c in grd.Columns)
            {
                if (c.UniqueName.StartsWith("tbl")) c.IsVisible = false;
                if (c.UniqueName == "RowVersion") c.IsVisible = false;
                //if (c.UniqueName == "TenderID") c.IsVisible = false;
            }
            //set some fields readonly
            foreach (Telerik.Windows.Controls.GridViewColumn c in grd.Columns)
            {
                if (c.UniqueName == "SortOrder") c.IsReadOnly = true;
                if (c.UniqueName == "UpdatedBy") c.IsReadOnly = true;
                if (c.UniqueName == "UpdatedDate") c.IsReadOnly = true;
            }
        }

      
      
    }
}
