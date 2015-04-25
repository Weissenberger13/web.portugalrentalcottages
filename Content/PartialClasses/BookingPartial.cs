using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using BootstrapVillas.Interfaces;
using BootstrapVillas.Models;
using System.Data.Entity;
using BootstrapVillas.Content.Classes;

//namespace must be the same
using WebGrease.Css.Extensions;

namespace BootstrapVillas.Models
{

    [Serializable]
    public partial class Booking : ICurrencyConvertable
    {
        [NotMapped]
        public int StepNo { get; set; }

        //get all bookings - Overload 1 - get all no param
        public static List<Booking> GetAllBookings()
        {
            PortugalVillasContext _db = new PortugalVillasContext();

            List<Booking> AllBookings; //= new List<Booking>();

            AllBookings = (from bookings in _db.Bookings
                           select bookings).ToList();


            return AllBookings;
        }


        //get individual booking by ID -- NEED TO TEST FOR EMPTY RESULT
        public static Booking GetBookingByID(int bookingID)
        {
            PortugalVillasContext _db = new PortugalVillasContext();

            var aBooking = _db.Bookings.Include(x => x.Property).FirstOrDefault(x => x.BookingID == bookingID);

            return aBooking;
        }

        //get an individual booking by Booking Reference -- NEED TO TEST FOR EMPTY RESULT
        public static Booking GetBookingByRef(string bookingRef)
        {
            PortugalVillasContext _db = new PortugalVillasContext();

            Booking theBooking = null;

            var aBooking = (from booking in _db.Bookings
                            where booking.BookingPRCReference == bookingRef
                            select booking);

            if (aBooking != null)
            {
                theBooking = (Booking)aBooking;
                return theBooking;
            }

            return theBooking;
        }

        //insert a booking
        private static bool CreateBooking(Booking aBooking)
        {
            PortugalVillasContext _db = new PortugalVillasContext();

            _db.Bookings.Add(aBooking);

            if (_db.SaveChanges() > 0)
            {

                return true;
            };
            return false;

        }


        //check for booking
        //if the property matches / if the start date is equal to or less than all other end dates with that property 
        public static List<Booking> CheckConfirmedBookingDatesOverlap(Property aProperty, DateTime startDate, DateTime endDate)
        {
            PortugalVillasContext _db = new PortugalVillasContext();



            var overlappingBookingList = (from booking in _db.Bookings
                                          .Where(s => s.PropertyID == aProperty.PropertyID)
                                          where
                                          booking.Confirmed == true

                                          && booking.Cancelled == false

                                              //if the end date of the proposed booking is later than another bookings startdate
                                         &&
                                         (
                                         (
                                              //my end date is later than any other startdate, 
                                          startDate >= booking.StartDate
                                          && startDate < booking.EndDate
                                          )
                                          ||
                                          (
                                          startDate <= booking.StartDate
                                          && endDate > booking.StartDate
                                          )

                                          )
                                          select booking).ToList();

            return overlappingBookingList;


        }


        public static List<Booking> CheckAllBookingDatesOverlap(Property aProperty, DateTime startDate, DateTime endDate)
        {
            PortugalVillasContext _db = new PortugalVillasContext();

            var overlappingBookingList = (from booking in _db.Bookings
                                          where
                                          booking.PropertyID == aProperty.PropertyID
                                              //if the end date of the proposed booking is later than another bookings startdate
                                         &&
                                         (
                                         (
                                              //my end date is later than any other startdate, 
                                          startDate >= booking.StartDate
                                          && startDate < booking.EndDate
                                          )
                                          ||
                                          (
                                          startDate <= booking.StartDate
                                          && endDate > booking.StartDate
                                          )

                                          )
                                          select booking).ToList();

            return overlappingBookingList;
        }


        public static List<Booking> CheckProvisionalBookingDatesOverlap(Property aProperty, DateTime startDate, DateTime endDate)
        {
            PortugalVillasContext _db = new PortugalVillasContext();

            var overlappingBookingList = (from booking in _db.Bookings
                                          where
                                          !booking.Confirmed
                                          &&
                                          booking.PropertyID == aProperty.PropertyID
                                              //if the end date of the proposed booking is later than another bookings startdate
                                         &&
                                         (
                                         (
                                              //my end date is later than any other startdate, 
                                          startDate >= booking.StartDate
                                          && startDate < booking.EndDate
                                          )
                                          ||
                                          (
                                          startDate <= booking.StartDate
                                          && endDate > booking.StartDate
                                          )

                                          )
                                          select booking).ToList();

            return overlappingBookingList;


        }



