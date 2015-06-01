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
       public override string this[string columnName]
        {
            get
            {
                hasError = true;
                if (columnName == "Title")
                {
                    if (this.Title==null ||this.Title.Length < 2)
                    {
                        
                        return "Title cannot be blank!!!";
                    }
                }
                

                if (columnName == "ContractorTypeID")
                {
                    if (this.ContractorTypeID<1)
                    {
                        return "Contractor Type cannot be blank!!!";
                    }
                }
                hasError = false;
                return null;
            }
        }
    }

    public partial class tblProject_Project
    {
        public  string this[string columnName]
        {
            get{return null;}
        }
    }

    public partial class tblPerson_Person
    {
        public  string this[string columnName]
        {
            get { return null; }
        }
    }

    public partial class tblTender_ContractorType
    {
        public override string this[string columnName]
        {
            get { return null; }
        }
    }
    public partial class tblWEX_RHSTab
    {
        public override string this[string columnName]
        {
            get { return null; }
        }
    }
    public partial class tblWEX_RHSTab_Security
    {
        public  string this[string columnName]
        {
            get { return null; }
        }
    }
    public partial class tblTender_Drawing
    {
        public override string this[string columnName]
        {
            get { return null; }
        }
    }
    public partial class tblTender_Item
    {
        public override string this[string columnName]
        {
            get { return null; }
        }
    }
    public partial class tblTender_Material
    {
        public override string this[string columnName]
        {
            get { return null; }
        }
    }
    public partial class tblTender_Status
    {
        public override string this[string columnName]
        {
            get { return null; }
        }
    }
    public partial class tblWEX_NTSecurityGroup
    {
        public override string this[string columnName]
        {
            get { return null; }
        }
    }
    public partial class tblTender_Step
    {
        public override string this[string columnName]
        {
            get { return null; }
        }
    }
    public partial class tblTender_Supplier
    {
        public override string this[string columnName]
        {
            get { return null; }
        }
    }
   
    public partial class tblWEX_TreeNodeType_RHSTab
    {
        public string this[string columnName]
        {
            get { return null; }
        }
    }
 

    public partial class tblWEX_Tree
    {
        public override string this[string columnName]
        {
            get { return null; }
        }
    }
    public partial class tblWEX_LHSTab
    {
        public override string this[string columnName]
        {
            get { return null; }
        }
    }
    public partial class tblTender
    {
        public override string this[string columnName]
        {
            get { return null; }
        }
    }


   

   
    public partial class tblTender_UnitOfMeasure
    {
        public override string this[string columnName]
        {
            get { return null; }
        }
    }

    public partial class tblTender_Schedule
    {
        public override string this[string columnName]
        {
            get { return null; }
        }
    }

}
