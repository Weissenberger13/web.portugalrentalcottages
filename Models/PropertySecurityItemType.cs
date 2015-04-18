using System;
using System.Collections.Generic;

namespace BootstrapVillas.Models
{
    public partial class PropertySecurityItemType
    {
        public PropertySecurityItemType()
        {
            this.PropertySecurityItems = new List<PropertySecurityItem>();
        }

        public long PropertySecurityItemTypeID { get; set; }
        public string PropertySecurityItemTypeName { get; set; }
        public virtual ICollection<PropertySecurityItem> PropertySecurityItems { get; set; }
    }
}
