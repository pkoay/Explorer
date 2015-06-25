using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalzExplorer.Common;
using WalzExplorer.Controls.Grid;
using WalzExplorer.Database;

namespace WalzExplorer.Controls.RHSTabs.Tender
{
    public class WorkGroupViewModel :GridEditViewModelBase
    {
        int TenderID;

        public WorkGroupViewModel(WEXSettings settings) //(string NodeType, string PersonID, int Id)
        {
            TenderID = ConvertLibrary.StringToInt(settings.node.FindID("TENDER", "-2"), -1);
            data = new ObservableCollection<ModelBase>(context.tblTender_WorkGroup.Where(x => x.TenderID == TenderID && x.SortOrder >= 0));  
        }

        public override ModelBase DefaultItem(ModelBase NearItem)
        {
            tblTender_WorkGroup i = new tblTender_WorkGroup();
            i.TenderID = TenderID;
            return i;
        }
    }
}
