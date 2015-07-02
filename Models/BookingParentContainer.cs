using System;
using System.Collections.Generic;

namespace BootstrapVillas.Models
{
    public partial class BookingParentContainer
    {
        public BookingParentContainer()
        {
            this.Bookings = new List<Booking>();
            this.BookingExtraSelections = new List<BookingExtraSelection>();
        }

        public long BookingParentContainerID { get; set; }
        public long CustomerID { get; set; }
        public Nullable<DateTime> CreationDate { get; set; }
        public Nullable<decimal> TotalBookingContainerPrice { get; set; }
        public string OverallBookingReference { get; set; }
        public Nullable<decimal> BookingParentContainerCurrencyConversionPrice { get; set; }
        public string BookingParentContainerCurrencyConversionSymbol { get; set; }
        public string BookingSummary { get; set; }
        public string BookingExtraSummary { get; set; }
        public Nullable<int> TotalVehicleCount { get; set; }
        public Nullable<decimal> SumCarRentalAndExtras { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; }
        public virtual ICollection<BookingExtraSelection> BookingExtraSelections { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
