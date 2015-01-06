using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;


namespace WalzExplorer.Database
{

    //[Required]
    //[StringLength(50)]
    //[Display(Name = "Last Name")]

    public class tblTender_Contractor_Validation
    {
        [Required]
        [StringLength(255)]
        public string Title;

        [Required]
        public string ContractorTypeID;

    }

}
