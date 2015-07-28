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
    public class OverheadDetailViewModel :GridEditViewModelBase2
    {
        int WorkGroupID;
        int TenderID;

        public OverheadDetailViewModel(WEXSettings settings) //(string NodeType, string PersonID, int Id)
        {
            TenderID = ConvertLibrary.StringToInt(settings.node.FindID("TENDER", "-2"), -1);
            WorkGroupID = ConvertLibrary.StringToInt(settings.node.FindID("TENDER_WORKGROUP", "-2"), -1);
            data = new ObservableCollection<ModelBase>(context.tblTender_OverheadItem.Where(x => x.WorkGroupID == WorkGroupID).OrderBy(x => x.tblTender_OverheadGroup.SortOrder).ThenBy(x=>x.SortOrder));  
        }

        public override ModelBase DefaultItem(ModelBase NearItem)
        {
            tblTender_OverheadItem i = new tblTender_OverheadItem();
            i.WorkGroupID = WorkGroupID;
            i.OverheadTypeID = 1;
            return i;
        }
        public List<object> cmbOverheadGroupList()
        {
            return context.tblTender_OverheadGroup.Where(x => x.TenderID == TenderID).OrderBy(x => x.SortOrder).ToList<object>();
        }

        public List<object> cmbOverheadTypeList()
        {
            return context.tblTender_OverheadType.OrderBy(x => x.SortOrder).ToList<object>();
        }
        //public Expression expTotal ()
        //{
        //    Expression<Func<tblTender_OverheadItem, double>> expression = x => (double) x.Count*x.Duration*x.Rate;

        //    return expression;
        //}
        //public AggregateFunction expTotalAgg()
        //{
        //    AggregateFunction<tblTender_OverheadItem, double> aggregate = new AggregateFunction<tblTender_OverheadItem, double>
        //    {
        //        AggregationExpression = tblTender_OverheadItem => tblTender_OverheadItem.Sum(x => x.Count * x.Duration * x.Rate),
        //        Caption = "="
        //    };


        //    return aggregate;
        //}
      
    }
}
