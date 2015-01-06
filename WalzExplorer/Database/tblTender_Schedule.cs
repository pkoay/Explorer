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
    
    public partial class tblTender_Schedule : BaseModel
    {
        public tblTender_Schedule()
        {
            this.tblTender_Item = new HashSet<tblTender_Item>();
        }
    
        private int _scheduleID;
    	public int ScheduleID 
    	{ 
    		get { return _scheduleID; } 
    		set { SetProperty(ref _scheduleID, value); } 
    	}
    
        private int _tenderID;
    	public int TenderID 
    	{ 
    		get { return _tenderID; } 
    		set { SetProperty(ref _tenderID, value); } 
    	}
    
        private string _clientCode;
    	public string ClientCode 
    	{ 
    		get { return _clientCode; } 
    		set { SetProperty(ref _clientCode, value); } 
    	}
    
        private string _clientDescription;
    	public string ClientDescription 
    	{ 
    		get { return _clientDescription; } 
    		set { SetProperty(ref _clientDescription, value); } 
    	}
    
        private Nullable<int> _parentID;
    	public Nullable<int> ParentID 
    	{ 
    		get { return _parentID; } 
    		set { SetProperty(ref _parentID, value); } 
    	}
    
        private int _sortOrder;
    	public int SortOrder 
    	{ 
    		get { return _sortOrder; } 
    		set { SetProperty(ref _sortOrder, value); } 
    	}
    
        private bool _canAllocate;
    	public bool CanAllocate 
    	{ 
    		get { return _canAllocate; } 
    		set { SetProperty(ref _canAllocate, value); } 
    	}
    
        private string _comment;
    	public string Comment 
    	{ 
    		get { return _comment; } 
    		set { SetProperty(ref _comment, value); } 
    	}
    
        private string _updatedBy;
    	public string UpdatedBy 
    	{ 
    		get { return _updatedBy; } 
    		set { SetProperty(ref _updatedBy, value); } 
    	}
    
        private Nullable<System.DateTime> _updatedDate;
    	public Nullable<System.DateTime> UpdatedDate 
    	{ 
    		get { return _updatedDate; } 
    		set { SetProperty(ref _updatedDate, value); } 
    	}
    
        private byte[] _rowVersion;
    	public byte[] RowVersion 
    	{ 
    		get { return _rowVersion; } 
    		set { SetProperty(ref _rowVersion, value); } 
    	}
    
    
        public virtual ICollection<tblTender_Item> tblTender_Item { get; set; }
    }
}
