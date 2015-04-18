using System;
using System.Collections.Generic;

namespace BootstrapVillas.Models
{
    public partial class EntityType
    {
        public EntityType()
        {
            this.EntityTypeDetailFields = new List<EntityTypeDetailField>();
        }

        public long EntityTypeID { get; set; }
        public string EntityTypeName { get; set; }
        public virtual ICollection<EntityTypeDetailField> EntityTypeDetailFields { get; set; }
    }
}
