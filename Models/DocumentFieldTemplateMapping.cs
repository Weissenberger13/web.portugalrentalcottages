using System;
using System.Collections.Generic;

namespace BootstrapVillas.Models
{
    public partial class DocumentFieldTemplateMapping
    {
        public string DocumentFieldTemplateMappingID { get; set; }
        public long DocumentFieldID { get; set; }
        public long DocumentTemplateID { get; set; }
        public virtual DocumentField DocumentField { get; set; }
        public virtual DocumentTemplate DocumentTemplate { get; set; }
    }
}
