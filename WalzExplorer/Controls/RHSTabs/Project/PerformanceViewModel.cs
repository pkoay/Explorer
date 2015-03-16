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
  
    public class PerformanceViewModel 
    {
       
        public WalzExplorerEntities context = new WalzExplorerEntities(false);
        public WEXSettings _settings;

        //history
        public ObservableCollection<spWEX_RHS_Project_Performance_History_Result> historyList;
        public tblProject_History historyData;
        public tblProject projectData;
        public tblPerson projectManagerSignatureData;
        public tblPerson operationalManagerSignatureData;


        //Flags
        public bool isOperationsManager = false;
        public bool isProjectManager = false;
        public bool isAdministrator = false;


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
        //int HistoryID;
        string NodeTypeID;
        string UserPersonID;

        public PerformanceViewModel(WEXSettings settings) //(string NodeType, string PersonID, int Id)
        {
            _settings=settings;
 
            ProjectID = ConvertLibrary.StringToInt(settings.node.FindID("PROJECT", "-2"), -1);
            NodeTypeID = settings.node.TypeID;
            

            //History Dropdown population
            historyList = new ObservableCollection<spWEX_RHS_Project_Performance_History_Result>(context.spWEX_RHS_Project_Performance_History(ProjectID));
            
            //multi purpose
            ratingList = new ObservableCollection<tblProject_HistoryRating>(context.tblProject_HistoryRating);
            earnedValueList = new ObservableCollection<tblProject_EarnedValueType>(context.tblProject_EarnedValueType);

            //Have history
            if (historyList.Count > 0)
            {
                LoadHistory(historyList.FirstOrDefault().HistoryID);
            }
        }

        public void RefreshHistory()
        {
            LoadHistory(historyData.HistoryID);
        }

        public void LoadHistory(int HistoryID)
        {
            //Save any data first
            context.SaveChanges();

            //Summary
            historyData = context.tblProject_History.Where(x => x.HistoryID == HistoryID).First();
            projectData = context.tblProjects.Where(x => x.ProjectID == historyData.ProjectID).First();
            projectManagerSignatureData = context.tblPersons.Where(x => x.PersonID == historyData.ProjectManagerSignID).FirstOrDefault();
            operationalManagerSignatureData = context.tblPersons.Where(x => x.PersonID == historyData.OperationsManagerSignID).FirstOrDefault();

            //Set flags
            isProjectManager = (_settings.user.MimicedPerson.PersonID==projectData.ManagerID);
            isOperationsManager = (_settings.user.MimicedPerson.PersonID == projectData.OperationsManagerID);
            isAdministrator = (_settings.user.SecurityGroups.Contains("WP_Admin_Senior") || _settings.user.SecurityGroups.Contains("WD_IT"));

            //General
            DateTime dtPeriodEnd = historyData.PeriodEnd;

            //Hours
            hoursActualData = new ObservableCollection<tblProject_HistoryHours>(context.tblProject_HistoryHours.Where(x => x.HistoryID == HistoryID && x.EarnedValueTypeID == 3));
            hoursEarnedData = new ObservableCollection<tblProject_HistoryHours>(context.tblProject_HistoryHours.Where(x => x.HistoryID == HistoryID && x.EarnedValueTypeID == 2));
            hoursPlannedData = new ObservableCollection<tblProject_HistoryHours>(context.tblProject_HistoryHours.Where(x => x.HistoryID == HistoryID && x.EarnedValueTypeID == 1));
            double dPlanned = hoursPlannedData.Where(x => x.WeekEnd == dtPeriodEnd).First().Value;
            double dEarned = hoursEarnedData.Where(x => x.WeekEnd == dtPeriodEnd).First().Value;
            double dActual = hoursActualData.Where(x => x.WeekEnd == dtPeriodEnd).First().Value;
            hoursSummaryData.Clear();
            hoursSummaryData.Add(new EarnedValueSummarydata()
            {
                Planned = dPlanned,
                Earned = dEarned,
                Actual = dActual,
                ScheduleVariance = dEarned - dPlanned,
                CostVariance = dEarned - dActual,
                SPI = dEarned / dPlanned,
                CPI = dEarned / dActual,
            });


            //Set safety data
            safetyDetailedData = new ObservableCollection<spWEX_RHS_Project_Performance_Safety_Result>(context.spWEX_RHS_Project_Performance_Safety(HistoryID));
            safteySummaryData.Clear();
            safteySummaryData.Add(new SafetySummarydata() { Title = "Target", LTI = 0, MTI = 0, FAI = null, NearMiss = null, LTIFR = 0, TRIFR = 5, Hours = null });
            safteySummaryData.Add(new SafetySummarydata()
            {
                Title = "Actual",
                LTI = (int?)historyData.SafetyLTI,
                MTI = (int?)historyData.SafetyMTI,
                FAI = (int?)historyData.SafetyFAI,
                NearMiss = (int?)historyData.SafetyNearMiss,
                LTIFR = (int?)historyData.SafetyLTIFR,
                TRIFR = (int?)historyData.SafetyTRIFR,
                Hours = (int?)historyData.SafetyHours,
            });
        }

        public void RecalculateHistoryData()
        {

            //var pHistoryID = new SqlParameter("@HistoryID", historyData.HistoryID);
            //var pPeriodEnd = new SqlParameter("@PeriodEnd",null);

            context.Database.ExecuteSqlCommand("EXEC [spProject.HistoryUpdate] @HistoryID=" + historyData.HistoryID + ",@PeriodEnd=null ");
            RefreshHistory();

        }

        private DateTime ServerDateTime()
        {
            DateTime? sdt= context.spExplorer_ServerDatetime().FirstOrDefault();
            if (!sdt.HasValue)
                 throw new Exception ("Cannot get server date time");
            else
                return sdt.Value;
        }

        public void Sign ()
        {
            switch (historyData.StatusID)
            {
                case 1:
                    if (isProjectManager)
                    {
                        historyData.ProjectManagerSignID = _settings.user.RealPerson.PersonID;
                        historyData.ProjectManagerSignDate = ServerDateTime();
                        historyData.StatusID = 2;
                    }
                    else
                        throw new Exception("Should not be able to sign as Project manager");
                    break;
                case 2:
                    if (isOperationsManager)
                    {
                        historyData.OperationsManagerSignID = _settings.user.RealPerson.PersonID;
                        historyData.OperationsManagerSignDate = ServerDateTime();
                        historyData.StatusID = 3;
                    }
                    else
                        throw new Exception("Should not be able to sign as Operational manager");
                    break;

            }
            context.SaveChanges();
            RefreshHistory();
        }

        public void ClearSignature()
        {
            switch (historyData.StatusID)
            {
                case 2:
                    if (isOperationsManager || isAdministrator)
                    {
                        historyData.ProjectManagerSignID = null;
                        historyData.ProjectManagerSignDate = null;
                        historyData.StatusID = 1;
                    }
                    else
                        throw new Exception("Should not be able to unsign Project manager");
                    break;
                case 3:
                    if (isAdministrator)
                    {
                        historyData.OperationsManagerSignID = null;
                        historyData.OperationsManagerSignDate = null;
                        historyData.StatusID = 2;
                    }
                    else
                        throw new Exception("Should not be able to unsign Operational manager");
                    break;

            }
            context.SaveChanges();
            RefreshHistory();
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
