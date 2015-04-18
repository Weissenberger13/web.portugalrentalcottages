using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace BootstrapVillas.Models
{
    public partial class PropertyPricingSeasonalInstance   : IComparable<PropertyPricingSeasonalInstance>
    {



        public static List<PropertyPricingSeasonalInstance> GetPricingByPropertyID(long? propertyID)
        {
            PortugalVillasContext _db = new PortugalVillasContext();


            var thePriceRange = _db.PropertyPricingSeasonalInstances.Where(x => x.PropertyID == propertyID).Include(r=>r.PropertyPricingSeason).ToList();

            return thePriceRange;
        }


      
    
        //if lowest pr
        public int CompareTo(PropertyPricingSeasonalInstance pp)
        {
            if (this.Price > pp.Price)
            {
                return 1;
            }
            else if (this.Price < pp.Price)
            {
                return -1;
            }
            else return 0;
        }

    }
}

