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
    using System.Collections.Generic;
    
    public partial class tblPerson_Person : BaseModel
    {
        public tblPerson_Person()
        {
            this.tblProject_Project = new HashSet<tblProject_Project>();
        }
    
        private string _personID;
    	public string PersonID 
    	{ 
    		get { return _personID; } 
    		set { SetProperty(ref _personID, value); } 
    	}
    
        private string _firstName;
    	public string FirstName 
    	{ 
    		get { return _firstName; } 
    		set { SetProperty(ref _firstName, value); } 
    	}
    
        private string _lastName;
    	public string LastName 
    	{ 
    		get { return _lastName; } 
    		set { SetProperty(ref _lastName, value); } 
    	}
    
        private string _login;
    	public string Login 
    	{ 
    		get { return _login; } 
    		set { SetProperty(ref _login, value); } 
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
    
    
        public virtual ICollection<tblProject_Project> tblProject_Project { get; set; }
    }
}
