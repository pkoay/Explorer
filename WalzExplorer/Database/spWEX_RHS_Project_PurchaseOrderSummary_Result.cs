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
    public partial class spWEX_RHS_Project_PurchaseOrderSummary_Result : ModelBase
    {
        private string _dATAAREAID;
    	public string DATAAREAID 
    	{ 
    		get { return _dATAAREAID; } 
    		set { SetProperty(ref _dATAAREAID, value); } 
    	}
    
        private string _purchaseOrderID;
    	public string PurchaseOrderID 
    	{ 
    		get { return _purchaseOrderID; } 
    		set { SetProperty(ref _purchaseOrderID, value); } 
    	}
    
        private string _projID;
    	public string ProjID 
    	{ 
    		get { return _projID; } 
    		set { SetProperty(ref _projID, value); } 
    	}
    
        private string _requisitioner;
    	public string Requisitioner 
    	{ 
    		get { return _requisitioner; } 
    		set { SetProperty(ref _requisitioner, value); } 
    	}
    
        private string _purchasePlacer;
    	public string PurchasePlacer 
    	{ 
    		get { return _purchasePlacer; } 
    		set { SetProperty(ref _purchasePlacer, value); } 
    	}
    
        private string _vendorName;
    	public string VendorName 
    	{ 
    		get { return _vendorName; } 
    		set { SetProperty(ref _vendorName, value); } 
    	}
    
        private string _purchaseName;
    	public string PurchaseName 
    	{ 
    		get { return _purchaseName; } 
    		set { SetProperty(ref _purchaseName, value); } 
    	}
    
        private Nullable<System.DateTime> _createDate;
    	public Nullable<System.DateTime> CreateDate 
    	{ 
    		get { return _createDate; } 
    		set { SetProperty(ref _createDate, value); } 
    	}
    
        private Nullable<decimal> _orderAmount;
    	public Nullable<decimal> OrderAmount 
    	{ 
    		get { return _orderAmount; } 
    		set { SetProperty(ref _orderAmount, value); } 
    	}
    
        private Nullable<decimal> _committedAmount;
    	public Nullable<decimal> CommittedAmount 
    	{ 
    		get { return _committedAmount; } 
    		set { SetProperty(ref _committedAmount, value); } 
    	}
    
        private Nullable<decimal> _paidAmount;
    	public Nullable<decimal> PaidAmount 
    	{ 
    		get { return _paidAmount; } 
    		set { SetProperty(ref _paidAmount, value); } 
    	}
    
    }
}
