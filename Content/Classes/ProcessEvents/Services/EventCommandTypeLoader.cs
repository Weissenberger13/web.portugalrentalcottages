using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BootstrapVillas.Models;

namespace BootstrapVillas.Content.Classes.ProcessEvents.Services
{
    public class EventTypeCommandSelector
    {
        PortugalVillasContext context = new PortugalVillasContext();
        //loads up the execution commands for an event:
        public EventTypeCommandSelector(PortugalVillasContext context)
        {
            context = context;

        }

        Event LoadEventCommandToExecute(Event theEvent)
        {
            EventType type = context.EventTypes.Where(x => x.EventTypeID == theEvent.EventTypeID).FirstOrDefault();

            //depending on subtype, need to get docs, emails, add reminder, whatever we need to do, 
            //and add it to commands to be executed

          /*  switch (type.EventSubTypeID)
            {
                    //it's an email, assign the email command
                case 2:
                    theEvent.EventCommands.Add(new EventCommandSendEmail());
                    break;
                case 3:
                    theEvent.EventCommands.Add(new EventCommandSendEmailWithDocAttachment());
                    break;
                default:
                    break;
            }*/



            return theEvent;
        }






    }
}