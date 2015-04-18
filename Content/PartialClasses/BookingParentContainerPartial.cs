using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BootstrapVillas.Models;

namespace BootstrapVillas.Models
{
    public partial class BookingParentContainer
    {
        /// <summary>
        /// Gets all Bookings / Extras and Sums Their Prices
        /// </summary>
        /// <returns></returns>
        public decimal CalculateTotalBookingPrice()
        {
            try
            {
                PortugalVillasContext _db = new PortugalVillasContext();
                decimal? runningTotal = 0.00M;

                runningTotal += _db.Bookings.Where(x => x.BookingParentContainerID.Equals(this.BookingParentContainerID)).Sum(x => x.BookingPrice);

                runningTotal += _db.BookingExtraSelections.Where(x => x.BookingParentContainerID.Equals(this.BookingParentContainerID)).Sum(x => x.BESPrice);

                this.TotalBookingContainerPrice = runningTotal;
                return (decimal)runningTotal;
            }
            catch (Exception ex)
            {
                
                throw new Exception("There has been an exception in CalculateTotalBookingPrice");
            }
        }

        public decimal CalculateExchangeRatePrice(decimal exchangeRate)
        {
            try
            {
                this.BookingParentContainerCurrencyConversionPrice = this.TotalBookingContainerPrice * exchangeRate;

                return (decimal)BookingParentContainerCurrencyConversionPrice;
            }
            catch (Exception ex)
            {
                
                throw new Exception("There has been a problem in the CalculateExchangeRatePrice of the BookinParentContainer") ;
            }
        }

    }
}