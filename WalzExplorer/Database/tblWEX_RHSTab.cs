//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WalzExplorer.Database
{
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    public partial class tblWEX_RHSTab : ModelBase
    {
        public tblWEX_RHSTab()
        {
            this.tblWEX_NTSecurityGroup = new HashSet<tblWEX_NTSecurityGroup>();
            this.tblWEX_TreeNodeType_RHSTab = new HashSet<tblWEX_TreeNodeType_RHSTab>();
        }
    
        private string _rHSTabID;
    	public string RHSTabID 
    	{ 
    		get { return _rHSTabID; } 
    		set { SetProperty(ref _rHSTabID, value); } 
    	}
    
        private string _name;
    	public string Name 
    	{ 
    		get { return _name; } 
    		set { SetProperty(ref _name, value); } 
    	}
    
        private string _tooltip;
    	public string Tooltip 
    	{ 
    		get { return _tooltip; } 
    		set { SetProperty(ref _tooltip, value); } 
    	}
    
    
        public virtual ICollection<tblWEX_NTSecurityGroup> tblWEX_NTSecurityGroup { get; set; }
        public virtual ICollection<tblWEX_TreeNodeType_RHSTab> tblWEX_TreeNodeType_RHSTab { get; set; }
    }
}
