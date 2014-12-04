﻿using System.Collections.Generic;
using WalzExplorer.Database;



namespace WalzExplorer.Controls.TreeView.ViewModel
{
    /// <summary>
    /// The RootViewModel.  This simply
    /// exposes a read-only collection of nodes.
    /// </summary>
    public class NodeViewModel : TreeViewItemViewModel
    {
        
        WalzExplorerEntities context= new WalzExplorerEntities();
        readonly WEXNode _node;
        readonly Dictionary<string, string> _dicSQLSubsitutes;

        public NodeViewModel(NodeViewModel parent,WEXNode node, Dictionary<string, string> dicSQLSubsitutes)
            : base(parent, node.HasChildren())
        {
            _dicSQLSubsitutes = dicSQLSubsitutes;
            _node = node;
        }

        public string NodeName
        {
            get { return _node.Name; }
        }

        public WEXNode Node
        {
            get { return _node; }
        }

        public string NodeTypeID
        {
            get { return _node.TypeID; }
        }

        public string NodeIcon
        {
            get 
            { 
                if (this.IsExpanded)
                    return _node.IconOpen;
                else
                    return _node.IconClosed; 
            } 
        }

        protected override void LoadChildren()
        {
            foreach (WEXNode node in context.GetNodes(_node.ChildSQL,_dicSQLSubsitutes))
                base.Children.Add(new NodeViewModel(this,node, _dicSQLSubsitutes));
        }
    }
}