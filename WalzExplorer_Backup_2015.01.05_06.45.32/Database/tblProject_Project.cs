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
    
    public partial class tblProject_Project
    {
        public string ProjectID { get; set; }
        public string Description { get; set; }
        public string ManagerID { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public byte[] RowVersion { get; set; }
    
        public virtual tblPerson_Person tblPerson_Person { get; set; }
    }
}
