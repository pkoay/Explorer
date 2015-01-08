using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Telerik.Windows.Controls;
using WalzExplorer.Common;
using WalzExplorer.Database;

namespace WalzExplorer.Controls.RHSTabs
{
    abstract public class RHSTabGridViewModelBase : ViewModelBase
        
    {
        

        public ObservableCollection<ModelBase> data;
        public WalzExplorerEntities context;
        protected Dictionary<string, object> columnDefault = new Dictionary<string, object>();
        

        public RHSTabGridViewModelBase()
        {
            this.context = new WalzExplorerEntities();
        }

        public virtual ModelBase DefaultItem()
        {
            return null;
        }
       
        public ModelBase InsertNew()
        {
            ModelBase i = DefaultItem();
            data.Insert(0, i );
            return i;
            
        }
        public ModelBase InsertNew(ModelBase InsertAbove)
        {
            ModelBase i = DefaultItem();
            data.Insert( this.data.IndexOf(InsertAbove), i);
            return i;
        }
      
      
       public void MoveItemsToIndex( List<ModelBase> items,int index)
       {
           
           if (this.data.IndexOf(items[0])>index) items.Reverse();
           foreach(ModelBase i in items)
           {
               this.data.Move(this.data.IndexOf(i), index);
           }
           SaveAndUpdateSortOrder();
       }

       public void MoveItemsToItem( List<ModelBase> items,ModelBase MoveAbove)
        {
            MoveItemsToIndex(items, this.data.IndexOf(MoveAbove));
        }

        public void Delete(IEnumerable<object> items)
        {
            foreach (ModelBase item in items)
            {
                context.Entry(item).State = System.Data.Entity.EntityState.Deleted;
            }
            SaveAndUpdateSortOrder();
        }

        public void SavePaste(List<ModelBase> items)
        {
            foreach (ModelBase item in items)
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

        //sets the defaults for a ModelBases (defaults as specified in dictionary columnDefault)
        public void SetDefaultsForPaste(object o)
        {

            foreach (KeyValuePair<string, object> def in columnDefault.ToList())
            {
                string DefaultKey = def.Key.ToString();
                object DefaultValue = def.Value;
               
                    //check to see if the property value in the ModelBase is the same as the property value  in a new instance
                    object ni = ObjectLibrary.CreateNewInstanace(o);

                    //this is dodgy..
                    if (ObjectLibrary.GetValue(o, DefaultKey).ToString() == ObjectLibrary.GetValue(ni, DefaultKey).ToString())
                        ObjectLibrary.SetValue(o, DefaultKey, DefaultValue);    //change to default value
               
            }
        }
       

        private void SaveAndUpdateSortOrder()
        {
            int i = 0;
            foreach (ModelBase item in this.data)
            {
                ObjectLibrary.SetValue(item, "SortOrder", i);
                i++;
            }   
            context.SaveChangesWithValidation();
        }

        public void ManualChange(ModelBase changedItem)
        {

            // If item not in database 
            if (context.Entry(changedItem).State == System.Data.Entity.EntityState.Detached)
            {
                //Add item to dataabse 
                context.Entry(changedItem).State = System.Data.Entity.EntityState.Added;
            }
            SaveAndUpdateSortOrder();
            
            
        }
        public bool HasErrors()
        {
            foreach (ModelBase item in this.data)
            {
                if (item.HasError) return true;
            }
            return false;
        }
      

    }
}
