using System;
using System.Collections.Generic;

namespace BootstrapVillas.Models
{
    public partial class DocumentField
    {
        public DocumentField()
        {
            this.DocumentFieldTemplateMappings = new List<DocumentFieldTemplateMapping>();
        }

        public long DocumentFieldID { get; set; }
        public long DocumentFieldTypeID { get; set; }
        public string DocumentFieldName { get; set; }
        public Nullable<long> LookupListID { get; set; }
        public Nullable<long> ValidationTypeID { get; set; }
        public string FieldName { get; set; }
        public bool Enabled { get; set; }
        public Nullable<System.DateTime> WhenCreated { get; set; }
        public Nullable<System.DateTime> WhenModified { get; set; }
        public virtual DocumentFieldType DocumentFieldType { get; set; }
        public virtual ICollection<DocumentFieldTemplateMapping> DocumentFieldTemplateMappings { get; set; }
    }
}
