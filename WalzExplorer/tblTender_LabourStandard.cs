//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WalzExplorer
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblTender_LabourStandard
    {
        public int LabourStandardID { get; set; }
        public int TenderID { get; set; }
        public int WorkGroupID { get; set; }
        public string Title { get; set; }
        public int UnitOfMeasureID { get; set; }
        public double StandardHours { get; set; }
    
        public virtual tblTender tblTender { get; set; }
        public virtual tblTender_UnitOfMeasure tblTender_UnitOfMeasure { get; set; }
        public virtual tblTender_WorkGroup tblTender_WorkGroup { get; set; }
    }
}
