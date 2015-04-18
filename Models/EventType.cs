using System;
using System.Collections.Generic;

namespace BootstrapVillas.Models
{
    public partial class EventType
    {
        public EventType()
        {
            this.Events = new List<Event>();
            this.ProcessEvents = new List<ProcessEvent>();
        }

        public long EventTypeID { get; set; }
        public string EventTypeName { get; set; }
        public string EventTypeDescription { get; set; }
        public long EventSubTypeID { get; set; }
        public Nullable<long> EventSchemeTypeID { get; set; }
        public Nullable<bool> ManuallyAvailable { get; set; }
        public Nullable<long> DocumentTypeID { get; set; }
        public Nullable<int> DocumentEnumID { get; set; }
        public Nullable<int> EmailEnumID { get; set; }
        public Nullable<int> EmailTemplateId { get; set; }
        public virtual DocumentType DocumentType { get; set; }
        public virtual EmailTemplate EmailTemplate { get; set; }
        public virtual ICollection<Event> Events { get; set; }
        public virtual EventSchemeType EventSchemeType { get; set; }
        public virtual EventSubType EventSubType { get; set; }
        public virtual ICollection<ProcessEvent> ProcessEvents { get; set; }
    }
}
