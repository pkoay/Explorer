using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalzExplorer.Database;

namespace WalzExplorer.Controls.RHSTabs.TenderContractor
{
    public class TenderContractorViewModel : RHSTabGridViewModelBase
    {
        private int _tenderId;


        public TenderContractorViewModel(int tenderId)
        {
            _tenderId = tenderId;
            data = new ObservableCollection<object>(context.tblTender_Contractor
                .Where(c=>c.TenderID==tenderId)
                .OrderBy(m => m.SortOrder)
                );
           
        }

        public override object DefaultItem()
        {
            tblTender_Contractor i= new tblTender_Contractor ();
            i.TenderID = _tenderId;
            //data.Insert(0,i);
            return i;
        }
        public List<object> cmbContractTypeList()
        {
            return context.tblTender_ContractorType.Where(d => d.TenderID == _tenderId).ToList<object>();
        }
    }
}
