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
    
    public partial class tblIntegrity_System
    {
        public tblIntegrity_System()
        {
            this.tblIntegrity_Check = new HashSet<tblIntegrity_Check>();
            this.tblIntegrity_EmailRecipient = new HashSet<tblIntegrity_EmailRecipient>();
        }
    
        public int SystemID { get; set; }
        public string Title { get; set; }
    
        public virtual ICollection<tblIntegrity_Check> tblIntegrity_Check { get; set; }
        public virtual ICollection<tblIntegrity_EmailRecipient> tblIntegrity_EmailRecipient { get; set; }
    }
}
