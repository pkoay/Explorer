
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;
using WalzExplorer.Common;
//namespace WalzExplorer.Controls.RHSTabs.Tender
//{
//    public partial class TenderContractorView : RHSTabGridViewBase
//    {
      
//        TenderContractorViewModel vm;
        
//        public TenderContractorView()
//        {
//            InitializeComponent();
//        }

//        public override void TabLoad()
//        {
//            base.Reset();
//            base.SetGrid(grd);

//            columnReadOnlyDeveloper.Add("RowVersion");
//            columnReadOnlyDeveloper.Add("TenderID");
//            columnReadOnlyDeveloper.Add("ContractorID");
//            columnReadOnlyDeveloper.Add("SortOrder");
//            columnReadOnlyDeveloper.Add("UpdatedBy");
//            columnReadOnlyDeveloper.Add("UpdatedDate");

//            gridAdd = true;
//            gridEdit = true;
//            gridDelete = true;

//            // set grid data
//            vm = new TenderContractorViewModel(settings);
//            viewModel = vm;
//            grd.DataContext = viewModel;
//            grd.ItemsSource = viewModel.data;
//            columnCombo.Clear();
//            columnCombo.Add("ContractorTypeID", GridLibrary.CreateCombo("cmbContractorTypeID", "Contractor Type", vm.cmbContractTypeList(),"ContractorTypeID", "Title"));
            
//            base.TabLoad();
//        }
//    }
//}
