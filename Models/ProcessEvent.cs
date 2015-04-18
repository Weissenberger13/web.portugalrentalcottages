using System;
using System.Collections.Generic;

namespace BootstrapVillas.Models
{
    public partial class ProcessEvent
    {
        public long ProcessEventID { get; set; }
        public string WhenCreated { get; set; }
        public long EventTypeID { get; set; }
        public long BookingID { get; set; }
        public virtual Booking Booking { get; set; }
        public virtual EventType EventType { get; set; }
    }
}
