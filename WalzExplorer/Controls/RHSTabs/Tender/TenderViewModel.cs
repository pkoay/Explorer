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
    public class TenderViewModel : RHSTabGridViewModelBase
    {



        public TenderViewModel(string NodeType, int PersonID, int tenderId)
        {
           
            switch (NodeType)
            {
                case "TendersMyOpen": data = new ObservableCollection<ModelBase>(context.tblTenders.Where(m => m.StatusID == 1 && m.ManagerID==PersonID).OrderBy(m => m.TenderNo)); break;
                case "TendersMy": 
                    data = new ObservableCollection<ModelBase>(context.tblTenders.Where(m => m.ManagerID == PersonID).OrderBy(m => m.TenderNo)); 
                    
                    break;
                case "TendersAllOpen": data = new ObservableCollection<ModelBase>(context.tblTenders.Where(m => m.StatusID == 1).OrderBy(m => m.TenderNo)); break;
                case "TendersAll": data = new ObservableCollection<ModelBase>(context.tblTenders.OrderBy(m => m.TenderNo)); break;
               
                default: throw new NotImplementedException();
            }
           
            columnDefault.Clear();
            columnDefault.Add("TenderID", tenderId);
            
        }

        public override ModelBase DefaultItem()
        {
            tblTender i= new tblTender();
          
            return i;
        }
        public List<object> cmbManagerList()
        {
            return context.tblPersons.ToList<object>();
        }
        public List<object> cmbStatusList()
        {
            return context.tblTender_Status.ToList<object>();
        }
        public void test()
        {
          
        }
    }
}
