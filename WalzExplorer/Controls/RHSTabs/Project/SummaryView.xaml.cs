
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
            grd.columnSettings.format.Add("Margin", Grid.Grid_Read.columnFormat.TWO_DECIMAL);
            grd.columnSettings.format.Add("MarginPercent", Grid.Grid_Read.columnFormat.PERCENT_NO_TOTAL_TWO_DECIMAL);
            grd.columnSettings.rename.Add("MarginPercent", "Margin%");
          
            grd.columnSettings.toolTip.Add("Contract", "Contract Value including all approved variations");
            grd.columnSettings.toolTip.Add("CostBudget", "Cost Budget including all approved variations");
            grd.columnSettings.toolTip.Add("Cost", "Costs to date (Includes Overheads) (Excludes Committed Costs)");
            grd.columnSettings.toolTip.Add("Committed", "Committed Costs to date (Open purchase orders)");
            grd.columnSettings.toolTip.Add("Invoiced", "Invoiced to client to date");
            grd.columnSettings.toolTip.Add("Margin", String.Join(Environment.NewLine,
            "Net profit: Invoiced-Costs",
            "Note: costs include overheads and excludes committed costs"));
            grd.columnSettings.toolTip.Add("MarginPercent", String.Join(Environment.NewLine,
             "Net profit margin: (Invoiced-Costs)/Invoiced ",
             "Note: if invoiced is 0 then this field will be blank",
             "Note: costs include overheads and excludes committed costs"));

            
            grd.columnSettings.format.Add("SignDate", Grid.Grid_Read.columnFormat.DATE);
            grd.columnSettings.format.Add("Planned", Grid.Grid_Read.columnFormat.TWO_DECIMAL);
            grd.columnSettings.format.Add("Earned", Grid.Grid_Read.columnFormat.TWO_DECIMAL);
            grd.columnSettings.format.Add("Actual", Grid.Grid_Read.columnFormat.TWO_DECIMAL);
            grd.columnSettings.format.Add("ScheduleVariance", Grid.Grid_Read.columnFormat.TWO_DECIMAL);
            grd.columnSettings.format.Add("CostVariance", Grid.Grid_Read.columnFormat.TWO_DECIMAL);
            grd.columnSettings.format.Add("SPI", Grid.Grid_Read.columnFormat.TWO_DECIMAL);
            grd.columnSettings.format.Add("CPI", Grid.Grid_Read.columnFormat.TWO_DECIMAL);
            


            grd.columnSettings.toolTip.Add("SignDate", "Latest signed Date for CostPlanned,CostEarned and CostActual columns (from Performance tab) ");
            grd.columnSettings.toolTip.Add("Planned", "Latest signed 'Planned' in cost dollars (from Performance tab)");
            grd.columnSettings.toolTip.Add("Earned", "Latest signed 'Earned' in cost dollars (from Performance tab)");
            grd.columnSettings.toolTip.Add("Actual", "Latest signed 'Actual' in cost dollars (from Performance tab)");
            grd.columnSettings.toolTip.Add("ScheduleVariance", String.Join(Environment.NewLine,
              "Latest signed 'Schedule Variance' ",
              "in cost dollars (from Performance tab)",
              "Calculated by Earned-Planned"));
            grd.columnSettings.toolTip.Add("CostVariance", String.Join(Environment.NewLine,
              "Latest signed 'Cost Variance' ",
              "in cost dollars (from Performance tab)",
              "Calculated by Earned-Actual"));
            grd.columnSettings.toolTip.Add("SPI", String.Join(Environment.NewLine,
                "Latest signed 'Schedule Performance Index' ",
                "in cost dollars (from Performance tab)",
                "Calculated by Earned/Planned"));
            grd.columnSettings.toolTip.Add("CPI", String.Join(Environment.NewLine,
                "Latest signed 'Cost Performance Index' ",
                "in cost dollars (from Performance tab)",
                "Calculated by Earned/Actual"));
            grd.columnSettings.drilldown.Add("Committed");
            grd.columnSettings.drilldown.Add("Cost");

        }
        public override string IssueIfClosed()
        {
            return "";
        }


        private void grd_Drilldown(object sender, EventArgs e)
        {
            //extract data from grid for drilldown
            WalzExplorer.Controls.Grid.Grid_Read.DrilldownResult ddInfo = (WalzExplorer.Controls.Grid.Grid_Read.DrilldownResult)sender;
            spWEX_RHS_Project_Summary_v2_Result ddRowdata = (spWEX_RHS_Project_Summary_v2_Result)ddInfo.RowData;

            //Pass data to drilldown
            string title = "";
            Dictionary<string, string> fields = new Dictionary<string, string>();
            fields.Add("ProjectID", ddRowdata.ProjectID.ToString());
           
            switch (ddInfo.ColumnUniqueName)
            {
                case "Committed":
                    title = "Drilldown on Project " + ddRowdata.AXProjectID + ", committed amount totaling " + ((decimal)ddRowdata.Committed).ToString("N2");
                    fields.Add("isCommitted", "true");
                    settings.drilldown = new WEXDrilldown(title, "Project.PurchaseOrderDetailView", DrilldownFilter.Project, fields);
                    DrilldownWindow.Open(settings);
                    break;
                case "Cost":
                    title = "Drilldown on Project " + ddRowdata.AXProjectID + ", Cost (including overheads) amount totaling " + ((decimal)ddRowdata.Cost).ToString("N2");
                    settings.drilldown = new WEXDrilldown(title, "Project.CostView", DrilldownFilter.Project, fields);
                    DrilldownWindow.Open(settings);
                    break;
            }

            //settings.drilldown = null;
        }
        

     
      
    }

}
