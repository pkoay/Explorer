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
    public class CostBudgetVsActualViewModel 
    {
        public ObservableCollection<spWEX_RHS_Project_CostBudgetVsActual_Result> data;
        public WalzExplorerEntities context = new WalzExplorerEntities(false);
        int ProjectID;
        

        public CostBudgetVsActualViewModel (WEXSettings settings) //(string NodeType, string PersonID, int Id)
        {
            ProjectID = ConvertLibrary.StringToInt(settings.node.FindID("PROJECT", "-2"), -1);
            //CustomerID = ConvertLibrary.StringToInt(settings.node.FindID("CUSTOMER", "-2"), -1).ToString();
            //NodeTypeID = settings.node.TypeID;
            //UserPersonID=settings.user.MimicedPerson.PersonID.ToString();

            data = new ObservableCollection<spWEX_RHS_Project_CostBudgetVsActual_Result>(context.spWEX_RHS_Project_CostBudgetVsActual(ProjectID));    
        }
    }
}
