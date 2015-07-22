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
    public class WorkgroupFuelViewModel :GridEditViewModelBase
    {
        int WorkGroupID;
        //int TenderID;

        public WorkgroupFuelViewModel(WEXSettings settings) //(string NodeType, string PersonID, int Id)
        {
            //TenderID = ConvertLibrary.StringToInt(settings.node.FindID("TENDER", "-2"), -1);
            WorkGroupID = ConvertLibrary.StringToInt(settings.node.FindID("TENDER_WORKGROUP", "-2"), -1);
            data = new ObservableCollection<ModelBase>(context.tblTender_WorkgroupFuel.Where(x => x.WorkGroupID == WorkGroupID).OrderBy(x => x.SortOrder));  
        }

        public override ModelBase DefaultItem(ModelBase NearItem)
        {
            tblTender_WorkgroupFuel i = new tblTender_WorkgroupFuel();
            i.WorkGroupID = WorkGroupID;
            return i;
        }
       
       
      
    }
}
