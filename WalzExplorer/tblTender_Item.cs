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
    
    public partial class tblTender_Item
    {
        public int ItemID { get; set; }
        public int TenderID { get; set; }
        public int ScheduleID { get; set; }
        public int DrawingID { get; set; }
        public string MarkNo { get; set; }
        public string Title { get; set; }
        public int ActivityID { get; set; }
        public double Quantity { get; set; }
        public double Factor { get; set; }
        public string Comment { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
    
        public virtual tblTender tblTender { get; set; }
        public virtual tblTender_Activity tblTender_Activity { get; set; }
        public virtual tblTender_Drawing tblTender_Drawing { get; set; }
        public virtual tblTender_Schedule tblTender_Schedule { get; set; }
    }
}
