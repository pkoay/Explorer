using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using WalzExplorer.Controls.RHSTabs;

namespace WalzExplorer
{
    public class WEXRHSTab
    {
        public string ID { get; set; }
        public string Header { get; set; }
        public RHSTabViewBase Content { get; set; }

        public void SetNode(WEXNode n, WEXUser u)
        {
            Content.node = n;
            Content.user =u;
        }

        public string IssueIfClosed()
        {
            return Content.IssueIfClosed();
        }
    }
    
}
