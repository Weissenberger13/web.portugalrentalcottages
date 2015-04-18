using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using BootstrapVillas.Models;

namespace BootstrapVillas.Content.Classes.ProcessEvents.Services
{

    //Writes all events to the DB
    public class EventLogger
    {

        //notifier to email when there's a problem
        //need to log the errors

        ICommandLogger _commandLogger { get; set; }
        PortugalVillasContext _dbContext { get; set; }

        public EventLogger(PortugalVillasContext dbContext, ICommandLogger commandLogger)
        {
            _commandLogger = commandLogger;
            _dbContext = dbContext;
        }

        public EventLogger(PortugalVillasContext dbContext)
        {
            _dbContext = dbContext;
        }


        public int LogEvents(List<Event> eventsToLog)
        {
            try
            {
                foreach (var anEvent in eventsToLog)
                {
                    _dbContext.Events.Add(anEvent);
                }
                _dbContext.SaveChanges();
                return 0;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }



        public int LogEvent(Event eventToLog)
        {

            try
            {
                _dbContext.Events.Add(eventToLog);
                _dbContext.SaveChanges();
                return 0;
            }
            catch (Exception ex)
            {
                return -1;
                //log error

                //contact someone
            }


        }


        /// <summary>
        /// Logs
        /// </summary>
        /// <returns></returns>
        public int LogEventAndCommandandCommandResult(Event anEvent, EventCommand eventCommand, EventCommandResult result)
        {
            anEvent.EventCommands = null;
            //can't log a command without an event
            if (LogEvent(anEvent).Equals(0))
            {
                _commandLogger.Log(anEvent.EventID, eventCommand);
                return 0;
            }

            return -1;


        }


    }

}