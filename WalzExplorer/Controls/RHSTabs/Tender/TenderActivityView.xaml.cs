
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
           
           
        }

        public override void TabLoad()
        {
            base.SetGrid(grd);
            base.Reset();

            columnReadOnlyDeveloper.Add("RowVersion");
            columnReadOnlyDeveloper.Add("TenderID");
            columnRename.Add("ActivityID", "ID");

            columnReadOnlyDeveloper.Add("SortOrder");
            columnReadOnlyDeveloper.Add("UpdatedBy");
            columnReadOnlyDeveloper.Add("UpdatedDate");

            gridEdit = true;
            gridDelete = true;
            gridAdd = (settings.node.TypeID == "TenderActivityFolder");
            
            // set grid data
            vm = new TenderActivityViewModel(settings.node.TypeID, settings.user.Person.PersonID, settings.node.IDAsInt());
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
