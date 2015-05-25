using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalzExplorer.Common;
using WalzExplorer.Database;

namespace WalzExplorer.Controls.RHSTabs.Project
{
    public class PurchaseOrderDetailViewModel 
    {
        public ObservableCollection<spWEX_RHS_Project_PurchaseOrder_Detail_Result> data;
        public WalzExplorerEntities context = new WalzExplorerEntities(false);
        
        public PurchaseOrderDetailViewModel(WEXSettings settings) //(string NodeType, string PersonID, int Id)
        {
            if (settings.drilldown == null)
            {
                int ProjectID;
                ProjectID = ConvertLibrary.StringToInt(settings.node.FindID("PROJECT", "-2"), -1);
                data = new ObservableCollection<spWEX_RHS_Project_PurchaseOrder_Detail_Result>(context.spWEX_RHS_Project_PurchaseOrder_Detail(ProjectID, null, null, null));
            }
            else
            {
                string DataAreaID = null;
                string PurchID = null;
                bool? isCommitted = null; //null is all, 1 is committed only, 0 paid only
                int? ProjectID = null;
                switch (settings.drilldown._filter)
                {
                    case WalzExplorer.Model.DrilldownFilter.PurchaseOrder:
                        DataAreaID = settings.drilldown._parameters["DataAreaID"];
                        PurchID = settings.drilldown._parameters["PurchID"];
                        isCommitted = ConvertLibrary.StringToNullableBool(settings.drilldown._parameters["isCommitted"],null);
                        break;
                    case WalzExplorer.Model.DrilldownFilter.Project:
                        ProjectID = ConvertLibrary.StringToInt(settings.drilldown._parameters["ProjectID"],-2);
                        isCommitted = ConvertLibrary.StringToNullableBool(settings.drilldown._parameters["isCommitted"], null);
                        break;
                    default:
                        break;
                }
                data = new ObservableCollection<spWEX_RHS_Project_PurchaseOrder_Detail_Result>(context.spWEX_RHS_Project_PurchaseOrder_Detail(ProjectID, DataAreaID, PurchID, isCommitted));

            }
        }
    }
}
