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
    public partial class spWEX_RHS_Project_ContractValue_Result : ModelBase
    {
        private string _dataAreaID;
    	public string DataAreaID 
    	{ 
    		get { return _dataAreaID; } 
    		set { SetProperty(ref _dataAreaID, value); } 
    	}
    
        private string _projID;
    	public string ProjID 
    	{ 
    		get { return _projID; } 
    		set { SetProperty(ref _projID, value); } 
    	}
    
        private string _projectName;
    	public string ProjectName 
    	{ 
    		get { return _projectName; } 
    		set { SetProperty(ref _projectName, value); } 
    	}
    
        private string _budgetType;
    	public string BudgetType 
    	{ 
    		get { return _budgetType; } 
    		set { SetProperty(ref _budgetType, value); } 
    	}
    
        private Nullable<decimal> _totalAmount;
    	public Nullable<decimal> TotalAmount 
    	{ 
    		get { return _totalAmount; } 
    		set { SetProperty(ref _totalAmount, value); } 
    	}
    
    }
}
