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
    public partial class tblTender_ActivityChildActivity : ModelBase
    {
        private int _activityChildActivityID;
    	public int ActivityChildActivityID 
    	{ 
    		get { return _activityChildActivityID; } 
    		set { SetProperty(ref _activityChildActivityID, value); } 
    	}
    
        private int _activityID;
    	public int ActivityID 
    	{ 
    		get { return _activityID; } 
    		set { SetProperty(ref _activityID, value); } 
    	}
    
        private int _childActivityID;
    	public int ChildActivityID 
    	{ 
    		get { return _childActivityID; } 
    		set { SetProperty(ref _childActivityID, value); } 
    	}
    
        private string _title;
    	public string Title 
    	{ 
    		get { return _title; } 
    		set { SetProperty(ref _title, value); } 
    	}
    
        private int _stepID;
    	public int StepID 
    	{ 
    		get { return _stepID; } 
    		set { SetProperty(ref _stepID, value); } 
    	}
    
        private double _quantity;
    	public double Quantity 
    	{ 
    		get { return _quantity; } 
    		set { SetProperty(ref _quantity, value); } 
    	}
    
        private double _factor;
    	public double Factor 
    	{ 
    		get { return _factor; } 
    		set { SetProperty(ref _factor, value); } 
    	}
    
        private string _comment;
    	public string Comment 
    	{ 
    		get { return _comment; } 
    		set { SetProperty(ref _comment, value); } 
    	}
    
        private double _sortOrder;
    	public double SortOrder 
    	{ 
    		get { return _sortOrder; } 
    		set { SetProperty(ref _sortOrder, value); } 
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
    
    
        public virtual tblTender_Activity tblTender_Activity { get; set; }
        public virtual tblTender_Activity tblTender_Activity1 { get; set; }
        public virtual tblTender_Step tblTender_Step { get; set; }
    }
}
