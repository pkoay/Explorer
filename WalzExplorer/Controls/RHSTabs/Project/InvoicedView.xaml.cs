
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;
using Telerik.Windows.Data;
using WalzExplorer.Common;
namespace WalzExplorer.Controls.RHSTabs.Project
{
    /// <summary>
    /// Interaction logic for TenderViewer.xaml
    /// </summary>


    public partial class InvoicedView : RHSTabViewBase
    {

        InvoicedViewModel vm;

        public InvoicedView()
        {
            InitializeComponent();
        }

        public override void TabLoad()
        {

            vm = new InvoicedViewModel(settings);
            grd.grd.DataContext = vm;
            grd.grd.ItemsSource = vm.data;

            grd.SetGrid(settings);
            grd.columnSettings.developer.Add("DataAreaID");
            grd.columnSettings.format.Add("PurchaseOrderID", Grid.Grid_Read.columnFormat.COUNT);
            grd.columnSettings.format.Add("ProjID", Grid.Grid_Read.columnFormat.TEXT);
            grd.columnSettings.format.Add("InvoiceDate", Grid.Grid_Read.columnFormat.DATE);
            grd.columnSettings.format.Add("ProjTransType", Grid.Grid_Read.columnFormat.TEXT);
            grd.columnSettings.format.Add("TransId", Grid.Grid_Read.columnFormat.TEXT);
            grd.columnSettings.format.Add("Invoiced", Grid.Grid_Read.columnFormat.TWO_DECIMAL);
        }
        public override string IssueIfClosed()
        {
            return "";
        }

      
    }

}
