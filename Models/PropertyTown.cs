using System;
using System.Collections.Generic;

namespace BootstrapVillas.Models
{
    public partial class PropertyTown
    {
        public PropertyTown()
        {
            this.Properties = new List<Property>();
        }

        public long PropertyTownID { get; set; }
        public long PropertyRegionID { get; set; }
        public string TownName { get; set; }
        public virtual ICollection<Property> Properties { get; set; }
        public virtual PropertyRegion PropertyRegion { get; set; }
    }
}
