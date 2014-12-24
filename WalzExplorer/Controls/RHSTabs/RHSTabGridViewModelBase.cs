﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalzExplorer.Database;

namespace WalzExplorer.Controls.RHSTabs
{
   abstract public class RHSTabGridViewModelBase
        
    {
        public ObservableCollection<object> data;
        public WalzExplorerEntities context;
        

        public RHSTabGridViewModelBase()
        {
            this.context = new WalzExplorerEntities();
        }

        public virtual object DefaultItem()
        {
            return null;
        }
        public object AddDefault()
        {
            object i = DefaultItem();
            data.Insert(0, i );
            return i;
        }
        //public object Insert(T NewItem, T BeforeItem)
        //{
        //    int index = this.data.IndexOf(BeforeItem);
        //    return Insert(NewItem, index);
        //}

        //public T Insert(T NewItem, int index)
        //{
        //    this.data.Insert(index, NewItem);
        //    context.SaveChanges();

        //    return this.data[index];
        //}

        public void ManualChange(object changedItem)
        {

            // If item not in database 
            if (context.Entry(changedItem).State == System.Data.Entity.EntityState.Detached)
            {
                //Add item to dataabse 
                context.Entry(changedItem).State = System.Data.Entity.EntityState.Added;
            }
          
            context.SaveChanges();

            //also update view model with database generated data e.g. Identity auto increment, Modified by, Modified date, etc.
            // if (context.Entry(m).State == System.Data.Entity.EntityState.Added)
            context.Entry(changedItem).Reload();
            
        }

    }
}