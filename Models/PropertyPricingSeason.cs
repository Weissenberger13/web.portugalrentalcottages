using System;
using System.Collections.Generic;

namespace BootstrapVillas.Models
{
    public partial class PropertyPricingSeason
    {
        public PropertyPricingSeason()
        {
            this.PropertyPricingSeasonalInstances = new List<PropertyPricingSeasonalInstance>();
        }

        public int PropertyPricingSeasonID { get; set; }
        public Nullable<DateTime> SeasonStartDate { get; set; }
        public Nullable<DateTime> SeasonEndDate { get; set; }
        public string Season_Name { get; set; }
        public Nullable<int> SeasonOrdinalInYear { get; set; }
        public Nullable<int> PropertyPricingComissionID { get; set; }
        public virtual PropertyPricingCommission PropertyPricingCommission { get; set; }
        public virtual ICollection<PropertyPricingSeasonalInstance> PropertyPricingSeasonalInstances { get; set; }
    }
}
