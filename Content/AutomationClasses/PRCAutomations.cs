using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BootstrapVillas.Content.Classes;
using BootstrapVillas.Interfaces;
using BootstrapVillas.Models;

namespace BootstrapVillas.Content.AutomationClasses
{
    public class PRCAutomations
    {

        public int UpdateCurrencyExchangeRates()
        {

            try
            {
                //new up everything
                IMailService mailerService = new MaintainanceMailer();
                CurrencyExchangeService ces = new CurrencyExchangeService(mailerService);
                

                //get all the currencies
                using (var _db = new PortVillasContext())
                {

                    var currencies = _db.CurrencyExchanges.ToList();

                    foreach (var currency in currencies)
                    {
                        ces.UpdateCurrency(currency);
                    }

                }

                return 0;
            }
            catch (Exception ex)
            {

                
                throw ex;
            }
            return -1;


        }


    }
}