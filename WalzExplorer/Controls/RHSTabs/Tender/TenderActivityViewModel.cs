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
    public class TenderActivityViewModel : RHSTabGridViewModelBase
    {

        public TenderActivityViewModel(string NodeType, string PersonID, int Id)
        {

            switch (NodeType)
            {
                case "TenderActivityFolder":
                    data = new ObservableCollection<ModelBase>(context.tblTender_Activity.Where(m => m.TenderID == Id).OrderBy(m => m.SortOrder));
                    columnDefault.Clear();
                    columnDefault.Add("TenderID", Id);
                    break;
                case "TenderActivity":
                    data = new ObservableCollection<ModelBase>(context.tblTender_Activity.Where(m => m.ActivityID == Id));
                    break;
                default: throw new NotImplementedException();
            }
                
        }

        public override ModelBase DefaultItem()
        {
            tblTender_Activity i = new tblTender_Activity();
          
            return i;
        }
        public List<object> cmbUnitOfMeasureList()
        {
            return context.tblTender_UnitOfMeasure.ToList<object>();
        }
        //public List<object> cmbStatusList()
        //{
        //    return context.tblTender_Status.ToList<object>();
        //}
        //public void test()
        //{
          
        //}
    }
}
