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
    
    public partial class tblIntegrity_Issue_temp
    {
        public int CheckID { get; set; }
        public string IssueObjectID { get; set; }
        public System.DateTime LastCheckDate { get; set; }
        public string IssueInfo1 { get; set; }
        public string IssueInfo2 { get; set; }
    
        public virtual tblIntegrity_Check tblIntegrity_Check { get; set; }
    }
}