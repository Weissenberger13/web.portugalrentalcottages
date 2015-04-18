using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BootstrapVillas.Models.ViewModels
{
    
    
    //this is used when the customer submits final details at the end of the booking process
    
    /// <summary>
    /// This is used when the customer submits final details at the end of the booking process
    /// Contains Customer, CustomerBankDetails, LIST(Booking), List(BES)
    /// </summary>
    public class FinalBookingDetailViewModel
    {

        public Customer Customer { get; set; }
        public CustomerBankDetail CustomerBankDetail { get; set; }
 
        public List<Booking> Bookings { get; set; }

        public List<BookingParticipant> BookingParticipants { get; set; }
      
        public List<BookingExtraSelection> BookingExtraSelections { get; set; }

        public FinalBookingDetailViewModel()
        {
            BookingExtraSelections = new List<BookingExtraSelection>();
            Bookings = new List<Booking>();
            BookingParticipants = new List<BookingParticipant>();
        }

    }
}