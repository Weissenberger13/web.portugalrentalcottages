using System;
using System.Collections.Generic;
using BootstrapVillas.Models.Events;

namespace BootstrapVillas.Models
{
    public partial class EventCommand : IEventCommand
    {
        public virtual EventCommandResult ExecuteCommand()
        {
            return new EventCommandResult
            {
                ResultMessage = "NULL object",
                ResultCode = 000
            };
        }
    }
}
