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

    public partial class tblTender_Drawing: ModelBase
    {
        public double Used
        {
            get
            {
                return ((tblTender_EstimateItem != null) ? tblTender_EstimateItem.Count(x => x.DrawingID == DrawingID) : 0);
            }
          
        }

    }

    public partial class tblTender_ObjectMaterial:ModelBase
    {
        public double Total
        {
            get { return Quantity * UnitCost * (1+Markup); }
        }

    }

    public partial class tblTender_EstimateItem : ModelBase
    {
      
        public bool IsHeader
        {
            get
            {
                return (EstimateItemTypeID == 3);
            }
        }
        public int Level
        {
            get
            {
                return ( (tblTender_Schedule != null)? tblTender_Schedule.ClientCode.Length:0);
               
            }
        }
        public double? WorkGroupRate
        {
            get
            {
                double? value = null;
                switch (EstimateItemTypeID)
                {
                    case 1: if (tblTender_WorkGroup != null)  value = tblTender_WorkGroup.vwTender_EstimateWorkGroupRate.Rate;
                        break;
                    default: value = null;
                        break;
                }
                return value;
            }

        }
        public double TotalLabourHours
        {
            get
            {
                double value = 0;
                switch (EstimateItemTypeID)
                {
                    case 1: value = Men * HoursPerDay * Days * Quantity;
                        break;
                    default: value = 0;
                        break;
                }
                return value;
            }

        }

        public double TotalCost
        {
            get 
            {
                double value = 0;
                switch (EstimateItemTypeID)
                {
                    case 1: if (tblTender_WorkGroup!=null) value = Men * HoursPerDay * Days *Quantity * tblTender_WorkGroup.vwTender_EstimateWorkGroupRate.Rate ;
                        break;
                    case 2:
                    case 4:
                        value = Cost* (1 + Markup);
                        break;
                    case 3:
                        value = 0;
                        break;
                    
                }
                return value; 
            }
        }
        public double Weight
        {
            get
            {
                double value = 0;
                switch (EstimateItemTypeID)
                {
                   
                    case 1:
                    case 2:
                    case 3:
                        value = 0;
                        break;
                    case 4:
                        if (tblTender_Material != null) value = Quantity * Length * Width * tblTender_Material.KG;
                        break;

                }
                return value;
            }
        }

        public double MetresSquared
        {
            get
            {
                double value = 0;
                switch (EstimateItemTypeID)
                {
                    case 4: if (tblTender_Material != null) value = Quantity * Length * Width * tblTender_Material.SQM;
                        break;
                    case 1:
                    case 2:
                    case 3:
                        value = 0;
                        break;

                }
                return value;
            }
        }


    }
    public partial class tblTender_WorkGroup : ModelBase
    {
        public double WorkGroupOverhead
        {
            get
            {
                if (vwTender_EstimateWorkGroupRate == null)
                    return 0;
                else
                    return vwTender_EstimateWorkGroupRate.WorkGroupOverhead;
            }
        }
        public double FuelOverhead
        {
            get
            {
                if (vwTender_EstimateWorkGroupRate == null)
                    return 0;
                else
                    return vwTender_EstimateWorkGroupRate.FuelOverhead;
            }
        }
        public double AddtionalHoursOverhead
        {
            get
            {
                if (vwTender_EstimateWorkGroupRate == null)
                    return 0;
                else
                    return vwTender_EstimateWorkGroupRate.AddtionalHoursOverhead;
            }
        }
        public double TotalOverhead
        {
            get 
            {
                if (vwTender_EstimateWorkGroupRate == null)
                    return 0;
                else
                    return vwTender_EstimateWorkGroupRate.TotalOverhead; 
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

        public double TotalAdditionalHours
        {
            get
            {
                if (vwTender_EstimateWorkGroupRate == null)
                    return 0;
                else
                    return vwTender_EstimateWorkGroupRate.AdditionalHours;
            }
        }

    }
    public partial class tblTender_WorkgroupFuel : ModelBase
    {
        public double TotalForProject
        {
            get { return Count * Week  * HoursPerWeek * LitrePerHour * .6; }
        }

        public double CostTotalPerItem
        {
            get { return Count * Week  * HoursPerWeek * LitrePerHour * .6 * CostPerLitre; }
        }

        public double FuelCost
        {
            get { return Count * Week  * HoursPerWeek * LitrePerHour * .6 * CostPerLitre * 0.825; }
        }
    }
    
    
    
     public partial class tblTender_WorkgroupAdditionalHours: ModelBase
    {
        public double BaseRate
        {
            get {return (tblTender_WorkGroup!=null?tblTender_WorkGroup.BaseRate:0);}
                
        }

         public double Total
         {
             get { return (tblTender_WorkGroup!=null?tblTender_WorkGroup.BaseRate * Count * Hours:0); }

        }
     
    }


    public partial class tblTender_OverheadItem : ModelBase
    {
        public double Hours
        {
            get
            {
                return (tblTender_WorkGroup!=null?tblTender_WorkGroup.TotalHours + tblTender_WorkGroup.TotalAdditionalHours:0);
            }
        }
        public double Total
        {
            get
            {
                if (OverheadTypeID == 1)
                {
                    return Count * Duration * Rate;
                }
                else
                {
                    return (tblTender_WorkGroup!=null?Count * Rate * tblTender_WorkGroup.TotalHours:0);
                }
            }
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
                return (tblTender_WorkGroup != null ? tblTender_WorkGroup.vwTender_EstimateWorkGroupRate.Rate * Hours * Quantity * Men * Days : 0);
            }
        }
        public double Rate
        {
            get
            {
                return (tblTender_WorkGroup != null ? tblTender_WorkGroup.vwTender_EstimateWorkGroupRate.Rate : 0);
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
