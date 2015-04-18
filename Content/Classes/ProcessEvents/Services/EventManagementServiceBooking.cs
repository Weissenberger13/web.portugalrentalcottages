using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BootstrapVillas.Models;

namespace BootstrapVillas.Content.Classes.ProcessEvents.Services
{

    //uses
    //IEventFactory
    //IEventLogger

    //Creates events then writes them to the base
    public class EventManagementServiceBooking
    {
        private Booking booking { get; set; }


        public EventManagementServiceBooking(Booking booking)
        {
            booking = booking;
        }



    /*    public void AddProcessEvent(EventType eventTypeToAdd, Booking booking = null, BookingExtraSelection bes = null)
        {
            
            //call event factory that does all instantiation
            //event service then runs and logs the event
            Event theEvent = new Event();
            EventFactory factory = new EventFactory();

            ////create event
            factory.InstantiateEvent(eventTypeToAdd, booking, bes, theEvent);
        }*/



     


        //use factory to create new event

        //run all events

        //if successful, save all relevant commands and the event to the DB.

    }

}
