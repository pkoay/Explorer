
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;
using WalzExplorer.Common;
namespace WalzExplorer.Controls.RHSTabs.Tender
{
    /// <summary>
    /// Interaction logic for TenderViewer.xaml
    /// </summary>
    
     
    public partial class TenderView : RHSTabGridViewBase
    {
      
        TenderViewModel vm;
        
        public TenderView()
        {
            InitializeComponent();
            base.SetGrid(grd);

            columnNotRequired.Add("RowVersion");
            columnNotRequired.Add("TenderID");
            columnNotRequired.Add("Comments");
            columnRename.Add("TenderID", "ID");

            columnNotRequired.Add("SortOrder");
            columnNotRequired.Add("UpdatedBy");
            columnNotRequired.Add("UpdatedDate");
        }

        public override void TabLoad()
        {
            // set grid data

            vm = new TenderViewModel(node.TypeID, user.Person.PersonID, node.IDAsInt());
            viewModel = vm;
            grd.DataContext = viewModel;
            grd.ItemsSource = viewModel.data;
            //columnCombo.Clear();
            columnCombo.Add("ManagerID", GridLibrary.CreateCombo("cmbManagerID", "Manager", vm.cmbManagerList(),"PersonID", "Name"));
            columnCombo.Add("StatusID", GridLibrary.CreateCombo("cmbStatusID", "Status", vm.cmbStatusList(),"StatusID", "Title"));
            base.TabLoad();

        }

     
      
    }

}
