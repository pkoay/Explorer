using System.Collections.ObjectModel;
using System.Linq;
using System.Collections.Generic;
using WalzExplorer.Controls.TreeView.ViewModel;
using WalzExplorer.Database;
using System;
using WalzExplorer.Controls.TreeView;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows;

namespace WalzExplorer.Controls.LHSTabs
{
    /// <summary>
    /// The LHSTabViewModel.  This simply
    /// exposes a read-only collection of Tabs and the name of the custom control that should be the content
    /// </summary>
    public sealed class LHSTabViewModel
    {
        WalzExplorerEntities context = new WalzExplorerEntities(false);
        public ObservableCollection<WEXLHSTab> LHSTabs { get; set; }
        public LHSTabViewModel()
        {
            LHSTabs = new ObservableCollection<WEXLHSTab>();
            foreach (tblWEX_LHSTab t in  context.tblWEX_LHSTab.OrderByDescending(x=>x.Icon))
            {
                WEXLHSTab lt=new WEXLHSTab { ID = t.LHSTabID, Icon = t.Icon };
                //NodeTreeView  tv=  new NodeTreeView(){Name=lt.TreeviewName(),Tag=lt.ID};
                //tv.Background = Brushes.Red;

                //Button b = new Button() { Background = Brushes.Red };
                //tv.PopulateRoot(user, dicSQLSubsitutes);
                //tv.NodeChanged+=new RoutedEventHandler(tvLHS_NodeChanged);

                //lt.Content=tv;

                LHSTabs.Add(lt);
            }
        }
    }
}