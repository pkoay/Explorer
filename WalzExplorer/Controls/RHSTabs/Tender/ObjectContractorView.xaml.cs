
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


    public partial class ObjectContractorView : RHSTabViewBase
    {

        ObjectContractorViewModel vm;

       public ObjectContractorView()
        {
            InitializeComponent();
        }

        public override void TabLoad()
        {
            vm = new ObjectContractorViewModel(settings);
            grd.vm = vm;
            grd.grd.DataContext = vm;
            grd.grd.ItemsSource = vm.data;
            
            if (settings.user.SecurityGroups.Contains("WD_Tender"))
                grd.SetGrid(settings,true,true,true);
            else
                grd.SetGrid(settings, false, false, false);

           
            grd.columnsettings.Add("ObjectContractorID", new GridEditViewBase.columnSetting() { isDeveloper = true });
            grd.columnsettings.Add("ObjectID", new GridEditViewBase.columnSetting() { isDeveloper = true });
            grd.columnsettings.Add("Title", new GridEditViewBase.columnSetting() { aggregation = GridEditViewBase.columnSetting.aggregationType.COUNT, format = GridEditViewBase.columnSetting.formatType.TEXT });
            grd.columnsettings.Add("Quantity", new GridEditViewBase.columnSetting() { format = GridEditViewBase.columnSetting.formatType.G });
            grd.columnsettings.Add("Rate", new GridEditViewBase.columnSetting() { format = GridEditViewBase.columnSetting.formatType.G });
            grd.columnsettings.Add("MarkUp", new GridEditViewBase.columnSetting() { format = GridEditViewBase.columnSetting.formatType.G });
 
            grd.columnCombo.Clear();
            grd.columnCombo.Add("ContractorID", GridLibrary.CreateCombo("cmbContractorID", "Contractor", vm.cmbContractorList(), "ContractorID", "Title"));
            grd.columnCombo.Add("StepID", GridLibrary.CreateCombo("cmbStepID", "Step", vm.cmbStepList(), "StepID", "Title"));
           

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