        public static IEnumerable<DateTime> GetDateConfirmedRangeForABooking(Booking aBooking)
        {
            //check the booking is confirmed
            if (aBooking.Confirmed == true)
            {
                //use null coalescing operator
                DateTime startingDate = aBooking.StartDate ?? DateTime.Parse("1/1/1901 00:00:00 AM");
                DateTime endingDate = aBooking.EndDate ?? DateTime.Parse("1/1/1901 00:00:00 AM");


                while (startingDate <= endingDate)
                {
                    yield return startingDate;
                    startingDate = startingDate.AddDays(1);
                }

            }


        }


        public static List<Booking> GetAllBookingsForAProperty(long propertyID)
        {
            PortugalVillasContext _db = new PortugalVillasContext();

            List<Booking> theBookingList = _db.Bookings.Include(x => x.Customer)
                .Where(x => x.Test == false)
                .Where(x => x.Cancelled != true)
                .Where(x => x.PropertyID == propertyID).ToList();

            return theBookingList;
        }

        public static List<Booking> GetAllFutureBookingsForAProperty(long propertyID)
        {
            var book = GetAllBookingsForAProperty(propertyID);
            var booklaterthanToday = book.Where(x => x.StartDate >= DateTime.Now && x.Test == false).OrderBy(x => x.StartDate).ToList();

            return booklaterthanToday;
        }


        public static List<DateTime?> GetLastDayOfAllFutureConfirmedBookings(long propertyID)
        {
            using (var db = new PortugalVillasContext())
            {

                var dates =
                    db.Bookings
                    .Where(x => x.Confirmed == true)
                .Where(x => x.Cancelled == false)
                .Where(x => x.PropertyID == propertyID)
                 .ToList();

                var lastdayeachBooking = new List<DateTime?>();
                foreach (var date in dates)
                {
                    lastdayeachBooking.Add(date.EndDate);
                }


                return lastdayeachBooking;

            }


        }

        //methods to calculate the booking prices and relevant fields
        /* we need to: 
         * 
         *1. Calculate the overall booking price PER DAY, for as many days are as in the booking
         *2. Calculate the deposit, breakage deposit, and any subsequent percentages
         * 
         * 
         * 
         */


        //start calc booking prices
        //this can take weeks or days then calc accordingly .
        //

        //days




        /////////////////////////////////////////////
        //////METHODS TO ASSIST IN BOOKING CREATION 
        /////////////////////////////////////////////


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool SetRentalBalanceDueDate()
        {
            try
            {
                var temp = (DateTime)this.StartDate;

                this.RentalBalanceDueDate = temp.AddMonths(-1);
                this.FinalRentalPaymentDueDate = temp.AddMonths(-1);

                return true;
            }
            catch (Exception ex)
            { throw ex; }


        }

        /// <summary>
        /// the deposit is due 1 month before the booking start date
        /// </summary>
        /// <returns></returns>
        public bool SetBreakageDepositDueDate()
        {
            try
            {
                var temp = (DateTime)this.StartDate;

                this.BreakageDepositDueDate = temp.AddMonths(-1);

                return true;

            }
            catch (Exception ex)
            { throw ex; }


        }


        /// <summary>
        /// Helper method used in Booking Creation that returns the property price for any one night
        /// </summary>
        //public decimal GetPropertyPriceForANight(DateTime aDate)
        //{
        //    decimal priceForThatNight = 0.00M;

        //    PortugalVillasContext _db = new PortugalVillasContext();

        //    //create a container with price, startdate, enddate List<dates> - which are all the dates between start and end date
        //    //these are concrete dates, with the year taken from the passed in date and appended.
        //    //cycle through every date until we find a match
        //    //return the price associated with that date

        //    //make a container class which takes a list of dates then calls this method - the container returns the overall price


        //  //  decimal thePrice = 




        //}


