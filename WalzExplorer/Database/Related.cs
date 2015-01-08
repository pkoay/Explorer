using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;


namespace WalzExplorer.Database
{

    public partial class tblTender_Contractor
    {
        public override  Dictionary<string, int> RelatedInformation (WalzExplorerEntities context)
        {
          
            
            Dictionary<string, int> rel = new Dictionary<string, int>();
            rel.Add("Activity Contractors", context.Entry(this).Collection(b => b.tblTender_ActivityContractor).Query().Count());

            return rel;
  
        }
    }

}