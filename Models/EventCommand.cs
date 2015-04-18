using System;
using System.Collections.Generic;

namespace BootstrapVillas.Models
{
    public partial class EventCommand
    {
        public EventCommand()
        {
            this.EventCommandResults = new List<EventCommandResult>();
        }

        public long EventCommandID { get; set; }
        public Nullable<long> EventID { get; set; }
        public string CommandName { get; set; }
        public virtual Event Event { get; set; }
        public virtual ICollection<EventCommandResult> EventCommandResults { get; set; }
    }
}
