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
    public class ObjectMaterialViewModel : GridEditViewModelBase
    {
        int TenderID;
        int ObjectID;
        public ObjectMaterialViewModel(WEXSettings settings) //(string NodeType, string PersonID, int Id)
        {
            TenderID = ConvertLibrary.StringToInt(settings.node.FindID("TENDER", "-2"), -1);
            ObjectID = ConvertLibrary.StringToInt(settings.node.FindID("OBJECT", "-2"), -1);
            switch(settings.node.TypeID)
            {
                case "TenderEstimate":
                    data = new ObservableCollection<ModelBase>(context.tblTender_ObjectMaterial.Where(x => x.tblTender_Object.TenderID == TenderID).OrderBy(x => x.SortOrder));
                    break;
                case "TenderObject":
                    data = new ObservableCollection<ModelBase>(context.tblTender_ObjectMaterial.Where(x => x.ObjectID == ObjectID).OrderBy(x => x.SortOrder));
                    break;
            }

            
        }

        public override ModelBase DefaultItem(ModelBase NearItem)
        {
            tblTender_ObjectMaterial i = new tblTender_ObjectMaterial();
            i.ObjectID = ObjectID;
            return i;
        }
        public List<object> cmbMaterialList()
        {
            return context.tblTender_Material.Where(x => x.TenderID == TenderID).OrderBy(x => x.Title).ToList<object>();
        }
        public List<object> cmbStepList()
        {
            return context.tblTender_Step.Where(x => x.TenderID == TenderID).OrderBy(x => x.Title).ToList<object>();
        }
        public List<object> cmbSupplierList()
        {
            return context.tblTender_Supplier.Where(x => x.TenderID == TenderID).OrderBy(x => x.Title).ToList<object>();
        }
        public List<object> cmbObjectList()
        {
            return context.tblTender_Object.Where(x => x.TenderID == TenderID).OrderBy(x => x.Title).ToList<object>();
        }
       

    }
}
