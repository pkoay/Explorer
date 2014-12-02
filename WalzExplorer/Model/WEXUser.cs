using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Principal;
using System.DirectoryServices.AccountManagement;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity.Core.Objects;

namespace WalzExplorer
{
    /// <summary>
    /// A simple data transfer object (DTO) that contains raw data about a User.
    /// </summary>
    public class WEXUser
    {
        private string _loginID;
        readonly List<string> _securityGroups = new List<string>(); //derived from Active directory
        public IList<string> SecurityGroups 
        {
            get { return _securityGroups; }
        }
        public tblPerson_Person Person { get; set; }  //derived from Person table via lookup of LoginID


        public string LoginID // derived from windows
        {
            get
            {
                return _loginID;
            }
            set
            {
                _loginID = value;
                //Set security groups
                foreach (GroupPrincipal group in GetGroups(_loginID))
                {
                    SecurityGroups.Add(group.Name);
                }

                using (WalzExplorerEntities db = new WalzExplorerEntities())
                {
                    Person = db.tblPerson_Person.Where(x => x.Login == _loginID).FirstOrDefault();
                }
                                     
            }
        }
        
        public string SecurityGroupAsString ()
        {
            string val="";
            foreach (string group in SecurityGroups)
                val = val+ group + "|";
            return val;
        }

       


            public List<GroupPrincipal> GetGroups(string userName)
        {
            List<GroupPrincipal> result = new List<GroupPrincipal>();

            // establish domain context
            PrincipalContext yourDomain = new PrincipalContext(ContextType.Domain);

            // find your user
            UserPrincipal user = UserPrincipal.FindByIdentity(yourDomain, userName);

            // if found - grab its groups
            if (user != null)
            {
                PrincipalSearchResult<Principal> groups = user.GetGroups();
                //PrincipalSearchResult<Principal> groups = user.GetAuthorizationGroups();

                // iterate over all groups
                foreach (Principal p in groups)
                {
                    // make sure to add only group principals
                    if (p is GroupPrincipal)
                    {
                        result.Add((GroupPrincipal)p);
                        result.AddRange(GetGroups((GroupPrincipal)p));
                    }
                }
            }

            return result;
        }
        
        public List<GroupPrincipal>  GetGroups (GroupPrincipal group)
        {
            List<GroupPrincipal> result = new List<GroupPrincipal>();

            PrincipalSearchResult<Principal> groups = group.GetGroups();
            //PrincipalSearchResult<Principal> groups = user.GetAuthorizationGroups();

            // iterate over all groups
            foreach (Principal p in groups)
            {
                // make sure to add only group principals
                if (p is GroupPrincipal)
                {
                    result.Add((GroupPrincipal)p);

                    //Recursive
                    result.AddRange(GetGroups((GroupPrincipal)p));
                }
            }

            return result;
        }
    }
    
}