using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Telerik.Windows.Data;
using WalzExplorer.Common;
using WalzExplorer.Controls.Grid;
using WalzExplorer.Database;

namespace WalzExplorer.Controls.RHSTabs.Project
{
    public class CostPivotViewModel 
    {
        public ObservableCollection<spWEX_RHS_Project_Cost_Result> data;
        public WalzExplorerEntities context = new WalzExplorerEntities(false);
        int ProjectID;
        public CostPivotViewModel(WEXSettings settings) 
        {
            ProjectID = ConvertLibrary.StringToInt(settings.node.FindID("PROJECT", "-2"), -1);
            data = new ObservableCollection<spWEX_RHS_Project_Cost_Result>(context.spWEX_RHS_Project_Cost(ProjectID)); 
        }
    }
}
