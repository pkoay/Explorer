
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


    public partial class CostView : RHSTabViewBase
    {
      
        CostViewModel vm;

        public CostView()
        {
            InitializeComponent();
           
        }

        public override void TabLoad()
        {

            vm = new CostViewModel(settings);
            grd.grd.DataContext = vm;
            grd.grd.ItemsSource = vm.data;

            grd.SetGrid(settings);
            //grd.Reset();
            grd.columnSettings.developer.Add("DataAreaID");
            grd.columnSettings.developer.Add("Invoiced");
            grd.columnSettings.format.Add("ProjId", Grid.Grid_Read.columnFormat.COUNT);
            grd.columnSettings.format.Add("Date", Grid.Grid_Read.columnFormat.DATE);
            grd.columnSettings.format.Add("CostAmount", Grid.Grid_Read.columnFormat.TWO_DECIMAL);
            grd.columnSettings.format.Add("CostOverhead", Grid.Grid_Read.columnFormat.TWO_DECIMAL);
            grd.columnSettings.format.Add("CostTotal", Grid.Grid_Read.columnFormat.TWO_DECIMAL);
            grd.columnSettings.format.Add("Hours", Grid.Grid_Read.columnFormat.TWO_DECIMAL);
            grd.columnSettings.format.Add("Quantity", Grid.Grid_Read.columnFormat.TWO_DECIMAL);
            grd.columnSettings.format.Add("Employee", Grid.Grid_Read.columnFormat.TEXT);
            grd.columnSettings.format.Add("Description", Grid.Grid_Read.columnFormat.TEXT_NO_GROUP);
            grd.columnSettings.format.Add("CategoryGroup", Grid.Grid_Read.columnFormat.TEXT);
            grd.columnSettings.format.Add("CategoryName", Grid.Grid_Read.columnFormat.TEXT);
            grd.columnSettings.format.Add("PurchQtyPrice", Grid.Grid_Read.columnFormat.TWO_DECIMAL_NO_TOTAL);

            grd.columnSettings.toolTip.Add("ProjId", "The AX project id (these could be header or sub projects, what ever projects have actaul costs booked to them)");
            grd.columnSettings.toolTip.Add("ProjectName", "The AX project name (these could be header or sub projects, what ever projects have actaul costs booked to them)");
            grd.columnSettings.toolTip.Add("CategoryGroup", 
                string.Join(Environment.NewLine,
                    "Hour - this is all the internal labour that has been booked through timesheets, excluding apprentices.",
                    "Apprentice - this is all Apperentice Hours thta have been booked through timesheets.",
                    "Allowances - this is all the internal labour allowances (including apprentices )",
                    "Item- All direct purchases for the project through accounts payable (i.e. Purchase order raised)",
                    "Expenses-All indirect purahcses/costs for the project."));
            grd.columnSettings.toolTip.Add("CategoryName", "This is a sub grouping of Category Name (the description in the field should be self explanitory)");
            grd.columnSettings.toolTip.Add("Date", "The date the cost was posted to the Project");
            grd.columnSettings.toolTip.Add("Employee", "The name of the employee, if applicable.");
            grd.columnSettings.toolTip.Add("Hours", "The hours, if applicable.");
            grd.columnSettings.toolTip.Add("Quantity", "The quantity, if applicable.​");
            grd.columnSettings.toolTip.Add("CostAmount", "​This is the costs amount excluding overheads and committed costs");
            grd.columnSettings.toolTip.Add("CostOverhead", "This is the overhead cost amount (excludes commited costs)");
            grd.columnSettings.toolTip.Add("CostTotal", "This is the sum of 'Cost Amount' and 'Cost Overhead' (Excludes commited costs)");
            grd.columnSettings.toolTip.Add("Description", "​Some addtional description for this cost item");
            grd.columnSettings.toolTip.Add("PurchOrderID", "Purchase Order number, if applicable.​");
            grd.columnSettings.toolTip.Add("PurchVendor", "The vendor of the purchase, if applicable.​");
            grd.columnSettings.toolTip.Add("PurchUnit", "​the Unit that should be applied to the 'Quantity'");
            grd.columnSettings.toolTip.Add("PurchQtyPrice", "​the price per unit of the purchase");
            grd.columnSettings.toolTip.Add("PurchInvoice", "this is the vendors purchase invoice number");

        }
        public override string IssueIfClosed()
        {
            return "";
        }
    }

}
