using System;
using System.Collections.Generic;

namespace BootstrapVillas.Models
{
    public partial class PropertyType
    {
        public PropertyType()
        {
            this.Properties = new List<Property>();
            this.PropertyTypeServicesChargeInstances = new List<PropertyTypeServicesChargeInstance>();
        }

        public long PropertyTypeID { get; set; }
        public string PropertyTypeName { get; set; }
        public Nullable<System.DateTime> WhenCreated { get; set; }
        public virtual ICollection<Property> Properties { get; set; }
        public virtual ICollection<PropertyTypeServicesChargeInstance> PropertyTypeServicesChargeInstances { get; set; }
    }
}
