using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WalzExplorer.Database;

namespace WalzExplorer
{
    /// <summary>
    /// Interaction logic for test.xaml
    /// </summary>
    public partial class test : Window
    {
        public WalzExplorerEntities context;
        public test()
        {
            InitializeComponent();
            this.context = new WalzExplorerEntities(false);
            grd.ItemsSource = new ObservableCollection<tblTender_ContractorType>(context.tblTender_ContractorType.Where(x => x.TenderID == 1));
        }
    }
}
