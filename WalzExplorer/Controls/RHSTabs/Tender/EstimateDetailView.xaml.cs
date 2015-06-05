
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


    public partial class EstimateDetailView : RHSTabViewBase
    {

        EstimateDetailViewModel vm;

        public EstimateDetailView()
        {
            InitializeComponent();
        }

        public override void TabLoad()
        {
            vm = new EstimateDetailViewModel(settings);
            grd.grd.DataContext = vm;
            grd.grd.ItemsSource = vm.data;

            grd.SetGrid(settings);
            
            //grd.SetGrid(settings,false, false, false);



            grd.columnSettings.developer.Add("TenderID");
            grd.columnSettings.developer.Add("ObjectID");
            grd.columnSettings.developer.Add("ComponentObjectID");
            grd.columnSettings.developer.Add("ObjectName");

        }

        public override string IssueIfClosed()
        {
            return "";
        }
       
      
    }

}
