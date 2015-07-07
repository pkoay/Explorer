
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

namespace WalzExplorer.Controls.RHSTabs.Project
{
    /// <summary>
    /// Interaction logic for TenderViewer.xaml
    /// </summary>


    public partial class CostcodeIndirectView : RHSTabViewBase
    {

        CostcodeIndirectViewModel vm;

        public CostcodeIndirectView()
        {
            InitializeComponent();
        }

        public override void TabLoad()
        {
            vm = new CostcodeIndirectViewModel(settings);
            grd.vm = vm;
            grd.grd.DataContext = vm;
            grd.grd.ItemsSource = vm.data;
            grd.SetGrid(settings, false, true, false);

            grd.columnsettings.Add("ProjectID", new GridEditViewBase.columnSetting() { isDeveloper = true });
            grd.columnsettings.Add("AXProjectID", new GridEditViewBase.columnSetting() { aggregation = GridEditViewBase.columnSetting.aggregationType.COUNT,isReadonly = true,format= GridEditViewBase.columnSetting.formatType.TEXT});
            grd.columnsettings.Add("TopParentID", new GridEditViewBase.columnSetting() { isDeveloper = true });
            grd.columnsettings.Add("Title", new GridEditViewBase.columnSetting() { isReadonly = true,format= GridEditViewBase.columnSetting.formatType.TEXT} );
            

          
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
