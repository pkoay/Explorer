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
//    //public class TenderActivityMaterialViewModel : RHSTabGridViewModelBase
//    //{
//    //    //int tenderID;
//    //    //int activityID;

//    //    //public TenderActivityMaterialViewModel(WEXSettings settings)
//    //    //{
//    //    //    tenderID = ConvertLibrary.StringToInt(settings.node.FindID("TENDER", "-2"), -1);
//    //    //    activityID = ConvertLibrary.StringToInt(settings.node.FindID("ACTIVITY", "-2"), -1);
//    //    //    switch (settings.node.TypeID)
//    //    //    {
//    //    //        case "TenderActivity":
//    //    //            data = new ObservableCollection<ModelBase>(context.tblTender_ActivityMaterial
//    //    //                .Join(context.tblTender_Step, l => l.StepID, s => s.StepID, (l, s) => new { labour = l, step = s }) //join required so we can order by step order
//    //    //                .OrderBy(a => a.step.SortOrder) 
//    //    //                .Select(a => a.labour)
//    //    //                .Where(a => a.ActivityID == activityID)
//    //    //                );
                        
//    //    //            columnDefault.Clear();
//    //    //            columnDefault.Add("Activity", activityID);
//    //    //            break;
//    //    //        default: throw new NotImplementedException();
//    //    //    }
                
//    //    //}

//    //    //public override ModelBase DefaultItem()
//    //    //{
//    //    //    tblTender_ActivityMaterial i = new tblTender_ActivityMaterial();
          
//    //    //    return i;
//    //    //}
     
//    //    //public List<object> cmbStepList()
//    //    //{
//    //    //    return context.tblTender_Step.Where(a => a.TenderID == tenderID).OrderBy(a => a.SortOrder).ToList<object>();
//    //    //}
//    //    //public List<object> cmbMaterialList()
//    //    //{
//    //    //    return context.tblTender_Material.Where(a => a.TenderID == tenderID).OrderBy(a => a.SortOrder).ToList<object>();
//    //    //}
//    //    //public List<object> cmbSupplierList()
//    //    //{
//    //    //    return context.tblTender_Supplier.Where(a => a.TenderID == tenderID).OrderBy(a => a.SortOrder).ToList<object>();
//    //    //}
       
//    //}
//}
