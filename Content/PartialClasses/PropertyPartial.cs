using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BootstrapVillas.Models;
using System.Data.Entity;
using System.Linq.Dynamic;
using BootstrapVillas.Content.Classes;

namespace BootstrapVillas.Models
{
    public partial class Property /*: IComparable<Property>*/
    {
        //find the lowest price, compare to other lowest price
       /* public int CompareTo(Property otherProperty)
        {
            if (this.PropertyPricings.Min(x => x.Price) > otherProperty.PropertyPricings.Min(x => x.Price))
            {
                return 1;
            }
            else if (this.PropertyPricings.Min(x => x.Price) <    otherProperty.PropertyPricings.Min(x => x.Price))
            {
                return -1;
            }
            else return 0;
        }*/

        public PriceRange PriceRange = null;

        
       
        public Property()
        {
            this.Bookings = new List<Booking>();
            this.Comments = new List<Comment>();
            this.Packages = new List<Package>();
            this.PropertyEntities = new List<PropertyEntity>();
            this.PropertyPricingSeasonalInstances = new List<PropertyPricingSeasonalInstance>();
            this.PropertySecurityItems = new List<PropertySecurityItem>();
            this.PropertyStaffTaskAssignments = new List<PropertyStaffTaskAssignment>();

            //set up datasets
            using (var _db = new PortugalVillasContext())
            {
                var pricesForThisProp = _db.PropertyPricingSeasonalInstances.Where(x => x.PropertyID == this.PropertyID).ToList();
                if (pricesForThisProp.Count() > 0)
                { this.PropertyPricingSeasonalInstances = _db.PropertyPricingSeasonalInstances.Where(x => x.PropertyID == this.PropertyID).ToList(); }
            }
            

            switch (this.Active)
            {
                case true:
                    CreatePriceRange();
                    break;
            }
        }

        /////////////////////////////
        //// ENUMS
        /////////////////////////////


        public enum PropertyVariables
        {
            MaxSleeps,
            Bedrooms,
            Bathrooms,
        };


        /////////////////////////////
        //// INSTANCE METHODS
        /////////////////////////////


        //this intialised the pricerange fr the property
        public void CreatePriceRange()
        {
           
            PortugalVillasContext _db = new PortugalVillasContext();

            var checkForPricings = _db.PropertyPricingSeasonalInstances
                .Where(x => x.PropertyID == PropertyID)
                .ToList();

            if(checkForPricings.Count > 1)
            {
            this.PriceRange = GetPriceRangeForProperty(this.PropertyID);
            }

            
        }


        
        /////////////////////////////
        //// STATIC METHODS
        /////////////////////////////
        
        //INT or STRING methods to get a list of properties- all return List<Property>

        //////////////////////////////////////////////////////////
        ////These methods pull back Properties based on given criteria
        //////////////////////////////////////////////////////////
       
       public static List<Property> GetAllProperties()
       {

           PortugalVillasContext _db = new PortugalVillasContext();

           var allProperties = _db.Properties.ToList();

           return allProperties;

       }


       public static List<Property> GetAllActiveProperties()
       {

           PortugalVillasContext _db = new PortugalVillasContext();

           var allProperties = _db.Properties.Where(x=>x.Active == true).ToList();

           return allProperties;

       }

       public static Property GetPropertyByID (long propertyID)
       {

           PortugalVillasContext _db = new PortugalVillasContext();

           Property aProperty = _db.Properties.Find(propertyID);

           return aProperty;
       }
        
        public static List<Property> GetPropertyByVacationType(int vacationTypeID)
        {
            PortugalVillasContext _db = new PortugalVillasContext();
            List<Property> propertyList;

            propertyList = (from props in _db.Properties
                                where props.PropertyVacationTypeID == vacationTypeID
                                select props).ToList();

            return propertyList;
        }


       public static Property GetPropertyByLegacyReference(string legacyReference)
       {
           PortugalVillasContext _db = new PortugalVillasContext();
           List<Property> propertyList;

           Property theProperty = (from props in _db.Properties
                                   where props.LegacyReference == legacyReference
                                   select props).FirstOrDefault();

           return theProperty;
       }


       public static List<Property> GetPropertyByOwnerID(int ownerID)
       {
           PortugalVillasContext _db = new PortugalVillasContext();
           List<Property> propertyList;

           propertyList = (from props in _db.Properties
                           where props.PropertyOwnerID == ownerID
                           select props).ToList();

           return propertyList;
       }

