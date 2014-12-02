using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;


//using WalzExplorer.Controls.RHSTabContent;
//using WalzExplorer.Controls.TreeView.ViewModel;


namespace WalzExplorer
{
    public static class Database
    {
        const string ConnectionStringname = "WalzExplorer";
        //private string strConnection = ConfigurationManager.ConnectionStrings["WalzExplorer"].ToString();
        //private static string strConnection="Data Source=localhost;Initial Catalog=WalzExplorer;Integrated Security=SSPI";
        
        //public static List<Node> GetRootNodes(string strLHSTabID, User user, Dictionary<string, string> dicSQLSubsitutes)
        //{
        //    List<Node> nodes = new List<Node>();
        //    //Create treeview root nodes

        //    try
        //    {
        //        using (SqlConnection con = new SqlConnection(strConnection))
        //        {
        //            using (SqlCommand cmd = new SqlCommand("spTreeList", con))
        //            {
        //                cmd.CommandType = CommandType.StoredProcedure;
        //                cmd.Parameters.Add("@LHSTabID", SqlDbType.VarChar).Value = strLHSTabID;
        //                cmd.Parameters.Add("@NTSecurityGroups", SqlDbType.VarChar).Value = user.SecurityGroupAsString();
        //                cmd.Parameters.Add("@NTSecurityGroupsSeperator", SqlDbType.VarChar).Value = "|";
        //                con.Open();
        //                using (var reader = cmd.ExecuteReader())
        //                {
        //                    while (reader.Read())
        //                    {
        //                        foreach (Node n in (GetNodes(reader["SQL"].ToString(), dicSQLSubsitutes)))
        //                        {
        //                            nodes.Add(n);
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //        return nodes;
        //    }
        //    catch (Exception ex)
        //    {

        //        throw new Exception(ex.Message);
        //    }
           
        //}

        //public static List<Node> GetNodes(string strNodeSQL, Dictionary<string, string> dicSQLSubsitutes)
        //{
            
        //    List<Node> nodes = new List<Node>();

        //    //Subsitute parameters
        //    foreach (KeyValuePair<string, string> entry in dicSQLSubsitutes)
        //    {
        //        strNodeSQL = strNodeSQL.Replace(entry.Key, entry.Value);
        //    }
            
        //    //Create node from strNodeSQL
        //    using (SqlConnection con = new SqlConnection(strConnection))
        //    {
        //        using (SqlCommand cmd = new SqlCommand(strNodeSQL, con))
        //        {
        //            con.Open();
        //            using (var reader = cmd.ExecuteReader())
        //            {
        //                while (reader.Read())
        //                {
        //                    Node node = new Node();
        //                    node.ID= reader["NodeID"].ToString();
        //                    node.Name = reader["NodeDescription"].ToString();
        //                    node.IconOpen = reader["NodeIconOpen"].ToString();
        //                    node.IconClosed = reader["NodeIconClosed"].ToString();
        //                    node.TypeID = reader["NodeType"].ToString();
        //                    node.ChildSQL = reader["NodeChildSQL"].ToString();
        //                    node.HasChildren = (node.ChildSQL != ""); // if no childSql then no children
        //                    nodes.Add(node);
        //                }
        //            }
        //        }
        //    }
        //    return nodes;
        //}

        //public static List<RHSTab> GetRHSTabs(NodeViewModel LHSNode, User user)
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
        //}
        public static string ConnectionString()
        {
            return ConfigurationManager.ConnectionStrings[ConnectionStringname].ConnectionString;
        }

        public static string ServerName()
        {
            string cs= ConfigurationManager.ConnectionStrings[ConnectionStringname].ConnectionString;
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(cs);
            // Retrieve the DataSource property.    
            return builder.DataSource;
        }

        public static string DatabaseName()
        {
            string cs = ConfigurationManager.ConnectionStrings[ConnectionStringname].ConnectionString;
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(cs);
            // Retrieve the DataSource property.    
            return builder.InitialCatalog;
        }
       

        public static Person GetFirstPerson(string strWhere)
        {
            return GetPersons(strWhere)[0];
        }

        public static List<Person> GetPersons(string strWhere)
        {
            List<Person> persons = new List<Person>();
            using (SqlConnection con = new SqlConnection(ConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * from dbo.[Person.Person] WHERE " + strWhere, con))
                {
                    con.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Person person = new Person();
                            person.PersonID = reader["PersonID"].ToString();
                            person.FirstName = reader["FirstName"].ToString();
                            person.LastName = reader["LastName"].ToString();
                            person.Login = reader["Login"].ToString();
                            persons.Add(person);
                        }
                    }
                }
            }
            return persons;
        }
    }
}
