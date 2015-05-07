
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


    public partial class P6EarnedValueView : RHSTabViewBase
    {

        P6EarnedValueViewModel vm;

        public P6EarnedValueView()
        {
            InitializeComponent();
        }

        public override void TabLoad()
        {
            vm = new P6EarnedValueViewModel(settings);
            grd.grd.DataContext = vm;
            grd.grd.ItemsSource = vm.data;

            grd.SetGrid(settings);
            grd.columnSettings.developer.Add("ProjectID");
            grd.columnSettings.developer.Add("AXProjectID");
            grd.columnSettings.developer.Add("proj_id");
            grd.columnSettings.developer.Add("baselineproj_id");
            grd.columnSettings.developer.Add("BaselineTaskID");
            grd.columnSettings.developer.Add("CurrentTaskID");
            grd.columnSettings.developer.Add("complete_pct_type");
            grd.columnSettings.developer.Add("PhysicalPercentComplete");
            grd.columnSettings.developer.Add("DurationPercentComplete");
            grd.columnSettings.developer.Add("UnitsPercentComplete");
            grd.columnSettings.developer.Add("BudgetedExpensesCost");
            grd.columnSettings.developer.Add("BudgetedMaterialCost");
            grd.columnSettings.developer.Add("BudgetedLabourCost");
            grd.columnSettings.developer.Add("BudgetedNonLabourCosts");
            grd.columnSettings.developer.Add("BudgetedMaterialQty");
            grd.columnSettings.developer.Add("BudgetedNonLabourQty");

            grd.columnSettings.rename.Add("BudgetedTotalCost", "Contract Value");
            grd.columnSettings.rename.Add("BudgetedLabourQty", "Direct Labour Budget");
            grd.columnSettings.rename.Add("target_start_date", "Baseline Start");
            grd.columnSettings.rename.Add("target_end_date", "Baseline End");

            grd.columnSettings.format.Add("TaskCode", Grid.Grid_Read.columnFormat.COUNT);
            grd.columnSettings.format.Add("TaskName", Grid.Grid_Read.columnFormat.TEXT);
            grd.columnSettings.format.Add("EarnedValueRevenue", Grid.Grid_Read.columnFormat.TWO_DECIMAL);
            grd.columnSettings.format.Add("EarnedValueDirectHours", Grid.Grid_Read.columnFormat.TWO_DECIMAL);
            grd.columnSettings.format.Add("PercentComplete", Grid.Grid_Read.columnFormat.PERCENT_NO_TOTAL_TWO_DECIMAL);
            grd.columnSettings.format.Add("BudgetedTotalCost", Grid.Grid_Read.columnFormat.TWO_DECIMAL);
            grd.columnSettings.format.Add("BudgetedLabourQty", Grid.Grid_Read.columnFormat.TWO_DECIMAL);
            grd.columnSettings.format.Add("target_start_date", Grid.Grid_Read.columnFormat.DATE);
            grd.columnSettings.format.Add("target_end_date", Grid.Grid_Read.columnFormat.DATE);

            grd.columnSettings.toolTip.Add("target_start_date", "Latest approved baseline start date");
            grd.columnSettings.toolTip.Add("target_end_date", "Latest approved baseline end date");

        }

        public override string IssueIfClosed()
        {
            return "";
        }
        

      
      
    }

}
