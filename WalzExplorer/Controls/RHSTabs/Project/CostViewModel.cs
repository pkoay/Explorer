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
    public class CostViewModel 
    {
        public ObservableCollection<spWEX_RHS_Project_Cost_Result> data;
        public WalzExplorerEntities context = new WalzExplorerEntities(false);
        int? ProjectID = null;
        //string CustomerID;
        //string NodeTypeID;
        //string UserPersonID;
       
        public CostViewModel (WEXSettings settings) //(string NodeType, string PersonID, int Id)
        {
            context.Database.CommandTimeout = 180;
            if (settings.drilldown == null)
            {
                ProjectID = ConvertLibrary.StringToInt(settings.node.FindID("PROJECT", "-2"), -1);
                
            }
            else
            {
                switch (settings.drilldown._filter)
                {
                    case WalzExplorer.Model.DrilldownFilter.Project:
                        ProjectID = ConvertLibrary.StringToInt(settings.drilldown._parameters["ProjectID"], -2);
                        break;
                    default:
                        break;
                }
            }
            data = new ObservableCollection<spWEX_RHS_Project_Cost_Result>(context.spWEX_RHS_Project_Cost(ProjectID));
        }
    }
}
