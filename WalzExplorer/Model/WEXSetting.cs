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
    public class WEXSettings
    {
        public bool DeveloperMode= false;
        public WEXUser user= new WEXUser();
        public WEXNode node= new WEXNode(); 

    }
    
}