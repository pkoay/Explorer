
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
using WalzExplorer.Controls.Grid;

namespace WalzExplorer.Controls.RHSTabs.Tender
{
    /// <summary>
    /// Interaction logic for TenderViewer.xaml
    /// </summary>


    public partial class WorkgroupAdditionalHoursView : RHSTabViewBase
    {

        WorkgroupAdditionalHoursViewModel vm;

        public WorkgroupAdditionalHoursView()
        {
            InitializeComponent();
        }

        public override void TabLoad()
        {
            vm = new WorkgroupAdditionalHoursViewModel(settings);
            grd.vm = vm;
            grd.grd.DataContext = vm;
            grd.grd.ItemsSource = vm.data;

            if (settings.user.SecurityGroups.Contains("WD_Tender"))
                grd.SetGrid(settings, true, true, true);
            else
                grd.SetGrid(settings, false, false, false);

            grd.columnsettings.Add("WorkgroupID", new GridEditViewBase.columnSetting() { isDeveloper = true });
            grd.columnsettings.Add("AdditionalItemID", new GridEditViewBase.columnSetting() { isDeveloper = true });
            grd.columnsettings.Add("Title", new GridEditViewBase.columnSetting() { aggregation = GridEditViewBase.columnSetting.aggregationType.COUNT, format = GridEditViewBase.columnSetting.formatType.TEXT });
            grd.columnsettings.Add("Hours", new GridEditViewBase.columnSetting() { aggregation = GridEditViewBase.columnSetting.aggregationType.SUM, format = GridEditViewBase.columnSetting.formatType.N2});

            grd.columnsettings.Add("Total", new GridEditViewBase.columnSetting()
            {
                format = GridEditViewBase.columnSetting.formatType.N2,
                aggregation = GridEditViewBase.columnSetting.aggregationType.SUM,
                isReadonly = true,
                order = 4,
                tooltip = "Calculated from Count*Hours*BaseRate"
            });
            grd.columnsettings.Add("BaseRate", new GridEditViewBase.columnSetting()
            {
                format = GridEditViewBase.columnSetting.formatType.N2,
                isReadonly = true,
                order = 3,
                tooltip = "Workgroup BaseRate"
            });
            grd.columnCombo.Clear();
            
                
                
        }

        public override string IssueIfClosed()
        {
            bool isvalid = grd.IsValid();
            if (!isvalid)
            {
                return "Not all data in the tab is saved (data in error not saved). Press ok to fix the errors, or press cancel to lose changes in error";
            }
            return "";
        }
       
      
    }

}
