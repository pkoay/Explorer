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
    public partial class tblTender : ModelBase
    {
        public tblTender()
        {
            this.tblTender_Drawing = new HashSet<tblTender_Drawing>();
            this.tblTender_Material = new HashSet<tblTender_Material>();
            this.tblTender_Supplier = new HashSet<tblTender_Supplier>();
            this.tblTender_UnitOfMeasure = new HashSet<tblTender_UnitOfMeasure>();
            this.tblTender_Object = new HashSet<tblTender_Object>();
            this.tblTender_LabourRate = new HashSet<tblTender_LabourRate>();
            this.tblTender_WorkGroup = new HashSet<tblTender_WorkGroup>();
            this.tblTender_Estimate = new HashSet<tblTender_Estimate>();
            this.tblTender_Subcontractor = new HashSet<tblTender_Subcontractor>();
            this.tblTender_SubcontractorType = new HashSet<tblTender_SubcontractorType>();
            this.tblTender_EstimateItem = new HashSet<tblTender_EstimateItem>();
        }
    
        private int _tenderID;
    	public int TenderID 
    	{ 
    		get { return _tenderID; } 
    		set { SetProperty(ref _tenderID, value); } 
    	}
    
        private string _tenderNo;
    	public string TenderNo 
    	{ 
    		get { return _tenderNo; } 
    		set { SetProperty(ref _tenderNo, value); } 
    	}
    
        private string _revision;
    	public string Revision 
    	{ 
    		get { return _revision; } 
    		set { SetProperty(ref _revision, value); } 
    	}
    
        private string _title;
    	public string Title 
    	{ 
    		get { return _title; } 
    		set { SetProperty(ref _title, value); } 
    	}
    
        private int _managerID;
    	public int ManagerID 
    	{ 
    		get { return _managerID; } 
    		set { SetProperty(ref _managerID, value); } 
    	}
    
        private string _comment;
    	public string Comment 
    	{ 
    		get { return _comment; } 
    		set { SetProperty(ref _comment, value); } 
    	}
    
        private int _statusID;
    	public int StatusID 
    	{ 
    		get { return _statusID; } 
    		set { SetProperty(ref _statusID, value); } 
    	}
    
        private int _sortOrder;
    	public int SortOrder 
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
    
        private int _customerID;
    	public int CustomerID 
    	{ 
    		get { return _customerID; } 
    		set { SetProperty(ref _customerID, value); } 
    	}
    
    
        public virtual tblPerson tblPerson { get; set; }
        public virtual ICollection<tblTender_Drawing> tblTender_Drawing { get; set; }
        public virtual ICollection<tblTender_Material> tblTender_Material { get; set; }
        public virtual ICollection<tblTender_Supplier> tblTender_Supplier { get; set; }
        public virtual tblTender_Status tblTender_Status { get; set; }
        public virtual ICollection<tblTender_UnitOfMeasure> tblTender_UnitOfMeasure { get; set; }
        public virtual tblCustomer tblCustomer { get; set; }
        public virtual ICollection<tblTender_Object> tblTender_Object { get; set; }
        public virtual ICollection<tblTender_LabourRate> tblTender_LabourRate { get; set; }
        public virtual ICollection<tblTender_WorkGroup> tblTender_WorkGroup { get; set; }
        public virtual ICollection<tblTender_Estimate> tblTender_Estimate { get; set; }
        public virtual ICollection<tblTender_Subcontractor> tblTender_Subcontractor { get; set; }
        public virtual ICollection<tblTender_SubcontractorType> tblTender_SubcontractorType { get; set; }
        public virtual ICollection<tblTender_EstimateItem> tblTender_EstimateItem { get; set; }
    }
}
