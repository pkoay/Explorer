
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


    public partial class WorkGroupView : RHSTabViewBase
    {
      
       WorkGroupViewModel vm;

       public WorkGroupView()
        {
            InitializeComponent();
        }

        public override void TabLoad()
        {
            vm = new WorkGroupViewModel(settings);
            grd.vm = vm;
            grd.grd.DataContext = vm;
            grd.grd.ItemsSource = vm.data;

            if (settings.user.SecurityGroups.Contains("WD_Tender"))
                grd.SetGrid(settings, true, true, true);
            else
                grd.SetGrid(settings, false, false, false);

            grd.columnsettings.Add("WorkGroupID", new GridEditViewBase.columnSetting() { isDeveloper = true });
            grd.columnsettings.Add("TenderID", new GridEditViewBase.columnSetting() { isDeveloper = true });
            grd.columnsettings.Add("Title", new GridEditViewBase.columnSetting() { aggregation = GridEditViewBase.columnSetting.aggregationType.COUNT, format = GridEditViewBase.columnSetting.formatType.TEXT });
            grd.columnsettings.Add("TotalHours", new GridEditViewBase.columnSetting() {  aggregation = GridEditViewBase.columnSetting.aggregationType.SUM, format = GridEditViewBase.columnSetting.formatType.N2, isReadonly = true, tooltip = "Total hours for this workgroup from estimate (does not include Addtional hours)" });
            grd.columnsettings.Add("TotalAdditionalHours", new GridEditViewBase.columnSetting() { aggregation = GridEditViewBase.columnSetting.aggregationType.SUM, format = GridEditViewBase.columnSetting.formatType.N2, isReadonly = true, tooltip = "Total hours from 'Addtional Hours' Tab" });
            grd.columnsettings.Add("TotalOverhead", new GridEditViewBase.columnSetting() {  aggregation = GridEditViewBase.columnSetting.aggregationType.SUM, format = GridEditViewBase.columnSetting.formatType.N2, isReadonly = true, tooltip = "All workgroup overheads (including fuel and Addtional hours costs)" });
            grd.columnsettings.Add("CalculatedRate", new GridEditViewBase.columnSetting() {  format = GridEditViewBase.columnSetting.formatType.N2, isReadonly = true, tooltip = "BaseRate+(TotalOverhead/Total Hours) note TotalHours does not include Additional Hours" });
        }

        public override string IssueIfClosed()
        {
            return "";
        }
       
      
    }

}
