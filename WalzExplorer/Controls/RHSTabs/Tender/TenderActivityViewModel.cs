using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalzExplorer.Common;
using WalzExplorer.Database;

//namespace WalzExplorer.Controls.RHSTabs.Tender
//{
//    //public class TenderActivityViewModel : RHSTabGridViewModelBase
//    //{
//    //    //int tenderID;
//    //    //int activityID;
//    //    //public TenderActivityViewModel (WEXSettings settings) //(string NodeType, string PersonID, int Id)
//    //    //{
//    //    //    tenderID = ConvertLibrary.StringToInt(settings.node.FindID("TENDER", "-2"), -1);
//    //    //    activityID = ConvertLibrary.StringToInt(settings.node.FindID("ACTIVITY", "-2"), -1);

//    //    //    switch (settings.node.TypeID)
//    //    //    {
//    //    //        case "TenderActivityFolder":
//    //    //            data = new ObservableCollection<ModelBase>(context.tblTender_Activity.Where(m => m.TenderID == tenderID).OrderBy(m => m.SortOrder));
//    //    //            columnDefault.Clear();
//    //    //            columnDefault.Add("TenderID", tenderID);
//    //    //            break;
//    //    //        case "TenderActivity":
//    //    //            data = new ObservableCollection<ModelBase>(context.tblTender_Activity.Where(m => m.ActivityID == activityID));
//    //    //            break;
//    //    //        default: throw new NotImplementedException();
//    //    //    }
                
//    //    //}

//    //    //public override ModelBase DefaultItem()
//    //    //{
//    //    //    tblTender_Activity i = new tblTender_Activity();
          
//    //    //    return i;
//    //    //}
//    //    //public List<object> cmbUnitOfMeasureList()
//    //    //{
//    //    //    return context.tblTender_UnitOfMeasure.Where(a => a.TenderID == tenderID).OrderBy(a => a.SortOrder).ToList<object>();
//    //    //}
       
//    //}
//}
