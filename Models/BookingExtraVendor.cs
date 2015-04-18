using System;
using System.Collections.Generic;

namespace BootstrapVillas.Models
{
    public partial class BookingExtraVendor
    {
        public BookingExtraVendor()
        {
            this.BookingExtraTypes = new List<BookingExtraType>();
        }

        public long BookingExtraVendorID { get; set; }
        public string VendorName { get; set; }
        public string VendorPrimaryTelephone { get; set; }
        public string VendorSecondaryTelephone { get; set; }
        public virtual ICollection<BookingExtraType> BookingExtraTypes { get; set; }
    }
}
