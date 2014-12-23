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
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity.Core.Metadata.Edm;
using System.ComponentModel.DataAnnotations;
namespace BasicGridTest
{
    public partial class BASICGRIDDATAEntities : DbContext
    {
        

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

}
