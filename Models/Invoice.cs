using System;
using System.Collections.Generic;

namespace BootstrapVillas.Models
{
    public partial class Invoice
    {
        public Invoice()
        {
            this.InvoiceItems = new List<InvoiceItem>();
        }

        public long BookingID { get; set; }
        public long InvoiceID { get; set; }
        public bool Paid { get; set; }
        public Nullable<decimal> SubTotal { get; set; }
        public Nullable<decimal> VAT { get; set; }
        public Nullable<decimal> Total { get; set; }
        public long InvoiceTypeID { get; set; }
        public Nullable<DateTime> PaidDate { get; set; }
        public DateTime WhenCreated { get; set; }
        public byte[] WhenModified { get; set; }
        public virtual Booking Booking { get; set; }
        public virtual InvoiceType InvoiceType { get; set; }
        public virtual ICollection<InvoiceItem> InvoiceItems { get; set; }
    }
}
