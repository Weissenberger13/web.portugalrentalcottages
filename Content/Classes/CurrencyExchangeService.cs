using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Web;
using BootstrapVillas.Interfaces;
using BootstrapVillas.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace BootstrapVillas.Content.Classes
{
    public class CurrencyExchangeService
    {
        private IMailService mailService;

        public CurrencyExchangeService(IMailService aMailService)
        {
            mailService = aMailService; //inject

        }

        private decimal? MakeCurrencyExchangeRequest(CurrencyExchange ce)
        {
            //request to webservice 
            try
            {
                string requestURL = string.Format("http://www.freecurrencyconverterapi.com/api/convert?q={0}", ce.CurrencyExchangeName);


                WebClient client = new WebClient();
                var response = client.DownloadString(requestURL);

                JObject root = JObject.Parse(response);
                //JArray items = new JArray(root);


                var newAmountForCurrency = (decimal)root["results"][ce.CurrencyExchangeName]["val"];

                return newAmountForCurrency;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public int UpdateCurrency(CurrencyExchange theCurrency)
        {
            try
            {
                theCurrency.CurrencyExchangeRate = MakeCurrencyExchangeRequest(theCurrency);
                theCurrency.LastUpdated = DateTime.Now;
                //update the db
                using (var db = new PortVillasContext())
                {
                    db.Entry(theCurrency).State = EntityState.Modified;
                    db.SaveChanges();

                    return 0;
                }

            }
            catch (Exception ex)
            {
                //log
                this.mailService.Mail("emailnadz@gmail.com!", "Problem in PRC with currency converter");
                throw ex;
            }
        }

    }
}