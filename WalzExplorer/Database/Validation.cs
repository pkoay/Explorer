using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;


namespace WalzExplorer.Database
{

    

    public partial class tblTender_Subcontractor
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


                if (columnName == "SubcontractorTypeID")
                {
                    if (this.SubcontractorTypeID<1)
                    {
                        return "Subcontractor Type cannot be blank!!!";
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

    public partial class tblTender_SubcontractorType
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
