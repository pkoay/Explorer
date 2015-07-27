
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


    public partial class ChargeView : RHSTabViewBase
    {
      
        ChargeViewModel vm;

        public ChargeView()
        {
            InitializeComponent();
        }
        
        public override void TabLoad()
        {
            vm = new ChargeViewModel(settings);
            grd.grd.DataContext = vm;
            grd.grd.ItemsSource = vm.data;

            grd.SetGrid(settings);


            grd.columnSettings.format.Add("DateFrom", Grid.Grid_Read.columnFormat.DATE);
            grd.columnSettings.format.Add("DateTo", Grid.Grid_Read.columnFormat.DATE);
            grd.columnSettings.format.Add("Quantity", Grid.Grid_Read.columnFormat.TWO_DECIMAL);
            grd.columnSettings.format.Add("ModifiedDate", Grid.Grid_Read.columnFormat.DATE);
            //grd.columnSettings.format.Add("Contract", Grid.Grid_Read.columnFormat.TWO_DECIMAL);
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
