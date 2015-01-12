using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalzExplorer.Database;

namespace WalzExplorer.Controls.RHSTabs.Tender
{
    public class TenderActivityMaterialViewModel : RHSTabGridViewModelBase
    {

        public TenderActivityMaterialViewModel(WEXSettings settings)
        {
            int ActivityID = settings.node.IDAsInt();
            switch (settings.node.TypeID)
            {
                case "TenderActivity":
                    data = new ObservableCollection<ModelBase>(context.tblTender_ActivityMaterial
                        .Join(context.tblTender_Step, l => l.StepID, s => s.StepID, (l, s) => new { labour = l, step = s }) //join required so we can order by step order
                        .OrderBy(a => a.step.SortOrder) 
                        .Select(a => a.labour)
                        .Where(a => a.ActivityID == ActivityID)
                        );
                        
                    columnDefault.Clear();
                    columnDefault.Add("Activity", ActivityID);
                    break;
                default: throw new NotImplementedException();
            }
                
        }

        public override ModelBase DefaultItem()
        {
            tblTender_ActivityMaterial i = new tblTender_ActivityMaterial();
          
            return i;
        }
     
        public List<object> cmbStepList()
        {
            return context.tblTender_Step.ToList<object>();
        }
        public List<object> cmbMaterialList()
        {
            return context.tblTender_Material.ToList<object>();
        }
        public List<object> cmbSupplierList()
        {
            return context.tblTender_Supplier.ToList<object>();
        }
       
    }
}
