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
using WalzExplorer.Controls.TreeView.ViewModel;
using WalzExplorer.Controls.RHSTabs;
using System.Data.Entity.Core.Metadata.Edm;

namespace WalzExplorer.Database
{
    public partial class WalzExplorerEntities
    {
        private readonly static Dictionary<Type, EntitySetBase> _mappingCache
         = new Dictionary<Type, EntitySetBase>();

        private ObjectContext _ObjectContext
        {
            get { return (this as IObjectContextAdapter).ObjectContext; }
        }
        public string Verification ()
        {
            ObjectContext octx = _ObjectContext;

            List<EntityType> el = new List<EntityType>();
            string v="";

            List<string> ColumnsToVerify = new List<string> () {"RowVersion","UpdatedBy","UpdatedDate"};
            List<string> ExcludeTables = new List<string>() { "tblWEX_LHSTab", "tblWEX_NTSecurityGroup", "tblWEX_RHSTab", "tblWEX_RHSTab_Security", "tblWEX_Tree", "tblWEX_Tree_Security", "tblWEX_TreeNodeType", "tblWEX_TreeNodeType_RHSTab" };

            el = octx.MetadataWorkspace.GetItemCollection(DataSpace.CSpace).OfType<EntityType>().ToList();
            foreach (EntityType e in el)
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
            return v;  
        }
     

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

        public List<WEXNode> GetRootNodes(string strLHSTabID, WEXUser user, Dictionary<string, string> dicSQLSubsitutes)
        {

            List<WEXNode> nodes = new List<WEXNode>();

            foreach (spWEX_TreeRootList_Result tr in spWEX_TreeRootList(strLHSTabID, user.SecurityGroupAsString(), "|"))
            {
                foreach (WEXNode n in (GetNodes(tr.RootSQL, dicSQLSubsitutes)))
                {
                    nodes.Add(n);
                }
            }
            return nodes;
        }

        public List<WEXNode> GetNodes(string strNodeSQL, Dictionary<string, string> dicSQLSubsitutes)
        {

            List<WEXNode> nodes = new List<WEXNode>();

            //Subsitute parameters
            foreach (KeyValuePair<string, string> entry in dicSQLSubsitutes)
            {
                strNodeSQL = strNodeSQL.Replace(entry.Key, entry.Value);
            }
            return this.Database.SqlQuery<WEXNode>(strNodeSQL).ToList();
        }

        public List<WEXRHSTab> GetRHSTabs(NodeViewModel LHSNode, WEXUser user)
        {
            List<WEXRHSTab> tabs = new List<WEXRHSTab>();
            if (LHSNode != null)
            {
                foreach (spWEX_RHSTabList_Result t in spWEX_RHSTabList(LHSNode.NodeTypeID, user.SecurityGroupAsString(), "|"))
                {
                    WEXRHSTab wt = new WEXRHSTab {ID=t.ID,Header=t.Header};
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
    