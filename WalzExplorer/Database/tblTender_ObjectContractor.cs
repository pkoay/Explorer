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
    public partial class tblTender_ObjectContractor : ModelBase
    {
        private int _objectContractorID;
    	public int ObjectContractorID 
    	{ 
    		get { return _objectContractorID; } 
    		set { SetProperty(ref _objectContractorID, value); } 
    	}
    
        private int _objectID;
    	public int ObjectID 
    	{ 
    		get { return _objectID; } 
    		set { SetProperty(ref _objectID, value); } 
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
    
        private int _contractorID;
    	public int ContractorID 
    	{ 
    		get { return _contractorID; } 
    		set { SetProperty(ref _contractorID, value); } 
    	}
    
        private double _quantity;
    	public double Quantity 
    	{ 
    		get { return _quantity; } 
    		set { SetProperty(ref _quantity, value); } 
    	}
    
        private double _rate;
    	public double Rate 
    	{ 
    		get { return _rate; } 
    		set { SetProperty(ref _rate, value); } 
    	}
    
        private double _markUp;
    	public double MarkUp 
    	{ 
    		get { return _markUp; } 
    		set { SetProperty(ref _markUp, value); } 
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
    
    
        public virtual tblTender_Contractor tblTender_Contractor { get; set; }
        public virtual tblTender_Object tblTender_Object { get; set; }
        public virtual tblTender_Step tblTender_Step { get; set; }
    }
}
