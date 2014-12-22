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
    public class MyViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<tblMain> main;
        public readonly BASICGRIDDATAEntities context;

        public MyViewModel()
        {
            this.context = new BASICGRIDDATAEntities();
            this.main = new ObservableCollection<tblMain>(context.tblMains.OrderBy(m => m.SortOrder));
            
        }
        public tblMain Move(tblMain MoveItem, tblMain BeforeItem)
        {
            MoveItem.SortOrder = this.SortOrderNumber(MoveItem);

            int oldIndex = this.main.IndexOf(MoveItem);
            int newIndex = this.main.IndexOf(BeforeItem);
            this.main.Move(oldIndex, newIndex);

            return MoveItem;
        }
        public tblMain Insert(tblMain NewItem, tblMain BeforeItem)
        {
            int index=this.main.IndexOf(BeforeItem);
            this.main.Insert(index, NewItem);
            
            return this.main[index];
        }

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
