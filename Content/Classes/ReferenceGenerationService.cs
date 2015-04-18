using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BootstrapVillas.Models;

namespace BootstrapVillas.Content.Classes
{
    public class ReferenceGenerationService
    {

        public string GenerateBookingReference(Booking booking, Property prop)
        {
            string reference = "";

            reference += prop.LegacyReference + "/B" + booking.BookingID + "/" + ((DateTime)booking.StartDate).ToString("ddMMyyyy").Replace("/", "").Replace("-", "");
            
            return reference;

        }


        public string GenerateBESReference(BookingExtraSelection bes, BookingExtra extra)
        {
            string reference = "";

          
            reference += extra.LegacyReference;
            reference += "/" + bes.BookingExtraSelectionID;
            reference += "/" + ((DateTime)bes.ExtraRentalDate).ToString("ddMMyyyy").Replace("/", "").Replace("-", "");


            return reference;
        }

    }
}