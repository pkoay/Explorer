
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
using WalzExplorer.Database;
using WalzExplorer.Model;
namespace WalzExplorer.Controls.RHSTabs.FixedAsset
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


            grd.columnSettings.format.Add("LastDateFrom", Grid.Grid_Read.columnFormat.DATE);
            grd.columnSettings.format.Add("LastDateTo", Grid.Grid_Read.columnFormat.DATE);
            grd.columnSettings.format.Add("DaysUnitlAvail", Grid.Grid_Read.columnFormat.INT);
           
            grd.columnSettings.format.Add("InsuredValue", Grid.Grid_Read.columnFormat.TWO_DECIMAL);
            //grd.columnSettings.format.Add("CostBudget", Grid.Grid_Read.columnFormat.TWO_DECIMAL);
            //grd.columnSettings.format.Add("Cost", Grid.Grid_Read.columnFormat.TWO_DECIMAL);
            //grd.columnSettings.format.Add("Committed", Grid.Grid_Read.columnFormat.TWO_DECIMAL);
            //grd.columnSettings.format.Add("Invoiced", Grid.Grid_Read.columnFormat.TWO_DECIMAL);
          
            //grd.columnSettings.toolTip.Add("Contract", "Contract Value including all approved variations");
            //grd.columnSettings.toolTip.Add("CostBudget", "Cost Budget including all approved variations");
            //grd.columnSettings.toolTip.Add("Cost", "Costs to date (Includes Overheads) (Excludes Committed Costs)");
            //grd.columnSettings.toolTip.Add("Committed", "Committed Costs to date (Open purchase orders)");
            //grd.columnSettings.toolTip.Add("Invoiced", "Invoiced to client to date");


            //grd.columnSettings.toolTip.Add("CostSignDate", "Latest signed Date for CostPlanned,CostEarned and CostActual columns (from Performance tab) ");
            //grd.columnSettings.toolTip.Add("CostPlanned", "Latest signed Planned cost amount (from Performance tab)");
            //grd.columnSettings.toolTip.Add("CostEarned", "Latest signed Earned cost amount (from Performance tab)");
            //grd.columnSettings.toolTip.Add("CostActual", "Latest signed Actual cost amount (from Performance tab)");
            //grd.columnSettings.format.Add("CostSignDate", Grid.Grid_Read.columnFormat.DATE);
            //grd.columnSettings.format.Add("CostPlanned", Grid.Grid_Read.columnFormat.TWO_DECIMAL);
            //grd.columnSettings.format.Add("CostEarned", Grid.Grid_Read.columnFormat.TWO_DECIMAL);
            //grd.columnSettings.format.Add("CostActual", Grid.Grid_Read.columnFormat.TWO_DECIMAL);

            //grd.columnSettings.drilldown.Add("Committed");
            //grd.columnSettings.drilldown.Add("Cost");

        }
        public override string IssueIfClosed()
        {
            return "";
        }


      
        

     
      
    }

}
