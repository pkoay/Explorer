﻿using System;
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
using WalzExplorer.Controls.TreeView.ViewModel;
using WalzExplorer.Controls.RHSTabs;
using System.Data.Entity.Core.Metadata.Edm;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data.Common;
using WalzExplorer.Common;

namespace WalzExplorer.Database
{
    public partial class WalzExplorerEntities
    {
        public  WalzExplorerEntities(bool test)
            : base("name=WalzExplorerEntities")
        {
            // Add the password programatically (should not store in config file
            var originalConnectionString = ConfigurationManager.ConnectionStrings["WalzExplorerEntities"].ConnectionString;
            var entityBuilder = new EntityConnectionStringBuilder(originalConnectionString);
            var factory = DbProviderFactories.GetFactory(entityBuilder.Provider);
            var providerBuilder = factory.CreateConnectionStringBuilder();
            providerBuilder.ConnectionString = entityBuilder.ProviderConnectionString;
            providerBuilder.Add("Password", "1JUDQlPsYzHm6s");
            entityBuilder.ProviderConnectionString = providerBuilder.ToString();
            //this.Database.Connection.ConnectionString = entityBuilder.ToString();
            this.Database.Connection.ConnectionString = providerBuilder.ToString();
        }

     

        protected override DbEntityValidationResult ValidateEntity(System.Data.Entity.Infrastructure.DbEntityEntry entityEntry, IDictionary<object, object> items)
        {
            var result = new DbEntityValidationResult(entityEntry, new List<DbValidationError>());
            if (entityEntry.Entity is tblTender_Contractor && entityEntry.State == EntityState.Added)
            {
                tblTender_Subcontractor contractor = entityEntry.Entity as tblTender_Subcontractor;
                //check for uniqueness of post title 
                if (contractor.SubcontractorTypeID<1)
                {
                    result.ValidationErrors.Add(new System.Data.Entity.Validation.DbValidationError("cmbContractorTypeID", "Must not be blank."));
                }
                if (contractor.Title=="")
                {
                    result.ValidationErrors.Add(new System.Data.Entity.Validation.DbValidationError("Title", "Must not be blank."));
                }
            }

            if (result.ValidationErrors.Count > 0)
            {
                return result;
            }
            else
            {
                return base.ValidateEntity(entityEntry, items);
            }
        }
        private readonly static Dictionary<Type, EntitySetBase> _mappingCache
         = new Dictionary<Type, EntitySetBase>();

        private ObjectContext _ObjectContext
        {
            get { return (this as IObjectContextAdapter).ObjectContext; }
        }

        public DateTime ServerDateTime()
        {
            DateTime? sdt = this.spExplorer_ServerDatetime().FirstOrDefault();
            if (!sdt.HasValue)
                throw new Exception("Cannot get server date time");
            else
                return sdt.Value;
        }
        public string Verification ()
        {
            ObjectContext octx = _ObjectContext;

            List<EntityType> el = new List<EntityType>();
            string v="";

            List<string> ColumnsToVerify = new List<string> () {"RowVersion","UpdatedBy","UpdatedDate"};
            List<string> ExcludeTables = new List<string>() { "tblWEX_Variables", "tblWEX_LHSTab", "tblWEX_NTSecurityGroup", "tblWEX_RHSTab", "tblWEX_RHSTab_Security", "tblWEX_Tree", "tblWEX_Tree_Security", "tblWEX_TreeNodeType", "tblWEX_TreeNodeType_RHSTab" };

            el = octx.MetadataWorkspace.GetItemCollection(DataSpace.CSpace).OfType<EntityType>().ToList();
            foreach (EntityType e in el)
            {
                if (!e.Name.StartsWith("vw")) // exclude views
                {
                    if (!ExcludeTables.Contains(e.Name))
                    {
                        //Verify columns exist
                        foreach (string c in ColumnsToVerify)
                        {
                            int Count = e.DeclaredMembers.Count(m => m.Name == c);
                            if (Count == 0) v = v + "Table:" + e.Name + " missing column " + c + Environment.NewLine;
                        }
                        //Verify timestamp has concurrecy=fixed
                        EdmMember em = e.DeclaredMembers.Where(m => m.Name == "RowVersion").FirstOrDefault();
                        if (em != null)
                        {
                            EdmProperty ep = (EdmProperty)em;
                            if (ep.ConcurrencyMode != ConcurrencyMode.Fixed)
                            {
                                v = v + "Table:" + e.Name + " column RowVersion not set to ConcurrencyMode=Fixed " + Environment.NewLine;
                            }
                        }
                    }
                }
            }
            return v;  
        }



