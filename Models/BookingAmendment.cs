using System;
using System.Collections.Generic;

namespace BootstrapVillas.Models
{
    public partial class BookingAmendment
    {
        public long BookingAmendmentID { get; set; }
        public System.DateTime WhenCreated { get; set; }
        public long BookingID { get; set; }
        public string AmendmentNote { get; set; }
        public Nullable<int> WhoUpdated { get; set; }
        public virtual Booking Booking { get; set; }
    }
}
