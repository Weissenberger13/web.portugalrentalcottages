using System;
using System.Collections.Generic;

namespace BootstrapVillas.Models
{
    public partial class AccountTransaction
    {
        public long AccountTransactionID { get; set; }
        public Nullable<long> BookingID { get; set; }
        public Nullable<long> BookingExtraSelectionID { get; set; }
        public decimal TransactionAmount { get; set; }
        public string TransactionNote { get; set; }
        public bool Paid { get; set; }
        public long AccountID { get; set; }
        public string TransactionReference { get; set; }
        public Nullable<DateTime> TransactionDate { get; set; }
        public string WhoCreated { get; set; }
        public Nullable<DateTime> WhenCreated { get; set; }
        public bool Voided { get; set; }
        public Nullable<DateTime> VoidedDate { get; set; }
        public string WhoVoided { get; set; }
        public virtual Booking Booking { get; set; }
        public virtual BookingExtraSelection BookingExtraSelection { get; set; }
        public virtual PropertyOwnerAccount PropertyOwnerAccount { get; set; }
    }
}
