using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WalzExplorer.Common;
using WalzExplorer.Database;

namespace WalzExplorer.Controls.RHSTabs.Project
{
    public class PurchaseOrderSummaryViewModel 
    {
        public ObservableCollection<spWEX_RHS_Project_PurchaseOrderSummary_v2_Result> data;
        public WalzExplorerEntities context = new WalzExplorerEntities(false);
        int ProjectID;
        //string CustomerID;
        string NodeTypeID;
        int UserPersonID;

        public PurchaseOrderSummaryViewModel(WEXSettings settings) //(string NodeType, string PersonID, int Id)
        {
            ProjectID = ConvertLibrary.StringToInt(settings.node.FindID("PROJECT", "-2"), -1);
            //CustomerID = ConvertLibrary.StringToInt(settings.node.FindID("CUSTOMER", "-2"), -1).ToString();
            NodeTypeID = settings.node.TypeID;
            UserPersonID = settings.user.MimicedPerson.PersonID;
            switch (NodeTypeID.ToUpper())
            {
                case "PROJECTPURCHASEORDER":
                    data = new ObservableCollection<spWEX_RHS_Project_PurchaseOrderSummary_v2_Result>(context.spWEX_RHS_Project_PurchaseOrderSummary_v2(ProjectID,-1)); 
                    break;
                 case "PROJECTSMYOPEN":
                    data = new ObservableCollection<spWEX_RHS_Project_PurchaseOrderSummary_v2_Result>(context.spWEX_RHS_Project_PurchaseOrderSummary_v2(-1, UserPersonID)); 
                    break;
                default:
                    MessageBox.Show("Not  valid node for 'Purchase order summmary'");
                    break;

            }

  

            ////var pTreeNodeTypeID = new SqlParameter("@TreeNodeTypeID", LHSNode.NodeTypeID);
            ////var pNodeTypeID= new SqlParameter("@NodeTypeID", settings.node.TypeID);
            ////var pNTSecurityGroupsSeperator = new SqlParameter("@NTSecurityGroupsSeperator", "|");
            ////data = context.Database.SqlQuery( "spWEX.RHSTabList", pTreeNodeTypeID, pNodeTypeID, pNTSecurityGroupsSeperator).ToList();
            //switch (NodeTypeID)
            //{
            //    case "PROJECT":
            //        data = new ObservableCollection<spWEX_RHS_Project_Summary_Result>(context.spWEX_RHS_Project_Summary(UserPersonID, NodeTypeID, settings.SearchCriteria, ProjectID, ManagerID, CustomerID));
            //        break;

            //}

        }
        
    }
}
