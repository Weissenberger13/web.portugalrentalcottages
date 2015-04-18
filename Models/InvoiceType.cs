using System;
using System.Collections.Generic;

namespace BootstrapVillas.Models
{
    public partial class InvoiceType
    {
        public InvoiceType()
        {
            this.Invoices = new List<Invoice>();
        }

        public long InvoiceTypeID { get; set; }
        public System.DateTime CreationDate { get; set; }
        public virtual ICollection<Invoice> Invoices { get; set; }
    }
}
