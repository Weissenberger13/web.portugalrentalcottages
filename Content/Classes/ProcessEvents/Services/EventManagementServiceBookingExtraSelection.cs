using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BootstrapVillas.Models;

namespace BootstrapVillas.Content.Classes.ProcessEvents.Services
{

    //runs the events and uses the context / logger to log them in the DB
    public class EventManagementServiceBookingExtraSelection
    {
        private BookingExtraSelection bes { get; set; }
        private List<Event> events; 

        public EventManagementServiceBookingExtraSelection(BookingExtraSelection bes)
        {
            bes = bes;
        }

    }
}