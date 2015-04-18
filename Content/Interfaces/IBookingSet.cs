using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BootstrapVillas.Models;
using BootstrapVillas.Content.Classes;


namespace BootstrapVillas.Content.Interfaces
{

    /// <summary>
    /// contains everything needed to edit/update all the necessary items for a booking
    /// </summary>
    interface IBookingSet : IDatasetBase
    {      
        List<long> BookingIDs { get; set; }     

        //props
         ICustomerSet theCustomerDetails { get; set; }
         Booking theBooking { get; set; }
         List<BookingParticipant> theParticipantsInTheBooking { get; set; }

        //meths
        bool SetCustomer(ICustomerSet aCustomerSet);

        bool SetBooking(Booking aBooking);
        Booking GetBooking();
        
        List<BookingParticipant> GetAllBookingParticpants();       
        bool AddBookingParticipant(BookingParticipant aParticipantToAdd);        
        bool RemoveBookingParticipant(long BookingParticipantID);                   

    }
}
