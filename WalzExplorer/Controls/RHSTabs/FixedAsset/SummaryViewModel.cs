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

namespace WalzExplorer.Controls.RHSTabs.FixedAsset
{
    public class SummaryViewModel 
    {
        public ObservableCollection<spWEX_RHS_FixedAsset_Summary2_Result> data;
        public WalzExplorerEntities context = new WalzExplorerEntities(false);
        string AssetID;
        string NodeTypeID;
        string AssetGroup;
        
       
        public SummaryViewModel (WEXSettings settings) //(string NodeType, string PersonID, int Id)
        {
            AssetID = settings.node.FindID("ASSET", "-1");
            AssetGroup = settings.node.FindID("ASSETGROUP", "-1");
            NodeTypeID = settings.node.TypeID;

            //var pTreeNodeTypeID = new SqlParameter("@TreeNodeTypeID", LHSNode.NodeTypeID);
            //var pNodeTypeID= new SqlParameter("@NodeTypeID", settings.node.TypeID);
            //var pNTSecurityGroupsSeperator = new SqlParameter("@NTSecurityGroupsSeperator", "|");
            //data = context.Database.SqlQuery( "spWEX.RHSTabList", pTreeNodeTypeID, pNodeTypeID, pNTSecurityGroupsSeperator).ToList();

            data = new ObservableCollection<spWEX_RHS_FixedAsset_Summary2_Result>(context.spWEX_RHS_FixedAsset_Summary2(NodeTypeID, settings.SearchCriteria, AssetID, AssetGroup));  
        }
    }
}
