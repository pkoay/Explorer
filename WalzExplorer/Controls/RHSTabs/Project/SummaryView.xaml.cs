
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
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


    public partial class SummaryView : RHSTabViewBase
    {
      
        SummaryViewModel vm;

        public SummaryView()
        {
            InitializeComponent();
        }
        
        public override void TabLoad()
        {
            vm = new SummaryViewModel(settings);
            grd.grd.DataContext = vm;
            grd.grd.ItemsSource = vm.data;

            grd.SetGrid(settings);
            //grd.Reset();
            grd.columnSettings.developer.Add("RowVersion");
            grd.columnSettings.developer.Add("ProjectID");
            grd.columnSettings.developer.Add("AXDataAreaID");
            grd.columnSettings.developer.Add("SortOrder");
            grd.columnSettings.developer.Add("UpdatedBy");
            grd.columnSettings.developer.Add("UpdatedDate");

            grd.columnSettings.rename.Add("AXProjectID", "ID");
            grd.columnSettings.rename.Add("OperationsManager", "Ops Manager");

            grd.columnSettings.format.Add("AXProjectID", Grid.Grid_Read.columnFormat.COUNT);
            grd.columnSettings.format.Add("Contract", Grid.Grid_Read.columnFormat.TWO_DECIMAL);
            grd.columnSettings.format.Add("CostBudget", Grid.Grid_Read.columnFormat.TWO_DECIMAL);
            grd.columnSettings.format.Add("Cost", Grid.Grid_Read.columnFormat.TWO_DECIMAL);
            grd.columnSettings.format.Add("Committed", Grid.Grid_Read.columnFormat.TWO_DECIMAL);
            grd.columnSettings.format.Add("Invoiced", Grid.Grid_Read.columnFormat.TWO_DECIMAL);
          
            grd.columnSettings.toolTip.Add("Contract", "Contract Value including all approved variations");
            grd.columnSettings.toolTip.Add("CostBudget", "Cost Budget including all approved variations");
            grd.columnSettings.toolTip.Add("Cost", "Costs to date (Includes Overheads) (Excludes Committed Costs)");
            grd.columnSettings.toolTip.Add("Committed", "Committed Costs to date (Open purchase orders)");
            grd.columnSettings.toolTip.Add("Invoiced", "Invoiced to client to date");

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
        //        case "AXProjectID":
        //            e.Column.AggregateFunctions.Add(new CountFunction() { Caption = "Count:" });
        //            column.ShowColumnWhenGrouped = false;
        //            break;
        //        case "Contract":
        //            SetColumn(column, "TWO_DECIMAL");
        //            ColumnToolTipStatic(grd, column, );
        //            break;
        //        case "Cost":
        //            SetColumn(column, "TWO_DECIMAL");
        //            ColumnToolTipStatic(grd,column, );
        //            break;
        //        case "Committed":
        //            SetColumn(column, "TWO_DECIMAL");
        //            ColumnToolTipStatic(grd, column, );
        //            break;
        //        case "Invoiced":
        //            SetColumn(column, "TWO_DECIMAL");
        //            ColumnToolTipStatic(grd, column,);
        //            break;
        //    }
        //}

     
      
    }

}