       public static List<Property> GetPropertyByOwnerRepresentativeID(int ownerRepresentativeID)
       {
           PortugalVillasContext _db = new PortugalVillasContext();
           List<Property> propertyList;

           propertyList = (from props in _db.Properties
                           where props.PropertyOwnerRepresentativeID == ownerRepresentativeID
                           select props).ToList();

           return propertyList;
       }


       public static List<Property> GetPropertyByTownID(int townID)
       {
           PortugalVillasContext _db = new PortugalVillasContext();
           List<Property> propertyList;

           propertyList = (from props in _db.Properties
                           where props.PropertyTownID == townID
                           select props).ToList();

           return propertyList;
       }



       public static List<Property> GetPropertyByMaxGuests (int maxGuests)
       {
           PortugalVillasContext _db = new PortugalVillasContext();
           List<Property> propertyList;

           propertyList = (from props in _db.Properties
                           where  props.MaxGuests <= maxGuests
                           select props).ToList();

           return propertyList;
       }

        

       public static List<Property> GetPropertyBySwimmingPool(string yesOrNo)
       {
           PortugalVillasContext _db = new PortugalVillasContext();
           List<Property> propertyList;

           propertyList = (from props in _db.Properties
                           where props.SwimmingPool == yesOrNo.ToString()
                           select props).ToList();

           return propertyList;
       }

       public static List<Property> GetPropertyBySwimmingPoolType(string swimmingPoolType)
       {
           PortugalVillasContext _db = new PortugalVillasContext();
           List<Property> propertyList;

           propertyList = (from props in _db.Properties
                           where props.SwimmingPoolType == swimmingPoolType.ToString()
                           select props).ToList();

           return propertyList;
       }


        ///////////////////////////////////////////
        ////Static Methods for binding to Dropdown/other list types
        //////////////////////////////////////////
       
        //get pool types
        
        public static List<string> GetSwimmingPoolTypes()
       {
           PortugalVillasContext _db = new PortugalVillasContext();
           List<string> swimmingPoolTypeList = new List<string>();

           var poolList = (from props in _db.Properties
                           where props.SwimmingPoolType != null && props.SwimmingPoolType != ""
                           select props.SwimmingPoolType).Distinct();

           foreach (var poolType in poolList)
           {
               if (poolType != null && poolType != "")
               {
                   swimmingPoolTypeList.Add(poolType);
               }
               else
               {
                   swimmingPoolTypeList.Add("NULL was returned from GetSwimmingPoolTypes()");
               }
           }

           return swimmingPoolTypeList;
       }

        /////get property vacation types
        /// <summary>
        /// RETURNS LINKED LIST
        /// </summary>
        /// <returns>linked list</returns>
        /// 
        public static LinkedList<PropertyVacationType> GetPropertyVacationTypes()
        {
            PortugalVillasContext _db = new PortugalVillasContext();
            LinkedList<PropertyVacationType> vacationTypeList = new LinkedList<PropertyVacationType>();

            var vType = (from vacType in _db.PropertyVacationTypes
                         orderby vacType.PropertyVacationTypeID descending
                         select vacType);


            foreach (var propertyVacationType in vType)
            {
                //add the latest key to the earliest element so every time you add a new one, it becomes the earliest, 
                //ending up with the first being the first
                vacationTypeList.AddFirst(propertyVacationType);

            }

            return vacationTypeList;
        }


        //gets a list of all propertyIDs
        public static IEnumerable<long> GetAllPropertyIDs()
        {
            PortugalVillasContext _db = new PortugalVillasContext();

            IEnumerable<long> propertyIDs = from properties in _db.Properties
                                            select properties.PropertyID;

            return propertyIDs;
        }


        //Get by property
        public static PriceRange GetPriceRangeForProperty(Property aProperty)
        {
           PortugalVillasContext _db = new PortugalVillasContext();

           long propertyid = aProperty.PropertyID;

           var thePricingMax = _db.PropertyPricingSeasonalInstances
                .Where(x => x.PropertyID == propertyid) 
                .Select(x => x.Price).Max();

           var thePricingMin = _db.PropertyPricingSeasonalInstances
               .Where(x => x.PropertyID == propertyid) 
              .Select(x => x.Price).Min();

            PriceRange thePriceRange = new PriceRange()
            {
                //it is DAY pricing so divide by seven
                MinPrice = (thePricingMin),
                MaxPrice = (thePricingMax),
                PropertyID = propertyid
        
            };


            return thePriceRange;

            }

