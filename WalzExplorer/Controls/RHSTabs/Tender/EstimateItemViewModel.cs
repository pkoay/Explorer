using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Telerik.Windows.Data;
using WalzExplorer.Common;
using WalzExplorer.Controls.Grid;
using WalzExplorer.Database;

namespace WalzExplorer.Controls.RHSTabs.Tender
{
    public class EstimateItemViewModel : GridEditViewModelBase2
    {
        int TenderID;
        int ScheduleID;
        int Default_SubcontractorID;
        int Default_WorkGroupID;
        int Default_UnitOfMeasureID;
        int Default_DrawingID;
        int Default_SupplierID;
        int Default_MaterialID;

        public EstimateItemViewModel(WEXSettings settings) //(string NodeType, string PersonID, int Id)
        {
            TenderID = ConvertLibrary.StringToInt(settings.node.FindID("TENDER", "-2"), -1);
            ScheduleID = ConvertLibrary.StringToInt(settings.node.FindID("EstimateScheduleID", "-2"), -1);
            if (ScheduleID == -1) // all items (not for a given scheduleID
            {
                data = new ObservableCollection<ModelBase>(context.tblTender_EstimateItem.Where(x => x.TenderID == TenderID).OrderBy(x => x.tblTender_Schedule.SortOrder).ThenBy(x => x.SortOrder));
            }
            else
            {
                data = new ObservableCollection<ModelBase>(context.tblTender_EstimateItem.Where(x => x.TenderID == TenderID && x.ScheduleID == ScheduleID).OrderBy(x => x.tblTender_Schedule.SortOrder).ThenBy(x => x.SortOrder));

            }
            
            Default_SubcontractorID = context.tblTender_Subcontractor.Where(x => x.TenderID == TenderID).OrderBy(x => x.SortOrder).First().SubcontractorID;
            Default_WorkGroupID = context.tblTender_WorkGroup.Where(x => x.TenderID == TenderID).OrderBy(x => x.SortOrder).First().WorkGroupID;
            Default_UnitOfMeasureID = context.tblTender_UnitOfMeasure.Where(x => x.TenderID == TenderID).OrderBy(x => x.SortOrder).First().UnitOfMeasureID;
            Default_DrawingID = context.tblTender_Drawing.Where(x => x.TenderID == TenderID).OrderBy(x => x.SortOrder).First().DrawingID;
            Default_SupplierID = context.tblTender_Supplier.Where(x => x.TenderID == TenderID).OrderBy(x => x.SortOrder).First().SupplierID;
            Default_MaterialID = context.tblTender_Material.Where(x => x.TenderID == TenderID).OrderBy(x => x.SortOrder).First().MaterialID;
        }

        public override ModelBase DefaultItem(ModelBase NearItem)
        {
            tblTender_EstimateItem nearItem = (tblTender_EstimateItem)NearItem;

            tblTender_EstimateItem i = new tblTender_EstimateItem();
            i.TenderID = TenderID;
            if (nearItem != null) i.ScheduleID = nearItem.ScheduleID;
            i.WorkGroupID = Default_WorkGroupID;
            i.SubcontractorID = Default_SubcontractorID;
            i.EstimateItemTypeID = 1;
            i.Markup = .1;
            i.UnitOfMeasureID = Default_UnitOfMeasureID;
            i.DrawingID = Default_DrawingID;
            i.SupplierID = Default_SupplierID;
            i.MaterialID = Default_MaterialID;
            return i;
        }

        
        public List<object> cmbScheduleList()
        {
            return context.tblTender_Schedule.Where(x => x.TenderID == TenderID).OrderBy(x => x.ClientCode).ToList<object>();
        }
        public List<object> cmbEstimateItemType()
        {
            return context.tblTender_EstimateItemType.OrderBy(x => x.SortOrder).ToList<object>();
        }
        public List<object> cmbWorkGroupList()
        {
            return context.tblTender_WorkGroup.Where(x => x.TenderID == TenderID).OrderBy(x => x.SortOrder).ToList<object>();
        }
        public List<object> cmbSubContractorList()
        {
            return context.tblTender_Subcontractor.Where(x => x.TenderID == TenderID).OrderBy(x => x.SortOrder).ToList<object>();
        }
        public List<object> cmbUnitOfMeasureList()
        {
            return context.tblTender_UnitOfMeasure.Where(x => x.TenderID == TenderID).OrderBy(x => x.SortOrder).ToList<object>();
        }
        public List<object> cmbDrawingList()
        {
            return context.tblTender_Drawing.Where(x => x.TenderID == TenderID).OrderBy(x => x.SortOrder).ToList<object>();
        }
        public List<object> cmbMaterialList()
        {
            return context.tblTender_Material.Where(x => x.TenderID == TenderID).OrderBy(x => x.SortOrder).ToList<object>();
        }
        public List<object> cmbSupplierList()
        {
            return context.tblTender_Supplier.Where(x => x.TenderID == TenderID).OrderBy(x => x.SortOrder).ToList<object>();
        }

    }
}
