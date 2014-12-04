using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WalzExplorer.Controls.TreeView.ViewModel;
using WalzExplorer.Database;

namespace WalzExplorer.Controls.TreeView
{
    /// <summary>
    /// Interaction logic for NodeTreeView.xaml
    /// </summary>
    public partial class WEXTreeView : UserControl
    {
        WalzExplorerEntities context = new WalzExplorerEntities();
        private RootViewModel _rootNode;
        private NodeViewModel _selectedNode;

        public WEXTreeView()
        {
            InitializeComponent();
        }

        public void PopulateRoot(WEXUser user, Dictionary<string, string> dicSQLSubsitutes)
        {
            //Populate Role tree
            
            List<WEXNode> nodesRole = context.GetRootNodes(this.Tag.ToString(), user, dicSQLSubsitutes);
            // Create UI-friendly wrappers around the 
            // raw data objects (i.e. the view-model).
            _rootNode = new RootViewModel(nodesRole, dicSQLSubsitutes);

            // Let the UI bind to the view-model.
            base.DataContext = _rootNode;
        
        }


        public NodeViewModel SelectedItem()
        {
            return _selectedNode;
        }

        public WEXNode SelectedNode()
        {
            return _selectedNode.Node;
        }

        private void tv_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (NodeChanged != null)
            {
                _selectedNode=(NodeViewModel) e.NewValue;

                NodeChanged(this, EventArgs.Empty);
            }
        }

        public event EventHandler NodeChanged;

    }
}
