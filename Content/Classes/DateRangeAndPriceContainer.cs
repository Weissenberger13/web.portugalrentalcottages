using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BootstrapVillas.Models;
using BootstrapVillas.Content.CustomExceptions;

namespace BootstrapVillas.Content.Classes
{

    /// <summary>
    /// This takes a propertyID and a booking date, then cycles through all database dateranges for that property until it finds date that matches the
    /// bookingDate - it then returns the corresponding price
    /// 
    /// EventChain: 
    /// Create with price and propertyID
    /// Pull all PropertyPricings for that property
    /// Foreach pricing (assign year from booking date), create all the dates in that pricing (add day +1) until reach end
    /// if match, return that price
    /// if no match, cycle through to next propertyPricing
    /// 
    /// </summary>
    public class BookingDateRangeAndPriceCalculator
    {

        /// <summary>
        /// Members
        /// </summary>
        public DateTime theBookingDate { get; set; }
        public long? PropertyID { get; set; }
        public List<PropertyPricingSeasonalInstance> thePricings { get; set; }

        /// <summary>
        /// Reusable List - for each range
        /// </summary>
        public List<DateTime> theRangeOfDatesBetweenStartAndEndDates { get; set; }

        /// <summary>
        /// Resuable - for each range
        /// </summary>
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal? CurrentPriceForRange { get; set; }


        //end members

        //////////////////
        //Methods
        //////////////////


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="theBookingDate"></param>
        /// <param name="propertyID"></param>
        public BookingDateRangeAndPriceCalculator(DateTime theBookingDate, long? propertyID)
        {
            this.theBookingDate = theBookingDate;
            this.PropertyID = PropertyID;
            this.thePricings = PropertyPricingSeasonalInstance.GetPricingByPropertyID(propertyID);
        }

        /// <summary>
        /// This takes a pseudo date MM-DD and converts it to a 'proper date' -TESTED
        /// </summary>
        /// <returns>DateTime dd-MM-YYYY</returns>
        private DateTime ConvertPropertyPricingDateToActualDate(string MM_DD_Date)
        {
            int year = this.theBookingDate.Year;

            string completeDate_MM_DD_YYYY = MM_DD_Date + "-" + year.ToString();

            DateTime ParsedDate = DateTime.ParseExact(completeDate_MM_DD_YYYY + " 00:00:00,000", "MM-dd-yyyy HH:mm:ss,fff", System.Globalization.CultureInfo.InvariantCulture);

            return ParsedDate;
        }


        /// <summary>
        /// Populates the daterange
        /// 
        ///For this price range -
        ///make the start and end dates proper dates (append year from 'theBookingDate'
        ///add start date to list. keep adding dates until enddate (and also add that)
        /// </summary>
        /// <returns></returns>
        /// 
        private bool PopulateTheRangeOfDatesBetweenStartAndEndDates(PropertyPricingSeasonalInstance aPropertyPricing)
        {
            try
            {
                //make sure list is blank
                theRangeOfDatesBetweenStartAndEndDates = null;
                theRangeOfDatesBetweenStartAndEndDates = new List<DateTime>();
              
                CurrentPriceForRange = null;
                CurrentPriceForRange = aPropertyPricing.Price;


                if(aPropertyPricing.PropertyPricingSeason == null)
                {
                    using (var db = new PortugalVillasContext())

                    {
                        aPropertyPricing.PropertyPricingSeason =
                            db.PropertyPricingSeasons.Find(aPropertyPricing.PropertyPricingSeasonID);
                    } 

                }


                DateTime startDate = (DateTime)aPropertyPricing.PropertyPricingSeason.SeasonStartDate;
                DateTime endDate = (DateTime)aPropertyPricing.PropertyPricingSeason.SeasonEndDate;


                int currentDateIterator = (endDate - startDate).Days;
                currentDateIterator -= 1;
                DateTime currentDate = startDate;
                theRangeOfDatesBetweenStartAndEndDates.Add(currentDate);

                //add a day to startdate for the number of days we need, then add this 
                for (int i = 0; i <= currentDateIterator; i++)
                {

                    currentDate = currentDate.AddDays(1);
                    theRangeOfDatesBetweenStartAndEndDates.Add(currentDate);

                }


                return true;
            }
            catch (Exception ex)
            {

                throw;
            }

        }


        public decimal? ReturnPriceForDateOfBooking()
        {
            decimal? price = 0.00M;

            foreach (PropertyPricingSeasonalInstance aPricing in thePricings)
            {
                PopulateTheRangeOfDatesBetweenStartAndEndDates(aPricing);

                
                //ADDED TO PATCH NEW PRICING
                //convert the pricing date to be the same as THIS YEAR
                var theRangeThisWithTheYearOfBooking = new List<DateTime>();

                foreach (var date in theRangeOfDatesBetweenStartAndEndDates)
                {
                    theRangeThisWithTheYearOfBooking.Add(new DateTime(theBookingDate.Year, date.Month, date.Day));
                }

                //reassign variable
                theRangeOfDatesBetweenStartAndEndDates = theRangeThisWithTheYearOfBooking;
                //END

                foreach (var date in this.theRangeOfDatesBetweenStartAndEndDates)
                {

                    //now check the booking date - if there's a match, break the loop and return the price;
                    if (this.theBookingDate.ToShortDateString().Substring(0, 5) == date.ToShortDateString().Substring(0, 5))
                    {
                        price = this.CurrentPriceForRange / 7.00M;
                        return price; //it's for one day, pricing is per week }
                    }


                }
            }
            //nothing matched in any of the prices - thow exception
            NoMatchException ex = new NoMatchException("There was no match in the ReturnPriceForDateOfBooking() method of the BookingDateRangeAndPriceCalculator instance");
            throw ex;

                


        }

    }
}