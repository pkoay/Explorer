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
    public partial class vwTender_EstimateWorkGroupRate : ModelBase
    {
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
    
        private string _workGroup;
    	public string WorkGroup 
    	{ 
    		get { return _workGroup; } 
    		set { SetProperty(ref _workGroup, value); } 
    	}
    
        private double _rate;
    	public double Rate 
    	{ 
    		get { return _rate; } 
    		set { SetProperty(ref _rate, value); } 
    	}
    
        private double _hours;
    	public double Hours 
    	{ 
    		get { return _hours; } 
    		set { SetProperty(ref _hours, value); } 
    	}
    
        private double _additionalHours;
    	public double AdditionalHours 
    	{ 
    		get { return _additionalHours; } 
    		set { SetProperty(ref _additionalHours, value); } 
    	}
    
        private double _workGroupOverhead;
    	public double WorkGroupOverhead 
    	{ 
    		get { return _workGroupOverhead; } 
    		set { SetProperty(ref _workGroupOverhead, value); } 
    	}
    
        private double _fuelOverhead;
    	public double FuelOverhead 
    	{ 
    		get { return _fuelOverhead; } 
    		set { SetProperty(ref _fuelOverhead, value); } 
    	}
    
        private double _addtionalHoursOverhead;
    	public double AddtionalHoursOverhead 
    	{ 
    		get { return _addtionalHoursOverhead; } 
    		set { SetProperty(ref _addtionalHoursOverhead, value); } 
    	}
    
        private double _totalOverhead;
    	public double TotalOverhead 
    	{ 
    		get { return _totalOverhead; } 
    		set { SetProperty(ref _totalOverhead, value); } 
    	}
    
    }
}
