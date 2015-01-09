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
        public string this[string columnName]
        {
            get{return null;}
        }
    }

    public partial class tblPerson_Person
    {
        public string this[string columnName]
        {
            get { return null; }
        }
    }

    public partial class tblTender_ContractorType
    {
        public string this[string columnName]
        {
            get { return null; }
        }
    }
    public partial class tblWEX_RHSTab
    {
        public string this[string columnName]
        {
            get { return null; }
        }
    }
    public partial class tblWEX_RHSTab_Security
    {
        public string this[string columnName]
        {
            get { return null; }
        }
    }
    public partial class tblTender_Drawing
    {
        public string this[string columnName]
        {
            get { return null; }
        }
    }
    public partial class tblTender_Item
    {
        public string this[string columnName]
        {
            get { return null; }
        }
    }
    public partial class tblTender_Material
    {
        public string this[string columnName]
        {
            get { return null; }
        }
    }
    public partial class tblTender_Status
    {
        public string this[string columnName]
        {
            get { return null; }
        }
    }
    public partial class tblWEX_NTSecurityGroup
    {
        public string this[string columnName]
        {
            get { return null; }
        }
    }
    public partial class tblTender_Step
    {
        public string this[string columnName]
        {
            get { return null; }
        }
    }
    public partial class tblTender_Supplier
    {
        public string this[string columnName]
        {
            get { return null; }
        }
    }
    public partial class tblWEX_TreeNodeType
    {
        public string this[string columnName]
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
    public partial class tblTender_WorkGroup
    {
        public string this[string columnName]
        {
            get { return null; }
        }
    }
    public partial class tblTender_WorkGroupHeader
    {
        public string this[string columnName]
        {
            get { return null; }
        }
    }
    public partial class tblWEX_Tree
    {
        public string this[string columnName]
        {
            get { return null; }
        }
    }
    public partial class tblWEX_LHSTab
    {
        public string this[string columnName]
        {
            get { return null; }
        }
    }
    public partial class tblTender
    {
        public string this[string columnName]
        {
            get { return null; }
        }
    }
    public partial class tblTender_Activity
    {
        public string this[string columnName]
        {
            get { return null; }
        }
    }
    public partial class tblTender_ActivityChildActivity
    {
        public string this[string columnName]
        {
            get { return null; }
        }
    }
    public partial class tblTender_LabourStandard
    {
        public string this[string columnName]
        {
            get { return null; }
        }
    }
    public partial class tblTender_ActivityContractor
    {
        public string this[string columnName]
        {
            get { return null; }
        }
    }
    public partial class tblTender_Supplier_Material
    {
        public string this[string columnName]
        {
            get { return null; }
        }
    }
    public partial class tblTender_UnitOfMeasure
    {
        public string this[string columnName]
        {
            get { return null; }
        }
    }
    public partial class tblTender_ActivityLabour
    {
        public string this[string columnName]
        {
            get { return null; }
        }
    }
    public partial class tblTender_ActivityMaterial
    {
        public string this[string columnName]
        {
            get { return null; }
        }
    }
    public partial class tblTender_WorkGroupItem
    {
        public string this[string columnName]
        {
            get { return null; }
        }
    }
    public partial class tblTender_Schedule
    {
        public string this[string columnName]
        {
            get { return null; }
        }
    }

}
