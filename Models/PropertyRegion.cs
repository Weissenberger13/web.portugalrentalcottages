using System;
using System.Collections.Generic;

namespace BootstrapVillas.Models
{
    public partial class PropertyRegion
    {
        public PropertyRegion()
        {
            this.PropertyTowns = new List<PropertyTown>();
        }

        public long PropertyRegionID { get; set; }
        public string RegionName { get; set; }
        public string RegionCode { get; set; }
        public virtual ICollection<PropertyTown> PropertyTowns { get; set; }
    }
}
