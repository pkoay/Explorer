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
    
    public partial class tblTender_LabourStandard : BaseModel
    {
        private int _labourStandardID;
    	public int LabourStandardID 
    	{ 
    		get { return _labourStandardID; } 
    		set { SetProperty(ref _labourStandardID, value); } 
    	}
    
        private int _tenderID;
    	public int TenderID 
    	{ 
    		get { return _tenderID; } 
    		set { SetProperty(ref _tenderID, value); } 
    	}
    
        private int _workGroupID;
    	public int WorkGroupID 
    	{ 
    		get { return _workGroupID; } 
    		set { SetProperty(ref _workGroupID, value); } 
    	}
    
        private string _title;
    	public string Title 
    	{ 
    		get { return _title; } 
    		set { SetProperty(ref _title, value); } 
    	}
    
        private int _unitOfMeasureID;
    	public int UnitOfMeasureID 
    	{ 
    		get { return _unitOfMeasureID; } 
    		set { SetProperty(ref _unitOfMeasureID, value); } 
    	}
    
        private double _standardHours;
    	public double StandardHours 
    	{ 
    		get { return _standardHours; } 
    		set { SetProperty(ref _standardHours, value); } 
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
    
    
        public virtual tblTender tblTender { get; set; }
        public virtual tblTender_UnitOfMeasure tblTender_UnitOfMeasure { get; set; }
        public virtual tblTender_WorkGroup tblTender_WorkGroup { get; set; }
    }
}
