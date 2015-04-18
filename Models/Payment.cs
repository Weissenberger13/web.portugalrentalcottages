using System;
using System.Collections.Generic;

namespace BootstrapVillas.Models
{
    public partial class Payment
    {
        public long BookingID { get; set; }
        public long PaymentID { get; set; }
        public System.DateTime WhenCreated { get; set; }
        public long PaymentTypeID { get; set; }
        public byte[] WhenModified { get; set; }
        public virtual Booking Booking { get; set; }
        public virtual PaymentType PaymentType { get; set; }
    }
}
