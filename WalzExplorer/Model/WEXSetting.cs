using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Principal;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Data.Entity.Core.Objects;
using WalzExplorer.Database;
using WalzExplorer.Model;

namespace WalzExplorer
{
    public class WEXSettings
    {
        public bool DeveloperMode= false;
        public WEXUser user= new WEXUser();
        public WEXLHSTab lhsTab = new WEXLHSTab();
        public WEXNode node= new WEXNode();
        public string SearchCriteria = "";
        public WEXDrilldown drilldown;
    }
    
}