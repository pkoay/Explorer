using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Principal;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Data.Entity.Core.Objects;
using WalzExplorer.Database;

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
        private WalzExplorerEntities db = new WalzExplorerEntities();

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
            RealPerson = db.tblPersons.Where(x => x.Login == loginID).FirstOrDefault();
            MimicedPerson = RealPerson;
        }

        private void setupMimicedUser()
        {
             
            SecurityGroups.Clear();
            foreach (GroupPrincipal group in GetGroups(MimicedPerson.Login))
            {
                SecurityGroups.Add(group.Name);
            }
            switch (MimicedPerson.Login)
            {
                case "WALZ\\IParungao":
                case "WALZ\\SGivney":
                    SecurityGroups.Add("WD_Project");
                    break;
                case "WALZ\\DWright":
                case "WALZ\\JHynes":
                    SecurityGroups.Add("WD_Project");
                    SecurityGroups.Add("WP_Project_Manager");
                    break;

            }
        }

        //public string LoginID // derived from windows
        //{
        //    get
        //    {
        //        return _loginID;
        //    }
        //    set
        //    {
        //        _loginID = value;
        //        ////Set security groups
        //        //foreach (GroupPrincipal group in GetGroups(_loginID))
        //        //{
        //        //    SecurityGroups.Add(group.Name);
        //        //}

        //        //RealPerson = db.tblPersons.Where(x => x.Login == _loginID).FirstOrDefault();
        //        //MimicedPerson = RealPerson;     
        //    }
        //}
        
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
            if (userName != null)
            {
                // establish domain context
                PrincipalContext yourDomain = new PrincipalContext(ContextType.Domain);

                // find your user
                UserPrincipal user = UserPrincipal.FindByIdentity(yourDomain, userName);

                // if found - grab its groups
                if (user != null)
                {
                    PrincipalSearchResult<Principal> groups = user.GetGroups();
                    //PrincipalSearchResult<Principal> groups = user.GetAuthorizationGroups();

                    //iterate over all groups
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