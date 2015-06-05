
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
          
            grd.columnSettings.toolTip.Add("Contract", "Contract Value including all approved variations");
            grd.columnSettings.toolTip.Add("CostBudget", "Cost Budget including all approved variations");
            grd.columnSettings.toolTip.Add("Cost", "Costs to date (Includes Overheads) (Excludes Committed Costs)");
            grd.columnSettings.toolTip.Add("Committed", "Committed Costs to date (Open purchase orders)");
            grd.columnSettings.toolTip.Add("Invoiced", "Invoiced to client to date");

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
            spWEX_RHS_Project_Summary_Result ddRowdata = (spWEX_RHS_Project_Summary_Result)ddInfo.RowData;

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
