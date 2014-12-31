
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;
using WalzExplorer.Common;
namespace WalzExplorer.Controls.RHSTabs.TenderContractor
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
            // set grid data
            vm = new TenderContractorViewModel(Convert.ToInt32(node.ID));
            viewModel = vm;
            grd.DataContext = viewModel;
            grd.ItemsSource = viewModel.data;
            columnCombo.Clear();
            columnCombo.Add("ContractorTypeID", GridLibrary.CreateCombo("cmbContractorTypeID", "Contractor Type", vm.cmbContractTypeList(), "Title"));
            base.TabLoad();

        }

        protected override void g_CellValidating(object sender, GridViewCellValidatingEventArgs e)
        {
            if (e.Cell.Column.UniqueName == "Title")
            {
                string newValue = e.NewValue.ToString();
                if (newValue=="")
                {
                    e.IsValid = false;
                    e.ErrorMessage = "Title must not be blank.";
                }
            }
            if (e.Cell.Column.UniqueName == "cmbContractorTypeID")
            {
                if (e.NewValue==null)
                {
                    e.IsValid = false;
                    e.ErrorMessage = "Contractior Type required.";
                }
            }
        }
      
    }

}
