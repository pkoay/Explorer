using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlClient;
using System.Data.Entity.Core.EntityClient;

namespace WalzExplorer
{
    public partial class WalzExplorerEntities 
    {
        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<tblTender>().Property(p => p.RowVersion).IsConcurrencyToken(true);
        //    modelBuilder.Entity<tblTender_Drawing>().Property(p => p.RowVersion).IsConcurrencyToken(true);
        //}

        
        public override int SaveChanges()
        {
            

            var modifiedEntries = ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified)
                .ToList();

            foreach (var entry in modifiedEntries)
            {
                Type type = entry.Entity.GetType();
                if (type.GetMethod("set_UpdatedBy") != null)
                {
                    PropertyInfo prop = type.GetProperty("UpdatedBy");
                    prop.SetValue(entry.Entity, WindowsIdentity.GetCurrent().Name, null);
                }

                if (type.GetMethod("set_UpdatedDate") != null)
                {
                    PropertyInfo prop = type.GetProperty("UpdatedDate");
                    prop.SetValue(entry.Entity, DateTime.Now, null);
                }
            }

            return base.SaveChanges();
        }
    }


    //public partial class WalzExplorerEntites : ObjectContext
    //{
    //    public WalzExplorerEntites()
    //        : base("name=Tender_PrototypeEntities", "Tender_PrototypeEntities")
    //    {
    //    }

    //    public override int SaveChanges()
    //    {

    //        foreach (ObjectStateEntry entry in
    //            ObjectStateManager.GetObjectStateEntries(
    //            EntityState.Added | EntityState.Modified))
    //        {
    //            if (HasMethod(entry.Entity, "UpdatedBy"))
    //            {
    //                Type type = entry.Entity.GetType();
    //                PropertyInfo prop = type.GetProperty("UpdatedBy");
    //                prop.SetValue(entry.Entity, WindowsIdentity.GetCurrent().Name, null);
    //            }

    //            if (HasMethod(entry.Entity, "UpdatedDate"))
    //            {
    //                Type type = entry.Entity.GetType();
    //                PropertyInfo prop = type.GetProperty("UpdatedDate");
    //                prop.SetValue(entry.Entity, DateTime.Now, null);
    //            }

    //        }
    //        return base.SaveChanges();
    //    }
 

}
