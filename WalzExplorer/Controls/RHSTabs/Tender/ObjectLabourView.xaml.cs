
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


    public partial class ObjectLabourView : RHSTabViewBase
    {

        ObjectLabourViewModel vm;

       public ObjectLabourView()
        {
            InitializeComponent();
        }

        public override void TabLoad()
        {
            vm = new ObjectLabourViewModel(settings);
            grd.vm = vm;
            grd.grd.DataContext = vm;
            grd.grd.ItemsSource = vm.data;
            
            if (settings.user.SecurityGroups.Contains("WD_Tender"))
                grd.SetGrid(settings,true,true,true);
            else
                grd.SetGrid(settings, false, false, false);

           
            grd.columnsettings.Add("ObjectLabourID", new GridEditViewBase.columnSetting() { isDeveloper = true });
            grd.columnsettings.Add("ObjectID", new GridEditViewBase.columnSetting() { isDeveloper = true });
            grd.columnsettings.Add("Title", new GridEditViewBase.columnSetting() { aggregation = GridEditViewBase.columnSetting.aggregationType.COUNT, format = GridEditViewBase.columnSetting.formatType.TEXT });
            grd.columnsettings.Add("Quantity", new GridEditViewBase.columnSetting() { format = GridEditViewBase.columnSetting.formatType.G });
            grd.columnsettings.Add("Men", new GridEditViewBase.columnSetting() { format = GridEditViewBase.columnSetting.formatType.G });
            grd.columnsettings.Add("Hours", new GridEditViewBase.columnSetting() { format = GridEditViewBase.columnSetting.formatType.G });
            grd.columnsettings.Add("Rate", new GridEditViewBase.columnSetting() { format = GridEditViewBase.columnSetting.formatType.N2, isReadonly = true , order=6});
            grd.columnsettings.Add("HoursTotal", new GridEditViewBase.columnSetting() { format = GridEditViewBase.columnSetting.formatType.N2, aggregation = GridEditViewBase.columnSetting.aggregationType.SUM, isReadonly = true });
            grd.columnsettings.Add("CostTotal", new GridEditViewBase.columnSetting() { format = GridEditViewBase.columnSetting.formatType.N2, aggregation = GridEditViewBase.columnSetting.aggregationType.SUM, isReadonly = true });
            grd.columnsettings.Add("Comment", new GridEditViewBase.columnSetting() { order=99});

 
            grd.columnCombo.Clear();
            grd.columnCombo.Add("UnitOfMeasureID", GridLibrary.CreateCombo("cmbUnitOfMeasureID", "Unit Of Measure", vm.cmbUnitOfMeasureList(), "UnitOfMeasureID", "Title"));
            grd.columnCombo.Add("StepID", GridLibrary.CreateCombo("cmbStepID", "Step", vm.cmbStepList(), "StepID", "Title"));
            grd.columnCombo.Add("WorkGroupID", GridLibrary.CreateCombo("cmbWorkGroupID", "WorkGroup", vm.cmbWorkGroupList(), "WorkGroupID", "Title"));
            grd.columnCombo.Add("LabourRateID", GridLibrary.CreateCombo("cmbLabourRateID", "LabourRate", vm.cmbLabourRateList(), "LabourRateID", "Title"));

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
