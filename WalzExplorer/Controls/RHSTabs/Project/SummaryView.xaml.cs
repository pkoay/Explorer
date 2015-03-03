
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

        public static void AddRectangleTemplateToResources(FrameworkElement element)
        {
            element.Resources.Add("ToolTipTemplate", CreateRectangleDataTemplate());
        }
        public static DataTemplate CreateRectangleDataTemplate()
        {
            var rectangleFactory = new FrameworkElementFactory(typeof(TextBlock));
            rectangleFactory.SetValue(TextBlock.TextProperty, "HELLO");
            
            DataTemplate template= new DataTemplate
            {
                VisualTree = rectangleFactory,
            };
            template.Seal();
            return template;
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
                    ColumnToolTipStatic(grd, column, "Contract Value including all approved variations");
                    break;
                case "Cost":
                    column.DataFormatString = "#,##0";
                    column.TextAlignment = TextAlignment.Right;
                    ColumnToolTipStatic(grd,column, "Costs to date (Includes Overheads) (Excludes Committed Costs)");
                    break;
                case "Committed":
                    column.DataFormatString = "#,##0";
                    column.TextAlignment = TextAlignment.Right;
                    ColumnToolTipStatic(grd, column, "Committed Costs to date (Open purchase orders)");
                    break;
                case "Invoiced":
                    column.DataFormatString = "#,##0";
                    column.TextAlignment = TextAlignment.Right;
                    ColumnToolTipStatic(grd, column, "Invoiced to client to date");
                    break;
            }
        }

     
      
    }

}
