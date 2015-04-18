using System;
using System.Collections.Generic;

namespace BootstrapVillas.Models
{
    public partial class InvoiceItem
    {
        public long InvoiceItemID { get; set; }
        public long InvoiceID { get; set; }
        public long InvoiceItemTypeID { get; set; }
        public string InvoiceItemName { get; set; }
        public virtual Invoice Invoice { get; set; }
        public virtual InvoiceItemType InvoiceItemType { get; set; }
    }
}
