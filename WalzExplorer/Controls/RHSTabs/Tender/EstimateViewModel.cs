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
    public class EstimateViewModel :GridEditViewModelBase
    {
        int TenderID;
        public EstimateViewModel(WEXSettings settings) //(string NodeType, string PersonID, int Id)
        {
            TenderID = ConvertLibrary.StringToInt(settings.node.FindID("TENDER", "-2"), -1);
            data = new ObservableCollection<ModelBase>(context.tblTender_Estimate.Where(x => x.TenderID == TenderID).OrderBy(x => x.SortOrder));  
        }

        public override ModelBase DefaultItem(ModelBase NearItem)
        {
            tblTender_Estimate i = new tblTender_Estimate();
            i.TenderID = TenderID;
            i.Title = "";
           
            return i;
        }
        public List<object> cmbScheduleList()
        {
            return context.tblTender_Schedule.Where(x => x.TenderID == TenderID).OrderBy(x => x.ClientCode).ToList<object>();
        }
        public List<object> cmbDrawingList()
        {
            return context.tblTender_Drawing.Where(x => x.TenderID == TenderID).OrderBy(x => x.Title).ToList<object>();
        }
        public List<object> cmbObjectList()
        {
            return context.tblTender_Object.Where(x => x.TenderID == TenderID).OrderBy(x => x.Title).ToList<object>();
        }
    }
}
