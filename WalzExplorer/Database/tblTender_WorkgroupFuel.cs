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
    public partial class tblTender_WorkgroupFuel : ModelBase
    {
        private int _fuelItemID;
    	public int FuelItemID 
    	{ 
    		get { return _fuelItemID; } 
    		set { SetProperty(ref _fuelItemID, value); } 
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
    
        private double _count;
    	public double Count 
    	{ 
    		get { return _count; } 
    		set { SetProperty(ref _count, value); } 
    	}
    
        private double _week;
    	public double Week 
    	{ 
    		get { return _week; } 
    		set { SetProperty(ref _week, value); } 
    	}
    
        private double _c10DayFortnight;
    	public double C10DayFortnight 
    	{ 
    		get { return _c10DayFortnight; } 
    		set { SetProperty(ref _c10DayFortnight, value); } 
    	}
    
        private double _hoursPerWeek;
    	public double HoursPerWeek 
    	{ 
    		get { return _hoursPerWeek; } 
    		set { SetProperty(ref _hoursPerWeek, value); } 
    	}
    
        private double _litrePerHour;
    	public double LitrePerHour 
    	{ 
    		get { return _litrePerHour; } 
    		set { SetProperty(ref _litrePerHour, value); } 
    	}
    
        private double _costPerLitre;
    	public double CostPerLitre 
    	{ 
    		get { return _costPerLitre; } 
    		set { SetProperty(ref _costPerLitre, value); } 
    	}
    
        private string _comments;
    	public string Comments 
    	{ 
    		get { return _comments; } 
    		set { SetProperty(ref _comments, value); } 
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
    
    
        public virtual tblTender_WorkGroup tblTender_WorkGroup { get; set; }
    }
}