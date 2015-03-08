
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
namespace WalzExplorer.Controls.RHSTabs.Project.Performance
{
    /// <summary>
    /// Interaction logic for TenderViewer.xaml
    /// </summary>
    
     
    public partial class SafetyView : RHSTabGridViewBase_ReadOnly
    {
      
        SafetyViewModel vm;

        public SafetyView()
        {
            InitializeComponent();
        }
        
        public override void TabLoad()
        {
            base.SetGrid(grd);
            base.Reset();

            columnReadOnlyDeveloper.Add("RowVersion");
            columnReadOnlyDeveloper.Add("ProjectID");
            columnRename.Add("AXProjectID", "ID");
            columnRename.Add("OperationsManager", "Ops Manager");
            columnReadOnlyDeveloper.Add("AXDataAreaID");
            columnReadOnlyDeveloper.Add("SortOrder");
            columnReadOnlyDeveloper.Add("UpdatedBy");
            columnReadOnlyDeveloper.Add("UpdatedDate");

            //Set view
            vm = new SafetyViewModel(settings);
            // set grid data
            grd.DataContext = vm;
            grd.ItemsSource = vm.data;
           

            // Set up summary grd
            grdSummary.ShowGroupPanel = false;
            grdSummary.CanUserFreezeColumns = false;
            grdSummary.CanUserInsertRows = false;
            grdSummary.IsFilteringAllowed = false;
            grdSummary.IsReadOnly = true;
            grdSummary.DataContext = vm;
            grdSummary.ItemsSource = vm.summaryData;
            


            base.TabLoad();

        }

        private void grd_AutoGeneratingColumn(object sender, GridViewAutoGeneratingColumnEventArgs e)
        {
           
            GridViewDataColumn column = e.Column as GridViewDataColumn;
            switch (e.Column.Header.ToString())
            {
                case "AXProjectID":
                    e.Column.AggregateFunctions.Add(new CountFunction() { Caption = "Count:" });
                    column.ShowColumnWhenGrouped = false;
                    break;
                case "Contract":
                    SetColumn(column, "TWO_DECIMAL");
                    ColumnToolTipStatic(grd, column, "DIFFERENT!!!");
                    break;
                case "Cost":
                    SetColumn(column, "TWO_DECIMAL");
                    ColumnToolTipStatic(grd,column, "Costs to date (Includes Overheads) (Excludes Committed Costs)");
                    break;
                case "Committed":
                    SetColumn(column, "TWO_DECIMAL");
                    ColumnToolTipStatic(grd, column, "Committed Costs to date (Open purchase orders)");
                    break;
                case "Invoiced":
                    SetColumn(column, "TWO_DECIMAL");
                    ColumnToolTipStatic(grd, column, "Invoiced to client to date");
                    break;
            }
        }

     
      
    }

}
