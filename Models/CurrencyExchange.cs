using System;
using System.Collections.Generic;

namespace BootstrapVillas.Models
{
    public partial class CurrencyExchange
    {
        public int CurrencyExchangeID { get; set; }
        public string CurrencyExchangeName { get; set; }
        public Nullable<decimal> CurrencyExchangeRate { get; set; }
        public Nullable<DateTime> LastUpdated { get; set; }
        public string CurrencyExchangeProperName { get; set; }
        public string CurrencyExchangeSymbol { get; set; }
    }
}
