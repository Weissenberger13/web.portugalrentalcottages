using System;
using System.Collections.Generic;

namespace BootstrapVillas.Models
{
    public partial class DocumentFieldType
    {
        public DocumentFieldType()
        {
            this.DocumentFields = new List<DocumentField>();
        }

        public long DocumentFieldTypeID { get; set; }
        public string DocumentFieldTypeName { get; set; }
        public string DocumentFieldTypeDescription { get; set; }
        public virtual ICollection<DocumentField> DocumentFields { get; set; }
    }
}
