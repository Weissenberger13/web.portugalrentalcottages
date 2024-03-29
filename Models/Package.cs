using System;
using System.Collections.Generic;

namespace BootstrapVillas.Models
{
    public partial class Package
    {
        public long PackageID { get; set; }
        public string PackageName { get; set; }
        public string PackageDescription { get; set; }
        public string WhenCreated { get; set; }
        public DateTime CreationDate { get; set; }
        public Nullable<DateTime> ValidFrom { get; set; }
        public Nullable<DateTime> ValidUntil { get; set; }
        public long PropertyID { get; set; }
        public decimal Price { get; set; }
        public bool Enabled { get; set; }
        public virtual Property Property { get; set; }
    }
}
