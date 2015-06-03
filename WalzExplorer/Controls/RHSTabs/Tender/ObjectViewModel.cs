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
    public class ObjectViewModel : GridEditViewModelBase
    {
        int TenderID;
        int ObjectID;
        public ObjectViewModel(WEXSettings settings) //(string NodeType, string PersonID, int Id)
        {
            switch (settings.node.TypeID)
            {
                case "TenderObjectFolder":
                    TenderID = ConvertLibrary.StringToInt(settings.node.FindID("TENDER", "-2"), -1);
                    data = new ObservableCollection<ModelBase>(context.tblTender_Object.Where(x => x.TenderID == TenderID).OrderBy(x => x.SortOrder));
                    break;
                case "TenderObject":
                    TenderID = ConvertLibrary.StringToInt(settings.node.FindID("TENDER", "-2"), -1);
                    ObjectID = ConvertLibrary.StringToInt(settings.node.FindID("OBJECT", "-2"), -1);
                    data = new ObservableCollection<ModelBase>(context.tblTender_Object.Where(x => x.ObjectID == ObjectID).OrderBy(x => x.SortOrder));
                    break;
            }
        }

        public override ModelBase DefaultItem()
        {
            tblTender_Object i = new tblTender_Object();
            i.TenderID = TenderID;
            return i;
        }
        public List<object> cmbUnitOfMeasureList()
        {
            return context.tblTender_UnitOfMeasure.Where(x => x.TenderID == TenderID).OrderBy(x => x.SortOrder).ToList<object>();
        }
        public List<object> cmbObjectTypeList()
        {
            return context.tblTender_ObjectType.Where(x => x.TenderID == TenderID).OrderBy(x => x.SortOrder).ToList<object>();
        }

    }
}
