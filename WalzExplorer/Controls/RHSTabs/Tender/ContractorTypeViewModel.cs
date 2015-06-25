using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Telerik.Windows.Data;
using WalzExplorer.Common;
using WalzExplorer.Controls.Grid;
using WalzExplorer.Database;

namespace WalzExplorer.Controls.RHSTabs.Tender
{
    public class ContractorTypeViewModel :GridEditViewModelBase
    {
        int TenderID;
        public ContractorTypeViewModel(WEXSettings settings) //(string NodeType, string PersonID, int Id)
        {
            TenderID = ConvertLibrary.StringToInt(settings.node.FindID("TENDER", "-2"), -1);
            data = new ObservableCollection<ModelBase>(context.tblTender_ContractorType.Where(x => x.TenderID == TenderID  && x.SortOrder>=0).OrderBy(x => x.SortOrder));  
        }

        public override ModelBase DefaultItem(ModelBase NearItem)
        {
            tblTender_ContractorType i = new tblTender_ContractorType();
            i.TenderID = TenderID;
            return i;
        }
    }
}
