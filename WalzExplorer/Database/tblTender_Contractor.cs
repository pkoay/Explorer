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
    
    public partial class tblTender_Contractor
    {
        public tblTender_Contractor()
        {
            this.tblTender_ActivityContractor = new HashSet<tblTender_ActivityContractor>();
        }
    
        public int ContractorID { get; set; }
        public int TenderID { get; set; }
        public string Title { get; set; }
        public Nullable<int> ContractorTypeID { get; set; }
        public string Comment { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public byte[] RowVersion { get; set; }
    
        public virtual tblTender tblTender { get; set; }
        public virtual ICollection<tblTender_ActivityContractor> tblTender_ActivityContractor { get; set; }
        public virtual tblTender_ContractorType tblTender_ContractorType { get; set; }
    }
}
