
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
    
     
    public partial class TenderActivityView : RHSTabGridViewBase
    {
      
        TenderActivityViewModel vm;

        public TenderActivityView()
        {
            InitializeComponent();
            base.SetGrid(grd);

            columnNotRequired.Add("RowVersion");
            columnNotRequired.Add("TenderID");
            columnRename.Add("ActivityID", "ID");

            columnNotRequired.Add("SortOrder");
            columnNotRequired.Add("UpdatedBy");
            columnNotRequired.Add("UpdatedDate");
           
        }

        public override void TabLoad()
        {
            gridEdit = true;
            gridDelete = true;
            gridAdd= (node.TypeID=="TenderActivityFolder") ;
            
            // set grid data
            vm = new TenderActivityViewModel(node.TypeID, user.Person.PersonID, node.IDAsInt());
            viewModel = vm;
            grd.DataContext = viewModel;
            grd.ItemsSource = viewModel.data;
            //columnCombo.Clear();
            columnCombo.Add("UnitOfMeasureID", GridLibrary.CreateCombo("cmbUnitOfMeasureID", "Unit Of Measure", vm.cmbUnitOfMeasureList(), "UnitOfMeasureID", "Title"));
            //columnCombo.Add("StatusID", GridLibrary.CreateCombo("cmbStatusID", "Status", vm.cmbStatusList(),"StatusID", "Title"));
            base.TabLoad();

        }

     
      
    }

}
