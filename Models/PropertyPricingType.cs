using System;
using System.Collections.Generic;

namespace BootstrapVillas.Models
{
    public partial class PropertyPricingType
    {
        public long PropertyPricingTypeID { get; set; }
        public string PropertyPricingSeasonName { get; set; }
        public string PropertyPricingSeasonDescription { get; set; }
        public Nullable<decimal> PropertyPricingCommissionRate { get; set; }
        public byte[] WhenUpdated { get; set; }
        public System.DateTime WhenCreated { get; set; }
    }
}