        //1st Overload of GetPriceRangeForProperty
        public static PriceRange GetPriceRangeForProperty(long aPropertyID)
        {
            PortugalVillasContext _db = new PortugalVillasContext();

            long propertyid = aPropertyID;

            var thePricingMax = _db.PropertyPricingSeasonalInstances
                 .Where(x => x.PropertyID == propertyid)
                 .Select(x => x.Price).Max();

            var thePricingMin = _db.PropertyPricingSeasonalInstances
                .Where(x => x.PropertyID == propertyid)
               .Select(x => x.Price).Min();

            PriceRange thePriceRange = new PriceRange()
            {
                //it is weekly pricing
                MinPrice = (thePricingMin),
                MaxPrice = (thePricingMax),
                PropertyID = propertyid

            };


            return thePriceRange;

        }

        public static List<PriceRange> GetPriceRangesForAllProperties()
        {
            PortugalVillasContext _db = new PortugalVillasContext();

            List<PriceRange> thePriceRanges = new List<PriceRange>();

            foreach (var propertyID in GetAllPropertyIDs())
            {

                var thePricingMax = _db.PropertyPricingSeasonalInstances
                .Where(x => x.PropertyID == propertyID)
                .Select(x => x.Price).Max();

                var thePricingMin = _db.PropertyPricingSeasonalInstances
               .Where(x => x.PropertyID == propertyID)
               .Select(x => x.Price).Min();

                thePriceRanges.Add(new PriceRange()
                    {
                        //it is DAY pricing so divide by seven
                        MinPrice = (thePricingMin),
                        MaxPrice = (thePricingMax),
                        PropertyID = propertyID
                    });

            }
            

            
            return thePriceRanges;
        }




       


        //this returns the max of any one variable so you can then bind a list or whatever to it
        public static int? GetHighestNoOfPropertyVariable(PropertyVariables valueToReturn)
        {
            PortugalVillasContext _db = new PortugalVillasContext();
            int? result = 0;


            switch (valueToReturn)
            {
                case PropertyVariables.MaxSleeps:
                    result = _db.Properties
                        //hotels have stupid numbers
                        .Where (x => x.PropertyTypeID != 2)
                        .Select(x => x.MaxGuests).Max();
                    break;
                case PropertyVariables.Bedrooms:
                    result = _db.Properties
                        .Where(x => x.PropertyTypeID != 2)
                        .Select(x => x.Bedrooms).Max();
                        break;
                case PropertyVariables.Bathrooms:
                    result = _db.Properties
                        .Where(x => x.PropertyTypeID != 2)
                        .Select(x => x.Bathrooms).Max();
                    break;
                default:
                    result = -1;
                    break;
            } 

            return result;
        }




        public static List<Property>GetFeaturedProperties()
        {
        //   
            PortugalVillasContext _db = new PortugalVillasContext();
            
            var theFeaturedProperties = _db.Properties
                .Where(x => x.IsFeatured == true)
                .ToList();

            List<Property> returnProperties = (List<Property>)theFeaturedProperties;

            return returnProperties;
        }


        public static decimal CalculateBreakageDeposit(Property aProperty)
        {
            var brDeposit = 0.00M;

                if( (aProperty.PropertyTypeID == 5 || aProperty.PropertyTypeID == 1) && aProperty.Bedrooms <= 2 )
                {brDeposit = 200.00M;}
                else if( (aProperty.PropertyTypeID == 5 || aProperty.PropertyTypeID == 1 ) && (aProperty.Bedrooms >= 3 && aProperty.Bedrooms <= 4) )
                {brDeposit = 300.00M;}
                else if( (aProperty.PropertyTypeID == 5 || aProperty.PropertyTypeID == 1 ) && (aProperty.Bedrooms >= 5 && aProperty.Bedrooms <= 6) )
                {brDeposit = 400.00M;}
                else if(aProperty.PropertyTypeID == 4)
                    {brDeposit = 200.00M;}


                return brDeposit;
        }
     
        
        public static decimal CalculateDeposit(Property aProperty)
        {
            var deposit = 0.00M;

            if(aProperty.Bedrooms <= 2 )
                {deposit = 200.00M;}
            else if(aProperty.Bedrooms >= 3 )
                {deposit = 300.00M;}

            return deposit;
        }


        
        /////////////////////////////
            //// END STATIC METHODS
            /////////////////////////////


      



        }
}