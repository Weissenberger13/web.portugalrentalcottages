using System;
using System.Collections.Generic;

namespace BootstrapVillas.Models
{
    public partial class AirportDestination
    {
        public AirportDestination()
        {
            this.BookingExtras = new List<BookingExtra>();
            this.BookingExtraSelections = new List<BookingExtraSelection>();
        }

        public int AirportPickupLocationID { get; set; }
        public string AirportPickupLocationName { get; set; }
        public virtual ICollection<BookingExtra> BookingExtras { get; set; }
        public virtual ICollection<BookingExtraSelection> BookingExtraSelections { get; set; }
    }
}
