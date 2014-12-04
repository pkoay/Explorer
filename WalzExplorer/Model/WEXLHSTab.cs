using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using WalzExplorer.Controls.TreeView;

namespace WalzExplorer
{
    public class WEXLHSTab
    {
        public string ID { get; set; }
        public string Icon { get; set; }
        public Button Content { get; set; }
        public string TreeviewName ()
        {
            return "tv" + ID;
        }

    }
    
}
