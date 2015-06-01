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

    public partial class tblTender_Overhead : ModelBase
    {
        public double Total
        {
            get { return tblTender_OverheadItem.Sum(x=>x.Total); }
        }
    }
    public partial class tblTender_OverheadItem : ModelBase
    {
        public double Total
        {
            get { return Count * Duration * Rate; }
        }
    }

    public partial class tblTender_ObjectContractor : ModelBase
    {
        public double Total
        {
            get { return Quantity * (1+MarkUp) * Rate; }
        }
    }
}
