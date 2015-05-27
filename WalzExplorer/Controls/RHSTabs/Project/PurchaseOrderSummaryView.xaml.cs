
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
using WalzExplorer.Database;
using WalzExplorer.Model;
namespace WalzExplorer.Controls.RHSTabs.Project
{
    /// <summary>
    /// Interaction logic for TenderViewer.xaml
    /// </summary>


    public partial class PurchaseOrderSummaryView : RHSTabViewBase
    {

        PurchaseOrderSummaryViewModel vm;

        public PurchaseOrderSummaryView()
        {
            InitializeComponent();
        }

        public override void TabLoad()
        {
            vm = new PurchaseOrderSummaryViewModel(settings);
            grd.grd.DataContext = vm;
            grd.grd.ItemsSource = vm.data;

            grd.SetGrid(settings);
            grd.columnSettings.developer.Add("DataAreaId");
            grd.columnSettings.format.Add("PurchaseOrderID", Grid.Grid_Read.columnFormat.COUNT);
            grd.columnSettings.format.Add("ProjID", Grid.Grid_Read.columnFormat.TEXT);
            grd.columnSettings.format.Add("Requisitioner", Grid.Grid_Read.columnFormat.TEXT);
            grd.columnSettings.format.Add("PurchasePlacer", Grid.Grid_Read.columnFormat.TEXT);
            grd.columnSettings.format.Add("VendorName", Grid.Grid_Read.columnFormat.TEXT);
            grd.columnSettings.format.Add("PurchaseName", Grid.Grid_Read.columnFormat.TEXT);
            grd.columnSettings.format.Add("CreateDate", Grid.Grid_Read.columnFormat.DATE);
            grd.columnSettings.format.Add("DeliveryDate", Grid.Grid_Read.columnFormat.DATE);
            grd.columnSettings.format.Add("OrderAmount", Grid.Grid_Read.columnFormat.TWO_DECIMAL);
            grd.columnSettings.format.Add("CommittedAmount", Grid.Grid_Read.columnFormat.TWO_DECIMAL);
            grd.columnSettings.format.Add("PaidAmount", Grid.Grid_Read.columnFormat.TWO_DECIMAL);
            grd.columnSettings.toolTip.Add("Status",
                string.Join(Environment.NewLine
                ,"Open Order - Order sent"
                ,"Recieved - Order recieved"
                ,"Invoiced - Order invoiced (final stage)"
                ,"Cancelled - Order cancelled (final stage)"
                ));
            //grd.grd.Columns[1].CellTemplate= FindResource("RedTemp") as DataTemplate;
            grd.columnSettings.drilldown.Add("OrderAmount");
            grd.columnSettings.drilldown.Add("CommittedAmount");
            grd.columnSettings.drilldown.Add("PaidAmount");

        }

        public override string IssueIfClosed()
        {
            return "";
        }

        private void grd_Drilldown(object sender, EventArgs e)
        {
           //extract data from grid for drilldown
           WalzExplorer.Controls.Grid.Grid_Read.DrilldownResult ddInfo=(WalzExplorer.Controls.Grid.Grid_Read.DrilldownResult) sender;
           spWEX_RHS_Project_PurchaseOrderSummary_v2_Result ddRowdata = (spWEX_RHS_Project_PurchaseOrderSummary_v2_Result) ddInfo.RowData;

           //Pass data to drilldown
           string title="";
           Dictionary<string, string> fields = new Dictionary<string, string>();
           fields.Add("PurchID", ddRowdata.PurchaseOrderID);
           fields.Add("DataAreaID", ddRowdata.DataAreaID);
           switch (ddInfo.ColumnUniqueName)
            {
                case "CommittedAmount":
                    title = "Drilldown on Purchase Order " + ddRowdata.PurchaseOrderID + ", committed amount totaling " + ((decimal)ddRowdata.CommittedAmount).ToString("N2");
                    fields.Add("isCommitted", "true");
                    break;
                case "PaidAmount":
                    title = "Drilldown on Purchase Order " + ddRowdata.PurchaseOrderID + ", paid amount totaling "  + ((decimal)ddRowdata.PaidAmount).ToString("N2");
                    fields.Add("isCommitted", "false");
                    break;
                case "OrderAmount":
                    title = "Drilldown on Purchase Order " + ddRowdata.PurchaseOrderID + ", order amount totaling " + ((decimal)ddRowdata.OrderAmount).ToString("N2");
                    fields.Add("isCommitted", null);
                    break;
            }
            settings.drilldown = new WEXDrilldown(title,"Project.PurchaseOrderDetailView", DrilldownFilter.PurchaseOrder, fields);
            DrilldownWindow.Open(settings);
           
        }
            
          

      
    }

}
