using System;
using System.Collections.Generic;

namespace BootstrapVillas.Models
{
    public partial class EmailTemplate
    {
        public EmailTemplate()
        {
            this.EventTypes = new List<EventType>();
        }

        public int EmailTemplateId { get; set; }
        public string EmailTemplateName { get; set; }
        public string EmailTemplateBodyHTML { get; set; }
        public string EmailSubject { get; set; }
        public virtual ICollection<EventType> EventTypes { get; set; }
    }
}
