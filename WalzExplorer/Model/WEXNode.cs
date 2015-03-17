using System;
using System.Collections.Generic;
using WalzExplorer.Common;

namespace WalzExplorer
{
    /// <summary>
    /// A simple data transfer object (DTO) that contains raw data about a Node.
    /// </summary>
    public class WEXNode
    {
        readonly List<WEXNode> _children = new List<WEXNode>();

        public IList<WEXNode> Children
        {
            get { return _children; }
        }
        public WEXNode Parent { get; set; }
        public string ID { get; set; }
        public string IDType { get; set; } //Type of id e.g. tender,activity etc
        public string Name { get; set; }
        public string TypeID { get; set; } // type of node 'Workgroup List header'
        public string IconOpen { get; set; }
        public string IconClosed { get; set; }
        public string ToolTip { get; set; }
        public string ChildSQL { get; set; }
        public bool HasChildren ()
        {
            if (ChildSQL == null)
                return false;
            else return ChildSQL.Trim() != "";
             
        }
        public int IDAsInt()
        {
            return ConvertLibrary.StringToInt(this.ID,-1);
        }

        public string FindID (string searchForTypeID,string NotFound)
        {
            WEXNode node = this;

            while (node != null && node.IDType.ToUpper() != searchForTypeID.ToUpper())
            {
                node = node.Parent;
            }
            if (node == null)
                return NotFound;
            else
                return node.ID;

        }
    }
}