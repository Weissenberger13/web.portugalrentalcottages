using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BootstrapVillas.Content.Classes
{
    public class BookingDate
    {
        public enum BookingDateType
        {
            Confirmed,
            Provisional,
            None

        }

        public DateTime bookingDate { get; set; }
        public BookingDateType bookingDateType { get; set; }


        public BookingDate()
        {
            

        }


    }
}