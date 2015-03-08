﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class ServicesEntities : DbContext
    {
        public ServicesEntities()
            : base("name=ServicesEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<tblFeedback_Type> tblFeedback_Type { get; set; }
        public virtual DbSet<tblFeedback_Item> tblFeedback_Item { get; set; }
        public virtual DbSet<tblFeedback_Status> tblFeedback_Status { get; set; }
    
        public virtual int spEventLogEntry(string application, string operation, string user)
        {
            var applicationParameter = application != null ?
                new ObjectParameter("Application", application) :
                new ObjectParameter("Application", typeof(string));
    
            var operationParameter = operation != null ?
                new ObjectParameter("Operation", operation) :
                new ObjectParameter("Operation", typeof(string));
    
            var userParameter = user != null ?
                new ObjectParameter("User", user) :
                new ObjectParameter("User", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("spEventLogEntry", applicationParameter, operationParameter, userParameter);
        }
    
        public virtual int spChangeLogEntry(string database, string table, string column, string user, string row, string operation, string newValue)
        {
            var databaseParameter = database != null ?
                new ObjectParameter("Database", database) :
                new ObjectParameter("Database", typeof(string));
    
            var tableParameter = table != null ?
                new ObjectParameter("Table", table) :
                new ObjectParameter("Table", typeof(string));
    
            var columnParameter = column != null ?
                new ObjectParameter("Column", column) :
                new ObjectParameter("Column", typeof(string));
    
            var userParameter = user != null ?
                new ObjectParameter("User", user) :
                new ObjectParameter("User", typeof(string));
    
            var rowParameter = row != null ?
                new ObjectParameter("Row", row) :
                new ObjectParameter("Row", typeof(string));
    
            var operationParameter = operation != null ?
                new ObjectParameter("Operation", operation) :
                new ObjectParameter("Operation", typeof(string));
    
            var newValueParameter = newValue != null ?
                new ObjectParameter("NewValue", newValue) :
                new ObjectParameter("NewValue", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("spChangeLogEntry", databaseParameter, tableParameter, columnParameter, userParameter, rowParameter, operationParameter, newValueParameter);
        }
    
        public virtual int spLogChange(string database, string table, string column, string user, string row, string operation, string newValue)
        {
            var databaseParameter = database != null ?
                new ObjectParameter("Database", database) :
                new ObjectParameter("Database", typeof(string));
    
            var tableParameter = table != null ?
                new ObjectParameter("Table", table) :
                new ObjectParameter("Table", typeof(string));
    
            var columnParameter = column != null ?
                new ObjectParameter("Column", column) :
                new ObjectParameter("Column", typeof(string));
    
            var userParameter = user != null ?
                new ObjectParameter("User", user) :
                new ObjectParameter("User", typeof(string));
    
            var rowParameter = row != null ?
                new ObjectParameter("Row", row) :
                new ObjectParameter("Row", typeof(string));
    
            var operationParameter = operation != null ?
                new ObjectParameter("Operation", operation) :
                new ObjectParameter("Operation", typeof(string));
    
            var newValueParameter = newValue != null ?
                new ObjectParameter("NewValue", newValue) :
                new ObjectParameter("NewValue", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("spLogChange", databaseParameter, tableParameter, columnParameter, userParameter, rowParameter, operationParameter, newValueParameter);
        }
    
        public virtual int spLogEvent(string application, string operation, string user)
        {
            var applicationParameter = application != null ?
                new ObjectParameter("Application", application) :
                new ObjectParameter("Application", typeof(string));
    
            var operationParameter = operation != null ?
                new ObjectParameter("Operation", operation) :
                new ObjectParameter("Operation", typeof(string));
    
            var userParameter = user != null ?
                new ObjectParameter("User", user) :
                new ObjectParameter("User", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("spLogEvent", applicationParameter, operationParameter, userParameter);
        }
    }
}
