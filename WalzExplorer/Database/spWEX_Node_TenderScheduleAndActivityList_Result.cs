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
    using System.Collections.Generic;
    public partial class spWEX_Node_TenderScheduleAndActivityList_Result : ModelBase
    {
        private string _nodeType;
    	public string NodeType 
    	{ 
    		get { return _nodeType; } 
    		set { SetProperty(ref _nodeType, value); } 
    	}
    
        private string _nodeDescription;
    	public string NodeDescription 
    	{ 
    		get { return _nodeDescription; } 
    		set { SetProperty(ref _nodeDescription, value); } 
    	}
    
        private int _nodeID;
    	public int NodeID 
    	{ 
    		get { return _nodeID; } 
    		set { SetProperty(ref _nodeID, value); } 
    	}
    
        private string _nodeIconOpen;
    	public string NodeIconOpen 
    	{ 
    		get { return _nodeIconOpen; } 
    		set { SetProperty(ref _nodeIconOpen, value); } 
    	}
    
        private string _nodeIconClosed;
    	public string NodeIconClosed 
    	{ 
    		get { return _nodeIconClosed; } 
    		set { SetProperty(ref _nodeIconClosed, value); } 
    	}
    
        private string _nodeChildSQL;
    	public string NodeChildSQL 
    	{ 
    		get { return _nodeChildSQL; } 
    		set { SetProperty(ref _nodeChildSQL, value); } 
    	}
    
    }
}
