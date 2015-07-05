using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BootstrapVillas.Models;

namespace BootstrapVillas.Content.Classes.CurrencyConverter
{
    public static class CurrencyConverterFactory
    {
        public static ICurrencyConvert GetCurrencyConverter(CurrencyEnum currencyType, IEnumerable<CurrencyExchange> theCurrencies)
        {
            ICurrencyConvert strategy = null;
            string currencyExchangeName = "GBP-";

            switch (currencyType)
            {
                case CurrencyEnum.GBP:
                    strategy = new PRCCurrencyConverter(CurrencyEnum.GBP, 1);
                    break;

                case CurrencyEnum.USD:
                    currencyExchangeName += "USD";
                    strategy = new PRCCurrencyConverter(CurrencyEnum.USD, (decimal)theCurrencies.First(x=>x.CurrencyExchangeName== currencyExchangeName).CurrencyExchangeRate);
                    break;

                case CurrencyEnum.EUR:
                    currencyExchangeName += "EUR";
                    strategy = new PRCCurrencyConverter(CurrencyEnum.EUR, (decimal)theCurrencies.First(x => x.CurrencyExchangeName == currencyExchangeName).CurrencyExchangeRate);
                    break;

                case CurrencyEnum.CAD:
                    currencyExchangeName += "CAD";
                    strategy = new PRCCurrencyConverter(CurrencyEnum.CAD, (decimal)theCurrencies.First(x => x.CurrencyExchangeName == currencyExchangeName).CurrencyExchangeRate);
                    break;

                default:
                    strategy = new PRCCurrencyConverter(CurrencyEnum.GBP, 1);
                    break;

            }

            return strategy;

        }

    }
}
