
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


    public partial class WorkgroupFuelView : RHSTabViewBase
    {

        WorkgroupFuelViewModel vm;

        public WorkgroupFuelView()
        {
            InitializeComponent();
        }

        public override void TabLoad()
        {
            vm = new WorkgroupFuelViewModel(settings);
            grd.vm = vm;
            grd.grd.DataContext = vm;
            grd.grd.ItemsSource = vm.data;

            if (settings.user.SecurityGroups.Contains("WD_Tender"))
                grd.SetGrid(settings, true, true, true);
            else
                grd.SetGrid(settings, false, false, false);


            grd.columnsettings.Add("WorkGroupID", new GridEditViewBase.columnSetting() { isDeveloper = true });
            grd.columnsettings.Add("FuelItemID", new GridEditViewBase.columnSetting() { isDeveloper = true });
            grd.columnsettings.Add("Title", new GridEditViewBase.columnSetting() { aggregation = GridEditViewBase.columnSetting.aggregationType.COUNT, format = GridEditViewBase.columnSetting.formatType.TEXT });
            grd.columnsettings.Add("Count", new GridEditViewBase.columnSetting() { format = GridEditViewBase.columnSetting.formatType.G });
            grd.columnsettings.Add("Week", new GridEditViewBase.columnSetting() { format = GridEditViewBase.columnSetting.formatType.G });
            grd.columnsettings.Add("C10DayFortnight", new GridEditViewBase.columnSetting() { isDeveloper = true });
            grd.columnsettings.Add("HoursPerWeek", new GridEditViewBase.columnSetting() { format = GridEditViewBase.columnSetting.formatType.G });
            grd.columnsettings.Add("LitrePerHour", new GridEditViewBase.columnSetting() { format = GridEditViewBase.columnSetting.formatType.G });
            grd.columnsettings.Add("CostPerLitre", new GridEditViewBase.columnSetting() { format = GridEditViewBase.columnSetting.formatType.N2 });

            grd.columnsettings.Add("TotalForProject", new GridEditViewBase.columnSetting()
            {
                format = GridEditViewBase.columnSetting.formatType.N2,
                aggregation = GridEditViewBase.columnSetting.aggregationType.SUM,
                isReadonly = true,
                order = 5,
                tooltip = "Calculated from Count*Week*HoursPerWeek*LitrePerHour*.6"
            });
            grd.columnsettings.Add("CostTotalPerItem", new GridEditViewBase.columnSetting()
           {
               format = GridEditViewBase.columnSetting.formatType.N2,
               aggregation = GridEditViewBase.columnSetting.aggregationType.SUM,
               isReadonly = true,
               order = 7,
               tooltip = "Calculated from TotalForProject*CostPerLitre"
           });
            grd.columnsettings.Add("FuelCost", new GridEditViewBase.columnSetting()
           {
               format = GridEditViewBase.columnSetting.formatType.N2,
               aggregation = GridEditViewBase.columnSetting.aggregationType.SUM,
               isReadonly = true,
               order = 8,
               tooltip = "Calculated from CostTotalPerItem*.825"
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
