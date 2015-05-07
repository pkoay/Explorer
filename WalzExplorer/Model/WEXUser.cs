using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Principal;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Data.Entity.Core.Objects;
using WalzExplorer.Database;
using System.DirectoryServices;

namespace WalzExplorer
{
    /// <summary>
    /// A simple data transfer object (DTO) that contains raw data about a User.
    /// </summary>
    public class WEXUser
    {
        //private string _loginID;
        readonly List<string> _securityGroups = new List<string>(); //derived from Active directory
        private tblPerson _mimicedPerson;
        private WalzExplorerEntities db = new WalzExplorerEntities(false);

        public IList<string> SecurityGroups 
        {
            get { return _securityGroups; }
        }

        public tblPerson RealPerson { get; set; }    //derived from Person table via lookup of LoginID

        public tblPerson MimicedPerson //Same as Real Person, unless mimic is activated
        { 
            get { return _mimicedPerson; }
            set
            {
                _mimicedPerson = value;
                setupMimicedUser();
            }
        }

       
        public void Login (string loginID)
        {
            RealPerson = db.tblPersons.Where(x => x.Login.ToUpper() == loginID.ToUpper()).FirstOrDefault();
            
            if (RealPerson != null)
            {
                // if pkoay then mimic Sam Givney (ease of testing)
                if (loginID.ToUpper() == "WALZ\\PKOAY")
                {
                    MimicedPerson = db.tblPersons.Where(x => x.Login == "WALZ\\SGIVNEY").FirstOrDefault();
                }
                else
                {
                    MimicedPerson = RealPerson;
                }
            }

        }

        private void setupMimicedUser()
        {
             
            SecurityGroups.Clear();
            foreach (string group in xGetGroups(MimicedPerson.Login.ToUpper().Replace("WALZ\\","")))
            {
                SecurityGroups.Add(group);
            }

          
        }

    
        
        public string SecurityGroupAsString ()
        {
            string val="";
            foreach (string group in SecurityGroups)
                val = val+ group + "|";
            return val;
        }

        //public List<GroupPrincipal> GetGroups(string userName)
        //{
        //    List<GroupPrincipal> result = new List<GroupPrincipal>();
        //    if (userName != null)
        //    {
        //        // establish domain context
        //        PrincipalContext yourDomain = new PrincipalContext(ContextType.Domain);

        //        // find your user
        //        UserPrincipal user = UserPrincipal.FindByIdentity(yourDomain, userName);

        //        // if found - grab its groups
        //        if (user != null)
        //        {
        //            PrincipalSearchResult<Principal> groups = user.GetGroups(yourDomain);
        //            //PrincipalSearchResult<Principal> groups = user.GetAuthorizationGroups();

        //            //iterate over all groups
        //            foreach (Principal p in groups)
        //            {
        //                // make sure to add only group principals
        //                if (p is GroupPrincipal)
        //                {
        //                    result.Add((GroupPrincipal)p);
        //                    result.AddRange(GetGroups((GroupPrincipal)p, yourDomain));
        //                }
        //            }
        //        }
        //    }
        //    return result;
        //}
        
        //public List<GroupPrincipal>  GetGroups (GroupPrincipal group, PrincipalContext yourDomain)
        //{
        //    List<GroupPrincipal> result = new List<GroupPrincipal>();

        //    PrincipalSearchResult<Principal> groups = group.GetGroups(yourDomain);
        //    //PrincipalSearchResult<Principal> groups = user.GetAuthorizationGroups();

        //    // iterate over all groups
        //    foreach (Principal p in groups)
        //    {
        //        // make sure to add only group principals
        //        if (p is GroupPrincipal)
        //        {
        //            result.Add((GroupPrincipal)p);

        //            //Recursive
        //            result.AddRange(GetGroups((GroupPrincipal)p, yourDomain));
        //        }
        //    }

        //    return result;
        //}


        IEnumerable<String> xGetGroups(String samAccountName)
        {
            var userNestedMembership = new List<string>();

            var domainConnection = new DirectoryEntry();
            domainConnection.AuthenticationType = System.DirectoryServices.AuthenticationTypes.Secure;

            var samSearcher = new DirectorySearcher();

            samSearcher.SearchRoot = domainConnection;
            samSearcher.Filter = "(samAccountName=" + samAccountName + ")";
            samSearcher.PropertiesToLoad.Add("displayName");

            var samResult = samSearcher.FindOne();

            if (samResult != null)
            {
                var theUser = samResult.GetDirectoryEntry();
                theUser.RefreshCache(new string[] { "tokenGroups" });

                foreach (byte[] resultBytes in theUser.Properties["tokenGroups"])
                {
                    var SID = new SecurityIdentifier(resultBytes, 0);
                    var sidSearcher = new DirectorySearcher();

                    sidSearcher.SearchRoot = domainConnection;
                    sidSearcher.Filter = "(objectSid=" + SID.Value + ")";
                    sidSearcher.PropertiesToLoad.Add("name");

                    var sidResult = sidSearcher.FindOne();
                    if (sidResult != null)
                    {
                        userNestedMembership.Add((string)sidResult.Properties["name"][0]);
                    }
                }
            }

            return userNestedMembership;
        }



    }
    
}