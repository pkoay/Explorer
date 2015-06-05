
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


    public partial class EstimateWorkGroupRateView : RHSTabViewBase
    {

        EstimateWorkGroupRateViewModel vm;

        public EstimateWorkGroupRateView()
        {
            InitializeComponent();
        }

        public override void TabLoad()
        {
            vm = new EstimateWorkGroupRateViewModel(settings);
            grd.grd.DataContext = vm;
            grd.grd.ItemsSource = vm.data;

            grd.SetGrid(settings);

            grd.columnSettings.developer.Add("TenderID");
            grd.columnSettings.developer.Add("WorkGroupID");
           

        }

        public override string IssueIfClosed()
        {
            return "";
        }
       
      
    }

}
