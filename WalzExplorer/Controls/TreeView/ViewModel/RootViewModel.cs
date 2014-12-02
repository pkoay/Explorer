using System.Collections.ObjectModel;
using System.Linq;
using System.Collections.Generic;

namespace WalzExplorer.Controls.TreeView.ViewModel
{
    /// <summary>
    /// The RootViewModel.  This simply
    /// exposes a read-only collection of root nodes.
    /// </summary>
    public class RootViewModel
    {
        readonly ReadOnlyCollection<NodeViewModel> _nodes;

        public RootViewModel(List<WEXNode> nodes, Dictionary<string, string> dicSQLSubsitutes)
        {
            _nodes = new ReadOnlyCollection<NodeViewModel>(
                (from node in nodes
                 select new NodeViewModel(null, node, dicSQLSubsitutes))
                .ToList());
        }

        public ReadOnlyCollection<NodeViewModel> RootNodes
        {
            get { return _nodes; }
        }
    }
}