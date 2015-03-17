
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
            
            //grd2.grd.DataContext = vm;
            //grd2.grd.ItemsSource = vm.data;

            //grd2.SetGrid(settings);
            //grd2.Reset();
            //grd2.columnSettings.developer.Add("DataAreaId");
            //grd2.columnSettings.format.Add("ProjId", Grid.Grid_Read.columnFormat.COUNT);
            //grd2.columnSettings.format.Add("Date", Grid.Grid_Read.columnFormat.DATE);
            //grd2.columnSettings.format.Add("CommCostAmount", Grid.Grid_Read.columnFormat.TWO_DECIMAL);
            //grd2.columnSettings.format.Add("Quantity", Grid.Grid_Read.columnFormat.TWO_DECIMAL);
            //grd2.columnSettings.format.Add("CategoryGroup", Grid.Grid_Read.columnFormat.TEXT);
            //grd2.columnSettings.format.Add("CategoryName", Grid.Grid_Read.columnFormat.TEXT);
            //grd2.columnSettings.format.Add("PurchQtyPrice", Grid.Grid_Read.columnFormat.TWO_DECIMAL_NO_TOTAL);


        }
        public override string IssueIfClosed()
        {
            return "";
        }

      
    }

}
