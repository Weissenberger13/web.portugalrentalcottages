using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BootstrapVillas.Content.Classes.CurrencyConverter;
using HtmlTags;

namespace BootstrapVillas.HTMLHelper
{
    public static class CurrencyHelpers
    {

        #region CurrencyHelpers

        /// <summary>
        /// Displays currency symbol e..g $, £,based on cache value
        /// </summary>
        /// <param name="helper"></param>
        /// <returns></returns>
        public static HtmlTag CurrencySymbol(this HtmlHelper helper)
        {
            try
            {             
                return new HtmlTag("span")                                                           
                               .Text(HttpRuntime.Cache.Get("currencySymbol").ToString())
                               ;
            }
            catch (Exception ex)
            {

                throw new Exception("Unable to render currency symbol", ex);
            }

        }


        /// <summary>
        /// Displays currency e.g. GBP, USD, CAD, based on cache value
        /// </summary>
        /// <param name="helper"></param>
        /// <returns></returns>
        public static HtmlTag Currency(this HtmlHelper helper)
        {

            try
            {
                return new HtmlTag("span").Text(HttpRuntime.Cache["defaultCurrency"].ToString());
            }
            catch (Exception ex)
            {

                throw new Exception("Unable to render currency ", ex);
            }

        }


        /// <summary>
        /// Renders the correct pricing depending on the value in the config.
        /// </summary>
        /// <returns></returns>
        public static IHtmlString CurrencyConvert(this HtmlHelper helper, decimal valueToconvert, ICurrencyConvert converter)
        {
            //strategy to pick correct conversion
            var newValue = decimal.Round(converter.Convert(valueToconvert), 2, MidpointRounding.AwayFromZero);
            return new HtmlString(newValue.ToString());

        }

        public static IHtmlString CurrencyConvert(this HtmlHelper helper, decimal? valueToconvert, ICurrencyConvert converter)
        {
            //strategy to pick correct conversion
            if (valueToconvert != null)
            {
                var newValue = decimal.Round(converter.Convert((decimal)valueToconvert), 2, MidpointRounding.AwayFromZero);
                return new HtmlString(newValue.ToString());
            }

            return new HtmlString("0.00");


        }

        #endregion

    }

}
