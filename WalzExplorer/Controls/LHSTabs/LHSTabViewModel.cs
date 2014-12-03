using System.Collections.ObjectModel;
using System.Linq;
using System.Collections.Generic;
using WalzExplorer.Controls.TreeView.ViewModel;
using WalzExplorer.Database;
using System;

namespace WalzExplorer.Controls.LHSTabs
{
    /// <summary>
    /// The LHSTabViewModel.  This simply
    /// exposes a read-only collection of Tabs and the name of the custom control that should be the content
    /// </summary>
    public sealed class LHSTabViewModel
    {
        WalzExplorerEntities context = new WalzExplorerEntities();
        public ObservableCollection<WEXLHSTab> LHSTabs { get; set; }
        public LHSTabViewModel()
        {
            LHSTabs = new ObservableCollection<WEXLHSTab>();
            foreach (tblWEX_LHSTab t in  context.tblWEX_LHSTab)
            {
                LHSTabs.Add(new WEXLHSTab { ID = t.LHSTabID, Icon = t.Icon });
            }
          
        }
    }
}