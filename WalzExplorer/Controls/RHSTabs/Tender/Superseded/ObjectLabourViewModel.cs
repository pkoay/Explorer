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
    public class ObjectLabourViewModel : GridEditViewModelBase
    {
        int TenderID;
        int ObjectID;
        public ObjectLabourViewModel(WEXSettings settings) //(string NodeType, string PersonID, int Id)
        {
            TenderID = ConvertLibrary.StringToInt(settings.node.FindID("TENDER", "-2"), -1);
            ObjectID = ConvertLibrary.StringToInt(settings.node.FindID("OBJECT", "-2"), -1);
            data = new ObservableCollection<ModelBase>(context.tblTender_ObjectLabour.Where(x => x.ObjectID == ObjectID).OrderBy(x => x.SortOrder));
        }

        public override ModelBase DefaultItem(ModelBase NearItem)
        {
            tblTender_ObjectLabour i = new tblTender_ObjectLabour();
            i.ObjectID = ObjectID;
            return i;
        }
        public List<object> cmbUnitOfMeasureList()
        {
            return context.tblTender_UnitOfMeasure.Where(x => x.TenderID == TenderID).OrderBy(x => x.SortOrder).ToList<object>();
        }
        public List<object> cmbStepList()
        {
            return context.tblTender_Step.Where(x => x.TenderID == TenderID).OrderBy(x => x.SortOrder).ToList<object>();
        }
        public List<object> cmbWorkGroupList()
        {
            return context.tblTender_WorkGroup.Where(x => x.TenderID == TenderID).OrderBy(x => x.SortOrder).ToList<object>();
        }
        public List<object> cmbLabourRateList()
        {
            return context.tblTender_LabourRate.Where(x => x.TenderID == TenderID).OrderBy(x => x.SortOrder).ToList<object>();
        }

    }
}
