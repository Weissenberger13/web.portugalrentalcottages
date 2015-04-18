using System;
using System.Collections.Generic;

namespace BootstrapVillas.Models
{
    public partial class InvoiceItemType
    {
        public InvoiceItemType()
        {
            this.InvoiceItems = new List<InvoiceItem>();
        }

        public long InvoiceItemTypeID { get; set; }
        public string InvoiceItemName { get; set; }
        public virtual ICollection<InvoiceItem> InvoiceItems { get; set; }
    }
}
