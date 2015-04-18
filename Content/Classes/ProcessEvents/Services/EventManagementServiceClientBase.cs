using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Aspose.Email.Logging;
using BootstrapVillas.Content.Classes.ProcessEvents.Services;
using BootstrapVillas.Models;

namespace BootstrapVillas.Content.Events.Services
{

    //does all the messy creation 
    public abstract class EventManagementServiceClientBase
    {
        private PortugalVillasContext EventContext;
        private EventLogger eventLogger;

        public EventManagementServiceClientBase(PortugalVillasContext db, EventLogger log)
        {
            this.EventContext = new PortugalVillasContext();

        }
            
      

        /*public EventManagementServiceClient(Booking booking)
        {
            EventContext = new PortugalVillasContext();
        }*/

        public virtual int RunProcessEvents(List<Event> theEvents )
        {            
            EventLogger logger = new EventLogger(this.EventContext);
            //execute all the events then log them to the DB
            foreach (var theEvent in theEvents)
            {
                foreach (var command in theEvent.EventCommands)
                {
                    var result = command.ExecuteCommand();

                }
            }

            throw new NotImplementedException();
            
        }


      




    }


}