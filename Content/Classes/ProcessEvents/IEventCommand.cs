using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BootstrapVillas.Models.Events
{

    //events commands are execute by events
    public interface IEventCommand
    {
        EventCommandResult ExecuteCommand();
    }
}
