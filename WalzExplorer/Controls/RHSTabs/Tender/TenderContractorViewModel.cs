using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalzExplorer.Common;
using WalzExplorer.Database;

namespace WalzExplorer.Controls.RHSTabs.Tender
{
    public class TenderContractorViewModel : RHSTabGridViewModelBase
    {
        private int tenderId;


        public TenderContractorViewModel(WEXSettings settings)
        {
            tenderId = ConvertLibrary.StringToInt(settings.node.FindID("TENDER", "-2"), -1);
            data = new ObservableCollection<ModelBase>(context.tblTender_Contractor
                .Where(c=>c.TenderID==tenderId)
                .OrderBy(m => m.SortOrder)
                );
            columnDefault.Clear();
            columnDefault.Add("TenderID", tenderId);
        }

        public override ModelBase DefaultItem()
        {
            tblTender_Contractor i= new tblTender_Contractor ();
            i.TenderID = tenderId;
            return i;
        }
        public List<object> cmbContractTypeList()
        {
            return context.tblTender_ContractorType.Where(a => a.TenderID == tenderId).OrderBy(a => a.SortOrder).ToList<object>();
        }
    }
}
