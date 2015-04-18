using System;
using System.Collections.Generic;

namespace BootstrapVillas.Models
{
    public partial class EntityTypeDetailField
    {
        public EntityTypeDetailField()
        {
            this.EntityInstanceDetailValues = new List<EntityInstanceDetailValue>();
        }

        public long EntityTypeDetailFieldID { get; set; }
        public Nullable<long> EntityTypeID { get; set; }
        public string EntityTypeDetailFieldName { get; set; }
        public string EntityTypeDetailFieldDescription { get; set; }
        public string EntityTypeDetailFieldNotes { get; set; }
        public Nullable<long> EntityTypeDetailFieldTypeID { get; set; }
        public Nullable<decimal> EntityTypeDetailFieldPrice { get; set; }
        public string EntityTypeDetailFieldCurrency { get; set; }
        public virtual ICollection<EntityInstanceDetailValue> EntityInstanceDetailValues { get; set; }
        public virtual EntityType EntityType { get; set; }
    }
}
