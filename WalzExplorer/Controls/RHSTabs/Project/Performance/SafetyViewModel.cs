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

namespace WalzExplorer.Controls.RHSTabs.Project.Performance
{
    public class SafetySummarydata
    {
        public String Title { get; set; }
        public int LTI { get; set; }
        public int MTI { get; set; }
        public int? FAI  { get; set; }
        public int? NearMiss { get; set; }
        public int LTIFR { get; set; }
        public int TRIFR { get; set; }
    }
    public class SafetyViewModel 
    {
        public WalzExplorerEntities context = new WalzExplorerEntities(false);
        
        
        public ObservableCollection<spWEX_RHS_Project_Summary_Result> data;
        public ObservableCollection<SafetySummarydata> summaryData = new ObservableCollection<SafetySummarydata>();
        
        int ProjectID;
        int ManagerID;
        int CustomerID;
        string NodeTypeID;
        string UserPersonID;

        public SafetyViewModel(WEXSettings settings) //(string NodeType, string PersonID, int Id)
        {
            //Sett Incident Data
            ManagerID = ConvertLibrary.StringToInt(settings.node.FindID("MANAGER", "-2"), -1);
            CustomerID = ConvertLibrary.StringToInt(settings.node.FindID("CUSTOMER", "-2"), -1);
            ProjectID = ConvertLibrary.StringToInt(settings.node.FindID("PROJECT", "-2"), -1);
            NodeTypeID = settings.node.TypeID;
            UserPersonID=settings.user.MimicedPerson.PersonID.ToString();
            //var pTreeNodeTypeID = new SqlParameter("@TreeNodeTypeID", LHSNode.NodeTypeID);
            //var pNodeTypeID= new SqlParameter("@NodeTypeID", settings.node.TypeID);
            //var pNTSecurityGroupsSeperator = new SqlParameter("@NTSecurityGroupsSeperator", "|");
            //data = context.Database.SqlQuery( "spWEX.RHSTabList", pTreeNodeTypeID, pNodeTypeID, pNTSecurityGroupsSeperator).ToList();
            data = new ObservableCollection<spWEX_RHS_Project_Summary_Result>(context.spWEX_RHS_Project_Summary(UserPersonID, NodeTypeID, ProjectID,ManagerID, CustomerID));  

            //Set Summary data
            summaryData.Add(new SafetySummarydata() { Title = "Target", LTI = 0, MTI = 0, FAI = null, NearMiss = null, LTIFR = 0, TRIFR = 5 });

        }
    }
}
