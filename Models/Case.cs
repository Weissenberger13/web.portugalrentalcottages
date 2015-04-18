using System;
using System.Collections.Generic;

namespace BootstrapVillas.Models
{
    public partial class Case
    {
        public Case()
        {
            this.Bookings = new List<Booking>();
            this.Documents = new List<Document>();
        }

        public long CaseID { get; set; }
        public Nullable<long> CustomerID { get; set; }
        public Nullable<System.DateTime> WhenCreated { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual ICollection<Document> Documents { get; set; }
    }
}
