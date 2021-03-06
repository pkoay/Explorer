﻿
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


    public partial class EstimateView : RHSTabViewBase
    {

        EstimateViewModel vm;

        public EstimateView()
        {
            InitializeComponent();
        }

        public override void TabLoad()
        {
            vm = new EstimateViewModel(settings);
            grd.vm = vm;
            grd.grd.DataContext = vm;
            grd.grd.ItemsSource = vm.data;

            if (settings.user.SecurityGroups.Contains("WD_Tender"))
                grd.SetGrid(settings, true, true, true);
            else
                grd.SetGrid(settings, false, false, false);

           
            grd.columnsettings.Add("EstimateID", new GridEditViewBase.columnSetting() { isDeveloper = true });
            grd.columnsettings.Add("TenderID", new GridEditViewBase.columnSetting() { isDeveloper = true });
            grd.columnsettings.Add("Title", new GridEditViewBase.columnSetting() { isDeveloper = true });
            grd.columnsettings.Add("ScheduleID", new GridEditViewBase.columnSetting() { aggregation = GridEditViewBase.columnSetting.aggregationType.COUNT, format = GridEditViewBase.columnSetting.formatType.TEXT });
            grd.columnCombo.Clear();

            grd.columnCombo.Add("ScheduleID", GridLibrary.CreateCombo("cmbScheduleID", "Schedule", vm.cmbScheduleList(), "ScheduleID", "ClientCode"));
            grd.columnCombo.Add("DrawingID", GridLibrary.CreateCombo("cmbDrawingID", "Drawing", vm.cmbDrawingList(), "DrawingID", "Title"));
            grd.columnCombo.Add("ObjectID", GridLibrary.CreateCombo("cmbObjectID", "Object", vm.cmbObjectList(), "ObjectID", "Title"));
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
