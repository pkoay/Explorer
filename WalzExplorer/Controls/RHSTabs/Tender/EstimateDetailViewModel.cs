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
    public class EstimateDetailViewModel : Grid_Read
    {
        public ObservableCollection<spTender_EstimateDetail_Result> data;
        public WalzExplorerEntities context = new WalzExplorerEntities(false);
        int TenderID;

        public EstimateDetailViewModel(WEXSettings settings) //(string NodeType, string PersonID, int Id)
        {
      
            TenderID = ConvertLibrary.StringToInt(settings.node.FindID("TENDER", "-2"), -1);
            data = new ObservableCollection<spTender_EstimateDetail_Result>(context.spTender_EstimateDetail(TenderID));
           
        }

    }
}
