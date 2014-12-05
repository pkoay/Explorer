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
using WalzExplorer.Controls.TreeView.ViewModel;
using WalzExplorer.Controls.RHSTabs;

namespace WalzExplorer.Database
{
    public partial class WalzExplorerEntities
    {


        //Not usable in Database First.
        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<tblTender>().Property(p => p.RowVersion).IsRowVersion();
        //    modelBuilder.Entity<tblTender_Drawing>().Property(p => p.RowVersion).IsRowVersion();
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
    