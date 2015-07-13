using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using BootstrapVillas.Models;
using System.Data.Entity;
using System.Linq.Dynamic;
using BootstrapVillas.Content.Classes;
using Microsoft.Ajax.Utilities;


namespace BootstrapVillas.Models
{
    public partial class BookingExtraSelection
    {

        //this is for the booking process
        public Guid BookingGuid;

        public static BookingExtraSelection GetSingleBookingExtraSelection(long id)
        {

            using (PortugalVillasContext _db = new PortugalVillasContext())
            {
                //get the booking extra

                BookingExtraSelection BookingExtra =
                    _db.BookingExtraSelections.Include(x => x.BookingExtra).Where(x => x.BookingExtraSelectionID == id).FirstOrDefault();


                return BookingExtra;
            }
        }


        public long? GetBookingExtraTypeIDFromBookingExtraSelection()
        {

            PortugalVillasContext _db = new PortugalVillasContext();

            //get the booking extra

            BookingExtra BookingExtra = _db.BookingExtras.Where(x => x.BookingExtraID == this.BookingExtraID).FirstOrDefault();


            return BookingExtra.BookingExtraTypeID;
        }


        public static BookingExtra GetBookingExtrasFromBookingExtraSelection(BookingExtraSelection aBookingExtraSelection)
        {
            BookingExtra aBookingExtra = new BookingExtra();

            PortugalVillasContext _db = new PortugalVillasContext();

            aBookingExtra = _db.BookingExtras.Where(x => x.BookingExtraID == aBookingExtraSelection.BookingExtraID).FirstOrDefault();

            return aBookingExtra;

        }

        public static string GetBookingExtraPRCReferenceFromID(BookingExtraSelection aBookingExtraSelection)
        {
            BookingExtra aBookingExtra = new BookingExtra();

            PortugalVillasContext _db = new PortugalVillasContext();

            aBookingExtra = _db.BookingExtras.Where(x => x.BookingExtraID == aBookingExtraSelection.BookingExtraID).FirstOrDefault();

            return aBookingExtra.LegacyReference;

        }


