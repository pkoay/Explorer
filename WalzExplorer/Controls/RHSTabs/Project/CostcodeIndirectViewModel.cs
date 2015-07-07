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
    public class CostcodeIndirectViewModel : GridEditViewModelBase
    {
        int ProjectID;
        public CostcodeIndirectViewModel(WEXSettings settings) 
        {
            ProjectID = ConvertLibrary.StringToInt(settings.node.FindID("PROJECT", "-2"), -1);
            data = new ObservableCollection<ModelBase>(context.vwProject_CostCodeIndirectView.Where(x => x.TopParentID == ProjectID).OrderBy(x => x.AXProjectID));  
        }

        public override ModelBase DefaultItem(ModelBase NearItem)
        {
            // should not be able to add
            throw new NotImplementedException();
        }
    }
}
