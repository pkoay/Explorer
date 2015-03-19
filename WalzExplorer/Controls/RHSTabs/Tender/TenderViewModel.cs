using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalzExplorer.Controls.Grid;
using WalzExplorer.Database;

namespace WalzExplorer.Controls.RHSTabs.Tender
{
    public class TenderViewModel : GridEditViewModelBase
    {

        public TenderViewModel(WEXSettings settings)
        {
            string NodeType= settings.node.TypeID;
            int PersonID=settings.user.MimicedPerson.PersonID;
            int tenderId=settings.node.IDAsInt();
           
            switch (NodeType)
            {
                case "TendersMyOpen":
                    data = new ObservableCollection<ModelBase>(context.tblTenders.Where(m => m.StatusID == 1 && m.ManagerID==PersonID).OrderBy(m => m.TenderNo));
                    break;
                case "TendersMy": 
                    data = new ObservableCollection<ModelBase>(context.tblTenders.Where(m => m.ManagerID == PersonID).OrderBy(m => m.TenderNo)); 
                    break;
                case "TendersAllOpen": 
                    data = new ObservableCollection<ModelBase>(context.tblTenders.Where(m => m.StatusID == 1).OrderBy(m => m.TenderNo)); 
                    break;
                case "TendersAll": 
                    data = new ObservableCollection<ModelBase>(context.tblTenders.OrderBy(m => m.TenderNo)); 
                    break;
               
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
            return context.tblPersons.OrderBy(x=>x.Name).ToList<object>();
        }
        public List<object> cmbStatusList()
        {
            return context.tblTender_Status.OrderBy(x=>x.Title).ToList<object>();
        }
        
    }
}
