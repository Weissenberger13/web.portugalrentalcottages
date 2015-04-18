using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BootstrapVillas.Interfaces
{
    public interface ICurrencyConvertable
    {
        //the type
        string BookingPreferredCurrency { get; set; }

        //the actual value
        Nullable<decimal> BookingCurrencyConversionPrice { get; set; }

        //the symbol
        string BookingCurrencyConversionSymbol { get; set; }

        //exchange rate
        Nullable<decimal> BookingCurrencyExchangeRate { get; set; }



        /*void Convert(string newCurrencyExchangeRateName, char newCurrencyExchangeRateSymbol,
            decimal newCurrencyExchangeRate);*/

    }
}