using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalzExplorer.Database;

namespace WalzExplorer.Controls.RHSTabs.TenderContractor
{
    public class TenderContractorViewModel : RHSTabGridViewModelBase<tblTender_Contractor>
    {
        public TenderContractorViewModel(int tenderId)
        {
            data = new ObservableCollection<tblTender_Contractor>(context.tblTender_Contractor
                .Where(c=>c.TenderID==tenderId)
                .OrderBy(m => m.SortOrder)
                );
        }
    }
}
