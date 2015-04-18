using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BootstrapVillas.Models;
using BootstrapVillas.Content.Classes;

namespace BootstrapVillas.Content.Interfaces
{
    interface IBookingExtraSelectionSet : IDatasetBase
    {
        //high level props       
     
        List<long> BookingExtraSelectionIDs { get; set; }

        //collection props
         ICustomerSet theCustomerDetails { get; set; }
         BookingExtraSelection theExtraSelection { get; set; }
         List<BookingExtraParticipant> aListOfTheParticipantsForThisBooking { get; set; }


        
       
    }

  


}
