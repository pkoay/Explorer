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
    public class OverheadViewModel :GridEditViewModelBase
    {
        int TenderID;

        public OverheadViewModel(WEXSettings settings) //(string NodeType, string PersonID, int Id)
        {
            TenderID = ConvertLibrary.StringToInt(settings.node.FindID("TENDER", "-2"), -1);
            data = new ObservableCollection<ModelBase>(context.tblTender_Overhead.Where(x=>x.TenderID==TenderID));  
        }

        public override ModelBase DefaultItem()
        {
            tblTender_Overhead i = new tblTender_Overhead();
            i.TenderID = TenderID;
            return i;
        }
    }
}
