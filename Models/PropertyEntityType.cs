using System;
using System.Collections.Generic;

namespace BootstrapVillas.Models
{
    public partial class PropertyEntityType
    {
        public PropertyEntityType()
        {
            this.PropertyEntities = new List<PropertyEntity>();
        }

        public long PropertyEntityTypeID { get; set; }
        public string PropertyEntityTypeName { get; set; }
        public System.DateTime WhenCreated { get; set; }
        public byte[] WhenUpdated { get; set; }
        public virtual ICollection<PropertyEntity> PropertyEntities { get; set; }
    }
}
