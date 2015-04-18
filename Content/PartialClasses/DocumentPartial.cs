using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using BootstrapVillas.Models;

namespace BootstrapVillas.Models
{

    //this is the class for document instance

    public partial class Document
    {
        /*steps to creating a new document:
         * -Get the type
         * -Get the fields for that document
         * -Ensure necessary objects are passed / linked to populate the fields - booking, bookingextra, customer, whatever is needed - need a chain of authority
         * -Validation of that data
         * -Call aspose methods on template / fields
         * -put generate document in DB
         * -call email methods on generated documents
         
         */

        
            //empty constructor for dynamic assignment
            public Document()
            {


            }

        
            //takes 
            public Document(Booking aBooking)
            {
                //get the type of document
                //pull all customer details from booking

            }

            public Document(BookingExtraSelection aBookingExtraSelection)
            {





            }
             
    
    
    
    }





}