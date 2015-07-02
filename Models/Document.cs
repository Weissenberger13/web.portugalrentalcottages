using System;
using System.Collections.Generic;

namespace BootstrapVillas.Models
{
    public partial class Document
    {
        public long DocumentID { get; set; }
        public long DocumentTemplateID { get; set; }
        public long CustomerID { get; set; }
        public string DocumentName { get; set; }
        public string DocumentDescription { get; set; }
        public string Filename { get; set; }
        public Nullable<bool> Sent { get; set; }
        public Nullable<DateTime> WhenCreated { get; set; }
        public Nullable<long> WhoCreated { get; set; }
        public byte[] DocumentBLOB { get; set; }
        public byte[] EmailBLOB { get; set; }
        public string DocumentFormat { get; set; }
        public string EmailHTML { get; set; }
        public string EmailFrom { get; set; }
        public string EmailTo { get; set; }
        public string EmailCC { get; set; }
        public string EmailBCC { get; set; }
        public string Encoding { get; set; }
        public Nullable<int> DocumentSize { get; set; }
        public Nullable<int> EmailSize { get; set; }
        public Nullable<long> BookingExtraSelectionID { get; set; }
        public Nullable<long> CaseID { get; set; }
        public Nullable<long> EventID { get; set; }
        public virtual BookingExtraSelection BookingExtraSelection { get; set; }
        public virtual Case Case { get; set; }
        public virtual Event Event { get; set; }
    }
}
