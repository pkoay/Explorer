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
    public class PivotViewModel 
    {
        public ObservableCollection<vwTender_EstimateDetail> data;
        public WalzExplorerEntities context = new WalzExplorerEntities(false);
        int TenderID;
        public PivotViewModel(WEXSettings settings) 
        {
            TenderID = ConvertLibrary.StringToInt(settings.node.FindID("TENDER", "-2"), -1);
            data = new ObservableCollection<vwTender_EstimateDetail>(context.vwTender_EstimateDetail.Where(x => x.TenderID == TenderID ));  
        }
    }
}
