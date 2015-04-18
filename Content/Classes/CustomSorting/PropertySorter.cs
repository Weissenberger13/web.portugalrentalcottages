using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BootstrapVillas.Models;

namespace BootstrapVillas.Content.Classes.CustomSorting
{
    public class PropertySorter
    {        

        public List<Property> SortProperty(List<Property> propList, string sortOrder)
        {
            try
            {
                if (sortOrder.Equals("LH"))
                {
                    return propList.OrderBy(x => x.PropertyPricingSeasonalInstances.Min(c=>c.Price)).ToList();
                }
                else if (sortOrder.Equals("HL"))
                {
                    return propList.OrderByDescending(x => x.PropertyPricingSeasonalInstances.Min(c => c.Price)).ToList();
                }
                else if (sortOrder.Equals("StarRating"))
                {
                    return propList.OrderByDescending(x => x.PriceRange).ToList();
                }
                else if (sortOrder.Equals("Bedrooms"))
                {
                    return propList.OrderByDescending(x => x.Bedrooms).ToList();
                }
                else if (sortOrder.Equals("SwimmingPool"))
                {
                    return propList.OrderBy(x => x.SwimmingPoolType).ToList();
                }              
                else if (sortOrder.Equals("PropertyName"))
                {
                    return propList.OrderBy(x => x.PropertyName).ToList();
                }
                else if (sortOrder.Equals("PropertyTown"))
                {
                    return propList.OrderBy(x => x.PropertyTown.TownName).ToList();
                }
                    //have a default - by name
              
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
            throw new Exception("An incorrect string was passed for the sort order for SortProperty");
        }


    }
}