using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalzExplorer.Database;

namespace WalzExplorer.Controls.RHSTabs
{
   abstract public class RHSTabGridViewModelBase<T>
        where T: class
    {
        public ObservableCollection<T> data;
        public WalzExplorerEntities context;
        

        public RHSTabGridViewModelBase()
        {
            this.context = new WalzExplorerEntities();
        }


        public T Insert(T NewItem, T BeforeItem)
        {
            int index = this.data.IndexOf(BeforeItem);
            return Insert(NewItem, index);
        }

        public T Insert(T NewItem, int index)
        {
            this.data.Insert(index, NewItem);
            context.SaveChanges();

            return this.data[index];
        }

    }
}
