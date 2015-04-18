using System;
using System.Collections.Generic;

namespace BootstrapVillas.Models
{
    public partial class Enquiry
    {
        public long BookingID { get; set; }
        public long EnquiryID { get; set; }
        public Nullable<System.DateTime> EnquiryStartDate { get; set; }
        public Nullable<System.DateTime> EnquiryEndDate { get; set; }
        public System.DateTime EnquiryWhenCreated { get; set; }
        public string EnquiryEmailAddress { get; set; }
        public string EnquiryHomeTelephone { get; set; }
        public string EnquiryMobileTelephone { get; set; }
        public string EnquiryNumberOfDays { get; set; }
        public virtual Booking Booking { get; set; }
    }
}
