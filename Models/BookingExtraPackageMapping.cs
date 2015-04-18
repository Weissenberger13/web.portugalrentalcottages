using System;
using System.Collections.Generic;

namespace BootstrapVillas.Models
{
    public partial class BookingExtraPackageMapping
    {
        public long BookingExtraPackageMappingID { get; set; }
        public long BookingExtraID { get; set; }
        public long PackageID { get; set; }
        public virtual BookingExtra BookingExtra { get; set; }
    }
}
