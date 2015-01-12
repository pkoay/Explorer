using System.Collections.ObjectModel;
using System.Linq;
using System.Collections.Generic;
using WalzExplorer.Controls.TreeView.ViewModel;
using WalzExplorer.Database;
using System;

namespace WalzExplorer.Controls.RHSTabs
{
    /// <summary>
    /// The RHSTabViewModel.  This simply
    /// exposes a read-only collection of Tabs and the name of the custom control that should be the content
    /// </summary>
    public sealed class RHSTabViewModel
    {
        WalzExplorerEntities context = new WalzExplorerEntities();
        public ObservableCollection<WEXRHSTab> RHSTabs { get; set; }
        public RHSTabViewModel(NodeViewModel LHSNode, WEXUser user)
        {
            RHSTabs = new ObservableCollection<WEXRHSTab>();
            foreach (WEXRHSTab t in context.GetRHSTabs(LHSNode, user))
            {
               
                //string strTabClass = t.ID.Substring(3); // remove tab prefix
                RHSTabViewBase tab = (RHSTabViewBase)Activator.CreateInstance(Type.GetType("WalzExplorer.Controls.RHSTabs." + t.ID + "View"));
                t.Content=tab;
                RHSTabs.Add(t);
            }
        }
    }
}