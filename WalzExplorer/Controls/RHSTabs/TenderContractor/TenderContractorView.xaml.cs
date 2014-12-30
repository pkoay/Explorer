//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.SqlClient;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows;
//using System.Windows.Controls;
//using System.Windows.Data;
//using System.Windows.Documents;
//using System.Windows.Input;
//using System.Windows.Media;
//using System.Windows.Media.Imaging;
//using System.Windows.Navigation;
//using System.Windows.Shapes;
//using Telerik.Windows.Controls;
//using Telerik.Windows.Controls.GridView;
//using Telerik.Windows.Controls.Input;
//using Telerik.Windows.Data;
//using System.Data.Entity;
//using System.Data.Entity.Infrastructure;
//using WalzExplorer.Database;
//using System.Text.RegularExpressions;
//using WalzExplorer.Controls.Common;
//using System.Data.Entity.Validation;

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;
using WalzExplorer.Common;
namespace WalzExplorer.Controls.RHSTabs.TenderContractor
{
    /// <summary>
    /// Interaction logic for TenderViewer.xaml
    /// </summary>
    
     
    public partial class TenderContractorView : RHSTabGridViewBase
    {
        const string GridDragData="GridDragData";
        Point startPoint;
        TenderContractorViewModel vm;
        //private TenderContractorViewModel viewModel;
        public TenderContractorView()
        {
            InitializeComponent();
            base.SetGrid(grd);

            columnNotRequired.Add("RowVersion");
            columnNotRequired.Add("TenderID");
            columnRename.Add("ContractorID", "ID");
            columnReadOnly.Add("ContractorID");
            columnReadOnly.Add("SortOrder");
            columnReadOnly.Add("UpdatedBy");
            columnReadOnly.Add("UpdatedDate");
           
        }


        public override void TabLoad()
        {
            // set grid data
            vm = new TenderContractorViewModel(Convert.ToInt32(node.ID));
            viewModel = vm;
            grd.DataContext = viewModel;
            grd.ItemsSource = viewModel.data;
            columnCombo.Clear();
            columnCombo.Add("ContractorTypeID", GridLibrary.CreateCombo("cmbContractorTypeID", "Contractor Type", vm.cmbContractTypeList(), "Title"));


            base.TabLoad();

        }

        private void grd_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Store the mouse position
            startPoint = e.GetPosition(null);
        }

        private void grd_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            // Get the current mouse position
            Point mousePos = e.GetPosition(null);
            Vector diff = startPoint - mousePos;

            if (e.LeftButton == MouseButtonState.Pressed &&
                (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance ||
                Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance))
            {
                // Initialize the drag & drop operation
                DataObject dragData = new DataObject(GridDragData, grd.SelectedItems);
                DragDrop.DoDragDrop(grd, dragData, DragDropEffects.Move);
            } 
        }

        private void grd_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(GridDragData))
            {
                
                GridViewRow row = FindAnchestor<GridViewRow>((DependencyObject)e.OriginalSource);
                if (row != null)
                {
                     List<object> moveItems= new List<object> ();
                     foreach (object item in grd.SelectedItems)
                         moveItems.Add(item);
                     viewModel.MoveItemsToItem(moveItems, row.Item);
                }
            }
        }

        // Helper to search up the VisualTree
        private static T FindAnchestor<T>(DependencyObject current)
            where T : DependencyObject
        {
            do
            {
                if (current is T)
                {
                    return (T)current;
                }
                current = VisualTreeHelper.GetParent(current);
            }
            while (current != null);
            return null;
        }

        private void grd_DragEnter(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(GridDragData))
            {
                e.Effects = DragDropEffects.Move;
            }
        }

      
    }

}
