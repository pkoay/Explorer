using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WalzExplorer.Common;
using WalzExplorer.Database;

namespace WalzExplorer.Controls.RHSTabs
{
   abstract public class RHSTabGridViewModelBase
        
    {
        public ObservableCollection<object> data;
        public WalzExplorerEntities context;
        protected Dictionary<string, object> columnDefault = new Dictionary<string, object>();
        

        public RHSTabGridViewModelBase()
        {
            this.context = new WalzExplorerEntities();
        }

        public virtual object DefaultItem()
        {
            return null;
        }
       
        public object InsertNew()
        {
            object i = DefaultItem();
            data.Insert(0, i );
            SaveAndUpdateSortOrder();
            return i;
            
        }
        public object InsertNew(object InsertAbove)
        {
            object i = DefaultItem();
            data.Insert( this.data.IndexOf(InsertAbove), i);
            SaveAndUpdateSortOrder();
            return i;
        }
        public object InsertNewBelow(object InsertBelow)
        {
            object i = DefaultItem();

            if (this.data.IndexOf(InsertBelow) < this.data.Count - 1)
            {
                //not last entry
                data.Insert(this.data.IndexOf(InsertBelow) + 1, i);
            }
            else
            {
                //Last entry add (not insert)
                data.Add(i);
            }
            SaveAndUpdateSortOrder();
            return i;
        }
        public void Move(object MoveAbove, List<object> items)
        {
            items.Reverse();
           foreach(object i in items)
           {
               this.data.Move(this.data.IndexOf(i), this.data.IndexOf(MoveAbove));
           }
           SaveAndUpdateSortOrder();
        }

        public void Delete(List<object> items)
        {
            foreach (object item in items)
            {
                context.Entry(item).State = System.Data.Entity.EntityState.Deleted;
            }
            SaveAndUpdateSortOrder();
        }

        public void SavePaste(List<object> items)
        {
            foreach (object item in items)
            {
                // If item not in database 
                if (context.Entry(item).State == System.Data.Entity.EntityState.Detached)
                {
                    //Add item to dataabse 
                    context.Entry(item).State = System.Data.Entity.EntityState.Added;
                }
            }
            SaveAndUpdateSortOrder();

        }

        //sets the defaults for an objects (defaults as specified in dictionary columnDefault
        public void SetDefaultsForPaste(object o)
        {

            foreach (KeyValuePair<string, object> def in columnDefault.ToList())
            {
                string DefaultKey = def.Key.ToString();
                object DefaultValue = def.Value;
               
                    //check to see if the property value in the object is the same as the property value  in a new instance
                    object ni = ObjectLibrary.CreateNewInstanace(o);

                    //this is dodgy..
                    if (ObjectLibrary.GetValue(o, DefaultKey).ToString() == ObjectLibrary.GetValue(ni, DefaultKey).ToString())
                        ObjectLibrary.SetValue(o, DefaultKey, DefaultValue);    //change to default value
               
            }
        }
       

        private void SaveAndUpdateSortOrder()
        {
            int i = 0;
            foreach (object item in this.data)
            {
                ObjectLibrary.SetValue(item, "SortOrder", i);
                i++;
            }   
            context.SaveChanges();
        }

        public void ManualChange(object changedItem)
        {

            // If item not in database 
            if (context.Entry(changedItem).State == System.Data.Entity.EntityState.Detached)
            {
                //Add item to dataabse 
                context.Entry(changedItem).State = System.Data.Entity.EntityState.Added;
            }
            SaveAndUpdateSortOrder();
            
            
        }

    }
}
