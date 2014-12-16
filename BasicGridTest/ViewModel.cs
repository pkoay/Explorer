using BasicGridTest;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalzExplorer.Database
{
    public class ViewModel
    {
        private readonly ObservableCollection<tblMain> mainView;
        public readonly BASICGRIDDATAEntities context;

        public ViewModel()
        {
            this.context = new BASICGRIDDATAEntities();
            this.mainView = new ObservableCollection<tblMain>(context.tblMains);
        }

        public ObservableCollection<tblMain> TenderContractorsView
        {
            get { return this.mainView; }
        }

    }
}
