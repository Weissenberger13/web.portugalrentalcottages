using System;
using System.Collections.Generic;

namespace BootstrapVillas.Models
{
    public partial class BookingExtraType
    {
        public BookingExtraType()
        {
            this.BookingExtras = new List<BookingExtra>();
        }

        public long BookingExtraTypeID { get; set; }
        public string ExtraTypeName { get; set; }
        public Nullable<long> BookingExtraVendorID { get; set; }
        public string ExtraTypeDescription { get; set; }
        public virtual ICollection<BookingExtra> BookingExtras { get; set; }
        public virtual BookingExtraVendor BookingExtraVendor { get; set; }
    }
}
