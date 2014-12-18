using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalzExplorer.Database
{
    public class WalzExplorerViewModel
    {
        private readonly ObservableCollection<tblTender_Contractor> tenderContractorsView;
        public readonly WalzExplorerEntities context;

        public WalzExplorerViewModel()
        {
            this.context = new WalzExplorerEntities();
            this.tenderContractorsView = new ObservableCollection<tblTender_Contractor>(context.tblTender_Contractor);
        }

        public ObservableCollection<tblTender_Contractor> TenderContractorsView
        {
            get { return this.tenderContractorsView; }
        }

    }
}
