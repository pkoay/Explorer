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
    
    public partial class tblTender_UnitOfMeasure
    {
        public tblTender_UnitOfMeasure()
        {
            this.tblTender_Activity = new HashSet<tblTender_Activity>();
            this.tblTender_ActivityLabour = new HashSet<tblTender_ActivityLabour>();
            this.tblTender_LabourStandard = new HashSet<tblTender_LabourStandard>();
            this.tblTender_Material = new HashSet<tblTender_Material>();
        }
    
        public int UnitOfMeasureID { get; set; }
        public int TenderID { get; set; }
        public string Title { get; set; }
        public string ShortTitle { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
    
        public virtual tblTender tblTender { get; set; }
        public virtual ICollection<tblTender_Activity> tblTender_Activity { get; set; }
        public virtual ICollection<tblTender_ActivityLabour> tblTender_ActivityLabour { get; set; }
        public virtual ICollection<tblTender_LabourStandard> tblTender_LabourStandard { get; set; }
        public virtual ICollection<tblTender_Material> tblTender_Material { get; set; }
    }
}