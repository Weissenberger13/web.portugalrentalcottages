using System;
using System.Collections.Generic;

namespace BootstrapVillas.Models
{
    public partial class Deposit
    {
        public long DepositID { get; set; }
        public Nullable<long> BookingID { get; set; }
        public Nullable<long> BookingExtraID { get; set; }
        public Nullable<long> CustomerID { get; set; }
        public Nullable<decimal> DepositAmount { get; set; }
        public Nullable<long> DepositTypeID { get; set; }
        public Nullable<bool> DepositPaid { get; set; }
        public Nullable<DateTime> DateDepositPaid { get; set; }
        public string Currency { get; set; }
        public string CurrencySymbol { get; set; }
        public Nullable<DateTime> DateDepositDue { get; set; }
    }
}
