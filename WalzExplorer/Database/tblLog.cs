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
    
    public partial class tblLog
    {
        public int LogID { get; set; }
        public int DatabaseID { get; set; }
        public int TableID { get; set; }
        public int ColumnID { get; set; }
        public int UserID { get; set; }
        public System.DateTime DateTime { get; set; }
        public int RowID { get; set; }
        public int OperationID { get; set; }
        public string NewValue { get; set; }
        public byte[] Timestamp { get; set; }
    
        public virtual tblLog_Column tblLog_Column { get; set; }
        public virtual tblLog_Database tblLog_Database { get; set; }
        public virtual tblLog_Operation tblLog_Operation { get; set; }
        public virtual tblLog_Table tblLog_Table { get; set; }
        public virtual tblLog_User tblLog_User { get; set; }
    }
}
