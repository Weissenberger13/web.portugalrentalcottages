using System;
using System.Collections.Generic;

namespace BootstrapVillas.Models
{
    public partial class PropertyPricingSeasonalInstance
    {
        public int PropertyPricingSeasonalInstanceID { get; set; }
        public int PropertyPricingSeasonID { get; set; }
        public long PropertyID { get; set; }
        public Nullable<decimal> Price { get; set; }
        public virtual Property Property { get; set; }
        public virtual PropertyPricingSeason PropertyPricingSeason { get; set; }
    }
}
