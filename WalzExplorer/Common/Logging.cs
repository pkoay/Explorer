using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using WalzExplorer.Database;

namespace WalzExplorer.Common
{
    public static class Logging
    {
        public static void LogEvent(string operation)
        {
            using (ServicesEntities db = new ServicesEntities(false))
            {
                db.spLogEvent("Explorer", operation, WindowsIdentity.GetCurrent().Name);
            }
        }

        //Note issue that on an ADD it does not save the new Identity values these are not know until after save. 
        public static  void LogChanges(List<DbEntityEntry> changes,ObjectContext oc)
        {
            string LogSql = "";
            foreach (var entry in changes)
            {
                LogSql = LogSql + Environment.NewLine +GetAuditRecordsForChange(entry, oc);
            }

            if (LogSql != "")
            {
                using (ServicesEntities db = new ServicesEntities(false))
                {
                    db.Database.ExecuteSqlCommand(LogSql);
                }
            }
         }


        private static string  GetAuditRecordsForChange(DbEntityEntry dbEntry, ObjectContext oc)
        {
            List<string> PropertiesToSkip = new List<string> () {"RowVersion", "UpdatedDate", "UpdatedBy" };

            // Get table name (if it has a Table attribute, use that, otherwise get the pluralized name)
            //string tableName = dbEntry.Entity.GetType().Name; not useful due to proxies add guid at end
            string tableName = GetTableName(dbEntry, oc);
            // Determine ID string for change (potentially multiple keys
            EntityKey entityKey = oc.ObjectStateManager.GetObjectStateEntry(dbEntry.Entity).EntityKey;
            string keyName = "";
            string sql = "";
            if (entityKey.EntityKeyValues == null)
            {
                keyName = "NEW IDENTITY";
            }
            else
            {
                foreach (EntityKeyMember key in entityKey.EntityKeyValues)
                {
                    keyName = key.Key + "=" + key.Value.ToString() + ",";
                }
                if (keyName != "") keyName = keyName.TrimEnd(',');
            }

          
                
            switch (dbEntry.State)
            {
                case EntityState.Added:
                    foreach (string propertyName in dbEntry.CurrentValues.PropertyNames)
                    {
                        if (!PropertiesToSkip.Contains(propertyName))
                        {
                            string AddedValue = dbEntry.CurrentValues.GetValue<object>(propertyName) == null ? null : dbEntry.CurrentValues.GetValue<object>(propertyName).ToString();
                            sql = SqlLogString("Explorer", tableName, propertyName, WindowsIdentity.GetCurrent().Name, keyName, dbEntry.State.ToString(), "<new>", AddedValue);

                        }
                    }
                    break;
                case EntityState.Deleted:
                    foreach (string propertyName in dbEntry.OriginalValues.PropertyNames)
                    {
                        if (!PropertiesToSkip.Contains(propertyName))
                        {
                            string OldValue = dbEntry.OriginalValues.GetValue<object>(propertyName) == null ? null : dbEntry.OriginalValues.GetValue<object>(propertyName).ToString();
                            sql = SqlLogString("Explorer", tableName, propertyName, WindowsIdentity.GetCurrent().Name, keyName, dbEntry.State.ToString(), OldValue, "<deleted>");
                        }
                    }
                    break;
                case EntityState.Modified:
                    foreach (string propertyName in dbEntry.OriginalValues.PropertyNames)
                    {
                        if (!PropertiesToSkip.Contains(propertyName))
                        {
                            if (!object.Equals(dbEntry.OriginalValues.GetValue<object>(propertyName), dbEntry.CurrentValues.GetValue<object>(propertyName)))
                            {
                                string NewValue = dbEntry.CurrentValues.GetValue<object>(propertyName) == null ? null : dbEntry.CurrentValues.GetValue<object>(propertyName).ToString();
                                string OldValue = dbEntry.OriginalValues.GetValue<object>(propertyName) == null ? null : dbEntry.OriginalValues.GetValue<object>(propertyName).ToString();

                                sql = SqlLogString("Explorer", tableName, propertyName, WindowsIdentity.GetCurrent().Name, keyName, dbEntry.State.ToString(), OldValue, NewValue);
                            }
                        }

                    }
                    break;
            
                    
            }
            return sql;
        }

        private static  string SqlLogString(string database,string table, string column, string user,string key, string operation,string oldvalue,string newvalue)
        {
            //database,table,column,user,key,operation,oldvalue,newvalue
            if (database != null) database = database.Replace("'", "''");
            if (table != null) table = table.Replace("'", "''");
            if (column != null) column = column.Replace("'", "''");
            if (user != null) user = user.Replace("'", "''");
            if (key != null) key = key.Replace("'", "''");
            if (operation != null) operation = operation.Replace("'", "''");
            if (oldvalue != null) oldvalue = oldvalue.Replace("'", "''");
            if (newvalue != null) newvalue = newvalue.Replace("'", "''");

            
            return String.Format("EXECUTE [dbo].[spLogChangev2] @Database='{0}',@Table='{1}',@Column='{2}',@User='{3}',@Row='{4}',@Operation='{5}',@OldValue='{6}',@NewValue='{7}'",database,table,column,user,key,operation,oldvalue,newvalue);
                
            

        }

        private static string GetTableName(DbEntityEntry ent,ObjectContext oc)
        {
           // ObjectContext objectContext = ((IObjectContextAdapter)this).ObjectContext;
            Type entityType = ent.Entity.GetType();

            if (entityType.BaseType != null && entityType.Namespace == "System.Data.Entity.DynamicProxies")
                entityType = entityType.BaseType;

            string entityTypeName = entityType.Name;

            EntityContainer container =
                oc.MetadataWorkspace.GetEntityContainer(oc.DefaultContainerName, DataSpace.CSpace);
            string entitySetName = (from meta in container.BaseEntitySets
                                    where meta.ElementType.Name == entityTypeName
                                    select meta.Name).First();
            return entitySetName;
        }

    }
}
