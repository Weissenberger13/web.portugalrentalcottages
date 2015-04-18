using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using BootstrapVillas.Content.Classes;

namespace BootstrapVillas.Models
{
    public partial class BookingExternal
    {



        public static List<BookingExternal> GetExternalBookingsForAProperty(long propertyID)
        {
            using (var db = new PortVillasContext())
            {

                var bookings = db.BookingExternals.ToList();

                return bookings;

            }
        }

        public static List<Booking> GetExternalBookingsAsBookings(long propertyID)
        {
            using (var db = new PortVillasContext())
            {

                var bookings = db.BookingExternals.Where(x=>x.PropertyID == propertyID).ToList();
                List<Booking> cal = new List<Booking>();

                foreach (var bookingExternal in bookings)
                {
                       cal.Add(new Booking
                       {
                           StartDate = bookingExternal.StartDate,
                           EndDate =  bookingExternal.EndDate,
                           PropertyID = bookingExternal.PropertyID,
                           Confirmed = true,
                           Cancelled = false
                       });
                }

                return cal;
            }
        }


    }
}