using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using BootstrapVillas.Interfaces;
using BootstrapVillas.Models;

namespace BootstrapVillas.Controllers
{


    public class CurrencyConverterController : Controller
    {
        //
        // GET: /CurrencyConverter/
        private PortugalVillasContext _db = new PortugalVillasContext();

        public ICurrencyConvertable InitialiseDefaultCurrencyForObject(ICurrencyConvertable booking)
        {


            //get values from either this class or web config
            booking.BookingPreferredCurrency = WebConfigurationManager.AppSettings["defaultCurrency"];
            booking.BookingCurrencyExchangeRate = 1; //alwasy sone for default cuurency in DB
            booking.BookingCurrencyConversionSymbol = WebConfigurationManager.AppSettings["defaultCurrencySymbol"].ToString();

            return booking;
        }




        [HttpGet]
        public decimal GetExchangeRate(string exchangeRatePattern)
        {

            try
            {
                var currencyExchange = _db.CurrencyExchanges.Where(
                           x => x.CurrencyExchangeName == exchangeRatePattern).First();


                return (decimal)currencyExchange.CurrencyExchangeRate;
            }
            catch (Exception ex)
            {         
                    throw new Exception("Unable to match currency pattern", ex);
            }

        }


        public decimal ConvertCurrency(string currencyToConvertFrom, string currencyToConvertTo, decimal originalPrice)
        {
            try
            {
                var currencyExchange = _db.CurrencyExchanges.Where(
                           x => x.CurrencyExchangeName == currencyToConvertFrom + "-" + currencyToConvertTo).First();


                var returnValue = ConvertBasepriceToCurrency(originalPrice, (decimal)currencyExchange.CurrencyExchangeRate);
             

                return returnValue;
            }
            catch (Exception ex)
            {
                throw new Exception(
                "The currency converter couldn't convert your currency", ex);
            }

            
        }

        

        private decimal ConvertBasepriceToCurrency(decimal originalPrice, decimal exchangeRate)
        {
            //get base from DB

            try
            {

                decimal newPrice = (decimal)exchangeRate * (decimal)originalPrice;

                return newPrice;
            }
            catch (Exception ex)
            {
                throw new Exception(
                "The currency converter couldn't convert your currency", ex);
            }
        }


    }
}
