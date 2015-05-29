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
    public partial class tblTender_Item : ModelBase
    {
        private int _itemID;
    	public int ItemID 
    	{ 
    		get { return _itemID; } 
    		set { SetProperty(ref _itemID, value); } 
    	}
    
        private int _tenderID;
    	public int TenderID 
    	{ 
    		get { return _tenderID; } 
    		set { SetProperty(ref _tenderID, value); } 
    	}
    
        private int _scheduleID;
    	public int ScheduleID 
    	{ 
    		get { return _scheduleID; } 
    		set { SetProperty(ref _scheduleID, value); } 
    	}
    
        private int _drawingID;
    	public int DrawingID 
    	{ 
    		get { return _drawingID; } 
    		set { SetProperty(ref _drawingID, value); } 
    	}
    
        private string _markNo;
    	public string MarkNo 
    	{ 
    		get { return _markNo; } 
    		set { SetProperty(ref _markNo, value); } 
    	}
    
        private string _title;
    	public string Title 
    	{ 
    		get { return _title; } 
    		set { SetProperty(ref _title, value); } 
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
    
        private int _objectID;
    	public int ObjectID 
    	{ 
    		get { return _objectID; } 
    		set { SetProperty(ref _objectID, value); } 
    	}
    
    
        public virtual tblTender_Drawing tblTender_Drawing { get; set; }
        public virtual tblTender_Schedule tblTender_Schedule { get; set; }
        public virtual tblTender tblTender { get; set; }
        public virtual tblTender_Object tblTender_Object { get; set; }
    }
}
