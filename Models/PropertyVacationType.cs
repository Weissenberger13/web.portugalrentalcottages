using System;
using System.Collections.Generic;

namespace BootstrapVillas.Models
{
    public partial class PropertyVacationType
    {
        public PropertyVacationType()
        {
            this.Properties = new List<Property>();
        }

        public long PropertyVacationTypeID { get; set; }
        public string PropertyVacationTypeDescription { get; set; }
        public virtual ICollection<Property> Properties { get; set; }
    }
}
