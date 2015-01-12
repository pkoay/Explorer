
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
    
     
    public partial class TenderContractorView : RHSTabGridViewBase
    {
      
        TenderContractorViewModel vm;
        
        public TenderContractorView()
        {
            InitializeComponent();
            base.SetGrid(grd);

            columnNotRequired.Add("RowVersion");
            columnNotRequired.Add("TenderID");
            columnRename.Add("ContractorID", "ID");
            columnReadOnly.Add("ContractorID");
            columnReadOnly.Add("SortOrder");
            columnReadOnly.Add("UpdatedBy");
            columnReadOnly.Add("UpdatedDate");
        }

        public override void TabLoad()
        {
            gridAdd = true;
            gridEdit = true;
            gridDelete = true;


            // set grid data
            vm = new TenderContractorViewModel(Convert.ToInt32(node.ID));
            viewModel = vm;
            grd.DataContext = viewModel;
            grd.ItemsSource = viewModel.data;
            columnCombo.Clear();
            columnCombo.Add("ContractorTypeID", GridLibrary.CreateCombo("cmbContractorTypeID", "Contractor Type", vm.cmbContractTypeList(),"ContractorTypeID", "Title"));
            
            base.TabLoad();

        }

     
      
    }

}
