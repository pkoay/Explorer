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
    
        public virtual DbSet<tblTender_Drawing> tblTender_Drawing { get; set; }
        public virtual DbSet<tblTender_Material> tblTender_Material { get; set; }
        public virtual DbSet<tblTender_Status> tblTender_Status { get; set; }
        public virtual DbSet<tblTender_Step> tblTender_Step { get; set; }
        public virtual DbSet<tblTender_Supplier> tblTender_Supplier { get; set; }
        public virtual DbSet<tblTender_UnitOfMeasure> tblTender_UnitOfMeasure { get; set; }
        public virtual DbSet<tblWEX_LHSTab> tblWEX_LHSTab { get; set; }
        public virtual DbSet<tblWEX_NTSecurityGroup> tblWEX_NTSecurityGroup { get; set; }
        public virtual DbSet<tblWEX_RHSTab> tblWEX_RHSTab { get; set; }
        public virtual DbSet<tblWEX_Tree> tblWEX_Tree { get; set; }
        public virtual DbSet<tblProject_History> tblProject_History { get; set; }
        public virtual DbSet<tblProject_HistoryMilestone> tblProject_HistoryMilestone { get; set; }
        public virtual DbSet<tblProject_Status> tblProject_Status { get; set; }
        public virtual DbSet<tblProject_EarnedValueType> tblProject_EarnedValueType { get; set; }
        public virtual DbSet<tblProject_HistoryCritical> tblProject_HistoryCritical { get; set; }
        public virtual DbSet<tblProject_HistoryStatus> tblProject_HistoryStatus { get; set; }
        public virtual DbSet<tblPerson> tblPersons { get; set; }
        public virtual DbSet<tblProject> tblProjects { get; set; }
        public virtual DbSet<tblTender> tblTenders { get; set; }
        public virtual DbSet<tblPerson_Mimic> tblPerson_Mimic { get; set; }
        public virtual DbSet<tblCustomer> tblCustomers { get; set; }
        public virtual DbSet<tblProject_ContractType> tblProject_ContractType { get; set; }
        public virtual DbSet<tblProject_HistoryDollars> tblProject_HistoryDollars { get; set; }
        public virtual DbSet<tblProject_HistoryHours> tblProject_HistoryHours { get; set; }
        public virtual DbSet<tblProject_HistoryIncident> tblProject_HistoryIncident { get; set; }
        public virtual DbSet<tblProject_HistoryNCR> tblProject_HistoryNCR { get; set; }
        public virtual DbSet<tblProject_HistoryRating> tblProject_HistoryRating { get; set; }
        public virtual DbSet<tblProject_HistoryRFI> tblProject_HistoryRFI { get; set; }
        public virtual DbSet<tblProject_Portfolio> tblProject_Portfolio { get; set; }
        public virtual DbSet<tblWEX_Variables> tblWEX_Variables { get; set; }
        public virtual DbSet<tblFavourite> tblFavourites { get; set; }
        public virtual DbSet<tblTender_OverheadGroup> tblTender_OverheadGroup { get; set; }
        public virtual DbSet<tblTender_Object> tblTender_Object { get; set; }
        public virtual DbSet<tblTender_LabourRate> tblTender_LabourRate { get; set; }
        public virtual DbSet<tblTender_ObjectChildObject> tblTender_ObjectChildObject { get; set; }
        public virtual DbSet<tblWEX_TreeNodeType> tblWEX_TreeNodeType { get; set; }
        public virtual DbSet<tblWEX_TreeNodeType_RHSTab> tblWEX_TreeNodeType_RHSTab { get; set; }
        public virtual DbSet<tblTender_Schedule> tblTender_Schedule { get; set; }
        public virtual DbSet<tblTender_ObjectType> tblTender_ObjectType { get; set; }
        public virtual DbSet<tblTender_WorkGroup> tblTender_WorkGroup { get; set; }
        public virtual DbSet<vwTender_ObjectHoursByWorkGroup> vwTender_ObjectHoursByWorkGroup { get; set; }
        public virtual DbSet<vwTender_EstimateWorkGroupRate> vwTender_EstimateWorkGroupRate { get; set; }
        public virtual DbSet<tblTender_ObjectContractor> tblTender_ObjectContractor { get; set; }
        public virtual DbSet<tblTender_ObjectMaterial> tblTender_ObjectMaterial { get; set; }
        public virtual DbSet<tblTender_ObjectLabour> tblTender_ObjectLabour { get; set; }
        public virtual DbSet<tblTender_EstimateItemType> tblTender_EstimateItemType { get; set; }
        public virtual DbSet<tblTender_Estimate> tblTender_Estimate { get; set; }
        public virtual DbSet<tblTender_Subcontractor> tblTender_Subcontractor { get; set; }
        public virtual DbSet<tblTender_SubcontractorType> tblTender_SubcontractorType { get; set; }
        public virtual DbSet<tblTender_EstimateItem> tblTender_EstimateItem { get; set; }
        public virtual DbSet<vwProject_CostCodeIndirectView> vwProject_CostCodeIndirectView { get; set; }
        public virtual DbSet<tblTender_WorkgroupFuel> tblTender_WorkgroupFuel { get; set; }
        public virtual DbSet<tblTender_OverheadItem> tblTender_OverheadItem { get; set; }
        public virtual DbSet<tblTender_OverheadType> tblTender_OverheadType { get; set; }
    
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
    
        public virtual ObjectResult<spWEX_RHS_Project_Summary_Result> spWEX_RHS_Project_Summary(string userPersonID, string nodeTypeID, string searchCriteria, Nullable<int> projectID, Nullable<int> managerID, Nullable<int> customerID)
        {
            var userPersonIDParameter = userPersonID != null ?
                new ObjectParameter("UserPersonID", userPersonID) :
                new ObjectParameter("UserPersonID", typeof(string));
    
            var nodeTypeIDParameter = nodeTypeID != null ?
                new ObjectParameter("NodeTypeID", nodeTypeID) :
                new ObjectParameter("NodeTypeID", typeof(string));
    
            var searchCriteriaParameter = searchCriteria != null ?
                new ObjectParameter("SearchCriteria", searchCriteria) :
                new ObjectParameter("SearchCriteria", typeof(string));
    
            var projectIDParameter = projectID.HasValue ?
                new ObjectParameter("ProjectID", projectID) :
                new ObjectParameter("ProjectID", typeof(int));
    
            var managerIDParameter = managerID.HasValue ?
                new ObjectParameter("ManagerID", managerID) :
                new ObjectParameter("ManagerID", typeof(int));
    
            var customerIDParameter = customerID.HasValue ?
                new ObjectParameter("CustomerID", customerID) :
                new ObjectParameter("CustomerID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spWEX_RHS_Project_Summary_Result>("spWEX_RHS_Project_Summary", userPersonIDParameter, nodeTypeIDParameter, searchCriteriaParameter, projectIDParameter, managerIDParameter, customerIDParameter);
        }
    
        public virtual ObjectResult<spWEX_RHS_Project_Committed_Result> spWEX_RHS_Project_Committed(Nullable<int> projectID)
        {
            var projectIDParameter = projectID.HasValue ?
                new ObjectParameter("ProjectID", projectID) :
                new ObjectParameter("ProjectID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spWEX_RHS_Project_Committed_Result>("spWEX_RHS_Project_Committed", projectIDParameter);
        }
    
        public virtual ObjectResult<spWEX_RHS_Project_Cost_Result> spWEX_RHS_Project_Cost(Nullable<int> projectID)
        {
            var projectIDParameter = projectID.HasValue ?
                new ObjectParameter("ProjectID", projectID) :
                new ObjectParameter("ProjectID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spWEX_RHS_Project_Cost_Result>("spWEX_RHS_Project_Cost", projectIDParameter);
        }
    
        public virtual ObjectResult<spWEX_RHS_Project_Performance_Safety_Result> spWEX_RHS_Project_Performance_Safety(Nullable<int> historyID)
        {
            var historyIDParameter = historyID.HasValue ?
                new ObjectParameter("HistoryID", historyID) :
                new ObjectParameter("HistoryID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spWEX_RHS_Project_Performance_Safety_Result>("spWEX_RHS_Project_Performance_Safety", historyIDParameter);
        }
    
        public virtual ObjectResult<spWEX_RHS_Project_Performance_History_Result> spWEX_RHS_Project_Performance_History(Nullable<int> projectID)
        {
            var projectIDParameter = projectID.HasValue ?
                new ObjectParameter("ProjectID", projectID) :
                new ObjectParameter("ProjectID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spWEX_RHS_Project_Performance_History_Result>("spWEX_RHS_Project_Performance_History", projectIDParameter);
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
    
        public virtual int spProject_HistoryUpdate(Nullable<int> historyID, Nullable<System.DateTime> periodEnd)
        {
            var historyIDParameter = historyID.HasValue ?
                new ObjectParameter("HistoryID", historyID) :
                new ObjectParameter("HistoryID", typeof(int));
    
            var periodEndParameter = periodEnd.HasValue ?
                new ObjectParameter("PeriodEnd", periodEnd) :
                new ObjectParameter("PeriodEnd", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("spProject_HistoryUpdate", historyIDParameter, periodEndParameter);
        }
    
        public virtual ObjectResult<spProject_Admin_WeekList_Result> spProject_Admin_WeekList()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spProject_Admin_WeekList_Result>("spProject_Admin_WeekList");
        }
    
        public virtual int spProject_HistoryInsert(Nullable<System.DateTime> reportEnd)
        {
            var reportEndParameter = reportEnd.HasValue ?
                new ObjectParameter("ReportEnd", reportEnd) :
                new ObjectParameter("ReportEnd", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("spProject_HistoryInsert", reportEndParameter);
        }
    
        public virtual ObjectResult<Nullable<System.DateTime>> spExplorer_ServerDatetime()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<System.DateTime>>("spExplorer_ServerDatetime");
        }
    
        public virtual ObjectResult<spWEX_RHS_Project_Invoiced_Result> spWEX_RHS_Project_Invoiced(Nullable<int> projectID)
        {
            var projectIDParameter = projectID.HasValue ?
                new ObjectParameter("ProjectID", projectID) :
                new ObjectParameter("ProjectID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spWEX_RHS_Project_Invoiced_Result>("spWEX_RHS_Project_Invoiced", projectIDParameter);
        }
    
        public virtual ObjectResult<spWEX_RHS_Project_PurchaseOrderSummary_Result> spWEX_RHS_Project_PurchaseOrderSummary(Nullable<int> projectID)
        {
            var projectIDParameter = projectID.HasValue ?
                new ObjectParameter("ProjectID", projectID) :
                new ObjectParameter("ProjectID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spWEX_RHS_Project_PurchaseOrderSummary_Result>("spWEX_RHS_Project_PurchaseOrderSummary", projectIDParameter);
        }
    
        public virtual ObjectResult<spWEX_RHS_Project_CostBudget_Result> spWEX_RHS_Project_CostBudget(Nullable<int> projectID)
        {
            var projectIDParameter = projectID.HasValue ?
                new ObjectParameter("ProjectID", projectID) :
                new ObjectParameter("ProjectID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spWEX_RHS_Project_CostBudget_Result>("spWEX_RHS_Project_CostBudget", projectIDParameter);
        }
    
        public virtual ObjectResult<spWEX_RHS_Project_ContractValue_Result> spWEX_RHS_Project_ContractValue(Nullable<int> projectID)
        {
            var projectIDParameter = projectID.HasValue ?
                new ObjectParameter("ProjectID", projectID) :
                new ObjectParameter("ProjectID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spWEX_RHS_Project_ContractValue_Result>("spWEX_RHS_Project_ContractValue", projectIDParameter);
        }
    
        public virtual ObjectResult<spWEX_RHS_Project_P6EarnedValue_Result> spWEX_RHS_Project_P6EarnedValue(Nullable<int> projectID)
        {
            var projectIDParameter = projectID.HasValue ?
                new ObjectParameter("ProjectID", projectID) :
                new ObjectParameter("ProjectID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spWEX_RHS_Project_P6EarnedValue_Result>("spWEX_RHS_Project_P6EarnedValue", projectIDParameter);
        }
    
        public virtual ObjectResult<spWEX_RHS_Project_CostBudgetVsActual_Result> spWEX_RHS_Project_CostBudgetVsActual(Nullable<int> projectID)
        {
            var projectIDParameter = projectID.HasValue ?
                new ObjectParameter("ProjectID", projectID) :
                new ObjectParameter("ProjectID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spWEX_RHS_Project_CostBudgetVsActual_Result>("spWEX_RHS_Project_CostBudgetVsActual", projectIDParameter);
        }
    
        public virtual ObjectResult<spWEX_RHS_Project_PurchaseOrderSummary_ForPM_Result> spWEX_RHS_Project_PurchaseOrderSummary_ForPM(Nullable<int> personID)
        {
            var personIDParameter = personID.HasValue ?
                new ObjectParameter("PersonID", personID) :
                new ObjectParameter("PersonID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spWEX_RHS_Project_PurchaseOrderSummary_ForPM_Result>("spWEX_RHS_Project_PurchaseOrderSummary_ForPM", personIDParameter);
        }
    
        public virtual ObjectResult<spWEX_RHS_Project_PurchaseOrderSummary_v2_Result> spWEX_RHS_Project_PurchaseOrderSummary_v2(Nullable<int> projectID, Nullable<int> personID)
        {
            var projectIDParameter = projectID.HasValue ?
                new ObjectParameter("ProjectID", projectID) :
                new ObjectParameter("ProjectID", typeof(int));
    
            var personIDParameter = personID.HasValue ?
                new ObjectParameter("PersonID", personID) :
                new ObjectParameter("PersonID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spWEX_RHS_Project_PurchaseOrderSummary_v2_Result>("spWEX_RHS_Project_PurchaseOrderSummary_v2", projectIDParameter, personIDParameter);
        }
    
        public virtual ObjectResult<spWEX_RHS_Project_PurchaseOrder_Detail_Result> spWEX_RHS_Project_PurchaseOrder_Detail(Nullable<int> projectID, string dataAreaID, string purchID, Nullable<bool> committed)
        {
            var projectIDParameter = projectID.HasValue ?
                new ObjectParameter("ProjectID", projectID) :
                new ObjectParameter("ProjectID", typeof(int));
    
            var dataAreaIDParameter = dataAreaID != null ?
                new ObjectParameter("DataAreaID", dataAreaID) :
                new ObjectParameter("DataAreaID", typeof(string));
    
            var purchIDParameter = purchID != null ?
                new ObjectParameter("PurchID", purchID) :
                new ObjectParameter("PurchID", typeof(string));
    
            var committedParameter = committed.HasValue ?
                new ObjectParameter("Committed", committed) :
                new ObjectParameter("Committed", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spWEX_RHS_Project_PurchaseOrder_Detail_Result>("spWEX_RHS_Project_PurchaseOrder_Detail", projectIDParameter, dataAreaIDParameter, purchIDParameter, committedParameter);
        }
    
        public virtual ObjectResult<spTender_ObjectDetail_Result> spTender_ObjectDetail(Nullable<int> objectID)
        {
            var objectIDParameter = objectID.HasValue ?
                new ObjectParameter("ObjectID", objectID) :
                new ObjectParameter("ObjectID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spTender_ObjectDetail_Result>("spTender_ObjectDetail", objectIDParameter);
        }
    
        public virtual ObjectResult<spTender_EstimateDetail_Result> spTender_EstimateDetail(Nullable<int> tenderID)
        {
            var tenderIDParameter = tenderID.HasValue ?
                new ObjectParameter("TenderID", tenderID) :
                new ObjectParameter("TenderID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spTender_EstimateDetail_Result>("spTender_EstimateDetail", tenderIDParameter);
        }
    
        public virtual int spProject_CostcodeIsIndirectUpdate(Nullable<int> projectID, Nullable<bool> isIndirect)
        {
            var projectIDParameter = projectID.HasValue ?
                new ObjectParameter("ProjectID", projectID) :
                new ObjectParameter("ProjectID", typeof(int));
    
            var isIndirectParameter = isIndirect.HasValue ?
                new ObjectParameter("IsIndirect", isIndirect) :
                new ObjectParameter("IsIndirect", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("spProject_CostcodeIsIndirectUpdate", projectIDParameter, isIndirectParameter);
        }
    
        public virtual ObjectResult<spWEX_RHS_Project_CostBudgetVsActualWithoutCategory_Result> spWEX_RHS_Project_CostBudgetVsActualWithoutCategory(Nullable<int> projectID)
        {
            var projectIDParameter = projectID.HasValue ?
                new ObjectParameter("ProjectID", projectID) :
                new ObjectParameter("ProjectID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spWEX_RHS_Project_CostBudgetVsActualWithoutCategory_Result>("spWEX_RHS_Project_CostBudgetVsActualWithoutCategory", projectIDParameter);
        }
    
        public virtual ObjectResult<spWEX_RHS_Project_PurchaseOrderSummary_v3_Result> spWEX_RHS_Project_PurchaseOrderSummary_v3(Nullable<int> projectID, Nullable<int> personID)
        {
            var projectIDParameter = projectID.HasValue ?
                new ObjectParameter("ProjectID", projectID) :
                new ObjectParameter("ProjectID", typeof(int));
    
            var personIDParameter = personID.HasValue ?
                new ObjectParameter("PersonID", personID) :
                new ObjectParameter("PersonID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spWEX_RHS_Project_PurchaseOrderSummary_v3_Result>("spWEX_RHS_Project_PurchaseOrderSummary_v3", projectIDParameter, personIDParameter);
        }
    }
}
