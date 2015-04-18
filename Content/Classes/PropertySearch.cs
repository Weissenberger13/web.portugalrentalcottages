using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BootstrapVillas.Models;
using System.Data.Entity;
using System.Linq.Dynamic;
using BootstrapVillas.Content.Classes;


namespace BootstrapVillas.Content.Classes
{

    /// <summary>
    /// Created by Nadav Drewe
    /// This class is to be included and populated via a controller with an [HttpPost] from the search, then instance methods can be run, depending 
    /// </summary>

    public class PropertySearch
    {
                public PriceRange TheSearchPriceRange { get; set;}
                public long? PropertyTypeID { get; set; }
                public long? VacationTypeID { get; set; }
                public String PoolType { get; set; }
                public int MaxSleeps { get; set; }

                [Display(Name = "NoOfAdults")]
              
                public int NoOfAdults { get; set; }

                [Display(Name = "NoOfAdults")]
      
                public int NoOfChildren { get; set; }
                public int NoOfInfants { get; set; }

                private IQueryable<Property> propertyQuery;
                

        //end props



        public PropertySearch()
        {
            //does nothing 
        }


        //constructor
        public PropertySearch(PriceRange theSearchPriceRange  = null, long propertyTypeID = 0, long vacationTypeID = 0, string poolType = null, int maxSleeps = 0, int noOfAdults = 0, int noOfChildren = 0, int noOfInfants = 0)
        {
            
            //assign params to members
            this.TheSearchPriceRange = theSearchPriceRange;
            this.PropertyTypeID = propertyTypeID;
            this.VacationTypeID = vacationTypeID;
            this.PoolType = poolType;
            this.MaxSleeps = maxSleeps;
            this.NoOfAdults = noOfAdults;
            this.NoOfChildren = noOfChildren;
            this.NoOfInfants = noOfInfants;

            //build query based on params passed
            //if not, just query properties
            
        }

        //helper method for building the query - if the passed param not null 
        //(object or string or zero (int or long or decimal)  the
        
        
        public List<Property> SearchForMatchingProperties()
        {
            PortugalVillasContext _db = new PortugalVillasContext();

            this.propertyQuery = _db.Properties.Include(x=>x.PropertyTown);

            propertyQuery = propertyQuery.Where(x => x.Active == true);

           //test if each the object is not null or zero then add a where clause
            if(this.PropertyTypeID.HasValue && this.PropertyTypeID != 0)
            {
                propertyQuery = propertyQuery.Where(x => x.PropertyTypeID == this.PropertyTypeID);
            }

            if(this.VacationTypeID.HasValue && this.VacationTypeID != 0)
            {
                propertyQuery = propertyQuery.Where(x => x.PropertyVacationTypeID == this.VacationTypeID);
            }

            if(!String.IsNullOrEmpty(this.PoolType))
            {
                propertyQuery = propertyQuery.Where(x => x.SwimmingPoolType == this.PoolType);
            }

            if (this.MaxSleeps != 0)
            {
                propertyQuery = propertyQuery.Where(x => x.MaxGuests >= this.MaxSleeps);
            }
                //implicit zero
            else if (MaxSleeps == 0 && ((NoOfAdults + NoOfChildren + NoOfInfants) > 0))
            {
                //there's some people, need a house big enough for them AND infants if they have
                if (NoOfInfants > 0)
                {

                    //add infants claaus
                    propertyQuery = propertyQuery.Where(x => x.Cots > 0);

                }


                //add where clause for total people
                var totalPeople = NoOfAdults + NoOfChildren + NoOfInfants;
                propertyQuery = propertyQuery.Where(x => x.MaxGuests >= totalPeople - NoOfInfants);

            }





            //run the search and bring back some properties
            List<Property> theResultsList = new List<Property>();
            theResultsList = propertyQuery.ToList();



            if (this.TheSearchPriceRange != null) //check it's not null i.e. that there was a price range passed
            {
                if ((this.TheSearchPriceRange.MaxPrice != 2000 | this.TheSearchPriceRange.MinPrice != 1))
                {
                    //we want to search on price because a value has been provided, so create price ranges for active properties 
                    //NB - NOW done in PriceRange.IsPropertyWithinPriceRange


                    List<Property> theResultsListWithPrice = new List<Property>();


                    foreach (var property in theResultsList)
                    {
                        if (PriceRange.IsPropertyWithinPriceRange(property, TheSearchPriceRange))
                            theResultsListWithPrice.Add(property);
                    }


                    return theResultsListWithPrice;
                }
            }


            //no price search, just return the list
        return theResultsList;
        }


        


    }
}