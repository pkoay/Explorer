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
        public override string Identification()
        {
            return "(ID:" + this.ContractorID.ToString() + ") Title:" + this.Title;
        }
    }

}