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
  

    public class SummaryViewModel 
    {
        public WalzExplorerEntities context = new WalzExplorerEntities(false);

        //history
        public ObservableCollection<spWEX_RHS_Project_Performance_History_Result> historyList;
        public ObservableCollection<tblProject_History> historyData;

        //multi purpose
        public ObservableCollection<tblProject_HistoryRating> ratingList;
        public ObservableCollection<tblProject_EarnedValueType> earnedValueList;

        //Hours
        public ObservableCollection<tblProject_HistoryHours> hoursPlannedData;
        public ObservableCollection<tblProject_HistoryHours> hoursEarnedData;
        public ObservableCollection<tblProject_HistoryHours> hoursActualData;
        public ObservableCollection<EarnedValueSummarydata> hoursSummaryData = new ObservableCollection<EarnedValueSummarydata>();

        //safety
        public ObservableCollection<spWEX_RHS_Project_Performance_Safety_Result> safetyDetailedData;
        public ObservableCollection<SafetySummarydata> safteySummaryData = new ObservableCollection<SafetySummarydata>();

        int ProjectID;
        int HistoryID;
        string NodeTypeID;
        string UserPersonID;

        public SummaryViewModel (WEXSettings settings) //(string NodeType, string PersonID, int Id)
        {
 
            ProjectID = ConvertLibrary.StringToInt(settings.node.FindID("PROJECT", "-2"), -1);
            NodeTypeID = settings.node.TypeID;
            UserPersonID=settings.user.MimicedPerson.PersonID.ToString();

            //var pTreeNodeTypeID = new SqlParameter("@TreeNodeTypeID", LHSNode.NodeTypeID);
            //var pNodeTypeID= new SqlParameter("@NodeTypeID", settings.node.TypeID);
            //var pNTSecurityGroupsSeperator = new SqlParameter("@NTSecurityGroupsSeperator", "|");
            //data = context.Database.SqlQuery( "spWEX.RHSTabList", pTreeNodeTypeID, pNodeTypeID, pNTSecurityGroupsSeperator).ToList();

            //History Dropdown population
            historyList = new ObservableCollection<spWEX_RHS_Project_Performance_History_Result>(context.spWEX_RHS_Project_Performance_History(ProjectID));

            //multi purpose
            ratingList = new ObservableCollection<tblProject_HistoryRating>(context.tblProject_HistoryRating);
            earnedValueList = new ObservableCollection<tblProject_EarnedValueType>(context.tblProject_EarnedValueType);

            //Have history
            if (historyList.Count > 0)
            {
                HistoryID = historyList.FirstOrDefault().HistoryID;
                RefreshData(HistoryID);
            }
        }

        public void RefreshData (int HistoryID)
        {
            

            //Summary
            historyData = new ObservableCollection<tblProject_History>(context.tblProject_History.Where(x => x.HistoryID == HistoryID));

            //General
            DateTime dtPeriodEnd=historyData.FirstOrDefault().PeriodEnd;

            //Multi use
            

            //Hours
            hoursActualData = new ObservableCollection<tblProject_HistoryHours>(context.tblProject_HistoryHours.Where(x => x.HistoryID == HistoryID && x.EarnedValueTypeID == 3));
            hoursEarnedData = new ObservableCollection<tblProject_HistoryHours>(context.tblProject_HistoryHours.Where(x => x.HistoryID == HistoryID && x.EarnedValueTypeID == 2));
            hoursPlannedData = new ObservableCollection<tblProject_HistoryHours>(context.tblProject_HistoryHours.Where(x => x.HistoryID == HistoryID && x.EarnedValueTypeID == 1));
            double dPlanned = hoursPlannedData.Where(x=>x.WeekEnd==dtPeriodEnd).First().Value;
             double  dEarned = hoursEarnedData.Where(x=>x.WeekEnd==dtPeriodEnd).First().Value;
             double dActual = hoursActualData.Where(x => x.WeekEnd == dtPeriodEnd).First().Value;
            hoursSummaryData.Add(new EarnedValueSummarydata(){
                Planned = dPlanned,
                Earned = dEarned,
                Actual = dActual,
                ScheduleVariance = dEarned-dPlanned,
                CostVariance = dEarned - dActual,
                SPI = dEarned/dPlanned,
                CPI = dEarned / dActual,
            });
          

            //Set safety data
            safetyDetailedData = new ObservableCollection<spWEX_RHS_Project_Performance_Safety_Result>(context.spWEX_RHS_Project_Performance_Safety(HistoryID));
            safteySummaryData.Add(new SafetySummarydata() { Title = "Target", LTI = 0, MTI = 0, FAI = null, NearMiss = null, LTIFR = 0, TRIFR = 5 ,Hours =null});
            safteySummaryData.Add(new SafetySummarydata() { 
                Title = "Actual",
                LTI = (int?) historyData[0].SafetyLTI,
                MTI = (int?)historyData[0].SafetyMTI,
                FAI = (int?)historyData[0].SafetyFAI,
                NearMiss = (int?)historyData[0].SafetyNearMiss,
                LTIFR = (int?)historyData[0].SafetyLTIFR,
                TRIFR = (int?)historyData[0].SafetyTRIFR,
                Hours = (int?)historyData[0].SafetyHours,
            });

            
            

        }
    }

    public class SafetySummarydata
    {
        public String Title { get; set; }
        public int? LTI { get; set; }
        public int? MTI { get; set; }
        public int? FAI { get; set; }
        public int? NearMiss { get; set; }
        public int? LTIFR { get; set; }
        public int? TRIFR { get; set; }
        public int? Hours { get; set; }
    }

    public class EarnedValueSummarydata
    {
        
        public double? Planned { get; set; }
        public double? Earned { get; set; }
        public double? Actual { get; set; }
        public double? ScheduleVariance { get; set; }
        public double? CostVariance { get; set; }
        public double? SPI { get; set; }
        public double? CPI { get; set; }
    }
}
