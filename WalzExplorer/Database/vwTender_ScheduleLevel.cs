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
    public partial class vwTender_ScheduleLevel : ModelBase
    {
        private int _scheduleID;
    	public int ScheduleID 
    	{ 
    		get { return _scheduleID; } 
    		set { SetProperty(ref _scheduleID, value); } 
    	}
    
        private Nullable<int> _level;
    	public Nullable<int> Level 
    	{ 
    		get { return _level; } 
    		set { SetProperty(ref _level, value); } 
    	}
    
    
        public virtual tblTender_Schedule tblTender_Schedule { get; set; }
    }
}
