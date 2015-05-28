
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


    public partial class OverheadGroupView : RHSTabViewBase
    {
      
       OverheadGroupViewModel vm;

       public OverheadGroupView()
        {
            InitializeComponent();
        }

        public override void TabLoad()
        {
            vm = new OverheadGroupViewModel(settings);
            grd.vm = vm;
            grd.grd.DataContext = vm;
            grd.grd.ItemsSource = vm.data;
           
            grd.SetGrid(settings,true,true,true);

            grd.columnsettings.Add("OverheadGroupID", new GridEditViewBase.columnSetting() { isDeveloper  = true });
            grd.columnsettings.Add("TenderID", new GridEditViewBase.columnSetting() { isDeveloper = true });
           
        }

        public override string IssueIfClosed()
        {
            return "";
        }
       
      
    }

}
