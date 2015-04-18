using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Windows.Input;
using BootstrapVillas.Models;

namespace BootstrapVillas.Content.Classes.ProcessEvents.Services
{
    public class CommandLogger : ICommandLogger
    {
        PortugalVillasContext context { get; set; }

        public CommandLogger(PortugalVillasContext context)
        {
            this.context = context;

        }


        public int Log(long eventID, EventCommand command)
        {
      
         //remove the 

            EventCommand commandToInsert = new EventCommand
            {
                CommandName = command.GetType().ToString(),
                EventID = eventID,

            };

            //take results from passed in command
         var result = command.EventCommandResults;


         context.EventCommands.Add(commandToInsert);

            if (context.SaveChanges() > 0)
            {

                foreach (var eventCommandResult in result)
                {
                    eventCommandResult.EventCommandID = commandToInsert.EventCommandID;
                    context.EventCommandResults.Add(eventCommandResult);                                       
                }
                context.SaveChanges();
                return 0;
              
            }
            return -1;

        }
    }
}