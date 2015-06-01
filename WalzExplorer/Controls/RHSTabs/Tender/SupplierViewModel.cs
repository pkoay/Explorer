﻿using System;
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
    public class SupplierViewModel :GridEditViewModelBase
    {
        int TenderID;
        public SupplierViewModel(WEXSettings settings) //(string NodeType, string PersonID, int Id)
        {
            TenderID = ConvertLibrary.StringToInt(settings.node.FindID("TENDER", "-2"), -1);
            data = new ObservableCollection<ModelBase>(context.tblTender_Supplier.Where(x => x.TenderID == TenderID).OrderBy(x => x.SortOrder));  
        }

        public override ModelBase DefaultItem()
        {
            tblTender_Supplier i = new tblTender_Supplier();
            i.TenderID = TenderID;
            return i;
        }
    }
}