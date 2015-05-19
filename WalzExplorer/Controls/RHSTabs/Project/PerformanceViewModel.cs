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
        public tblProject_HistoryRating summaryRating;


        //Flags
        public bool isOperationsManager = false;
        public bool isProjectManager = false;
        public bool isAdministrator = false;


        //multi purpose
        public ObservableCollection<tblProject_HistoryRating> ratingList;
        public ObservableCollection<tblProject_EarnedValueType> earnedValueList;


        //Revenue
        public ObservableCollection<tblProject_HistoryDollars> revenuePlannedData;
        public ObservableCollection<tblProject_HistoryDollars> revenueEarnedData;
        public ObservableCollection<tblProject_HistoryDollars> revenueActualData;
        public ObservableCollection<EarnedValueSummarydata> revenueSummaryData = new ObservableCollection<EarnedValueSummarydata>();
        public ObservableCollection<CustomLegendItem> revenueLegendData = new ObservableCollection<CustomLegendItem>();
        public string revenueSPIcolor;
        public string revenueCPIcolor;
        public string revenueToolTipPlanned;
        public string revenueToolTipEarned;
        public string revenueToolTipActual;
        public tblProject_HistoryRating revenueRating;


        //Hours
        //public ObservableCollection<tblProject_HistoryHours> hoursPlannedData;
        //public ObservableCollection<tblProject_HistoryHours> hoursEarnedData;
        //public ObservableCollection<tblProject_HistoryHours> hoursActualData;
        //public ObservableCollection<EarnedValueSummarydata> hoursSummaryData = new ObservableCollection<EarnedValueSummarydata>();
        //public ObservableCollection<CustomLegendItem> hoursLegendData = new ObservableCollection<CustomLegendItem>();
        //public string hoursSPIcolor;
        //public string hoursCPIcolor;
        //public string hoursToolTipPlanned;
        //public string hoursToolTipEarned;
        //public string hoursToolTipActual;
        //public tblProject_HistoryRating hoursRating;


        //Basic
        public tblProject_HistoryRating basicRating;
        public double? basicEarned;
        public double? basicCPI;

        //safety
        //public ObservableCollection<spWEX_RHS_Project_Performance_Safety_Result> safetyDetailedData;
        //public ObservableCollection<SafetySummarydata> safteySummaryData = new ObservableCollection<SafetySummarydata>();

        int ProjectID;
        //int HistoryID;
        string NodeTypeID;
        //string UserPersonID;

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

          

          

         

            //Hours
            //hoursActualData = new ObservableCollection<tblProject_HistoryHours>(context.tblProject_HistoryHours.Where(x => x.HistoryID == HistoryID && x.EarnedValueTypeID == 3));
            //hoursEarnedData = new ObservableCollection<tblProject_HistoryHours>(context.tblProject_HistoryHours.Where(x => x.HistoryID == HistoryID && x.EarnedValueTypeID == 2));
            //hoursPlannedData = new ObservableCollection<tblProject_HistoryHours>(context.tblProject_HistoryHours.Where(x => x.HistoryID == HistoryID && x.EarnedValueTypeID == 1));
            //hoursToolTipPlanned = earnedValueList.Where(x => x.Title == "Planned").First().ToolTipHours;
            //hoursToolTipEarned = earnedValueList.Where(x => x.Title == "Earned").First().ToolTipHours;
            //hoursToolTipActual = earnedValueList.Where(x => x.Title == "Actual").First().ToolTipHours;
         
            // dPlanned = (hoursPlannedData.Count == 0) ? 0 : hoursPlannedData.Where(x => x.WeekEnd == dtPeriodEnd).First().Value;
            // dEarned = (hoursEarnedData.Count == 0) ? 0 : hoursEarnedData.Where(x => x.WeekEnd == dtPeriodEnd).First().Value;
            // dActual = (hoursActualData.Count == 0) ? 0 : hoursActualData.Where(x => x.WeekEnd == dtPeriodEnd).First().Value;
            
            //hoursSummaryData.Clear();
            //hoursSummaryData.Add(new EarnedValueSummarydata()
            //{
            //    Planned = dPlanned,
            //    Earned = dEarned,
            //    Actual = dActual,
            //    ScheduleVariance = dEarned - dPlanned,
            //    CostVariance = dEarned - dActual,
            //    SPI = dEarned / dPlanned,
            //    CPI = dEarned / dActual,
            //});
         
            //hoursSPIcolor = GetRating(dEarned / dPlanned, "HoursCPISPI").Color;
            //hoursCPIcolor = GetRating(dEarned / dActual, "HoursCPISPI").Color;
            //hoursRating=GetRating(dEarned / Math.Max(dPlanned, dActual), "HoursCPISPI");

            //HoursLegend
            //foreach (tblProject_EarnedValueType evt in earnedValueList)
            //{
            //    CustomLegendItem hoursLegendItem = new CustomLegendItem() { Title = evt.Title, Color = evt.Color, ToolTip = evt.ToolTipHours };
            //    hoursLegendData.Add(hoursLegendItem);
            //}

            switch (historyData.TypeID)
            {
                case 1: //Basic Cost performance & Notes
                    BasicSetEarnedAndCPIAndRating();
                    break;
                case 2: //Advanced (P6)
                    RevenueDataSetup(HistoryID);
                    break;
                case 3: //notes only
                    //Nothing
                    break;
            }
        }
        private void RevenueDataSetup(int HistoryID)
        {
            //General
            DateTime dtPeriodEnd = historyData.PeriodEnd;

            double dPlanned;
            double dEarned;
            double dActual;
            //Revenue
            revenueActualData = new ObservableCollection<tblProject_HistoryDollars>(context.tblProject_HistoryDollars.Where(x => x.HistoryID == HistoryID && x.EarnedValueTypeID == 3));
            revenueEarnedData = new ObservableCollection<tblProject_HistoryDollars>(context.tblProject_HistoryDollars.Where(x => x.HistoryID == HistoryID && x.EarnedValueTypeID == 2));
            revenuePlannedData = new ObservableCollection<tblProject_HistoryDollars>(context.tblProject_HistoryDollars.Where(x => x.HistoryID == HistoryID && x.EarnedValueTypeID == 1));
            revenueToolTipPlanned = earnedValueList.Where(x => x.Title == "Planned").First().ToolTipCost;
            revenueToolTipEarned = earnedValueList.Where(x => x.Title == "Earned").First().ToolTipCost;
            revenueToolTipActual = earnedValueList.Where(x => x.Title == "Actual").First().ToolTipCost;

            dPlanned = (revenuePlannedData.Count == 0) ? 0 : revenuePlannedData.Where(x => x.WeekEnd == dtPeriodEnd).FirstOrDefault().Value;
            dEarned = (revenueEarnedData.Count == 0) ? 0 : revenueEarnedData.Where(x => x.WeekEnd == dtPeriodEnd).FirstOrDefault().Value;
            dActual = (revenueActualData.Count == 0) ? 0 : revenueActualData.Where(x => x.WeekEnd == dtPeriodEnd).FirstOrDefault().Value;

            revenueSummaryData.Clear();
            revenueSummaryData.Add(new EarnedValueSummarydata()
            {
                Planned = dPlanned,
                Earned = dEarned,
                Actual = dActual,
                ScheduleVariance = dEarned - dPlanned,
                CostVariance = dEarned - dActual,
                SPI = dEarned / dPlanned,
                CPI = dEarned / dActual,
            });

            revenueSPIcolor = GetRating(dEarned / dPlanned, "CostCPISPI").Color;
            revenueCPIcolor = GetRating(dEarned / dActual, "CostCPISPI").Color;
            revenueRating = GetRating(dEarned / Math.Max(dPlanned, dActual), "CostCPISPI");
            summaryRating = revenueRating;

            //Revenue Legend
            foreach (tblProject_EarnedValueType evt in earnedValueList)
            {
                CustomLegendItem costLegendItem = new CustomLegendItem() { Title = evt.Title, Color = evt.Color, ToolTip = evt.ToolTipCost };
                revenueLegendData.Add(costLegendItem);
            }

        }
            ////Set safety data
            //safetyDetailedData = new ObservableCollection<spWEX_RHS_Project_Performance_Safety_Result>(context.spWEX_RHS_Project_Performance_Safety(HistoryID));
            //safteySummaryData.Clear();
            //safteySummaryData.Add(new SafetySummarydata() { Title = "Target", LTI = 0, MTI = 0, FAI = null, NearMiss = null, LTIFR = 0, TRIFR = 5, Hours = null });
            //safteySummaryData.Add(new SafetySummarydata()
            //{
            //    Title = "Actual",
            //    LTI = (int?)historyData.SafetyLTI,
            //    MTI = (int?)historyData.SafetyMTI,
            //    FAI = (int?)historyData.SafetyFAI,
            //    NearMiss = (int?)historyData.SafetyNearMiss,
            //    LTIFR = (int?)historyData.SafetyLTIFR,
            //    TRIFR = (int?)historyData.SafetyTRIFR,
            //    Hours = (int?)historyData.SafetyHours,
            //});



            //switch (historyData.TypeID)
            //{
            //    case 1: //Basic Cost performance & Notes
            //        summaryRating = GetRating(earned / actual, "CostCPISPI");
            //        break;
            //    case 2: //Advanced (P6)
                        
            //        int LowestRating = new int[] { hoursRating.RatingID, 5 }.Min(); // add more here!
            //        summaryRating = context.tblProject_HistoryRating.Where(x => x.RatingID == LowestRating).First();
            //        break;
            //    case 3: //notes only
            //        TabBasic.Visibility = System.Windows.Visibility.Hidden;
            //        TabRevenue.Visibility = System.Windows.Visibility.Hidden;
            //        tcHistory.Visibility = System.Windows.Visibility.Hidden;
            //        break;
            //}


            // summary rating =lowest rating

        


        //private tblProject_HistoryHours GetFirstValue (ObservableCollection<tblProject_HistoryDollars> c)
        //{
        //    c.
        //}


        public void BasicSetEarnedAndCPIAndRating()
        {
            if (historyData.BasicCostAtCompletion != null)
            {
                double costToDate = historyData.BasicCostToReportDate.GetValueOrDefault(0);
                double earned = (costToDate==0)?0:historyData.BasicCostBudget.GetValueOrDefault(0) * costToDate / historyData.BasicCostAtCompletion.GetValueOrDefault(0);
                double actual = historyData.BasicCostToReportDate.GetValueOrDefault(0);
                basicEarned =earned;
                double cpi = (earned == 0) ? 1 :(actual == 0) ? 1 :earned / actual;
                basicCPI = cpi;
                basicRating =GetRating(cpi, "CostCPISPI");
                summaryRating = GetRating(cpi, "CostCPISPI");
            }
            else
            {
                basicEarned=null;
                basicRating=null;
                basicCPI=null;
                summaryRating = GetRating(999, "CostCPISPI"); ;
            }
        }

        private tblProject_HistoryRating GetRating(double value, string column)
        {
            if (double.IsPositiveInfinity(value)) value = 99999999999;
            if (double.IsNaN(value)) value = 0;
            tblProject_HistoryRating result = null;
            switch (column)
            {
                case "CostCPISPI":
                    result = (context.tblProject_HistoryRating.Where(x => x.CostCPISPI <= value || x.RatingID == 0).OrderByDescending(x => x.RatingID)).First();
                    break;
                case "HoursCPISPI":
                    result = (context.tblProject_HistoryRating.Where(x => x.HoursCPISPI <= value || x.RatingID == 0).OrderByDescending(x => x.RatingID)).First();
                    break;
                default:
                    break;

            }
            return result;
        }

        public void RecalculateHistoryData()
        {

            //var pHistoryID = new SqlParameter("@HistoryID", historyData.HistoryID);
            //var pPeriodEnd = new SqlParameter("@PeriodEnd",null);

            context.Database.ExecuteSqlCommand("EXEC [spProject.HistoryUpdate] @HistoryID=" + historyData.HistoryID + ",@PeriodEnd=null ");
            RefreshHistory();

        }

       

        public void Sign ()
        {
            switch (historyData.StatusID)
            {
                case 1:
                    if (isProjectManager || isOperationsManager||isAdministrator)
                    {
                        
                        historyData.ProjectManagerSignID = _settings.user.RealPerson.PersonID;
                        historyData.ProjectManagerSignDate = context.ServerDateTime();
                        historyData.StatusID = 2;
                        context.SaveChanges();
                        RefreshHistory();
                    }
                    else
                        throw new Exception("Should not be able to sign as Project manager");
                    break;
                case 2:
                    if (isOperationsManager || isAdministrator)
                    {
                        historyData.OperationsManagerSignID = _settings.user.RealPerson.PersonID;
                        historyData.OperationsManagerSignDate = context.ServerDateTime();
                        historyData.StatusID = 3;
                        context.SaveChanges();
                        RefreshHistory();
                    }
                    else
                        throw new Exception("Should not be able to sign as Operational manager");
                    break;

            }
           
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


    public class CustomLegendItem
    {
        public String Title { get; set; }
        public String Color { get; set; }
        public String ToolTip { get; set; }
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
