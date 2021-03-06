﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalzExplorer.Common;
using WalzExplorer.Database;

namespace WalzExplorer.Controls.RHSTabs.Project
{
    public class InvoicedViewModel 
    {
        public ObservableCollection<spWEX_RHS_Project_Invoiced_Result> data;
        public WalzExplorerEntities context = new WalzExplorerEntities(false);
        int ProjectID;
        

        public InvoicedViewModel (WEXSettings settings) //(string NodeType, string PersonID, int Id)
        {
            ProjectID = ConvertLibrary.StringToInt(settings.node.FindID("PROJECT", "-2"), -1);
            //CustomerID = ConvertLibrary.StringToInt(settings.node.FindID("CUSTOMER", "-2"), -1).ToString();
            //NodeTypeID = settings.node.TypeID;
            //UserPersonID=settings.user.MimicedPerson.PersonID.ToString();

            data = new ObservableCollection<spWEX_RHS_Project_Invoiced_Result>(context.spWEX_RHS_Project_Invoiced(ProjectID));    
        }
    }
}
