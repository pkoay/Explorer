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
                    m.SortOrder = viewModel.SortOrderNumber((tblMain)grd.SelectedItem);

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

        //#region DRAG DROP
        //// Based off http://wpftutorial.net/DragAndDrop.html

        //public bool IsDragging { get; set; }


        //private void grd_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        //{
        //    var s = e.OriginalSource as FrameworkElement;
        //    if (s is TextBlock)
        //    {
        //        var parentRow = s.ParentOfType<GridViewRow>();

        //        //only drag selected rows while not edting grid
        //        if (parentRow != null && parentRow.IsSelected && !this.isGridEditing)
        //        {
        //            // determine Drag String
        //            string drag = "";
        //            foreach (GridViewRow r in (grd.ChildrenOfType<GridViewRow>()).Where(r => r.IsSelected == true))
        //            {
        //                foreach (GridViewCellBase c in r.Cells)
        //                {
        //                    if (c.Visibility == System.Windows.Visibility.Visible)
        //                    {
        //                        TextBlock t = (TextBlock)c.Content;
        //                        drag = drag + c.Column.Header.ToString() + "=" + t.Text + ",";
        //                    }
        //                }
        //                drag.TrimEnd(',');
        //                drag = drag + Environment.NewLine;
        //            }

        //        }

        //    }

        //}


      

       

        //private void grd_DragEnter(object sender, System.Windows.DragEventArgs e)
        //{
           
        //    if (!e.Data.GetDataPresent("myFormat") ||sender == e.Source)
        //    {
        //        e.Effects = DragDropEffects.None;
        //    }
        //}

        //private void grd_Drop(object sender, System.Windows.DragEventArgs e)
        //{
        //    if (e.Data.GetDataPresent("myFormat"))
        //    {
        //        //MessageBox.Show("DROP");
        //    }
        //}
      

      

      
        //#endregion
        //private void grd_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        //{
        //    // Store the mouse position
        //    startPoint = e.GetPosition(null);
        //} 

        //private void grd_PreviewMouseMove(object sender, MouseEventArgs e)
        //{
        //    if (startPoint != new Point())
        //    {
        //        // Get the current mouse position
        //        Point mousePos = e.GetPosition(null);
        //        Vector diff = startPoint - mousePos;

        //        if (e.LeftButton == MouseButtonState.Pressed &&
        //            Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance ||
        //            Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance)
        //        {
        //            //// Get the dragged ListViewItem
        //            //ListView listView = sender as ListView;
        //            //ListViewItem listViewItem =
        //            //    FindAnchestor<ListViewItem>((DependencyObject)e.OriginalSource);

        //            //// Find the data behind the ListViewItem
        //            //Contact contact = (Contact)listView.ItemContainerGenerator.
        //            //    ItemFromContainer(listViewItem);

        //            // Initialize the drag & drop operation
        //            DataObject dragData = new DataObject("myFormat", new object());
        //            DragDrop.DoDragDrop(grd, dragData, DragDropEffects.Move);
        //        }
        //    }
        //}
      
    }
}
