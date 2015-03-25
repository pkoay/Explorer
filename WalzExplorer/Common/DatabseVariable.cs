using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalzExplorer.Database;

namespace WalzExplorer.Common
{
    public class DatabseVariable
    {
        public static string Read(string section, string variable)
        {
            using (WalzExplorerEntities db = new WalzExplorerEntities(false))
            {
                return db.tblWEX_Variables.FirstOrDefault(x => x.Section == section && x.Variable == variable).Value;
            }
        }
    }
}
