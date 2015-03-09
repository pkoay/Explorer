
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
            base.Reset(grd);


            GridColumnSettings setting = new GridColumnSettings();
            setting.columnReadOnlyDeveloper.Add("RowVersion");
            setting.columnReadOnlyDeveloper.Add("ProjectID");
            setting.columnRename.Add("AXProjectID", "ID");
            setting.columnRename.Add("OperationsManager", "Ops Manager");
            setting.columnReadOnlyDeveloper.Add("AXDataAreaID");
            setting.columnReadOnlyDeveloper.Add("SortOrder");
            setting.columnReadOnlyDeveloper.Add("UpdatedBy");
            setting.columnReadOnlyDeveloper.Add("UpdatedDate");
            gridColumnSettings.Add(grd, setting);

            // set grid data
            vm = new InvoicedViewModel(settings);
            grd.DataContext = vm;
            grd.ItemsSource = vm.data;
           
            base.TabLoad();

        }

     
      
    }

}