        /// <summary>
        /// Assigns Price To This Booking Extra Selection
        /// Check how many days for each booking (make sure the rental is done by days and not nights)
        /// Check which season each day falls under (e.g. the price)
        /// Divide that price by the requite amount and add that amount to the running total
        /// 
        /// 
        /// </summary>
        /// 
        public static decimal GetBookingExtraPrice(BookingExtraSelection bes, PortugalVillasContext _db)
        {
            decimal BESPrice = 0.00M; //reset any existing price

            var thisBookingExtraType = BookingExtraSelection.GetBookingExtrasFromBookingExtraSelection(bes);
            var thisBookingExtraTypeID = thisBookingExtraType.BookingExtraTypeID;

            try
            {
                //need to know what type it is - car rental prices by the day, rest have flat fee
                if (thisBookingExtraTypeID == 1)
                {

                    /*Low season Nov - Mar  
                   * Mid season Apr, May and Oct
                   * High Season June and Sept
                   * Peak season July and August
                   * 
                  */

                    
                    decimal runningTotal = 0.00M;

                    List<DateTime> DatesToCalulcuatePriceFor = new List<DateTime>();


                    DateTime CurrentDate = (DateTime)bes.ExtraRentalDate;

                    //we don't add the last day, because it needs to be returned on that day (they don't get charged)

                    while (CurrentDate < bes.ExtraReturnDate)
                    {
                        DatesToCalulcuatePriceFor.Add(CurrentDate);
                        CurrentDate = CurrentDate.AddDays(1);
                    }



                    

                    foreach (var day in DatesToCalulcuatePriceFor)
                    {



                        int monthThatTheRentalIsIn = ((DateTime)day).Month;

                        //check where the start date is - that's the season it's in
                        string SeasonOfRental = bes.GetRentalSeasonFromMonth(monthThatTheRentalIsIn);


                        switch (SeasonOfRental)
                        {
                            case "LowSeason":
                                bes.BESPrice = thisBookingExtraType.LowSeasonPrice;
                                break;
                            case "MidSeason":
                                bes.BESPrice = thisBookingExtraType.MidSeasonPrice;
                                break;
                            case "HighSeason":
                                bes.BESPrice = thisBookingExtraType.HighSeasonPrice;
                                break;
                            case "PeakSeason":
                                bes.BESPrice = thisBookingExtraType.PeakSeasonPrice;
                                break;
                            default:
                                bes.BESPrice = 0.00M;
                                break;

                        }

                        //now get the price per day
                        runningTotal += Convert.ToDecimal(bes.BESPrice / 7);


                    }

                    //all done, assign and exit
                    return Decimal.Round(runningTotal, 2);
                }


                //airport = nothing to do, fixed price

                //end airport

                //wine tours and tours - price is per person and depends on how many people are going
                else if (((new[] { 2, 4 }).Contains(Convert.ToInt32(thisBookingExtraTypeID))))
                {

              
                    int monthThatTheRentalIsIn = ((DateTime)bes.ExtraRentalDate).Month;

                    //check where the start date is - that's the season it's in
                    string SeasonOfRental = bes.GetRentalSeasonFromMonth(monthThatTheRentalIsIn);


                    var noOfPeopleOnThisTour = (bes.NumberOfAdults + bes.NumberOfChildren);


                    //get the list of all types for that and order correctly so lowest is first
                    List<BookingExtra> potentialPrices =
                        _db.BookingExtras.Where(
                            a => a.BookingExtraSubTypeID == thisBookingExtraType.BookingExtraSubTypeID).OrderBy(d => d.MaxPersons).ToList();


                    BookingExtra theCorrectPricing = null;

                    foreach (var potentialPrice in potentialPrices)
                    {
                        //get the minimum no of people left
                        if (noOfPeopleOnThisTour <= potentialPrice.MaxPersons)
                        {
                            theCorrectPricing = potentialPrice;
                            break;
                        }
                    }

                    decimal? individualPrice = 0.00M;

                    //get the correct season
                    switch (SeasonOfRental)
                    {
                        case "LowSeason":
                            individualPrice = theCorrectPricing.LowSeasonPrice;
                            break;
                        case "MidSeason":
                            individualPrice = theCorrectPricing.MidSeasonPrice;
                            break;
                        case "HighSeason":
                            individualPrice = theCorrectPricing.HighSeasonPrice;
                            break;
                        case "PeakSeason":
                            individualPrice = theCorrectPricing.PeakSeasonPrice;
                            break;
                        default:
                            individualPrice = 0.00M;
                            break;

                    }



                    // multiply number of people by price and we have a final price - for tours and wine tours


                    decimal? totalPriceToReturn = 0.00M;

                    if (thisBookingExtraType.BookingExtraTypeID == 2 || thisBookingExtraType.BookingExtraTypeID == 4)
                    {
                        totalPriceToReturn += noOfPeopleOnThisTour * individualPrice;
                    }
                    if (thisBookingExtraType.BookingExtraTypeID == 3)
                    {
                        totalPriceToReturn += individualPrice;
                    }

                    return Decimal.Round((decimal)totalPriceToReturn, 2);

                    //else return the group price (for airport transfers) - do nothing

                }

                    //airport transfers
                 else if (((new[] {3}).Contains(Convert.ToInt32(thisBookingExtraTypeID))))
                 {
                     int monthThatTheRentalIsIn = ((DateTime)bes.ExtraRentalDate).Month;

                     //check where the start date is - that's the season it's in
                     string SeasonOfRental = bes.GetRentalSeasonFromMonth(monthThatTheRentalIsIn);


                     var noOfPeopleOnThisTour = (bes.NumberOfAdults + bes.NumberOfChildren);


                     //get the list of all types for that and order correctly so lowest is first
                     List<BookingExtra> potentialPrices =
                         _db.BookingExtras
                         .Where(x => x.BookingExtraTypeID == thisBookingExtraType.BookingExtraTypeID)
                             .Where(x=>x.AirportPickupLocationID == bes.AirportPickupLocationID)
                             .OrderBy(d => d.MaxPersons).ToList();


                     BookingExtra theCorrectPricing = null;

                     foreach (var potentialPrice in potentialPrices)
                     {
                         //get the minimum no of people left
                         if (noOfPeopleOnThisTour <= potentialPrice.MaxPersons)
                         {
                             theCorrectPricing = potentialPrice;
                             break;
                         }
                     }

                     decimal? totalPriceToReturn = 0.00M;

                     //get the correct season
                     switch (SeasonOfRental)
                     {
                         case "LowSeason":
                             totalPriceToReturn = theCorrectPricing.LowSeasonPrice;
                             break;
                         case "MidSeason":
                             totalPriceToReturn = theCorrectPricing.MidSeasonPrice;
                             break;
                         case "HighSeason":
                             totalPriceToReturn = theCorrectPricing.HighSeasonPrice;
                             break;
                         case "PeakSeason":
                             totalPriceToReturn = theCorrectPricing.PeakSeasonPrice;
                             break;
                         default:
                             totalPriceToReturn = 0.00M;
                             break;

                     }

                     return Decimal.Round((decimal)totalPriceToReturn, 2);
                     
                 }
                //default
                else
                {
                    throw new Exception("Couldn't calculate a price for this BookingExtraSelection!!!");
                }



            }
            catch (Exception ex)
            {

                throw ex;
            }


        }




