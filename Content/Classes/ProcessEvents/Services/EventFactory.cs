using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Web;
using BootstrapVillas.Models;
using BootstrapVillas.Models.Events;

namespace BootstrapVillas.Content.Classes.ProcessEvents.Services
{

    //Creates all event to normal state
    public class EventFactory
    {
        private PortugalVillasContext context;

        public EventFactory(PortugalVillasContext context)
        {
            context = context;
        }

        //PRIVATE METHODS

        /* private CreateEmailBody()
         {
            

         }*/

        /////////MAIN PUBLIC EXPOSED METHODS



        //takes a type, creates an Event
        public Event CreateEmailEvent(EventType eventType/*EmailTypeToCreate*/)
        {
            var theEvent = new Event
            {



            };

            return theEvent;
        }

        //passes back a fully formed document
        public Event CreateDocumentEvent(EventType eventType, List<IEventCommand> commandsToRun)
        {
            //new it up and add the commands
            var theEvent = new Event
            {



            };

            return theEvent;
        }


        //NOT PROCESS TYPE SPECIFIC!!
        public Event CreateEvent(EventType type)
        {
              //set up standard filds
            Event eve = new Event
            {
                WhenCreated = DateTime.Now,
                EventTypeID =  type.EventTypeID,
            };


        
            return eve;

        }
    }
}