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
    
    public partial class WalzExplorerEntities : DbContext
    {
        public WalzExplorerEntities()
            : base("name=WalzExplorerEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<tblTender_Activity> tblTender_Activity { get; set; }
        public virtual DbSet<tblTender_ActivityChildActivity> tblTender_ActivityChildActivity { get; set; }
        public virtual DbSet<tblTender_ActivityContractor> tblTender_ActivityContractor { get; set; }
        public virtual DbSet<tblTender_ActivityLabour> tblTender_ActivityLabour { get; set; }
        public virtual DbSet<tblTender_ActivityMaterial> tblTender_ActivityMaterial { get; set; }
        public virtual DbSet<tblTender_ContractorType> tblTender_ContractorType { get; set; }
        public virtual DbSet<tblTender_Drawing> tblTender_Drawing { get; set; }
        public virtual DbSet<tblTender_Item> tblTender_Item { get; set; }
        public virtual DbSet<tblTender_Material> tblTender_Material { get; set; }
        public virtual DbSet<tblTender_Schedule> tblTender_Schedule { get; set; }
        public virtual DbSet<tblTender_Status> tblTender_Status { get; set; }
        public virtual DbSet<tblTender_Step> tblTender_Step { get; set; }
        public virtual DbSet<tblTender_Supplier> tblTender_Supplier { get; set; }
        public virtual DbSet<tblTender_Supplier_Material> tblTender_Supplier_Material { get; set; }
        public virtual DbSet<tblTender_UnitOfMeasure> tblTender_UnitOfMeasure { get; set; }
        public virtual DbSet<tblWEX_LHSTab> tblWEX_LHSTab { get; set; }
        public virtual DbSet<tblWEX_NTSecurityGroup> tblWEX_NTSecurityGroup { get; set; }
        public virtual DbSet<tblWEX_RHSTab> tblWEX_RHSTab { get; set; }
        public virtual DbSet<tblWEX_Tree> tblWEX_Tree { get; set; }
        public virtual DbSet<tblWEX_TreeNodeType> tblWEX_TreeNodeType { get; set; }
        public virtual DbSet<tblTender_Contractor> tblTender_Contractor { get; set; }
        public virtual DbSet<tblTender_Workgroup> tblTender_Workgroup { get; set; }
        public virtual DbSet<tblTender_WorkgroupHeader> tblTender_WorkgroupHeader { get; set; }
        public virtual DbSet<tblTender_WorkgroupItem> tblTender_WorkgroupItem { get; set; }
        public virtual DbSet<tblTender_LabourStandard> tblTender_LabourStandard { get; set; }
        public virtual DbSet<tblProject_History> tblProject_History { get; set; }
        public virtual DbSet<tblProject_HistoryMilestone> tblProject_HistoryMilestone { get; set; }
        public virtual DbSet<tblProject_Status> tblProject_Status { get; set; }
        public virtual DbSet<tblProject_EarnedValueType> tblProject_EarnedValueType { get; set; }
        public virtual DbSet<tblProject_HistoryCritical> tblProject_HistoryCritical { get; set; }
        public virtual DbSet<tblProject_HistoryManHours> tblProject_HistoryManHours { get; set; }
        public virtual DbSet<tblProject_HistoryStatus> tblProject_HistoryStatus { get; set; }
        public virtual DbSet<tblPerson> tblPersons { get; set; }
        public virtual DbSet<tblProject> tblProjects { get; set; }
        public virtual DbSet<tblTender> tblTenders { get; set; }
    
        [DbFunction("WalzExplorerEntities", "fnCommon_Split_VarcharToTable")]
        public virtual IQueryable<string> fnCommon_Split_VarcharToTable(string input, string seperator)
        {
            var inputParameter = input != null ?
                new ObjectParameter("input", input) :
                new ObjectParameter("input", typeof(string));
    
            var seperatorParameter = seperator != null ?
                new ObjectParameter("seperator", seperator) :
                new ObjectParameter("seperator", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<string>("[WalzExplorerEntities].[fnCommon_Split_VarcharToTable](@input, @seperator)", inputParameter, seperatorParameter);
        }
    
        [DbFunction("WalzExplorerEntities", "fnTender_Schedule_List")]
        public virtual IQueryable<fnTender_Schedule_List_Result> fnTender_Schedule_List(Nullable<int> tenderID)
        {
            var tenderIDParameter = tenderID.HasValue ?
                new ObjectParameter("TenderID", tenderID) :
                new ObjectParameter("TenderID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<fnTender_Schedule_List_Result>("[WalzExplorerEntities].[fnTender_Schedule_List](@TenderID)", tenderIDParameter);
        }
    
        public virtual int sp_alterdiagram(string diagramname, Nullable<int> owner_id, Nullable<int> version, byte[] definition)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var versionParameter = version.HasValue ?
                new ObjectParameter("version", version) :
                new ObjectParameter("version", typeof(int));
    
            var definitionParameter = definition != null ?
                new ObjectParameter("definition", definition) :
                new ObjectParameter("definition", typeof(byte[]));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_alterdiagram", diagramnameParameter, owner_idParameter, versionParameter, definitionParameter);
        }
    
        public virtual int sp_creatediagram(string diagramname, Nullable<int> owner_id, Nullable<int> version, byte[] definition)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var versionParameter = version.HasValue ?
                new ObjectParameter("version", version) :
                new ObjectParameter("version", typeof(int));
    
            var definitionParameter = definition != null ?
                new ObjectParameter("definition", definition) :
                new ObjectParameter("definition", typeof(byte[]));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_creatediagram", diagramnameParameter, owner_idParameter, versionParameter, definitionParameter);
        }
    
        public virtual int sp_dropdiagram(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_dropdiagram", diagramnameParameter, owner_idParameter);
        }
    
        public virtual int sp_helpdiagramdefinition(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_helpdiagramdefinition", diagramnameParameter, owner_idParameter);
        }
    
        public virtual int sp_helpdiagrams(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_helpdiagrams", diagramnameParameter, owner_idParameter);
        }
    
        public virtual int sp_renamediagram(string diagramname, Nullable<int> owner_id, string new_diagramname)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var new_diagramnameParameter = new_diagramname != null ?
                new ObjectParameter("new_diagramname", new_diagramname) :
                new ObjectParameter("new_diagramname", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_renamediagram", diagramnameParameter, owner_idParameter, new_diagramnameParameter);
        }
    
        public virtual int sp_upgraddiagrams()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_upgraddiagrams");
        }
    
        public virtual int spTender_CloneActivity(Nullable<int> componentID, Nullable<int> factor)
        {
            var componentIDParameter = componentID.HasValue ?
                new ObjectParameter("ComponentID", componentID) :
                new ObjectParameter("ComponentID", typeof(int));
    
            var factorParameter = factor.HasValue ?
                new ObjectParameter("Factor", factor) :
                new ObjectParameter("Factor", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("spTender_CloneActivity", componentIDParameter, factorParameter);
        }
    
        public virtual int spTender_Drawing_ComboList(Nullable<int> tenderID)
        {
            var tenderIDParameter = tenderID.HasValue ?
                new ObjectParameter("TenderID", tenderID) :
                new ObjectParameter("TenderID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("spTender_Drawing_ComboList", tenderIDParameter);
        }
    
        public virtual int spTender_Schedule_ComboList(Nullable<int> tenderID)
        {
            var tenderIDParameter = tenderID.HasValue ?
                new ObjectParameter("TenderID", tenderID) :
                new ObjectParameter("TenderID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("spTender_Schedule_ComboList", tenderIDParameter);
        }
    
        public virtual ObjectResult<spWEX_Node_AllProjects_Result> spWEX_Node_AllProjects()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spWEX_Node_AllProjects_Result>("spWEX_Node_AllProjects");
        }
    
        public virtual int spWEX_Node_AllProjectsList()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("spWEX_Node_AllProjectsList");
        }
    
        public virtual ObjectResult<spWEX_Node_FoundProjects_Result> spWEX_Node_FoundProjects()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spWEX_Node_FoundProjects_Result>("spWEX_Node_FoundProjects");
        }
    
        public virtual int spWEX_Node_FoundProjectsList(string searchString)
        {
            var searchStringParameter = searchString != null ?
                new ObjectParameter("SearchString", searchString) :
                new ObjectParameter("SearchString", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("spWEX_Node_FoundProjectsList", searchStringParameter);
        }
    
        public virtual ObjectResult<spWEX_Node_MyProjects_Result> spWEX_Node_MyProjects()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spWEX_Node_MyProjects_Result>("spWEX_Node_MyProjects");
        }
    
        public virtual int spWEX_Node_MyProjectsList(string userPersonID)
        {
            var userPersonIDParameter = userPersonID != null ?
                new ObjectParameter("UserPersonID", userPersonID) :
                new ObjectParameter("UserPersonID", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("spWEX_Node_MyProjectsList", userPersonIDParameter);
        }
    
        public virtual ObjectResult<spWEX_Node_TenderComponentList_Result> spWEX_Node_TenderComponentList(Nullable<int> tenderID, Nullable<int> componentParentID)
        {
            var tenderIDParameter = tenderID.HasValue ?
                new ObjectParameter("TenderID", tenderID) :
                new ObjectParameter("TenderID", typeof(int));
    
            var componentParentIDParameter = componentParentID.HasValue ?
                new ObjectParameter("ComponentParentID", componentParentID) :
                new ObjectParameter("ComponentParentID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spWEX_Node_TenderComponentList_Result>("spWEX_Node_TenderComponentList", tenderIDParameter, componentParentIDParameter);
        }
    
        public virtual ObjectResult<spWEX_Node_TenderDrawingList_Result> spWEX_Node_TenderDrawingList(Nullable<int> tenderID)
        {
            var tenderIDParameter = tenderID.HasValue ?
                new ObjectParameter("TenderID", tenderID) :
                new ObjectParameter("TenderID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spWEX_Node_TenderDrawingList_Result>("spWEX_Node_TenderDrawingList", tenderIDParameter);
        }
    
        public virtual ObjectResult<spWEX_Node_TenderFolders_Result> spWEX_Node_TenderFolders(Nullable<int> tenderID)
        {
            var tenderIDParameter = tenderID.HasValue ?
                new ObjectParameter("TenderID", tenderID) :
                new ObjectParameter("TenderID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spWEX_Node_TenderFolders_Result>("spWEX_Node_TenderFolders", tenderIDParameter);
        }
    
        public virtual ObjectResult<spWEX_Node_TendersAll_Result> spWEX_Node_TendersAll()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spWEX_Node_TendersAll_Result>("spWEX_Node_TendersAll");
        }
    
        public virtual ObjectResult<spWEX_Node_TendersAllList_Result> spWEX_Node_TendersAllList()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spWEX_Node_TendersAllList_Result>("spWEX_Node_TendersAllList");
        }
    
        public virtual ObjectResult<spWEX_Node_TenderScheduleAndActivityList_Result> spWEX_Node_TenderScheduleAndActivityList(Nullable<int> tenderID, Nullable<int> scheduleParentID)
        {
            var tenderIDParameter = tenderID.HasValue ?
                new ObjectParameter("TenderID", tenderID) :
                new ObjectParameter("TenderID", typeof(int));
    
            var scheduleParentIDParameter = scheduleParentID.HasValue ?
                new ObjectParameter("ScheduleParentID", scheduleParentID) :
                new ObjectParameter("ScheduleParentID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spWEX_Node_TenderScheduleAndActivityList_Result>("spWEX_Node_TenderScheduleAndActivityList", tenderIDParameter, scheduleParentIDParameter);
        }
    
        public virtual ObjectResult<spWEX_Node_TenderSetupList_Result> spWEX_Node_TenderSetupList(Nullable<int> tenderID)
        {
            var tenderIDParameter = tenderID.HasValue ?
                new ObjectParameter("TenderID", tenderID) :
                new ObjectParameter("TenderID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spWEX_Node_TenderSetupList_Result>("spWEX_Node_TenderSetupList", tenderIDParameter);
        }
    
        public virtual ObjectResult<spWEX_Node_TendersOpen_Result> spWEX_Node_TendersOpen()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spWEX_Node_TendersOpen_Result>("spWEX_Node_TendersOpen");
        }
    
        public virtual ObjectResult<spWEX_Node_TendersOpenList_Result> spWEX_Node_TendersOpenList()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spWEX_Node_TendersOpenList_Result>("spWEX_Node_TendersOpenList");
        }
    
        public virtual ObjectResult<spWEX_TreeRootList_Result> spWEX_TreeRootList(string lHSTabID, string nTSecurityGroups, string nTSecurityGroupsSeperator)
        {
            var lHSTabIDParameter = lHSTabID != null ?
                new ObjectParameter("LHSTabID", lHSTabID) :
                new ObjectParameter("LHSTabID", typeof(string));
    
            var nTSecurityGroupsParameter = nTSecurityGroups != null ?
                new ObjectParameter("NTSecurityGroups", nTSecurityGroups) :
                new ObjectParameter("NTSecurityGroups", typeof(string));
    
            var nTSecurityGroupsSeperatorParameter = nTSecurityGroupsSeperator != null ?
                new ObjectParameter("NTSecurityGroupsSeperator", nTSecurityGroupsSeperator) :
                new ObjectParameter("NTSecurityGroupsSeperator", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spWEX_TreeRootList_Result>("spWEX_TreeRootList", lHSTabIDParameter, nTSecurityGroupsParameter, nTSecurityGroupsSeperatorParameter);
        }
    
        public virtual ObjectResult<spWEX_Node_TenderActivityList_Result> spWEX_Node_TenderActivityList(Nullable<int> tenderID)
        {
            var tenderIDParameter = tenderID.HasValue ?
                new ObjectParameter("TenderID", tenderID) :
                new ObjectParameter("TenderID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spWEX_Node_TenderActivityList_Result>("spWEX_Node_TenderActivityList", tenderIDParameter);
        }
    
        public virtual ObjectResult<spWEX_RHSTabList_Result> spWEX_RHSTabList(string treeNodeTypeID, string nTSecurityGroups, string nTSecurityGroupsSeperator)
        {
            var treeNodeTypeIDParameter = treeNodeTypeID != null ?
                new ObjectParameter("TreeNodeTypeID", treeNodeTypeID) :
                new ObjectParameter("TreeNodeTypeID", typeof(string));
    
            var nTSecurityGroupsParameter = nTSecurityGroups != null ?
                new ObjectParameter("NTSecurityGroups", nTSecurityGroups) :
                new ObjectParameter("NTSecurityGroups", typeof(string));
    
            var nTSecurityGroupsSeperatorParameter = nTSecurityGroupsSeperator != null ?
                new ObjectParameter("NTSecurityGroupsSeperator", nTSecurityGroupsSeperator) :
                new ObjectParameter("NTSecurityGroupsSeperator", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spWEX_RHSTabList_Result>("spWEX_RHSTabList", treeNodeTypeIDParameter, nTSecurityGroupsParameter, nTSecurityGroupsSeperatorParameter);
        }
    }
}
