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

namespace WalzExplorer.Controls.RHSTabs.Tender
{
    public class WorkgroupAdditionalHoursViewModel :GridEditViewModelBase
    {
        int WorkgroupID;
        //int TenderID;

        public WorkgroupAdditionalHoursViewModel(WEXSettings settings) //(string NodeType, string PersonID, int Id)
        {
            //TenderID = ConvertLibrary.StringToInt(settings.node.FindID("TENDER", "-2"), -1);
            WorkgroupID = ConvertLibrary.StringToInt(settings.node.FindID("TENDER_WORKGROUP", "-2"), -1);
            data = new ObservableCollection<ModelBase>(context.tblTender_WorkgroupAdditionalHours.Where(x => x.WorkgroupID == WorkgroupID).OrderBy(x => x.SortOrder));  
        }

        public override ModelBase DefaultItem(ModelBase NearItem)
        {
            tblTender_WorkgroupAdditionalHours i = new tblTender_WorkgroupAdditionalHours();
            i.WorkgroupID = WorkgroupID;
            return i;
        }
       
       
      
    }
}
