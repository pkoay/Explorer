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

namespace WalzExplorer.Controls.Grid
{
    abstract public class GridEditViewModelBase : ViewModelBase
    // important to consider when writing new routines that they are an attomic commit. i.e. all changes commit or don't commit
    // note commit is completed on 
    {
        

        public ObservableCollection<ModelBase> data;
        public WalzExplorerEntities context;

        private bool _canOrder;
        //protected Dictionary<string, object> columnDefault = new Dictionary<string, object>();


        public GridEditViewModelBase()
        {
            this.context = new WalzExplorerEntities(false);
        }

        public void CanOrder( bool canOrder)
        {
            _canOrder = canOrder;
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


        public EfStatus MoveItemsToIndex(List<ModelBase> items, int index)
       {
           
           if (this.data.IndexOf(items[0])>index) items.Reverse();
           foreach(ModelBase i in items)
           {
               this.data.Move(this.data.IndexOf(i), index);
           }
           return SaveWithValidationAndUpdateSortOrder();
       }

        public EfStatus MoveItemsToItem(List<ModelBase> items, ModelBase MoveAbove)
        {
             return MoveItemsToIndex(items, this.data.IndexOf(MoveAbove));
        }
       

       public EfStatus Delete(IEnumerable<object> items)
        {
            foreach (ModelBase item in items)
            {
                context.Entry(item).State = System.Data.Entity.EntityState.Deleted;
            }
            return SaveWithValidationAndUpdateSortOrder();
        }

       public void SavePaste()
       {
            SaveWithValidationAndUpdateSortOrder();

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
            //return SaveWithValidationAndUpdateSortOrder();

        }

        //sets the defaults for a ModelBases (defaults as specified in dictionary columnDefault)
        //public void SetDefaultsForPaste(object o)
        //{
        //    ModelBase i = DefaultItem();

        //    foreach (KeyValuePair<string, object> def in columnDefault.ToList())
        //    {
        //        string DefaultKey = def.Key.ToString();
        //        object DefaultValue = def.Value;
               
        //            //check to see if the property value in the ModelBase is the same as the property value  in a new instance
        //            object ni = ObjectLibrary.CreateNewInstanace(o);

        //            //this is dodgy..
        //            if (ObjectLibrary.GetValue(o, DefaultKey).ToString() == ObjectLibrary.GetValue(ni, DefaultKey).ToString())
        //                ObjectLibrary.SetValue(o, DefaultKey, DefaultValue);    //change to default value
               
        //    }
        //}
        //sets the defaults for a ModelBases (defaults as specified in dictionary columnDefault)
        public void SetDefaultsForPaste(object currentitem)
        {
            //default item is with defaults set
            object defaultItem = DefaultItem();
            Type defaultItemType = defaultItem.GetType();

            //create new object (no defaults set)
            object basicItem = Activator.CreateInstance(defaultItemType);

            //Compare basic and Default item to see if there are any changes (i.e. Defaults set)
            //IF so apply them to the current item
            foreach (PropertyInfo propertyInfo in defaultItemType.GetProperties())
            {
                if (propertyInfo.CanWrite)
                {
                    object defaultValue = propertyInfo.GetValue(defaultItem, null);
                    object basicValue = propertyInfo.GetValue(basicItem, null);
                    if (!object.Equals(defaultValue, basicValue))
                    {
                        propertyInfo.SetValue(currentitem, defaultValue);
                    }
                }
            }

        }

        private EfStatus SaveWithValidationAndUpdateSortOrder()
        {
            if (_canOrder)
            {
                // update sort order
                int i = 0;
                foreach (ModelBase item in this.data)
                {
                    ObjectLibrary.SetValue(item, "SortOrder", i);
                    i++;
                }
            }
            return context.SaveChangesWithValidation();
            
        }

        public EfStatus ManualChange(ModelBase changedItem)
        {

            // If item not in database 
            if (changedItem!=null && context.Entry(changedItem).State == System.Data.Entity.EntityState.Detached)
            {
                //Add item to dataabse 
                context.Entry(changedItem).State = System.Data.Entity.EntityState.Added;
            }
            return SaveWithValidationAndUpdateSortOrder();
            
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
