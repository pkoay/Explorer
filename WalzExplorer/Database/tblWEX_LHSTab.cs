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
    public partial class tblWEX_LHSTab : ModelBase,IDataErrorInfo
    {
        public tblWEX_LHSTab()
        {
            this.tblWEX_Tree = new HashSet<tblWEX_Tree>();
        }
    
        private string _lHSTabID;
    	public string LHSTabID 
    	{ 
    		get { return _lHSTabID; } 
    		set { SetProperty(ref _lHSTabID, value); } 
    	}
    
        private string _icon;
    	public string Icon 
    	{ 
    		get { return _icon; } 
    		set { SetProperty(ref _icon, value); } 
    	}
    
        private int _sortOrder;
    	public int SortOrder 
    	{ 
    		get { return _sortOrder; } 
    		set { SetProperty(ref _sortOrder, value); } 
    	}
    
    
        public virtual ICollection<tblWEX_Tree> tblWEX_Tree { get; set; }
    }
}
