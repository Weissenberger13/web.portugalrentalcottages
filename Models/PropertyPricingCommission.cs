using System;
using System.Collections.Generic;

namespace BootstrapVillas.Models
{
    public partial class PropertyPricingCommission
    {
        public PropertyPricingCommission()
        {
            this.PropertyPricingSeasons = new List<PropertyPricingSeason>();
        }

        public int PropertyPricingComissionID { get; set; }
        public decimal PropertyPricingCommissionRate { get; set; }
        public virtual ICollection<PropertyPricingSeason> PropertyPricingSeasons { get; set; }
    }
}
