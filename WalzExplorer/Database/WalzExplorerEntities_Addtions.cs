using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalzExplorer.Database
{
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    public partial class tblTender_OverheadItem : ModelBase
    {
        public double Total
        {
            get { return Count * Duration * Rate; }
        }
    }
}
