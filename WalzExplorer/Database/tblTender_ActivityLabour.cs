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
    public partial class tblTender_ActivityLabour : ModelBase
    {
        private int _activityLabourID;
    	public int ActivityLabourID 
    	{ 
    		get { return _activityLabourID; } 
    		set { SetProperty(ref _activityLabourID, value); } 
    	}
    
        private int _activityID;
    	public int ActivityID 
    	{ 
    		get { return _activityID; } 
    		set { SetProperty(ref _activityID, value); } 
    	}
    
        private int _labourStandardID;
    	public int LabourStandardID 
    	{ 
    		get { return _labourStandardID; } 
    		set { SetProperty(ref _labourStandardID, value); } 
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
    
        private int _workgroupID;
    	public int WorkgroupID 
    	{ 
    		get { return _workgroupID; } 
    		set { SetProperty(ref _workgroupID, value); } 
    	}
    
        private double _hours;
    	public double Hours 
    	{ 
    		get { return _hours; } 
    		set { SetProperty(ref _hours, value); } 
    	}
    
        private double _men;
    	public double Men 
    	{ 
    		get { return _men; } 
    		set { SetProperty(ref _men, value); } 
    	}
    
        private int _unitOfMeasureID;
    	public int UnitOfMeasureID 
    	{ 
    		get { return _unitOfMeasureID; } 
    		set { SetProperty(ref _unitOfMeasureID, value); } 
    	}
    
        private double _quantity;
    	public double Quantity 
    	{ 
    		get { return _quantity; } 
    		set { SetProperty(ref _quantity, value); } 
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
        public virtual tblTender_Step tblTender_Step { get; set; }
        public virtual tblTender_UnitOfMeasure tblTender_UnitOfMeasure { get; set; }
        public virtual tblTender_Workgroup tblTender_Workgroup { get; set; }
    }
}
