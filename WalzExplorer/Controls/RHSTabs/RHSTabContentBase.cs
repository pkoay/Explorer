using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WalzExplorer.Controls.RHSTabs
{
    public abstract class RHSTabViewBase: UserControl
    {
        public WEXNode node { get; set; }

        public abstract void Load();
        
    }

    
}
