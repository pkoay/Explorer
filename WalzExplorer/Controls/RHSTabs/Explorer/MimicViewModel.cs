using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalzExplorer.Common;
using WalzExplorer.Controls.Grid;
using WalzExplorer.Database;

namespace WalzExplorer.Controls.RHSTabs.Explorer
{
    public class MimicViewModel :GridEditViewModelBase
    {
        public MimicViewModel(WEXSettings settings) //(string NodeType, string PersonID, int Id)
        {
            data = new ObservableCollection<ModelBase>(context.tblPerson_Mimic);  
        }

        public override ModelBase DefaultItem()
        {
           tblPerson_Mimic i = new tblPerson_Mimic();
            return i;
        }

        public List<object> cmbUserList()
        {
            return context.tblPersons.Where(x=>x.Login!=null).OrderBy(x => x.Name).ToList<object>();
        }
    }
}
