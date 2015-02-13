
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;
using WalzExplorer.Common;
namespace WalzExplorer.Controls.RHSTabs.Project
{
    /// <summary>
    /// Interaction logic for TenderViewer.xaml
    /// </summary>
    
     
    public partial class InvoicedView : RHSTabGridViewBase_ReadOnly
    {

        InvoicedViewModel vm;

        public InvoicedView()
        {
            InitializeComponent();
        }

        public override void TabLoad()
        {
            base.SetGrid(grd);
            base.Reset();

            columnReadOnlyDeveloper.Add("RowVersion");
            columnReadOnlyDeveloper.Add("ProjectID");
            columnRename.Add("AXProjectID", "ID");
            columnRename.Add("OperationsManager", "Ops Manager");
            columnReadOnlyDeveloper.Add("AXDataAreaID");
            columnReadOnlyDeveloper.Add("SortOrder");
            columnReadOnlyDeveloper.Add("UpdatedBy");
            columnReadOnlyDeveloper.Add("UpdatedDate");

            // set grid data
            vm = new InvoicedViewModel(settings);
            grd.DataContext = vm;
            grd.ItemsSource = vm.data;
           
            base.TabLoad();

        }

     
      
    }

}
