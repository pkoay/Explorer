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
    
    public partial class tblTender_ActivityMaterial
    {
        public int ActivityMaterialID { get; set; }
        public int ActivityID { get; set; }
        public string Title { get; set; }
        public int StepID { get; set; }
        public int MaterialID { get; set; }
        public int SupplierID { get; set; }
        public double Length { get; set; }
        public double Width { get; set; }
        public string Grade { get; set; }
        public int Quantity { get; set; }
        public double Markup { get; set; }
        public string Comment { get; set; }
        public double SortOrder { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
    
        public virtual tblTender_Activity tblTender_Activity { get; set; }
        public virtual tblTender_Material tblTender_Material { get; set; }
        public virtual tblTender_Step tblTender_Step { get; set; }
        public virtual tblTender_Supplier tblTender_Supplier { get; set; }
    }
}
