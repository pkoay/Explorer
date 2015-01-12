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
    public class TenderActivityLabourViewModel : RHSTabGridViewModelBase
    {

        public TenderActivityLabourViewModel(string NodeType, string PersonID, int Id)
        {
            switch (NodeType)
            {
                case "TenderActivity":
                    data = new ObservableCollection<ModelBase>(context.tblTender_ActivityLabour
                        .Join(context.tblTender_Step, l => l.StepID, s => s.StepID, (l, s) => new { labour = l, step = s }) //join required so we can order by step order
                        .OrderBy(a => a.step.SortOrder) 
                        .Select(a => a.labour)
                        .Where(a => a.ActivityID == Id)
                        );
                        
                    columnDefault.Clear();
                    columnDefault.Add("Activity", Id);
                    break;
                default: throw new NotImplementedException();
            }
                
        }

        public override ModelBase DefaultItem()
        {
            tblTender_ActivityLabour i = new tblTender_ActivityLabour();
          
            return i;
        }
        public List<object> cmbLabourStandardList()
        {
            return context.tblTender_LabourStandard.ToList<object>();
        }
        public List<object> cmbStepList()
        {
            return context.tblTender_Step.ToList<object>();
        }
        public List<object> cmbWorkGroupList()
        {
            return context.tblTender_WorkGroup.ToList<object>();
        }
        public List<object> cmbUnitOfMeasureList()
        {
            return context.tblTender_UnitOfMeasure.ToList<object>();
        }
       
    }
}
