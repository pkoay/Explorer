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
    
    public partial class tblTender_WorkGroup
    {
        public tblTender_WorkGroup()
        {
            this.tblTender_ActivityLabour = new HashSet<tblTender_ActivityLabour>();
            this.tblTender_LabourStandard = new HashSet<tblTender_LabourStandard>();
            this.tblTender_WorkGroupItem = new HashSet<tblTender_WorkGroupItem>();
        }
    
        public int WorkGroupID { get; set; }
        public int TenderID { get; set; }
        public string Title { get; set; }
        public double OnsiteLabourRate { get; set; }
        public double LabourMarkup { get; set; }
        public int Rate { get; set; }
        public string Comment { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
    
        public virtual tblTender tblTender { get; set; }
        public virtual ICollection<tblTender_ActivityLabour> tblTender_ActivityLabour { get; set; }
        public virtual ICollection<tblTender_LabourStandard> tblTender_LabourStandard { get; set; }
        public virtual ICollection<tblTender_WorkGroupItem> tblTender_WorkGroupItem { get; set; }
    }
}
