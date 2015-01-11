using System;
using System.Collections.Generic;

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
        public string ID { get; set; }
        public string Name { get; set; }
        public string TypeID { get; set; }
        public string IconOpen { get; set; }
        public string IconClosed { get; set; }
        public string ChildSQL { get; set; }
        public bool HasChildren ()
        {
            if (ChildSQL == null)
                return false;
            else return ChildSQL.Trim() != "";
             
        }
        public int IDAsInt()
        {
            int i;
            Int32.TryParse(this.ID, out i);
            return i;
        }
    }
}