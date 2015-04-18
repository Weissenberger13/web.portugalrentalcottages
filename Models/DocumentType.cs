using System;
using System.Collections.Generic;

namespace BootstrapVillas.Models
{
    public partial class DocumentType
    {
        public DocumentType()
        {
            this.DocumentTemplates = new List<DocumentTemplate>();
            this.EventTypes = new List<EventType>();
        }

        public long DocumentTypeID { get; set; }
        public string DocumentTypeName { get; set; }
        public string DocumentTypeDescription { get; set; }
        public virtual ICollection<DocumentTemplate> DocumentTemplates { get; set; }
        public virtual ICollection<EventType> EventTypes { get; set; }
    }
}
