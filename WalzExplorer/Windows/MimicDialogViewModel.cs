using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalzExplorer.Database;

namespace WalzExplorer.Windows
{
    class MimicDialogViewModel
    {
        WalzExplorerEntities context = new WalzExplorerEntities(false);
        public ObservableCollection<tblPerson> MimicList { get; private set; }

        public MimicDialogViewModel(WEXSettings settings)
        {
            var query =
                from p in context.tblPersons
                join m in context.tblPerson_Mimic on p.PersonID equals m.MimicPersonID into pm
                from m in pm.DefaultIfEmpty()
                where (
                    ((m.PersonID == settings.user.RealPerson.PersonID && m.MimicPersonID != null) //Only those in mimic table 
                    || p.PersonID == settings.user.RealPerson.PersonID))  //always have self (to mimic back to self)
                orderby p.Name
                select p ;
            
            MimicList = new ObservableCollection<tblPerson>(query.ToList());
        }
    }

}
