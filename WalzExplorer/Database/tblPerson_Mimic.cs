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
    public partial class tblPerson_Mimic : ModelBase
    {
        private int _personID;
    	public int PersonID 
    	{ 
    		get { return _personID; } 
    		set { SetProperty(ref _personID, value); } 
    	}
    
        private int _mimicPersonID;
    	public int MimicPersonID 
    	{ 
    		get { return _mimicPersonID; } 
    		set { SetProperty(ref _mimicPersonID, value); } 
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
    
    
        public virtual tblPerson tblPerson { get; set; }
        public virtual tblPerson tblPerson1 { get; set; }
    }
}
