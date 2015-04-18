using System;
using System.Collections.Generic;

namespace BootstrapVillas.Models
{
    public partial class PaymentType
    {
        public PaymentType()
        {
            this.Payments = new List<Payment>();
        }

        public long PaymentTypeID { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
    }
}
