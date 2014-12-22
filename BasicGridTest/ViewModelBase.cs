using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicGridTest
{
    abstract public class ViewModelBase<T>
        where T: class
    {
        public ObservableCollection<T> main;
        public  BASICGRIDDATAEntities context;

        public ViewModelBase()
        {
            this.context = new BASICGRIDDATAEntities();
        }


        public T Insert(T NewItem, T BeforeItem)
        {
            int index = this.main.IndexOf(BeforeItem);
            return Insert(NewItem, index);
        }

        public T Insert(T NewItem, int index)
        {
            //NewItem.SortOrder = this.SortOrderNumber(this.main[index]);
            this.main.Insert(index, NewItem);
            context.SaveChanges();

            return this.main[index];
        }

    }

}
