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
    public partial class tblTender_Activity : ModelBase,IDataErrorInfo
    {
        public tblTender_Activity()
        {
            this.tblTender_ActivityContractor = new HashSet<tblTender_ActivityContractor>();
            this.tblTender_ActivityLabour = new HashSet<tblTender_ActivityLabour>();
            this.tblTender_ActivityMaterial = new HashSet<tblTender_ActivityMaterial>();
            this.tblTender_ActivityChildActivity = new HashSet<tblTender_ActivityChildActivity>();
            this.tblTender_ActivityChildActivity1 = new HashSet<tblTender_ActivityChildActivity>();
            this.tblTender_Item = new HashSet<tblTender_Item>();
        }
    
        private int _activityID;
    	public int ActivityID 
    	{ 
    		get { return _activityID; } 
    		set { SetProperty(ref _activityID, value); } 
    	}
    
        private int _tenderID;
    	public int TenderID 
    	{ 
    		get { return _tenderID; } 
    		set { SetProperty(ref _tenderID, value); } 
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
    
    
        public virtual tblTender tblTender { get; set; }
        public virtual tblTender_UnitOfMeasure tblTender_UnitOfMeasure { get; set; }
        public virtual ICollection<tblTender_ActivityContractor> tblTender_ActivityContractor { get; set; }
        public virtual ICollection<tblTender_ActivityLabour> tblTender_ActivityLabour { get; set; }
        public virtual ICollection<tblTender_ActivityMaterial> tblTender_ActivityMaterial { get; set; }
        public virtual ICollection<tblTender_ActivityChildActivity> tblTender_ActivityChildActivity { get; set; }
        public virtual ICollection<tblTender_ActivityChildActivity> tblTender_ActivityChildActivity1 { get; set; }
        public virtual ICollection<tblTender_Item> tblTender_Item { get; set; }
    }
}
