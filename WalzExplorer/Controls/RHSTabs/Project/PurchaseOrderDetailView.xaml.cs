
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;
using Telerik.Windows.Data;
using WalzExplorer.Common;
namespace WalzExplorer.Controls.RHSTabs.Project
{
    /// <summary>
    /// Interaction logic for TenderViewer.xaml
    /// </summary>


    public partial class PurchaseOrderDetailView : RHSTabViewBase
    {
      
        PurchaseOrderDetailViewModel vm;

        public PurchaseOrderDetailView()
        {
            InitializeComponent();
        }

        public override void TabLoad()
        {
            vm = new PurchaseOrderDetailViewModel(settings);
            grd.grd.DataContext = vm;
            grd.grd.ItemsSource = vm.data;

            grd.SetGrid(settings);
            
            grd.columnSettings.developer.Add("DataAreaId");
            grd.columnSettings.developer.Add("IsCommitted");
            grd.columnSettings.format.Add("ProjId", Grid.Grid_Read.columnFormat.COUNT);
            grd.columnSettings.format.Add("Date", Grid.Grid_Read.columnFormat.DATE);
            grd.columnSettings.rename.Add("PurchID", "Purchase Order ID");
            grd.columnSettings.format.Add("OrderAmount", Grid.Grid_Read.columnFormat.TWO_DECIMAL);
            grd.columnSettings.format.Add("CommittedAmount", Grid.Grid_Read.columnFormat.TWO_DECIMAL);
            grd.columnSettings.format.Add("PaidAmount", Grid.Grid_Read.columnFormat.TWO_DECIMAL);
            grd.columnSettings.format.Add("Quantity", Grid.Grid_Read.columnFormat.TWO_DECIMAL);
            grd.columnSettings.format.Add("CategoryGroup", Grid.Grid_Read.columnFormat.TEXT);
            grd.columnSettings.format.Add("CategoryName", Grid.Grid_Read.columnFormat.TEXT);
            grd.columnSettings.format.Add("PurchQtyPrice", Grid.Grid_Read.columnFormat.TWO_DECIMAL_NO_TOTAL);
            
            
            
        }


        public override string IssueIfClosed()
        {
            return "";
        }
       

       
     
      
    }

}
