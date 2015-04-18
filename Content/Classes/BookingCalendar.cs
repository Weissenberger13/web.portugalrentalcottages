using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BootstrapVillas.Content.Classes;
using BootstrapVillas.Models;


/*
 * 
 * Created By Nadav 'Crackdoctor' Drewe
 * Class to hold bookingdates for a property - booking calendar is generated from this.
 * Links via PropertyID
 */

namespace BootstrapVillas.Content.Classes
{
    //every property may have one booking calendar - a booking calendar
    //is for one property.
    public class BookingCalendar
    {
        public BookingCalendar()
        {
                
        }
        public BookingCalendar(long propertyID)
        {
            this.PropertyID = propertyID;

        }

        ////////////////////////////////
        /// ////Instance Vars
        ////////////////////////////////
        
        //to link to property - set once at creation
        public long PropertyID { get; set; }
        //Earliest and Latest Booking Dates for easy reference
        public DateTime EarliestBooking;
        public DateTime LatestBookingDate;
        
        //start date of calendar - i.e today - we don't need to worry about 
        //date before then
        protected DateTime CalendarStartDate = DateTime.Now;


        //list of all the dates from DateTime Now > 
        public List<BookingDate> bookingDates = new List<BookingDate>();
        public List<BookingExternal> externalBookings = new List<BookingExternal>();


        ////////////////////////////////
        ////Instance Methods
        ////////////////////////////////
    
        //called at creation 
        protected DateTime? GetMinBooking()
        {
            PortugalVillasContext _db = new PortugalVillasContext();
            DateTime? minBooking = _db.Bookings
               .Min(x => x.StartDate);


            return minBooking;
        }

        protected DateTime? GetMaxBooking()
        {
            PortugalVillasContext _db = new PortugalVillasContext();
            DateTime? maxBooking = _db.Bookings
                .Max(x => x.StartDate);
     
      
            return maxBooking;
        }


        //this function creates a BookingDate for every booking date and adds it to the list of booking dates
        public void GetAllBookingDatesAndAddToThisCalendar()
        {
            PortugalVillasContext _db = new PortugalVillasContext();
            List<Booking> theBookingDates = Booking.GetAllBookingsForAProperty(this.PropertyID);
            var externalDates = BookingExternal.GetExternalBookingsAsBookings(this.PropertyID);

            //add external dates
            theBookingDates.AddRange(externalDates);

            //add our system dates
            if (theBookingDates.Any())
            {

                //create and add each booked date to the 'bookingDates' list. 
                foreach (Booking aBooking in theBookingDates)
                {
                    try
                    {

                        if (aBooking.Confirmed == true && aBooking.Cancelled == false)
                        {
                            //foreach booking 
                            DateTime tempDate = aBooking.StartDate.Value;
                            BookingDate tempBookingDate = new BookingDate();

                            switch (aBooking.Confirmed)
                            {
                                case true:
                                    tempBookingDate.bookingDateType = BookingDate.BookingDateType.Confirmed;
                                    break;
                                    /* case false:
                                tempBookingDate.bookingDateType = BookingDate.BookingDateType.Provisional;
                                break;*/

                            }

                            //THIS STOPS A DAY BEFORE THE ACTUAL BOOKING 
                            //add a new booking date to the list - stop at last date as we want that to be available for another booking
                            while (tempDate < aBooking.EndDate)
                            {

                                bookingDates.Add(
                                    new BookingDate()
                                    {
                                        bookingDate = tempDate,
                                        bookingDateType = tempBookingDate.bookingDateType

                                    });
                                tempDate = tempDate.AddDays(1);
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        
                        //ignore it, process the next one
                    }


                }
            }


        }
    
    }


  
}