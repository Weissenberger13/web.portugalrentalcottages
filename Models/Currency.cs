using System;
using System.Collections.Generic;

namespace BootstrapVillas.Models
{
    public partial class Currency
    {
        public long CurrencyID { get; set; }
        public string CurrencyCode { get; set; }
        public string CurrencySymbol { get; set; }
        public Nullable<decimal> CurrencyInflationFromDBPriceEquation { get; set; }
    }
}
