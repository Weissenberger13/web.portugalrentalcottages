using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BootstrapVillas.Content.Classes.CurrencyConverter
{
    public class PRCCurrencyConverter : ICurrencyConvert
    {
        public PRCCurrencyConverter(CurrencyEnum currencyType, decimal exchangeRate)
        {
            CurrencyType = currencyType;
            ExchangeRate = exchangeRate;
        }


        public CurrencyEnum CurrencyType { get; set; }
        public decimal ExchangeRate { get; set; }


        public decimal Convert(decimal amountToConvert)
        {
            return amountToConvert*ExchangeRate;
        }
    }
}
