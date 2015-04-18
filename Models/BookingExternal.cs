using System;
using System.Collections.Generic;

namespace BootstrapVillas.Models
{
    public partial class BookingExternal
    {
        public long BookingExternalID { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public long PropertyID { get; set; }
        public string Notes { get; set; }
        public virtual Property Property { get; set; }
    }
}
