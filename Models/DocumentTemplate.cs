using System;
using System.Collections.Generic;

namespace BootstrapVillas.Models
{
    public partial class DocumentTemplate
    {
        public DocumentTemplate()
        {
            this.DocumentFieldTemplateMappings = new List<DocumentFieldTemplateMapping>();
        }

        public long DocumentTemplateID { get; set; }
        public Nullable<long> DocumentTypeID { get; set; }
        public string DocumentTemplateTitle { get; set; }
        public string DocumentTemplateDescription { get; set; }
        public string DocumentTemplateLegacyReference { get; set; }
        public string DocumentTemplateEmailSubject { get; set; }
        public string DocumentTemplateEmailBodyHTML { get; set; }
        public string DocumentTemplateOutputFormat { get; set; }
        public Nullable<bool> Enabled { get; set; }
        public Nullable<System.DateTime> WhenCreated { get; set; }
        public byte[] WhenUpdated { get; set; }
        public Nullable<long> WhoUpdated { get; set; }
        public byte[] DocumentTemplateBLOB { get; set; }
        public byte[] DocumentEmailTemplateBLOB { get; set; }
        public string DocumentTemplateMergeTabels { get; set; }
        public string DocumentEmailMergeTabels { get; set; }
        public virtual ICollection<DocumentFieldTemplateMapping> DocumentFieldTemplateMappings { get; set; }
        public virtual DocumentType DocumentType { get; set; }
    }
}
