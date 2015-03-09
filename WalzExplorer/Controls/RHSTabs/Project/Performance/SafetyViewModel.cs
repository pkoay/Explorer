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
    //public class SafetySummarydata
    //{
    //    public String Title { get; set; }
    //    public int LTI { get; set; }
    //    public int MTI { get; set; }
    //    public int? FAI  { get; set; }
    //    public int? NearMiss { get; set; }
    //    public int LTIFR { get; set; }
    //    public int TRIFR { get; set; }
    //}
    public class SafetyViewModel 
    {
        public WalzExplorerEntities context = new WalzExplorerEntities(false);

        public ObservableCollection<spWEX_RHS_Project_Performance_History_Result> historyList;
        public ObservableCollection<spWEX_RHS_Project_Performance_Safety_Result> data;
        public ObservableCollection<SafetySummarydata> summaryData = new ObservableCollection<SafetySummarydata>();

        int ProjectID;
        int HistoryID;
        string NodeTypeID;
       

        public SafetyViewModel(WEXSettings settings) //(string NodeType, string PersonID, int Id)
        {
            ProjectID = ConvertLibrary.StringToInt(settings.node.FindID("ProjectID", "-2"), -1);
            NodeTypeID = settings.node.TypeID;

            //History Dropdown population
            historyList = new ObservableCollection<spWEX_RHS_Project_Performance_History_Result>(context.spWEX_RHS_Project_Performance_History(ProjectID));

            //Set Incident Data
            data = new ObservableCollection<spWEX_RHS_Project_Performance_Safety_Result>(context.spWEX_RHS_Project_Performance_Safety(HistoryID));  

            //Set Summary data
            summaryData.Add(new SafetySummarydata() { Title = "Target", LTI = 0, MTI = 0, FAI = null, NearMiss = null, LTIFR = 0, TRIFR = 5 });

        }
    }
}
