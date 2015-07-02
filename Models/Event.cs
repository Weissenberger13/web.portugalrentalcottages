using System;
using System.Collections.Generic;

namespace BootstrapVillas.Models
{
    public partial class Event
    {
        public Event()
        {
            this.Documents = new List<Document>();
            this.EventCommands = new List<EventCommand>();
        }

        public long EventID { get; set; }
        public Nullable<long> CaseID { get; set; }
        public Nullable<long> EventTypeID { get; set; }
        public DateTime WhenCreated { get; set; }
        public Nullable<long> BookingID { get; set; }
        public Nullable<long> BookingExtraSelectionID { get; set; }
        public bool IsDeleted { get; set; }
        public Nullable<long> PropertyOwnerID { get; set; }
        public virtual Booking Booking { get; set; }
        public virtual BookingExtraSelection BookingExtraSelection { get; set; }
        public virtual ICollection<Document> Documents { get; set; }
        public virtual PropertyOwner PropertyOwner { get; set; }
        public virtual EventType EventType { get; set; }
        public virtual ICollection<EventCommand> EventCommands { get; set; }
    }
}
