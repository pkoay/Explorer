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
    public partial class tblTender_Drawing : ModelBase
    {
        public tblTender_Drawing()
        {
            this.tblTender_Item = new HashSet<tblTender_Item>();
        }
    
        private int _drawingID;
    	public int DrawingID 
    	{ 
    		get { return _drawingID; } 
    		set { SetProperty(ref _drawingID, value); } 
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
    
        private string _revisionNo;
    	public string RevisionNo 
    	{ 
    		get { return _revisionNo; } 
    		set { SetProperty(ref _revisionNo, value); } 
    	}
    
        private string _comments;
    	public string Comments 
    	{ 
    		get { return _comments; } 
    		set { SetProperty(ref _comments, value); } 
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
        public virtual ICollection<tblTender_Item> tblTender_Item { get; set; }
    }
}
