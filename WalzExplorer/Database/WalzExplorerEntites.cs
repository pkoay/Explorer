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

namespace WalzExplorer.Database
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

        public  List<WEXNode> GetRootNodes(string strLHSTabID, WEXUser user, Dictionary<string, string> dicSQLSubsitutes)
        {

            List<WEXNode> nodes = new List<WEXNode>();

            var pLHSTabID = new SqlParameter("@LHSTabID", strLHSTabID);
            var pNTSecurityGroups = new SqlParameter("@NTSecurityGroups", 4);
            var pNTSecurityGroupsSeperator = new SqlParameter("@NTSecurityGroupsSeperator", "|");
            foreach (WEXTreeRoot tr in this.Database.SqlQuery<WEXTreeRoot>("spWEX.TreeRootList", pLHSTabID, pNTSecurityGroups, pNTSecurityGroupsSeperator))
            {
                foreach (WEXNode n in (GetNodes(tr.SQL, dicSQLSubsitutes)))
                {
                    nodes.Add(n);
                }
            }
            return nodes;
        }

        public  List<WEXNode> GetNodes(string strNodeSQL, Dictionary<string, string> dicSQLSubsitutes)
        {

            List<WEXNode> nodes = new List<WEXNode>();

            //Subsitute parameters
            foreach (KeyValuePair<string, string> entry in dicSQLSubsitutes)
            {
                strNodeSQL = strNodeSQL.Replace(entry.Key, entry.Value);
            }
            return this.Database.SqlQuery<WEXNode>(strNodeSQL).ToList();
        }

        //  public static List<RHSTab> GetRHSTabs(NodeViewModel LHSNode, WEXUser user)
        //{
        //    List<RHSTab> tabs = new List<RHSTab>();
        //    if (LHSNode != null)
        //    {
        //        string strSQL = "spRHSTabList";


        //        //Create node from strNodeSQL
        //        using (SqlConnection con = new SqlConnection(strConnection))
        //        {
        //            using (SqlCommand cmd = new SqlCommand(strSQL, con))
        //            {
        //                cmd.CommandType = CommandType.StoredProcedure;

        //                cmd.Parameters.Add("@TreeNodeTypeID", SqlDbType.VarChar).Value = LHSNode.NodeTypeID;
        //                cmd.Parameters.Add("@NTSecurityGroups", SqlDbType.VarChar).Value = user.SecurityGroupAsString();
        //                cmd.Parameters.Add("@NTSecurityGroupsSeperator", SqlDbType.VarChar).Value = "|";
        //                con.Open();
        //                using (var reader = cmd.ExecuteReader())
        //                {
        //                    while (reader.Read())
        //                    {
        //                        RHSTab tab = new RHSTab();
        //                        tab.ID = reader["ID"].ToString();
        //                        tab.Name = reader["Name"].ToString();
        //                        //tab.Content = new ProjectDetail();
        //                        string strTabClass = tab.ID.Substring(3); // remove tab prefix
        //                        tab.Content = (RHSTabContentBase)Activator.CreateInstance(Type.GetType("WalzExplorer.Controls.RHSTabContent." + strTabClass + "Tab." + strTabClass));
        //                        //switch (tab.ID)
        //                        //{
        //                        //    case "TabProjectDetail":
        //                        //        tab.Content = new ProjectDetail();
        //                        //        break;
        //                        //    case "TabProjectReport":
        //                        //        tab.Content = new ProjectReport();
        //                        //        break;
        //                        //    default: 
        //                        //        tab.Content = new ProjectDetail();
        //                        //        break;
        //                        //}
        //                        tabs.Add(tab);
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    return tabs;
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
