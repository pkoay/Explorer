using BasicGridTest;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicGridTest
{
    public class MyViewModel : ViewModelBase<tblMain>,INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        //private ObservableCollection<tblMain> main;
        //public readonly BASICGRIDDATAEntities context;

        public MyViewModel()
        {
            this.context = new BASICGRIDDATAEntities();
            this.main = new ObservableCollection<tblMain>(context.tblMains.OrderBy(m => m.SortOrder));
            
        }
       
        //public tblMain Insert(tblMain NewItem, tblMain BeforeItem)
        //{
        //    int index=this.main.IndexOf(BeforeItem);
        //    return Insert( NewItem,  index);
        //}

        //public  tblMain Insert(tblMain NewItem, int index)
        //{
        //    NewItem.SortOrder = this.SortOrderNumber(this.main[index]);
        //    this.main.Insert(index, NewItem);
        //    context.SaveChanges();
           
        //    return this.main[index];
        //}

        public tblMain Move(tblMain Item, int newIndex)
        {
            Item.SortOrder = this.SortOrderNumber(this.main[newIndex]);
            this.main.Move(this.main.IndexOf(Item), newIndex);
            context.SaveChanges();
            return this.main[newIndex];
        }

        private int returnIndex(object o)
        {
            int i=this.main.IndexOf((tblMain)o);
            return i;
        }
        public tblMain Move(ObservableCollection<object> Items, int newIndex)
        {
            List<object>items = Items.ToList();
            //Remeber object to move to, as removed stuff up indexes
            object MoveToItem = this.main[newIndex];
       
            //Change order to that of observable collection, not the oredr selected
            var itemSorted = items.OrderBy(s => this.main.IndexOf((tblMain)s));

            //Deletes then adds otherwise order get stuffed up
            foreach (tblMain Item in itemSorted)
            {
                this.main.Remove(Item);
            }

            //remeber Newindex, as this has changed because of removes
            newIndex = this.main.IndexOf((tblMain)MoveToItem);
            foreach (tblMain Item in itemSorted.Reverse())
            {    
            Item.SortOrder = this.SortOrderNumber(this.main[newIndex]);
            this.main.Insert(newIndex,Item);
            }
            context.SaveChanges();
            return this.main[newIndex];
        }
        //public void Remove<T> (ObservableCollection<object> items)
        //{
        //    foreach (object i in items)
        //        this.main.Remove(i);
        //}

        public double SortOrderNumber(tblMain InsertLocation)
        {
            //calculate the sortorder number
            int index = this.main.IndexOf(InsertLocation);
            if (index == 0)
                // if first in list then halfway between given sort order number and zero
                return InsertLocation.SortOrder / 2;
            else
            {
                return (InsertLocation.SortOrder - this.main[index - 1].SortOrder) / 2 + this.main[index - 1].SortOrder;
            }
        }

        public ObservableCollection<tblMain> Main 
        {
            get 
            {
                return this.main; 
            }
        }

        

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, args);
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            this.OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

    }
}
