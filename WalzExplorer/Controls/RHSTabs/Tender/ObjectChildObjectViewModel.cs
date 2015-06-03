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
    public class ObjectChildObjectViewModel :GridEditViewModelBase
    {
        int TenderID;
        int ObjectID;
        public ObjectChildObjectViewModel(WEXSettings settings) //(string NodeType, string PersonID, int Id)
        {
            TenderID = ConvertLibrary.StringToInt(settings.node.FindID("TENDER", "-2"), -1);
            ObjectID = ConvertLibrary.StringToInt(settings.node.FindID("OBJECT", "-2"), -1);
            data = new ObservableCollection<ModelBase>(context.tblTender_ObjectChildObject.Where(x => x.ObjectID == ObjectID).OrderBy(x => x.SortOrder));  
        }

        public override ModelBase DefaultItem()
        {
            tblTender_ObjectChildObject i = new tblTender_ObjectChildObject();
            i.ObjectID = ObjectID;
            return i;
        }

        public List<object> cmbChildObjectList()
        {
            return context.tblTender_Object.Where(x => x.TenderID == TenderID && x.ObjectID != ObjectID).OrderBy(x => x.SortOrder).ToList<object>();
        }
        public List<object> cmbStepList()
        {
            return context.tblTender_Step.Where(x => x.TenderID == TenderID).OrderBy(x => x.SortOrder).ToList<object>();
        }
    }
}
