
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
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


    public partial class OverheadDetailView : RHSTabViewBase
    {

        OverheadDetailViewModel vm;

       public OverheadDetailView()
        {
            InitializeComponent();
        }

        public override void TabLoad()
        {
            vm = new OverheadDetailViewModel(settings);
            grd.vm = vm;
            grd.grd.DataContext = vm;
            grd.grd.ItemsSource = vm.data;

            if (settings.user.SecurityGroups.Contains("WD_Tender"))
                grd.SetGrid(settings, true, true, true);
            else
                grd.SetGrid(settings, false, false, false);

            grd.columnsettings.Add("OverheadTypeID", new GridEditViewBase.columnSetting() { aggregation = GridEditViewBase.columnSetting.aggregationType.COUNT });
            grd.columnsettings.Add("WorkGroupID", new GridEditViewBase.columnSetting() { isDeveloper = true });
            grd.columnsettings.Add("OverheadItemID", new GridEditViewBase.columnSetting() { isDeveloper = true });
            grd.columnsettings.Add("Title", new GridEditViewBase.columnSetting() { format=  GridEditViewBase.columnSetting.formatType.TEXT});
            grd.columnsettings.Add("Duration", new GridEditViewBase.columnSetting() { format = GridEditViewBase.columnSetting.formatType.G });
            grd.columnsettings.Add("Count", new GridEditViewBase.columnSetting() { format = GridEditViewBase.columnSetting.formatType.G });
            grd.columnsettings.Add("Rate", new GridEditViewBase.columnSetting() { format = GridEditViewBase.columnSetting.formatType.G });
            grd.columnsettings.Add("Hours", new GridEditViewBase.columnSetting()
            {
                format = GridEditViewBase.columnSetting.formatType.N2,
                aggregation = GridEditViewBase.columnSetting.aggregationType.SUM,
                isReadonly = true,
                order = 8,
                tooltip = "Calculated from Estimate hours (for the workgroup) + Additional hours (for the workgroup)" 
            });
            grd.columnsettings.Add("Total", new GridEditViewBase.columnSetting()
            {
                format = GridEditViewBase.columnSetting.formatType.N2,
                aggregation = GridEditViewBase.columnSetting.aggregationType.SUM,
                isReadonly = true,
                order = 9,
                tooltip = String.Join(
                     Environment.NewLine,
                     "Calculation is base on Overhead Type:",
                     "If 'Normal' then Count*Duration*Rate",
                     "If 'Hours' then Count*Hours*Rate")
            });
            grd.columnCombo.Clear();
            grd.columnCombo.Add("OverheadGroupID", GridLibrary.CreateCombo("cmbOverheadGroupID", "Overhead Group", vm.cmbOverheadGroupList(), "OverheadGroupID", "Title"));
            grd.columnCombo.Add("OverheadTypeID", GridLibrary.CreateCombo("cmbOverheadTypeID", "Overhead Type", vm.cmbOverheadTypeList(), "OverheadTypeID", "Title"));

            
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
