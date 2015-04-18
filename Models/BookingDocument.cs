using System;
using System.Collections.Generic;

namespace BootstrapVillas.Models
{
    public partial class BookingDocument
    {
        public long BookingDocumentID { get; set; }
        public System.DateTime WhenCreated { get; set; }
        public string EmailTo { get; set; }
        public string EmailCC { get; set; }
        public long BookingID { get; set; }
        public Nullable<int> WhoUpdated { get; set; }
    }
}
