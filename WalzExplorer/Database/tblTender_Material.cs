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
    public partial class tblTender_Material : ModelBase
    {
        public tblTender_Material()
        {
            this.tblTender_ObjectMaterial = new HashSet<tblTender_ObjectMaterial>();
            this.tblTender_EstimateItem = new HashSet<tblTender_EstimateItem>();
        }
    
        private int _materialID;
    	public int MaterialID 
    	{ 
    		get { return _materialID; } 
    		set { SetProperty(ref _materialID, value); } 
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
    
        private double _sQM;
    	public double SQM 
    	{ 
    		get { return _sQM; } 
    		set { SetProperty(ref _sQM, value); } 
    	}
    
        private double _kG;
    	public double KG 
    	{ 
    		get { return _kG; } 
    		set { SetProperty(ref _kG, value); } 
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
    
        private int _sortOrder;
    	public int SortOrder 
    	{ 
    		get { return _sortOrder; } 
    		set { SetProperty(ref _sortOrder, value); } 
    	}
    
    
        public virtual tblTender_UnitOfMeasure tblTender_UnitOfMeasure { get; set; }
        public virtual tblTender tblTender { get; set; }
        public virtual ICollection<tblTender_ObjectMaterial> tblTender_ObjectMaterial { get; set; }
        public virtual ICollection<tblTender_EstimateItem> tblTender_EstimateItem { get; set; }
    }
}
