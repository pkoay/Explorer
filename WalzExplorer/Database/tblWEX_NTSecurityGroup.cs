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
    
    public partial class tblWEX_NTSecurityGroup
    {
        public tblWEX_NTSecurityGroup()
        {
            this.tblWEX_RHSTab = new HashSet<tblWEX_RHSTab>();
            this.tblWEX_Tree = new HashSet<tblWEX_Tree>();
        }
    
        public string NTSecurityGroup { get; set; }
    
        public virtual ICollection<tblWEX_RHSTab> tblWEX_RHSTab { get; set; }
        public virtual ICollection<tblWEX_Tree> tblWEX_Tree { get; set; }
    }
}