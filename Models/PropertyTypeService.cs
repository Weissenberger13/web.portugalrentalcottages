using System;
using System.Collections.Generic;

namespace BootstrapVillas.Models
{
    public partial class PropertyTypeService
    {
        public PropertyTypeService()
        {
            this.PropertyTypeServicesChargeInstances = new List<PropertyTypeServicesChargeInstance>();
        }

        public int PropertyTypeServicesID { get; set; }
        public string ServiceName { get; set; }
        public int Property1OrExtraType2 { get; set; }
        public virtual ICollection<PropertyTypeServicesChargeInstance> PropertyTypeServicesChargeInstances { get; set; }
    }
}
