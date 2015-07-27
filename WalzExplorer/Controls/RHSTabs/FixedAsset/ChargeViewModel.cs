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
    public class ChargeViewModel 
    {
        public ObservableCollection<spWEX_RHS_FixedAsset_Charge_Result> data;
        public WalzExplorerEntities context = new WalzExplorerEntities(false);
        string AssetID;
        string NodeTypeID;
        int ProjectID;
        
       
        public ChargeViewModel (WEXSettings settings) //(string NodeType, string PersonID, int Id)
        {
            AssetID = settings.node.FindID("ASSET", "-1");
            ProjectID = ConvertLibrary.StringToInt(settings.node.FindID("PROJECT", "-1"), -2);
            NodeTypeID = settings.node.TypeID;

            data = new ObservableCollection<spWEX_RHS_FixedAsset_Charge_Result>(context.spWEX_RHS_FixedAsset_Charge(NodeTypeID, AssetID, ProjectID));  
        }
    }
}