        public static decimal CalculateBookingExtraAdditionalCostsAndAssignToThisBooking(BookingExtraSelection bookingExtraSelection, PortugalVillasContext _db)
        {
            /*Extra services
             * 8 = Detours
             * 9 = Child seats
             * 10 = extra luggage
             * 12 = Car rental child seats
             * */

            var serviceChargeInstances = PropertyTypeServicesChargeInstance.GetExtraTypeServicesChargeInstances();


            decimal additionalCost = 0.00M;

            switch (bookingExtraSelection.GetBookingExtraTypeIDFromBookingExtraSelection())
            {
                //specialist only for cars
                // it's PER DAY
                case 1:
                    {
                        additionalCost +=
                            (bookingExtraSelection.NumberOfChildseats *
                            serviceChargeInstances.First(x => x.PropertyTypeServicesID == 12).ServicePriceGBP) * bookingExtraSelection.NumberOfDays ?? 0.00M;

                       
                        break;
                    }
                // it's EACH WAY
                case 2:
                    {
                        additionalCost +=
                            (bookingExtraSelection.NumberOfChildseats *
                            serviceChargeInstances.First(x => x.PropertyTypeServicesID == 9).ServicePriceGBP) * 2 ?? 0.00M;
                        break;
                    }
                // it's EACH WAY
                case 3:
                    {
                        additionalCost +=
                            (bookingExtraSelection.NumberOfChildseats *
                            serviceChargeInstances.First(x => x.PropertyTypeServicesID == 9).ServicePriceGBP) * 2 ?? 0.00M;
                        break;
                    }
                // it's EACH WAY
                case 4:
                    {
                        additionalCost +=
                            (bookingExtraSelection.NumberOfChildseats *
                            serviceChargeInstances.First(x => x.PropertyTypeServicesID == 9).ServicePriceGBP) * 2 ?? 0.00M;
                        break;
                    }
            }


            //calcs across the board
            //detours
            // straight billing
            additionalCost +=
                  (bookingExtraSelection.Detours *
                  serviceChargeInstances.First(x => x.PropertyTypeServicesID == 10).ServicePriceGBP) ?? 0.00M;

            //extra luggage
            // it's EACH WAY
            additionalCost +=
                  (bookingExtraSelection.PiecesOfLuggage *
                  serviceChargeInstances.First(x => x.PropertyTypeServicesID == 8).ServicePriceGBP) * 2 ?? 0.00M;


            return (decimal)additionalCost;
        }

        public string GetRentalSeasonFromMonth(int monthThatTheRentalIsIn)
        {
            string seasonToReturn = "";

            switch (monthThatTheRentalIsIn)
            {
                case 11:
                case 12:
                case 1:
                case 2:
                case 3:
                    seasonToReturn = "LowSeason";
                    break;
                case 4:
                case 5:
                case 10:
                    seasonToReturn = "MidSeason";
                    break;
                case 6:
                case 9:
                    seasonToReturn = "HighSeason";
                    break;
                case 7:
                case 8:
                    seasonToReturn = "PeakSeason";
                    break;

            }

            return seasonToReturn;

        }

   

        internal void CalculateNoOfGuests()
        {
           this.NumberOfGuests = (this.NumberOfAdults ?? 0)  + (this.NumberOfChildren ?? 0)  + (this.NumberOfInfants ?? 0);
        }

        internal static decimal? GetBookingExtraTotalServicesPrice(BookingExtraSelection bookingExtraSelection, PortugalVillasContext db)
        {
            return bookingExtraSelection.BESPrice + bookingExtraSelection.BESExtraServicesPrice;
        }
    }
}