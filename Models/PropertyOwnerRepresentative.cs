using System;
using System.Collections.Generic;

namespace BootstrapVillas.Models
{
    public partial class PropertyOwnerRepresentative
    {
        public PropertyOwnerRepresentative()
        {
            this.Properties = new List<Property>();
        }

        public long PropertyOwnerRepresentativeID { get; set; }
        public string PropertyOwnerRepresentativeName { get; set; }
        public string PropertyOwnerRepresentativeContactNumberLandline { get; set; }
        public string PropertyOwnerRepresentativeContactNumberMobile { get; set; }
        public string PropertyOwnerRepresentativeContactEmail { get; set; }
        public DateTime WhenCreated { get; set; }
        public virtual ICollection<Property> Properties { get; set; }
    }
}
