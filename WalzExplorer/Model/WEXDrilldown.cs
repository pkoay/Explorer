using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalzExplorer.Model
{
    public enum DrilldownFilter
    {
        PurchaseOrder,
        Project,
    }
    public class WEXDrilldown
    {
        public string _rhstab; // tab to display in window (i.e. new information that is shown when original object was drilled into)
        public DrilldownFilter _filter;
        public Dictionary<string, string> _parameters;

        public WEXDrilldown(string rhstab, DrilldownFilter filter, Dictionary<string, string> parameters)
        {
            _rhstab = rhstab;
            _filter = filter;
            _parameters = parameters;
        }
    }
}
