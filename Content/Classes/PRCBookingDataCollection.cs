using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BootstrapVillas.Content.Classes
{
    public class BookingDataCollectionEventType
        {           


            public enum Types
            {
                Customer,
                Payment,
                Booking,
                BookingParticipant,
                BookingExtraSelection,
                BookingExtraParticipant,
            }

        /// <summary>
        /// this is used to redirect to a specific controller action to gather data for the customer from that action
        /// </summary>
        public class PRCDataGatheringControllerActionRedirect
        {
            
            public string Controller {get; set;}
            public string Action {get; set;}
            public string CompositeBookingID { get; set; } //prc ref_StartDate_EndDate
            public long? BookingID {get; set;}
            public long? CustomerID { get; set; }
            public string PRCRef { get; set; }
            public long? PropertyID { get; set; }
            public DateTime? StartDate { get; set; }
            public DateTime? EndDate { get; set; }
            public long? ExtraTypeID { get; set; }
            public int? NoTowels { get; set; }
            public int? NoSwimmingPool { get; set; }
            public string SpecialRequests { get; set; }
            public int? NoLinen { get; set; }
            public decimal? Price { get; set; }

            //extra type shite


         
        }

        



        
    
    }
    
}