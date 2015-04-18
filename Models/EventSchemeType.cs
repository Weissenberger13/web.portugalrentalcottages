using System;
using System.Collections.Generic;

namespace BootstrapVillas.Models
{
    public partial class EventSchemeType
    {
        public EventSchemeType()
        {
            this.EventTypes = new List<EventType>();
        }

        public long EventSchemeTypeID { get; set; }
        public string EventSchemeTypeName { get; set; }
        public virtual ICollection<EventType> EventTypes { get; set; }
    }
}
