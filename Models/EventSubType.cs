using System;
using System.Collections.Generic;

namespace BootstrapVillas.Models
{
    public partial class EventSubType
    {
        public EventSubType()
        {
            this.EventTypes = new List<EventType>();
        }

        public long EventSubTypeID { get; set; }
        public string EventSubTypeName { get; set; }
        public virtual ICollection<EventType> EventTypes { get; set; }
    }
}
