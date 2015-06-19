using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalzExplorer.Database
{
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;


    public partial class tblTender_ObjectMaterial:ModelBase
    {
        public double Total
        {
            get { return Quantity * UnitCost * (1+Markup); }
        }

    }
    public partial class tblTender_WorkGroup : ModelBase
    {
        public double TotalOverhead
        {
            get 
            {
                if (vwTender_EstimateWorkGroupRate == null)
                    return 0;
                else
                    return vwTender_EstimateWorkGroupRate.Overhead; 
            }
        }
        public double TotalHours
        {
            get 
            { 
                if (vwTender_EstimateWorkGroupRate == null)
                    return 0;
                else
                    return vwTender_EstimateWorkGroupRate.Hours; 
            }
        }
        public double CalculatedRate
        {
            get 
            { 
                if (vwTender_EstimateWorkGroupRate == null)
                    return 0;
                else
                    return vwTender_EstimateWorkGroupRate.Rate; 
            }

        }

    }
    public partial class tblTender_OverheadItem : ModelBase
    {
        public double Total
        {
            get { return Count * Duration * Rate; }
        }
    }
    public partial class tblTender_ObjectLabour: ModelBase
    {
        public double HoursTotal
        {
            get { return Hours* Quantity * Men * Days; }
        }
        public double CostTotal
        {
            get
            {
                if (tblTender_WorkGroup == null)
                    return 0;
                else
                    return tblTender_WorkGroup.vwTender_EstimateWorkGroupRate.Rate * Hours * Quantity * Men * Days;
            }
        }
        public double Rate
        {
            get
              {
                if (tblTender_WorkGroup == null)
                    return 0;
                else
                    return tblTender_WorkGroup.vwTender_EstimateWorkGroupRate.Rate;
            }
            
        }
    }

    public partial class tblTender_ObjectContractor : ModelBase
    {
        public double Total
        {
            get { return Quantity * (1+MarkUp) * Rate; }
        }
    }
}
