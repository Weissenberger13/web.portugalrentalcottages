using System;
using System.Collections.Generic;

namespace BootstrapVillas.Models
{
    public partial class BookingExtraAttribute
    {
        public long BookingExtraAttributeID { get; set; }
        public Nullable<long> BookingExtraID { get; set; }
        public string AttributeName { get; set; }
        public string AttributeContent { get; set; }
        public Nullable<int> AttributeOrder { get; set; }
        public virtual BookingExtra BookingExtra { get; set; }
    }
}
