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
    public class EstimateItemViewModel : GridEditViewModelBase
    {
        int TenderID;
        int Default_SubContractorID;
        int Default_WorkGroupID;
        int Default_UnitOfMeasureID;

        public EstimateItemViewModel(WEXSettings settings) //(string NodeType, string PersonID, int Id)
        {
            TenderID = ConvertLibrary.StringToInt(settings.node.FindID("TENDER", "-2"), -1);
            data = new ObservableCollection<ModelBase>(context.tblTender_EstimateItem.Where(x => x.TenderID == TenderID).OrderBy(x =>x.ScheduleID).ThenBy(x=>x.SortOrder));
            Default_SubContractorID = context.tblTender_Contractor.Where(x => x.TenderID == TenderID).OrderBy(x => x.SortOrder).First().ContractorID;
            Default_WorkGroupID = context.tblTender_WorkGroup.Where(x => x.TenderID == TenderID).OrderBy(x => x.SortOrder).First().WorkGroupID;
            Default_UnitOfMeasureID = context.tblTender_UnitOfMeasure.Where(x => x.TenderID == TenderID).OrderBy(x => x.SortOrder).First().UnitOfMeasureID;
            
        }

        public override ModelBase DefaultItem(ModelBase NearItem)
        {
            tblTender_EstimateItem nearItem = (tblTender_EstimateItem)NearItem;

            tblTender_EstimateItem i = new tblTender_EstimateItem();
            i.TenderID = TenderID;
            if (nearItem != null) i.ScheduleID = nearItem.ScheduleID;
            i.WorkGroupID = Default_WorkGroupID;
            i.SubcontractorID = Default_SubContractorID;
            i.EstimateItemTypeID = 1;
            
            i.UnitOfMeasureID = Default_UnitOfMeasureID;
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
            return context.tblTender_Contractor.Where(x => x.TenderID == TenderID).OrderBy(x => x.SortOrder).ToList<object>();
        }
        public List<object> cmbUnitOfMeasureList()
        {
            return context.tblTender_UnitOfMeasure.Where(x => x.TenderID == TenderID).OrderBy(x => x.SortOrder).ToList<object>();
        }
        public List<object> cmbDrawingList()
        {
            return context.tblTender_Drawing.Where(x => x.TenderID == TenderID).OrderBy(x => x.SortOrder).ToList<object>();
        }

    }
}
