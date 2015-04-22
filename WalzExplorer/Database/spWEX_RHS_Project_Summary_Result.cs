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
    public partial class spWEX_RHS_Project_Summary_Result : ModelBase
    {
        private int _projectID;
    	public int ProjectID 
    	{ 
    		get { return _projectID; } 
    		set { SetProperty(ref _projectID, value); } 
    	}
    
        private string _aXProjectID;
    	public string AXProjectID 
    	{ 
    		get { return _aXProjectID; } 
    		set { SetProperty(ref _aXProjectID, value); } 
    	}
    
        private string _aXDataAreaID;
    	public string AXDataAreaID 
    	{ 
    		get { return _aXDataAreaID; } 
    		set { SetProperty(ref _aXDataAreaID, value); } 
    	}
    
        private string _title;
    	public string Title 
    	{ 
    		get { return _title; } 
    		set { SetProperty(ref _title, value); } 
    	}
    
        private string _manager;
    	public string Manager 
    	{ 
    		get { return _manager; } 
    		set { SetProperty(ref _manager, value); } 
    	}
    
        private string _operationsManager;
    	public string OperationsManager 
    	{ 
    		get { return _operationsManager; } 
    		set { SetProperty(ref _operationsManager, value); } 
    	}
    
        private string _customer;
    	public string Customer 
    	{ 
    		get { return _customer; } 
    		set { SetProperty(ref _customer, value); } 
    	}
    
        private string _status;
    	public string Status 
    	{ 
    		get { return _status; } 
    		set { SetProperty(ref _status, value); } 
    	}
    
        private Nullable<double> _contract;
    	public Nullable<double> Contract 
    	{ 
    		get { return _contract; } 
    		set { SetProperty(ref _contract, value); } 
    	}
    
        private Nullable<double> _costBudget;
    	public Nullable<double> CostBudget 
    	{ 
    		get { return _costBudget; } 
    		set { SetProperty(ref _costBudget, value); } 
    	}
    
        private Nullable<double> _cost;
    	public Nullable<double> Cost 
    	{ 
    		get { return _cost; } 
    		set { SetProperty(ref _cost, value); } 
    	}
    
        private Nullable<double> _committed;
    	public Nullable<double> Committed 
    	{ 
    		get { return _committed; } 
    		set { SetProperty(ref _committed, value); } 
    	}
    
        private Nullable<double> _invoiced;
    	public Nullable<double> Invoiced 
    	{ 
    		get { return _invoiced; } 
    		set { SetProperty(ref _invoiced, value); } 
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
    
        private string _contractType;
    	public string ContractType 
    	{ 
    		get { return _contractType; } 
    		set { SetProperty(ref _contractType, value); } 
    	}
    
    }
}