        public bool CalculateBookingPricingForAPropertyBooking(PortugalVillasContext db)
        {

            BookingPrice = 0.00M;
            try
            {
                decimal? theCalculatedBookingPrice = 0.00M;


                if (this.StartDate == null || this.EndDate == null || this.BookingPRCReference == "")
                { throw new ArgumentNullException(); }
                //we need the property ID, let's get it
                if (this.PropertyID != 0 && this.PropertyID != null)
                {
                    Property theProp = db.Properties.Where(x => x.PropertyID == this.PropertyID).First();
                    this.PropertyID = theProp.PropertyID;
                }

                //get a list of nights they have booked

                List<DateTime> BookingNIGHTS = GetListOfNIGHTSForThisBooking();

                foreach (var night in BookingNIGHTS)
                {
                    BookingDateRangeAndPriceCalculator calculator = new BookingDateRangeAndPriceCalculator(night, this.PropertyID);
                    theCalculatedBookingPrice += calculator.ReturnPriceForDateOfBooking();

                }


                this.BookingPrice = decimal.Round((decimal)theCalculatedBookingPrice, 2);

                return true;
                //get the nightly pricing for each night they have booked by checking against propertyPRicing

                //add them all together


                //calculate all the breakage deposits (check what is executed in the DB already via defualts)


            }
            catch (Exception)
            {
                return false;
                //ignore,you won't have a price
            }
        }

        private List<DateTime> GetListOfNIGHTSForThisBooking()
        {
            try
            {
                List<DateTime> theNIGHTSToReturn = new List<DateTime>();

                int currentDateIterator = ((DateTime)this.EndDate - (DateTime)this.StartDate).Days; //NOT DAYS, IT'S NIGHTS
                currentDateIterator -= 2; //because they don't ACTUALLY STAY the last day, it's 7 nights. 
                DateTime currentDate = (DateTime)this.StartDate;
                theNIGHTSToReturn.Add(currentDate);

                //add a day to startdate for the number of days we need, then add this 
                for (int i = 0; i <= currentDateIterator; i++)
                {

                    currentDate = currentDate.AddDays(1);
                    theNIGHTSToReturn.Add(currentDate);

                }

                return theNIGHTSToReturn;
            }
            catch (Exception)
            {

                throw;
            }
        }



        internal void CalculateExtrasPriceForAPropertyBooking(Property property, Booking booking, PortugalVillasContext db)
        {
            var allTheServices = db.PropertyTypeServicesChargeInstances.Where(x => x.PropertyTypeID == property.PropertyTypeID).ToSafeReadOnlyCollection();

            var weeks = booking.NumberOfNights / 7;
            if (weeks == 0)
            {
                weeks = 1;
            }
            //towels           
            try
            {
                booking.TowelsPrice = (booking.NoOfTowelsRequested * allTheServices.First(x => x.PropertyTypeServicesID.Equals(5)).ServicePriceGBP) * weeks;
            }
            catch (Exception)
            {


            }
            //cleans
            try
            {
                booking.MidVactionCleaningPrice = booking.MidVactionCleaning * allTheServices.First(x => x.PropertyTypeServicesID.Equals(1)).ServicePriceGBP;
            }
            catch (Exception)
            {


            }
            //heating
            try
            {
                booking.HeatingPrice = booking.HeatingNoNights * allTheServices.First(x => x.PropertyTypeServicesID.Equals(3)).ServicePriceGBP;
            }
            catch (Exception)
            {

            }
            //cleaning laundry post visit
            try
            {
                booking.CleaningPostVisitPrice = allTheServices.First(x => x.PropertyTypeServicesID.Equals(4)).ServicePriceGBP;
            }
            catch (Exception)
            {


            }
            try
            {
                //swimming pool heating
                booking.SwimmingPoolHeatingPrice = booking.SwimmingPoolHeating * allTheServices.First(x => x.PropertyTypeServicesID.Equals(6)).ServicePriceGBP;
            }

            catch (Exception)
            {


            }
            try
            {
                //linen set    
                booking.ExtraLininSetPrice = (booking.ExtraLininSet * allTheServices.First(x => x.PropertyTypeServicesID.Equals(7)).ServicePriceGBP);
            }

            catch (Exception)
            {


            }

        }

