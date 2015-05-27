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
//    public class TenderActivityLabourViewModel : RHSTabGridViewModelBase
//    {
//        //int tenderID;
//        //int activityID;
//        //public TenderActivityLabourViewModel(WEXSettings settings) //(WEXNode node, string PersonID, int Id)
//        //{
//        //    tenderID = ConvertLibrary.StringToInt(settings.node.FindID("TENDER", "-2"), -1);
//        //    activityID = ConvertLibrary.StringToInt(settings.node.FindID("ACTIVITY", "-2"), -1);
//        //    switch (settings.node.TypeID)
//        //    {
//        //        case "TenderActivity":
//        //            data = new ObservableCollection<ModelBase>(context.tblTender_ActivityLabour
//        //                .Join(context.tblTender_Step, l => l.StepID, s => s.StepID, (l, s) => new { labour = l, step = s }) //join required so we can order by step order
//        //                .OrderBy(a => a.step.SortOrder) 
//        //                .Select(a => a.labour)
//        //                .Where(a => a.ActivityID == activityID)
//        //                );
                        
//        //            columnDefault.Clear();
//        //            columnDefault.Add("Activity", activityID);
//        //            break;
//        //        default: throw new NotImplementedException();
//        //    }
                
//        //}

//        //public override ModelBase DefaultItem()
//        //{
//        //    tblTender_ActivityLabour i = new tblTender_ActivityLabour();
          
//        //    return i;
//        //}
//        //public List<object> cmbLabourStandardList()
//        //{
//        //    return context.tblTender_LabourStandard.Where(a=>a.TenderID==tenderID).OrderBy(a=>a.SortOrder).ToList<object>();
//        //}
//        //public List<object> cmbStepList()
//        //{
//        //    return context.tblTender_Step.Where(a => a.TenderID == tenderID).OrderBy(a => a.SortOrder).ToList<object>();
//        //}
//        //public List<object> cmbWorkgroupList()
//        //{
//        //    return context.tblTender_Workgroup.Where(a => a.TenderID == tenderID).OrderBy(a => a.SortOrder).ToList<object>();
//        //}
//        //public List<object> cmbUnitOfMeasureList()
//        //{
//        //    return context.tblTender_UnitOfMeasure.Where(a => a.TenderID == tenderID).OrderBy(a => a.SortOrder).ToList<object>();
//        //}
       
//    }
//}
