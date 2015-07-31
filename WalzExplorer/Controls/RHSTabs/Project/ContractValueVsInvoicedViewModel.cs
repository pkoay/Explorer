using System;
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
    public class ContractValueVsInvoicedViewModel 
    {
        public ObservableCollection<spWEX_RHS_Project_ContractValueVsInvoiced_Result> data;
        public WalzExplorerEntities context = new WalzExplorerEntities(false);
        int ProjectID;


        public ContractValueVsInvoicedViewModel(WEXSettings settings) 
        {
            ProjectID = ConvertLibrary.StringToInt(settings.node.FindID("PROJECT", "-2"), -1);
            data = new ObservableCollection<spWEX_RHS_Project_ContractValueVsInvoiced_Result>(context.spWEX_RHS_Project_ContractValueVsInvoiced(ProjectID).OrderBy(x=>x.ProjID));    
        }
    }
}
