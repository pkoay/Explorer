using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using WalzExplorer.Database;

namespace WalzExplorer.Common
{
    public static class Logging
    {
        public static void LogEvent(string operation)
        {
            using (ServicesEntities db = new ServicesEntities(false))
            {
                db.spLogEvent("Explorer", operation, WindowsIdentity.GetCurrent().Name);
            }
        }
    
    }
}
