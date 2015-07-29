
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

namespace WalzExplorer.Controls.RHSTabs.Tender
{
    /// <summary>
    /// Interaction logic for TenderViewer.xaml
    /// </summary>


    public partial class PivotView : RHSTabViewBase
    {

        PivotViewModel vm;
        

       public PivotView()
        {
            InitializeComponent();
        }

        public override void TabLoad()
        {
            vm = new PivotViewModel(settings);

            LocalDataSourceProvider provider = new LocalDataSourceProvider();
            provider.PrepareDescriptionForField+=provider_PrepareDescriptionForField;
            provider.ItemsSource = vm.data;
            
            provider.RowGroupDescriptions.Add(new PropertyGroupDescription() { PropertyName = "Schedule" });
            provider.ColumnGroupDescriptions.Add(new PropertyGroupDescription() { PropertyName = "EstimateItemType" });
            provider.AggregateDescriptions.Add(new PropertyAggregateDescription() { PropertyName = "Cost"  ,StringFormat="#,##0.00"});
            provider.FieldDescriptionsProvider = new CustomFieldDescriptionsProvider();

            //IField f =FieldList.ViewModel.Fields.First(x => x.FieldInfo.DisplayName == "TenderID");


            FieldList.DataProvider = provider;
            Pivot.DataProvider = provider;

           
            //grd.grd.DataContext = vm;
            //grd.grd.ItemsSource = vm.data;

            //if (settings.user.SecurityGroups.Contains("WD_Tender"))
            //    grd.SetGrid(settings, true, true, true);
            //else
            //    grd.SetGrid(settings, false, false, false);

           
            //grd.columnsettings.Add("DrawingID", new GridEditViewBase.columnSetting() { isDeveloper = true });
            //grd.columnsettings.Add("TenderID", new GridEditViewBase.columnSetting() { isDeveloper = true });
            //grd.columnsettings.Add("Title", new GridEditViewBase.columnSetting() { aggregation = GridEditViewBase.columnSetting.aggregationType.COUNT, format = GridEditViewBase.columnSetting.formatType.TEXT });
            //grd.columnsettings.Add("Used", new GridEditViewBase.columnSetting() { tooltip="Number of times the Drawing has been selected in the estimate", format = GridEditViewBase.columnSetting.formatType.G, isReadonly=true });
          
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
                List<string> PropertiesToSkip = new List<string>() { "TenderID", "Error","HasError","Item" };

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
            List<string> PropertiesToFormat = new List<string>() { "Cost", "Hours" };
            var aggregateDescription = e.Description as PropertyAggregateDescriptionBase;

            if (e.DescriptionType == Telerik.Pivot.Core.DataProviderDescriptionType.Aggregate && aggregateDescription != null && PropertiesToFormat.Contains(aggregateDescription.PropertyName))
            {
                aggregateDescription.StringFormat = "#,##0.00";
            }
        }
       
      
    }

}
