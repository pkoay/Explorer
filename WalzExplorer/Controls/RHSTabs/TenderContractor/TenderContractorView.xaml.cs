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
namespace WalzExplorer.Controls.RHSTabs.TenderContractor
{
    /// <summary>
    /// Interaction logic for TenderViewer.xaml
    /// </summary>
    public partial class TenderContractorView : RHSTabViewBase
    {
        //WalzExplorerViewModel vm = new WalzExplorerViewModel();
        ////private readonly WalzExplorerEntities context= new WalzExplorerEntities();
        //private string item;
        //int id;

        public TenderContractorView()
        {
            InitializeComponent();

        }


        public override void Load()
        {
            TenderContractorViewModel viewModel = new TenderContractorViewModel(Convert.ToInt32(node.ID));
            this.DataContext = viewModel;
            grd.ItemsSource = viewModel.data;
        }
    }

}
