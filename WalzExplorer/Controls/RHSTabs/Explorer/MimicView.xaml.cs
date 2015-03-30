
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
namespace WalzExplorer.Controls.RHSTabs.Explorer
{
    /// <summary>
    /// Interaction logic for TenderViewer.xaml
    /// </summary>


    public partial class MimicView : RHSTabViewBase
    {
      
       MimicViewModel vm;

        public MimicView()
        {
            InitializeComponent();
        }

        public override void TabLoad()
        {
            vm = new MimicViewModel(settings);
            grd.vm = vm;
            grd.grd.DataContext = vm;
            grd.grd.ItemsSource = vm.data;
           
            grd.SetGrid(settings,true,true,true);
            grd.columnSettings.developer.Add("SortOrder");
            grd.columnSettings.developer.Add("RowVersion");
            grd.columnSettings.developer.Add("UpdatedBy");
            grd.columnSettings.developer.Add("UpdatedDate");
            grd.columnSettings.rename.Add("MimicPersonID", "Can Mimic");
            grd.columnCombo.Clear();
            grd.columnCombo.Add("PersonID", GridLibrary.CreateCombo("cmbPersonID", "Grant To", vm.cmbUserList(), "PersonID", "Name"));
            grd.columnCombo.Add("MimicPersonID", GridLibrary.CreateCombo("cmbMimicPersonID", "Can Mimic", vm.cmbUserList(), "PersonID", "Name"));
        }

        public override string IssueIfClosed()
        {
            return "";
        }
       
      
    }

}