        private static readonly Dictionary<int, string> _sqlErrorTextDict = new Dictionary<int, string>
{
    {547,
     "This operation failed because another data entry uses this entry."},        
    {2601,
     "One of the properties is marked as Unique index and there is already an entry with that value."}
};

        /// <summary>
        /// This decodes the DbUpdateException. If there are any errors it can
        /// handle then it returns a list of errors. Otherwise it returns null
        /// which means rethrow the error as it has not been handled
        /// </summary>
        /// <param name="ex"></param>
        /// <returns>null if cannot handle errors, otherwise a list of errors</returns>
        IEnumerable<ValidationResult> TryDecodeDbUpdateException(DbUpdateException ex)
        {
            if (!(ex.InnerException is System.Data.Entity.Core.UpdateException) ||
                !(ex.InnerException.InnerException is System.Data.SqlClient.SqlException))
                return null;
            var sqlException =
                (System.Data.SqlClient.SqlException)ex.InnerException.InnerException;
            var result = new List<ValidationResult>();
            for (int i = 0; i < sqlException.Errors.Count; i++)
            {
                var errorNum = sqlException.Errors[i].Number;
                string errorText;
                if (_sqlErrorTextDict.TryGetValue(errorNum, out errorText))
                    result.Add(new ValidationResult(errorText));
            }
            return result.Any() ? result : null;
        }
       
        public bool HasChanged()
        {
            return !(this.ChangeTracker.Entries().Where(p => p.State == EntityState.Added || p.State == EntityState.Deleted || p.State == EntityState.Modified).Count()==0);
        }

        
        public override int SaveChanges()
        {
            
            List<DbEntityEntry> LogEntries =  this.ChangeTracker.Entries().Where(p => p.State == EntityState.Added || p.State == EntityState.Deleted || p.State == EntityState.Modified).ToList();
            //Task.Factory.StartNew(Logging.LogChanges(LogEntries, this._ObjectContext));
            new Task(() => { Logging.LogChanges(LogEntries, this._ObjectContext); }).Start();
            //Logging.LogChanges(LogEntries, this._ObjectContext);


            //Task.Run(Logging.LogChanges(LogEntries, this._ObjectContext));

            //Add logging info
            var modifiedEntries = ChangeTracker.Entries()
                .Where(e => (e.State == EntityState.Added || e.State == EntityState.Modified) )
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
            try
            {

                return base.SaveChanges();

            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                // Combine the original exception message with the new one.
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                // Throw a new DbEntityValidationException with the improved exception message.
                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }
            //else it isn't an exception we understand so it throws in the normal way
            catch
            {
                throw;
            }
        }

        public void RollBackUncommitedChanges()
        {
            var changedEntries = this.ChangeTracker.Entries().Where(x => x.State != EntityState.Unchanged).ToList();
            RollBackUncommitedChanges(changedEntries);
        }
        public void RollBackUncommitedChanges(IEnumerable <DbEntityEntry> changes)
        {

            if (changes == null) changes = this.ChangeTracker.Entries().Where(x => x.State != EntityState.Unchanged).ToList();

            foreach (var entry in changes.Where(x => x.State == EntityState.Modified))
            {
                entry.CurrentValues.SetValues(entry.OriginalValues);
                entry.State = EntityState.Unchanged;
            }

            foreach (var entry in changes.Where(x => x.State == EntityState.Added))
            {
                entry.State = EntityState.Detached;
            }

            foreach (var entry in changes.Where(x => x.State == EntityState.Deleted))
            {
                entry.State = EntityState.Unchanged;
            }

        }


