using System;
using System.Collections.Generic;

namespace BootstrapVillas.Models
{
    public partial class FlightBooking
    {
        public long FlightBookingID { get; set; }
        public Nullable<long> FlightID { get; set; }
        public Nullable<long> BookingID { get; set; }
    }
}
