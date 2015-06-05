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
    public class ObjectWorkGroupViewModel : Grid_Read
    {
        public ObservableCollection<vwTender_ObjectHoursByWorkGroup> data;
        public WalzExplorerEntities context = new WalzExplorerEntities(false);
        int ObjectID;

        public ObjectWorkGroupViewModel(WEXSettings settings) //(string NodeType, string PersonID, int Id)
        {
      
            ObjectID = ConvertLibrary.StringToInt(settings.node.FindID("OBJECT", "-2"), -1);
            data = new ObservableCollection<vwTender_ObjectHoursByWorkGroup>(context.vwTender_ObjectHoursByWorkGroup.Where (x=>x.ObjectID==ObjectID));
           
        }

    }
}
