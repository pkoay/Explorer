
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
    
     
    public partial class TenderActivityLabourView : RHSTabGridViewBase
    {
      
        TenderActivityLabourViewModel vm;

        public TenderActivityLabourView()
        {
            InitializeComponent();
            base.SetGrid(grd);

            columnNotRequired.Add("RowVersion");
            columnNotRequired.Add("TenderID");
            columnNotRequired.Add("ActivityID");
            columnNotRequired.Add("ActivityLabourID");

            columnNotRequired.Add("SortOrder");
            columnNotRequired.Add("UpdatedBy");
            columnNotRequired.Add("UpdatedDate");
           
        }

        public override void TabLoad()
        {
            gridEdit = true;
            gridDelete = true;
            gridAdd = true;
            
            // set grid data
            vm = new TenderActivityLabourViewModel(node.TypeID, user.Person.PersonID, node.IDAsInt());
            viewModel = vm;
            grd.DataContext = viewModel;
            grd.ItemsSource = viewModel.data;
            columnCombo.Add("LabourStandardID", GridLibrary.CreateCombo("cmbLabourStandradID", "LabourStandard", vm.cmbLabourStandardList(), "LabourStandardID", "Title"));
            columnCombo.Add("StepID", GridLibrary.CreateCombo("cmbStepID", "Step", vm.cmbStepList(), "StepID", "Title"));
            columnCombo.Add("WorkGroupID", GridLibrary.CreateCombo("cmbWorkGroupID", "WorkGroup", vm.cmbWorkGroupList(), "WorkGroupID", "Title"));
            columnCombo.Add("UnitOfMeasureID", GridLibrary.CreateCombo("cmbUnitOfMeasureID", "Unit Of Measure", vm.cmbUnitOfMeasureList(), "UnitOfMeasureID", "Title"));
            base.TabLoad();

        }

     
      
    }

}
