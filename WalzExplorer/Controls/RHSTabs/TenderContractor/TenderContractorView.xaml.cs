//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.SqlClient;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows;
//using System.Windows.Controls;
//using System.Windows.Data;
//using System.Windows.Documents;
//using System.Windows.Input;
//using System.Windows.Media;
//using System.Windows.Media.Imaging;
//using System.Windows.Navigation;
//using System.Windows.Shapes;
//using Telerik.Windows.Controls;
//using Telerik.Windows.Controls.GridView;
//using Telerik.Windows.Controls.Input;
//using Telerik.Windows.Data;
//using System.Data.Entity;
//using System.Data.Entity.Infrastructure;
//using WalzExplorer.Database;
//using System.Text.RegularExpressions;
//using WalzExplorer.Controls.Common;
//using System.Data.Entity.Validation;

using System;
using System.Windows;
using System.Windows.Input;
using WalzExplorer.Controls.Common;
namespace WalzExplorer.Controls.RHSTabs.TenderContractor
{
    /// <summary>
    /// Interaction logic for TenderViewer.xaml
    /// </summary>
    public partial class TenderContractorView : RHSTabGridViewBase
    {
        TenderContractorViewModel vm;
        //private TenderContractorViewModel viewModel;
        public TenderContractorView()
        {
            InitializeComponent();
            base.SetGrid(grd);

            columnNotRequired.Add("RowVersion");
            columnRename.Add("ContractorID", "ID");
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
            cmb = GridLibrary.CreateCombo("cmbContractorTypeID", "Contractor Type", vm.cmbContractTypeList(), "Title");
            base.TabLoad();
        }

        public override void GridLoaded()
        {
  
            //foreach (Telerik.Windows.Controls.GridViewColumn c in grd.Columns)
            //{
            //    if (c.UniqueName == "ContractorID") c.Header="ID"; //Change column header 
            //    //if (c.UniqueName == "TenderID") c.IsVisible = false;
            //}
         
            //Add Combo
            //GridLibrary.ReplaceColumnWithCombo(viewModel.context, grd.Columns["ContractorTypeID"], new string[] { node.ID });
            base.GridLoaded();
        }

      

      
    }

}
