using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Web;
using BootstrapVillas.Models;
using System.Data.Entity;


namespace BootstrapVillas.Content.Classes
{
    public class PriceRange
    {
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        
        public long? PropertyID { get; set; }
     


        //const - must have min and max to be a range
        public PriceRange(decimal? minPrice, decimal? maxPrice, long? propertyID = 0)
        {
            MinPrice = minPrice;
            MaxPrice = maxPrice;

            if (propertyID != 0)
            {
                PropertyID = propertyID;
                
            }

        }

        public PriceRange()
            {
            //for dynamic linq object creation
            }

        //////////////////OVERALL IMPLEMENATION NOTES ON METHODS/////////////////
        /// 
        /// 
        /*
         * passesdRange1 is what I use as the customer or form range
         * comparisonRange2 is what I generally get from the database PropertyPricing table

        */

        ///////////END NOTES



        /*
        need to check against a specific property's overall range, 
        if there is no booking dates, or a range withing certain months if there are
        */

        //1st method
        public static bool IsPropertyWithinPriceRange(Property aProperty, PriceRange customerRange)
        {
            //check the passed property has has a price range, if not create one
            if(aProperty.PriceRange == null)
            aProperty.CreatePriceRange();

            //need to check AGAIN if a pricerange has been created - if there's nothing in the DB, it won't be
            if (aProperty.PriceRange != null)
                //this is saying - return true if the result is not null, else return false.
                //LOOK HOW VARS ARE PASSED
                return IsPriceWithinRange(customerRange, aProperty.PriceRange) != null;
            else
            {
                return false;
            }
        }


        public static PriceRange IsPriceWithinRange(PriceRange customerRequestedRange, PriceRange propertyRange)
        {
            if (customerRequestedRange.MinPrice <= propertyRange.MaxPrice)
            {
                if (customerRequestedRange.MaxPrice >= propertyRange.MinPrice)
                {
                    return propertyRange;
                }
            }
            //check one range against another and return the Range if it's within it (i.e. 'ok')
            
            //check the min is higher or equal to  the passed in min

            //if so, check the max is lower or equal to the passed in max

            //if that passes, return the second range
            return null;
        }

        //1st overload - works for a list of ranges, compares every one and returns them in a list if they are within the range of 'passedrange1'
        public static List<PriceRange> IsPriceWithinRange( PriceRange customerRequestedRange, 
            List<PriceRange> comparisonRange2)
          {

            List<PriceRange> pricesInRange = new List<PriceRange>();


            foreach(PriceRange priceRange in comparisonRange2)
            {
                if (IsPriceWithinRange(customerRequestedRange, priceRange) != null)
                {
                pricesInRange.Add(IsPriceWithinRange(customerRequestedRange, priceRange));
                }
                    
            }

            return pricesInRange;

          }
       
     


       



         


    }
}