        internal void CalculateFinalRentalPaymentAmount()
        {
            var initDeposit = InitialDeposit ?? 0;

            FinalRentalPayment = (BookingPrice + CleaningPostVisitPrice + ExtraLininSetPrice + HeatingPrice
                + SwimmingPoolHeatingPrice +
                  MidVactionCleaningPrice + TowelsPrice) - initDeposit + BreakageDeposit;

            FinalRentalPaymentIncBreakageDepositLateBooking = (BookingPrice + CleaningPostVisitPrice + ExtraLininSetPrice +
                                                    HeatingPrice
                                                    + SwimmingPoolHeatingPrice +
                                                    MidVactionCleaningPrice + TowelsPrice + BreakageDeposit);


            FinalRentalPaymentNoBreakageDepositLateBooking = (BookingPrice + CleaningPostVisitPrice + ExtraLininSetPrice +
                                                   HeatingPrice
                                                   + SwimmingPoolHeatingPrice +
                                                   MidVactionCleaningPrice + TowelsPrice);



        }

        internal void SetBreakageDepositAmount()
        {
            using (var db = new PortugalVillasContext())
            {

                BreakageDeposit = db.Properties.Find(PropertyID).Deposit_GBP_;

            }
        }

        internal void SetBreakageDepositRemittanceDate()
        {
            BreakageDepositRemittanceDueDate = ((DateTime)EndDate).AddDays(7);
        }

        internal void SetInitalDepositDate()
        {
            InitialDepositDueDate = DateTime.Now;
        }

        internal void SetInitialDepositAmount()
        {
            var bookigPRice = BookingPrice;

            //var extrasPrice = CleaningPostVisitPrice + ExtraLininSetPrice + HeatingPrice
            //                  + SwimmingPoolHeatingPrice + TowelsPrice +
            //                  MidVactionCleaningPrice;

            var deposit = (bookigPRice) * 0.3M;


            InitialDeposit = deposit;

        }

        internal void SetBreakageDepositRemittanceAmount()
        {
            RemittanceAmount = BreakageDeposit;
        }

        internal void SetHomeownerAndPRCComissionAmount(PortugalVillasContext db)
        {

            var totalComission = 0.00M;


            var pricings = db.PropertyPricingSeasonalInstances.Where(x => x.PropertyID == this.PropertyID).ToSafeReadOnlyCollection();

            var seasons = db.PropertyPricingSeasons.ToList();

            var comissions = db.PropertyPricingCommissions.ToSafeReadOnlyCollection();

            var bookingDates = new List<DateTime>();

            //don't add the last day cos it isn't charged
            for (var dt = (DateTime)this.StartDate; dt < (DateTime)this.EndDate; dt = dt.AddDays(1))
            {
                bookingDates.Add(dt);
            }

            //
            foreach (var date in bookingDates)
            {
                var dateAs1901 = new DateTime(1901, date.Month, date.Day);

                foreach (var season in seasons)
                {
                    if (dateAs1901 >= (DateTime)season.SeasonStartDate && dateAs1901 <= (DateTime)season.SeasonEndDate)
                    {
                        try
                        {
                            var price = Convert.ToDecimal(pricings.Where(x => x.PropertyPricingSeasonID == season.PropertyPricingSeasonID).First().Price);
                            var commissionRate = comissions.First(x => x.PropertyPricingComissionID == season.PropertyPricingComissionID).PropertyPricingCommissionRate;
                            totalComission += commissionRate * (price / 7); //day rate * commission
                        }
                        catch (Exception ex)
                        {
                            
                            throw new Exception();
                        }
                    }

                }
            }


            CommissionAmount = totalComission;
            HomeownerPaymentAmount = (decimal)this.BookingPrice - totalComission;

            //all commission needs to be in Euros too
            var exchangeToEuros =
                        db.CurrencyExchanges.First(x => x.CurrencyExchangeName == "GBP-EUR");

            CommissionAmount_AlternateCurrency = exchangeToEuros.CurrencyExchangeRate * totalComission;
            CommissionAmount_ExchangeRate = exchangeToEuros.CurrencyExchangeRate;
            CommissionAmount_AlternateCurrencyType = exchangeToEuros.CurrencyExchangeName;

            HomeownerPaymentAmount_AlternateCurrency = exchangeToEuros.CurrencyExchangeRate * HomeownerPaymentAmount;
            HomeownerPaymentAmount_ExchangeRate = exchangeToEuros.CurrencyExchangeRate;
            HomeownerPaymentAmount_AlternateCurrencyType = exchangeToEuros.CurrencyExchangeName;
        }
    }
}