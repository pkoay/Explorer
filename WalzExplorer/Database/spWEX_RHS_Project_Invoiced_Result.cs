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
    public partial class spWEX_RHS_Project_Invoiced_Result : ModelBase
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
    
        private Nullable<System.DateTime> _invoiceDate;
    	public Nullable<System.DateTime> InvoiceDate 
    	{ 
    		get { return _invoiceDate; } 
    		set { SetProperty(ref _invoiceDate, value); } 
    	}
    
        private string _projTransType;
    	public string ProjTransType 
    	{ 
    		get { return _projTransType; } 
    		set { SetProperty(ref _projTransType, value); } 
    	}
    
        private string _invoiceNo;
    	public string InvoiceNo 
    	{ 
    		get { return _invoiceNo; } 
    		set { SetProperty(ref _invoiceNo, value); } 
    	}
    
        private decimal _invoicedAmount;
    	public decimal InvoicedAmount 
    	{ 
    		get { return _invoicedAmount; } 
    		set { SetProperty(ref _invoicedAmount, value); } 
    	}
    
    }
}
