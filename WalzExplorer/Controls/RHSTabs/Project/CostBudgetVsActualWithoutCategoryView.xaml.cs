
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


    public partial class CostBudgetVsActualWithoutCategoryView : RHSTabViewBase
    {

        CostBudgetVsActualViewWithoutCategoryModel vm;

        public CostBudgetVsActualWithoutCategoryView()
        {
            InitializeComponent();
        }

        public override void TabLoad()
        {

            vm = new CostBudgetVsActualViewWithoutCategoryModel(settings);
            grd.grd.DataContext = vm;
            grd.grd.ItemsSource = vm.data;

            grd.SetGrid(settings);
            grd.columnSettings.developer.Add("DataAreaID");
            grd.columnSettings.format.Add("ProjID", Grid.Grid_Read.columnFormat.COUNT);
            grd.columnSettings.format.Add("ProjName", Grid.Grid_Read.columnFormat.TEXT);
            grd.columnSettings.format.Add("OriginalBudget", Grid.Grid_Read.columnFormat.TWO_DECIMAL);
            grd.columnSettings.format.Add("BudgetVariation", Grid.Grid_Read.columnFormat.TWO_DECIMAL);
            grd.columnSettings.format.Add("CurrentBudget", Grid.Grid_Read.columnFormat.TWO_DECIMAL);
            grd.columnSettings.format.Add("Remaining", Grid.Grid_Read.columnFormat.TWO_DECIMAL);
            grd.columnSettings.format.Add("Actual", Grid.Grid_Read.columnFormat.TWO_DECIMAL);
            grd.columnSettings.format.Add("Committed", Grid.Grid_Read.columnFormat.TWO_DECIMAL);

        }
        public override string IssueIfClosed()
        {
            return "";
        }

      
    }

}
