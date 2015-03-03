
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;
using WalzExplorer.Common;
namespace WalzExplorer.Controls.RHSTabs.Project
{
    /// <summary>
    /// Interaction logic for TenderViewer.xaml
    /// </summary>
    
     
    public partial class SummaryView : RHSTabGridViewBase_ReadOnly
    {
      
        SummaryViewModel vm;

        public SummaryView()
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

            // set grid data
            vm = new SummaryViewModel(settings);
            grd.DataContext = vm;
            grd.ItemsSource = vm.data;
           
            base.TabLoad();

        }

        private void grd_AutoGeneratingColumn(object sender, GridViewAutoGeneratingColumnEventArgs e)
        {
            GridViewDataColumn column = e.Column as GridViewDataColumn;
            switch (e.Column.Header.ToString())
            {
                case "Contract":
                    column.DataFormatString = "#,##0";
                    column.TextAlignment = TextAlignment.Right;
                    break;
                case "Cost":
                    column.DataFormatString = "#,##0";
                    column.TextAlignment = TextAlignment.Right;
                    e.Column.ToolTip = "Costs (Includeds Overheads) (Excludeds Committed Costs)";
                    break;
                case "Committed":
                    column.DataFormatString = "#,##0";
                    column.TextAlignment = TextAlignment.Right;

                    //e.Column.Header = new TextBlock() { Text = "HEllo", ToolTip = "New tooltip" };
                    break;
                case "Invoiced":
                    column.DataFormatString = "#,##0";
                    column.TextAlignment = TextAlignment.Right;
                    column.ToolTip = "Invoiced to client";
                    break;
          

            }
        }

     
      
    }

}
