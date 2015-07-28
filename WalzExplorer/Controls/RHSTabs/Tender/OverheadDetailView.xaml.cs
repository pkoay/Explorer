
using System;
using System.Collections;
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
using System.Linq;
using System.Data;
using WalzExplorer.Database;

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
                grd.SetGrid(settings, true, true, true,true);
            else
                grd.SetGrid(settings, false, false, false);

            
            grd.columnsettings.Add("WorkGroupID", new GridEditViewBase2.columnSetting() { isDeveloper = true });
            grd.columnsettings.Add("OverheadItemID", new GridEditViewBase2.columnSetting() { isDeveloper = true });
            grd.columnsettings.Add("Title", new GridEditViewBase2.columnSetting() { format = GridEditViewBase2.columnSetting.formatType.TEXT });
            grd.columnsettings.Add("Duration", new GridEditViewBase2.columnSetting() { format = GridEditViewBase2.columnSetting.formatType.G });
            grd.columnsettings.Add("Count", new GridEditViewBase2.columnSetting() { format = GridEditViewBase2.columnSetting.formatType.G });
            grd.columnsettings.Add("Rate", new GridEditViewBase2.columnSetting() { format = GridEditViewBase2.columnSetting.formatType.G });
            grd.columnsettings.Add("OverheadGroupID", new GridEditViewBase2.columnSetting() { aggregation = GridEditViewBase2.columnSetting.aggregationType.COUNT });
           
            grd.columnsettings.Add("Hours", new GridEditViewBase2.columnSetting()
            {
                format = GridEditViewBase2.columnSetting.formatType.N2,
                isReadonly = true,
                order = 8,
                tooltip = "Calculated from Estimate hours (for the workgroup) + Additional hours (for the workgroup)" 
            });
            grd.columnsettings.Add("Total", new GridEditViewBase2.columnSetting()
            {
                format = GridEditViewBase2.columnSetting.formatType.N2,
                aggregation = GridEditViewBase2.columnSetting.aggregationType.SUM,
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


            //grd.grd.CellEditEnded += grd_CellEditEnded;
        }

        //void grd_CellEditEnded(object sender, GridViewCellEditEndedEventArgs e)
        //{

        //    if (e.EditAction == GridViewEditAction.Commit)
        //    {
        //        if (e.Cell.Column.UniqueName == "cmbOverheadTypeID")
        //        {
        //            if ((int)e.NewData==2)
        //            {
        //              //Example how to change cell values on user action
        //                ((tblTender_OverheadItem)e.Cell.ParentRow.DataContext).Duration= 0;
        //            }

        //        }
        //    }
        //}

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