        public EfStatus SaveChangesWithValidation()
        {
            var status = new EfStatus();
            try
            {
                SaveChanges(); //then update it
            }
            catch (DbUpdateConcurrencyException ex)
            {
                // Update the values of the entity that failed to save from the store 
                //ex.Entries.Single().Reload();
                //grd.Rebind();
                DbEntityEntry x = ex.Entries.First<DbEntityEntry>();
                ModelBase m = (ModelBase)x.Entity;
               // m.
                status.SetErrors("This data was changed by " + ex.Entries.Single().CurrentValues.GetValue<string>("UpdatedBy") + ". This change is now shown, and your change has been lost.","ContractorID");
                RollBackUncommitedChanges(ex.Entries);
            }
            catch (DbEntityValidationException ex)
            {
                status.SetErrors(ex.EntityValidationErrors);
                return status;
            }

            catch (DbUpdateException ex)
            {
                var decodedErrors = TryDecodeDbUpdateException(ex);
                if (decodedErrors == null)
                    throw; //it isn't something we understand so rethrow
                status.SetErrors(decodedErrors);
                return status;
            }
            //else it isn't an exception we understand so it throws in the normal way
            catch 
            {
                throw;
            }
            return status;
        }



        // this stuff realls should't be here this stuff was done before I started using EF then I converted it so that it would work with EF, really the underlying storder procedures should be added to ef and then those access directly in the approriate VM code.
        public List<WEXNode> GetRootNodes(string strLHSTabID, WEXUser user, Dictionary<string, string> dicSQLSubsitutes)
        {

            List<WEXNode> nodes = new List<WEXNode>();

            foreach (spWEX_TreeRootList_Result tr in spWEX_TreeRootList(strLHSTabID, user.SecurityGroupAsString(), "|"))
            {
                foreach (WEXNode n in (GetNodes(null, tr.RootSQL, dicSQLSubsitutes)))
                {
                    nodes.Add(n);
                }
            }
            return nodes;
        }

        public List<WEXNode> GetNodes(WEXNode parent,string  strNodeSQL ,Dictionary<string, string> dicSQLSubsitutes)
        {
            
            List<WEXNode> nodes;

            //Subsitute parameters
            foreach (KeyValuePair<string, string> entry in dicSQLSubsitutes)
            {
                strNodeSQL = strNodeSQL.Replace(entry.Key, entry.Value);
            }
            nodes=this.Database.SqlQuery<WEXNode>(strNodeSQL).ToList();

            //Maybe this should be done in linq?
            foreach (WEXNode node in nodes)
                node.Parent = parent;

            return nodes;
        }

        public List<WEXRHSTab> GetRHSTabs(NodeViewModel LHSNode, WEXUser user)
        {
            List<WEXRHSTab> tabs = new List<WEXRHSTab>();
            if (LHSNode != null)
            {
                foreach (spWEX_RHSTabList_Result t in spWEX_RHSTabList(LHSNode.NodeTypeID, user.SecurityGroupAsString(), "|"))
                {
                    WEXRHSTab wt = new WEXRHSTab {ID=t.ID,Header=t.Header,Tooltip=t.Tooltip};
                    tabs.Add(wt);
                }

                //var pTreeNodeTypeID = new SqlParameter("@TreeNodeTypeID", LHSNode.NodeTypeID);
                //var pNTSecurityGroups = new SqlParameter("@NTSecurityGroups", user.SecurityGroupAsString());
                //var pNTSecurityGroupsSeperator = new SqlParameter("@NTSecurityGroupsSeperator", "|");
                //tabs = this.Database.SqlQuery<WEXRHSTab>("spWEX.RHSTabList", pTreeNodeTypeID, pNTSecurityGroups, pNTSecurityGroupsSeperator).ToList();
            }
            return tabs;
        }
    }

}
    