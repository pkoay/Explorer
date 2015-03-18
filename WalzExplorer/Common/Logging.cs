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


        public static void LogChanges(List<DbEntityEntry> changes,ObjectContext oc)
        {
            foreach (var entry in changes)
            {
                GetAuditRecordsForChange(entry,oc);
            }
         }

        private static void GetAuditRecordsForChange(DbEntityEntry dbEntry, ObjectContext oc)
        {
            List<string> PropertiesToSkip = new List<string> () {"RowVersion", "UpdatedDate", "UpdatedBy" };

            // Get table name (if it has a Table attribute, use that, otherwise get the pluralized name)
            //string tableName = dbEntry.Entity.GetType().Name; not useful due to proxies add guid at end
            string tableName = GetTableName(dbEntry, oc);
            // Determine ID string for change (potentially multiple keys
            EntityKey entityKey = oc.ObjectStateManager.GetObjectStateEntry(dbEntry.Entity).EntityKey;
            string keyName = "";
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

            //determine reference fro values (original,
            using (ServicesEntities db = new ServicesEntities(false))
            {
                //DbPropertyValues values = dbEntry.OriginalValues;
                switch (dbEntry.State)
                {
                    case EntityState.Added:
                        foreach (string propertyName in dbEntry.CurrentValues.PropertyNames)
                        {
                            if (!PropertiesToSkip.Contains(propertyName))
                            {
                                string AddedValue = dbEntry.CurrentValues.GetValue<object>(propertyName) == null ? null : dbEntry.CurrentValues.GetValue<object>(propertyName).ToString();
                                db.spLogChange("Explorer", tableName, propertyName, WindowsIdentity.GetCurrent().Name, keyName, dbEntry.State.ToString(), AddedValue);
                            }
                        }
                        break;
                    case EntityState.Deleted:
                        foreach (string propertyName in dbEntry.OriginalValues.PropertyNames)
                        {
                            if (!PropertiesToSkip.Contains(propertyName))
                            {
                                string DeletedValue = dbEntry.OriginalValues.GetValue<object>(propertyName) == null ? null : dbEntry.OriginalValues.GetValue<object>(propertyName).ToString();
                                db.spLogChange("Explorer", tableName, propertyName, WindowsIdentity.GetCurrent().Name, keyName, dbEntry.State.ToString(), DeletedValue);
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
                                    db.spLogChange("Explorer", tableName, propertyName, WindowsIdentity.GetCurrent().Name, keyName, dbEntry.State.ToString(), NewValue);
                                }
                            }
                        }
                        break;
                }

            }

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
