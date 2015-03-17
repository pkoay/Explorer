
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


    public partial class CommittedView : RHSTabViewBase
    {
      
        CommittedViewModel vm;

        public CommittedView()
        {
            InitializeComponent();
        }

        public override void TabLoad()
        {
            vm = new CommittedViewModel(settings);
            grd.grd.DataContext = vm;
            grd.grd.ItemsSource = vm.data;

            grd.SetGrid(settings);
            grd.Reset();
            grd.columnSettings.developer.Add("DataAreaId");
            grd.columnSettings.format.Add("ProjId", Grid.Grid_Read.columnFormat.COUNT);
            grd.columnSettings.format.Add("Date", Grid.Grid_Read.columnFormat.DATE);
            grd.columnSettings.format.Add("CommCostAmount", Grid.Grid_Read.columnFormat.TWO_DECIMAL);
            grd.columnSettings.format.Add("Quantity", Grid.Grid_Read.columnFormat.TWO_DECIMAL);
            grd.columnSettings.format.Add("CategoryGroup", Grid.Grid_Read.columnFormat.TEXT);
            grd.columnSettings.format.Add("CategoryName", Grid.Grid_Read.columnFormat.TEXT);
            grd.columnSettings.format.Add("PurchQtyPrice", Grid.Grid_Read.columnFormat.TWO_DECIMAL_NO_TOTAL);
            
            //base.TabLoad();
            
        }

        public override string IssueIfClosed()
        {
            return "";
        }
        //private void grd_AutoGeneratingColumn(object sender, GridViewAutoGeneratingColumnEventArgs e)
        //{
        //    GridViewDataColumn column = e.Column as GridViewDataColumn;
        //    switch (e.Column.Header.ToString())
        //    {
        //        case "ProjId":
        //            e.Column.AggregateFunctions.Add(new CountFunction() { Caption = "Count:" });
        //            column.ShowColumnWhenGrouped = false;
        //            break;
        //        case "Date":
        //            SetColumn(column, "DATE");
        //            break;
        //        case "CommCostAmount":
        //            SetColumn(column, "TWO_DECIMAL");
        //            break;
        //        case "Quantity":
        //            SetColumn(column, "TWO_DECIMAL");
        //            break;
        //        case "CategoryGroup":
        //            SetColumn(column, "TEXT");
        //            break;
        //        case "CategoryName":
        //            SetColumn(column, "TEXT");
        //            break;
        //        case "PurchQtyPrice":
        //            SetColumn(column, "TWO_DECIMAL_NO_TOTAL");
        //            break;
        //    }
        //}

       
     
      
    }

}
