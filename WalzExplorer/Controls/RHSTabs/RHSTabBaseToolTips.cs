using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalzExplorer.Controls.RHSTabs
{
    public static class RHSTabBaseDefaultColumnSettings
    {
        enum Field
        {
            AX_DATAAREAID,
            AX_PROJECTID,
            AX_PROJECTTITLE,
            AX_PROJECTMANAGER,
            AX_OPERATIONSMANAGER,
            AX_COSTCODE,
            AX_COMMITTEDCOST,
            AX_BASECOST,
            AX_OVERHEADCOST,
            AX_COST,
            AX_PROJECTCATEGORYGROUP,
            AX_PROJECTCATEGORY,
            P6_TASKCODE,
            P6_TASKNAME,
            P6_LAB_STARTDATE,
            P6_LAB_ENDDATE,
            P6_EARNEDVALUEREVENUE,
            P6_EARNEDVALUEDIRECTLABOUR,
            P6_PERCENTCOMPLETE,
            P6_LAB_CONTRACTVALUE,
            P6_LAB_DIRECTLABOURBUDGET,
        }
        public static string DefaultToolTip (string field)
        {
            string ret="";

            switch (field.ToUpper())
            {
                case"COMMCOSTAMOUNT":
                case "COMMITTEDCOST":
                case "COMMITTEDAMOUNT":
                case "COMMITTED": ret = 
                    string.Join(Environment.NewLine
                        , "Committed Costs to date (Open purchase orders)"
                        );
                    break;
                case "PROJID":
                     string.Join(Environment.NewLine
                        , "AX Project/sub project ID (also known as CostCode)"
                        );
                    break;
                case "COSTOVERHEAD":
                    string.Join(Environment.NewLine
                        ,"Overhead cost from AX"
                        , "Does NOT include committed costs (Open purchase orders)"
                        );
                    break;
                case "COSTAMOUNT":
                    string.Join(Environment.NewLine
                        , "Actual cost from AX "
                        , "Does NOT include overheads"
                        , "Does NOT include committed costs (Open purchase orders)"
                        );
                    break;
                case "COSTTOTAL":
                case "ACTUAL":
                    string.Join(Environment.NewLine
                        , "Actual cost from AX including overheads"
                        , "Does NOT include committed costs (Open purchase orders)"
                        );
                    break;
                case "TARGET_START_DATE":
                    string.Join(Environment.NewLine
                        , "Latest Approved Baseline start date from P6"
                        );
                    break;
                case "TARGET_END_DATE":
                    string.Join(Environment.NewLine
                        , "Latest Approved Baseline end date from P6"
                        );
                    break;
                default:
                    break;

                      
        

            //grd.columnSettings.format.Add("TaskCode", Grid.Grid_Read.columnFormat.COUNT);
            //grd.columnSettings.format.Add("TaskName", Grid.Grid_Read.columnFormat.TEXT);
            //grd.columnSettings.format.Add("EarnedValueRevenue", Grid.Grid_Read.columnFormat.TWO_DECIMAL);
            //grd.columnSettings.format.Add("EarnedValueDirectHours", Grid.Grid_Read.columnFormat.TWO_DECIMAL);
            //grd.columnSettings.format.Add("PercentComplete", Grid.Grid_Read.columnFormat.PERCENT_NO_TOTAL_TWO_DECIMAL);
            //grd.columnSettings.format.Add("BudgetedTotalCost", Grid.Grid_Read.columnFormat.TWO_DECIMAL);
            //grd.columnSettings.format.Add("BudgetedLabourQty", Grid.Grid_Read.columnFormat.TWO_DECIMAL);
            //grd.columnSettings.format.Add("target_start_date", Grid.Grid_Read.columnFormat.DATE);
            //grd.columnSettings.format.Add("target_end_date", Grid.Grid_Read.columnFormat.DATE);

            //grd.columnSettings.toolTip.Add("target_start_date", "Latest approved baseline start date");
            //grd.columnSettings.toolTip.Add("target_end_date", "Latest approved baseline end date");
            }

            return "";
        }
    }
}
