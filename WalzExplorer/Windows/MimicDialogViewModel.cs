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
        WalzExplorerEntities context = new WalzExplorerEntities();
        public ObservableCollection<tblPerson> MimicList { get; private set; }

        public MimicDialogViewModel()
        {
            var query =
                from p in context.tblPersons
                join m in context.tblPerson_Mimic on p.PersonID equals m.MimicPersonID into pm
                from m in pm.DefaultIfEmpty()
                where (m.MimicPersonID != null || p.PersonID == 1470) 
                select p ;

            MimicList = new ObservableCollection<tblPerson>(query.ToList());
        }
    }

}
