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
    public partial class spWEX_RHS_Project_CostBudgetVsActualWithoutCategory_Result : ModelBase
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
    
        private string _projName;
    	public string ProjName 
    	{ 
    		get { return _projName; } 
    		set { SetProperty(ref _projName, value); } 
    	}
    
        private Nullable<decimal> _originalBudget;
    	public Nullable<decimal> OriginalBudget 
    	{ 
    		get { return _originalBudget; } 
    		set { SetProperty(ref _originalBudget, value); } 
    	}
    
        private Nullable<decimal> _budgetVariation;
    	public Nullable<decimal> BudgetVariation 
    	{ 
    		get { return _budgetVariation; } 
    		set { SetProperty(ref _budgetVariation, value); } 
    	}
    
        private Nullable<decimal> _currentBudget;
    	public Nullable<decimal> CurrentBudget 
    	{ 
    		get { return _currentBudget; } 
    		set { SetProperty(ref _currentBudget, value); } 
    	}
    
        private Nullable<decimal> _actual;
    	public Nullable<decimal> Actual 
    	{ 
    		get { return _actual; } 
    		set { SetProperty(ref _actual, value); } 
    	}
    
        private Nullable<decimal> _committed;
    	public Nullable<decimal> Committed 
    	{ 
    		get { return _committed; } 
    		set { SetProperty(ref _committed, value); } 
    	}
    
        private Nullable<decimal> _remaining;
    	public Nullable<decimal> Remaining 
    	{ 
    		get { return _remaining; } 
    		set { SetProperty(ref _remaining, value); } 
    	}
    
    }
}