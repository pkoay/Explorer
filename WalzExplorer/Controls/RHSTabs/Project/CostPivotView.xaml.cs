
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using Telerik.Pivot.Core;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;
using Telerik.Windows.Data;
using WalzExplorer.Common;
using WalzExplorer.Controls.Grid;
using System.Linq;
using Telerik.Windows.Controls.FieldList;
using Telerik.Pivot.Core.Fields;
using System.Windows.Controls;
using System.Diagnostics;
using System.IO;

namespace WalzExplorer.Controls.RHSTabs.Project
{
    /// <summary>
    /// Interaction logic for TenderViewer.xaml
    /// </summary>


    public partial class CostPivotView : RHSTabViewBase
    {

        CostPivotViewModel vm;


        public CostPivotView()
        {
            InitializeComponent();
        }

        public override void TabLoad()
        {
            vm = new CostPivotViewModel(settings);

            LocalDataSourceProvider provider = new LocalDataSourceProvider();
            provider.PrepareDescriptionForField += provider_PrepareDescriptionForField;
            provider.ItemsSource = vm.data;

            //provider.RowGroupDescriptions.Add(new PropertyGroupDescription() { PropertyName = "Schedule" });
            //provider.ColumnGroupDescriptions.Add(new PropertyGroupDescription() { PropertyName = "EstimateItemType" });
            //provider.AggregateDescriptions.Add(new PropertyAggregateDescription() { PropertyName = "Cost"  ,StringFormat="#,##0.00"});
            provider.FieldDescriptionsProvider = new CustomFieldDescriptionsProvider();

            FieldList.DataProvider = provider;
            Pivot.DataProvider = provider;
            Pivot.ContextMenu = CreateContextMenu();

        }

        public override string IssueIfClosed()
        {
            //bool isvalid = grd.IsValid();
            //if (!isvalid)
            //{
            //    return "Not all data in the tab is saved (data in error not saved). Press ok to fix the errors, or press cancel to lose changes in error";
            //}
            return "";
        }
        public class CustomFieldDescriptionsProvider : LocalDataSourceFieldDescriptionsProvider
        {
            protected override System.Collections.Generic.IEnumerable<IPivotFieldInfo> GetDescriptions(IFieldInfoExtractor getter)
            {
                var result = base.GetDescriptions(getter);
                List<string> PropertiesToSkip = new List<string>() { "DataAreaID", "ProjectID", "Error", "HasError", "Item" };

                // filter some of the results
                List<PropertyInfoFieldInfo> itemsToShow = new List<PropertyInfoFieldInfo>();

                foreach (PropertyInfoFieldInfo item in result)
                {
                    if (!PropertiesToSkip.Contains(item.DisplayName))
                    {
                        itemsToShow.Add(item);
                    }
                }
                return itemsToShow;
            }
        }
        private void provider_PrepareDescriptionForField(object sender, PrepareDescriptionForFieldEventArgs e)
        {
            List<string> PropertiesToFormat = new List<string>() { "PurchQtyPrice", "CostTotal", "CostOverhead", "CostAmount", "Quantity", "Hours" };
            var aggregateDescription = e.Description as PropertyAggregateDescriptionBase;

            if (e.DescriptionType == Telerik.Pivot.Core.DataProviderDescriptionType.Aggregate && aggregateDescription != null && PropertiesToFormat.Contains(aggregateDescription.PropertyName))
            {
                aggregateDescription.StringFormat = "#,##0.00";
            }
        }
        private ContextMenu CreateContextMenu()
        {
            // add context menu
            ContextMenu cm = new ContextMenu();
            cm.FontSize = 12;
            MenuItem mi;

            cm.Items.Add(new MenuItem() { Name = "miExportExcel", Header = "Export to Excel", Icon = GraphicsLibrary.ResourceIconCanvasToSize("appbar_page_excel", 16, 16) });

            foreach (object o in cm.Items)
            {
                if (!(o is Separator))
                {
                    mi = (MenuItem)o;
                    mi.Click += new RoutedEventHandler(cm_ItemClick);
                }
            }
            return cm;
        }

        public void cm_ItemClick(object sender, RoutedEventArgs e)
        {
            MenuItem mi = (MenuItem)sender;
            switch (mi.Name)
            {


                case "miExportExcel":
                    if (Pivot.ColumnGroups.Count > 0 || Pivot.RowGroups.Count > 0)
                    {
                        //provider.ColumnGroupDescriptions.Add(new PropertyGroupDescription() { PropertyName = "EstimateItemType" });
                        //provider.AggregateDescriptions.Add(new PropertyAggregateDescription() { PropertyName = "Cost"  ,StringFormat="#,##0.00"});
                        string fileName = System.IO.Path.GetTempPath() + Guid.NewGuid().ToString() + ".xlsx";
                        using (Stream stream = File.Create(fileName))
                        {
                            PivotExportToExcel.ExportToExcel(stream, Pivot);
                            //grd.Export(stream,
                            // new GridViewExportOptions()
                            // {
                            //     Format = ExportFormat.ExcelML,
                            //     ShowColumnHeaders = true,
                            //     ShowColumnFooters = true,
                            //     ShowGroupFooters = false,
                            // });
                        }
                        Process excel = new Process();
                        excel.StartInfo.FileName = fileName;
                        excel.Start();
                    }
                    else
                    {
                        MessageBox.Show("No pivot to export");
                    }
                    break;
            }
        }

    }

}
