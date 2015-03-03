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
    public partial class tblProject_HistoryIncident : ModelBase
    {
        private int _historyID;
    	public int HistoryID 
    	{ 
    		get { return _historyID; } 
    		set { SetProperty(ref _historyID, value); } 
    	}
    
        private string _incidentID;
    	public string IncidentID 
    	{ 
    		get { return _incidentID; } 
    		set { SetProperty(ref _incidentID, value); } 
    	}
    
        private System.DateTime _reportedDate;
    	public System.DateTime ReportedDate 
    	{ 
    		get { return _reportedDate; } 
    		set { SetProperty(ref _reportedDate, value); } 
    	}
    
        private string _incidentType;
    	public string IncidentType 
    	{ 
    		get { return _incidentType; } 
    		set { SetProperty(ref _incidentType, value); } 
    	}
    
        private string _detailedDescription;
    	public string DetailedDescription 
    	{ 
    		get { return _detailedDescription; } 
    		set { SetProperty(ref _detailedDescription, value); } 
    	}
    
        private string _injuryDescription;
    	public string InjuryDescription 
    	{ 
    		get { return _injuryDescription; } 
    		set { SetProperty(ref _injuryDescription, value); } 
    	}
    
        private string _actualConsequLevel;
    	public string ActualConsequLevel 
    	{ 
    		get { return _actualConsequLevel; } 
    		set { SetProperty(ref _actualConsequLevel, value); } 
    	}
    
        private string _potentialConsqLevel;
    	public string PotentialConsqLevel 
    	{ 
    		get { return _potentialConsqLevel; } 
    		set { SetProperty(ref _potentialConsqLevel, value); } 
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