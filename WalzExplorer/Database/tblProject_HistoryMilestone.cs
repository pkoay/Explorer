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
    public partial class tblProject_HistoryMilestone : ModelBase
    {
        private int _milestoneID;
    	public int MilestoneID 
    	{ 
    		get { return _milestoneID; } 
    		set { SetProperty(ref _milestoneID, value); } 
    	}
    
        private int _historyID;
    	public int HistoryID 
    	{ 
    		get { return _historyID; } 
    		set { SetProperty(ref _historyID, value); } 
    	}
    
        private string _title;
    	public string Title 
    	{ 
    		get { return _title; } 
    		set { SetProperty(ref _title, value); } 
    	}
    
        private Nullable<System.DateTime> _approved;
    	public Nullable<System.DateTime> Approved 
    	{ 
    		get { return _approved; } 
    		set { SetProperty(ref _approved, value); } 
    	}
    
        private Nullable<System.DateTime> _actual;
    	public Nullable<System.DateTime> Actual 
    	{ 
    		get { return _actual; } 
    		set { SetProperty(ref _actual, value); } 
    	}
    
        private string _riskOpp;
    	public string RiskOpp 
    	{ 
    		get { return _riskOpp; } 
    		set { SetProperty(ref _riskOpp, value); } 
    	}
    
        private int _p6TaskID;
    	public int P6TaskID 
    	{ 
    		get { return _p6TaskID; } 
    		set { SetProperty(ref _p6TaskID, value); } 
    	}
    
        private string _p6TaskCode;
    	public string P6TaskCode 
    	{ 
    		get { return _p6TaskCode; } 
    		set { SetProperty(ref _p6TaskCode, value); } 
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
    
    
        public virtual tblProject_History tblProject_History { get; set; }
    }
}
