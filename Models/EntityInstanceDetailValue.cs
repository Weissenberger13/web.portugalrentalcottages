using System;
using System.Collections.Generic;

namespace BootstrapVillas.Models
{
    public partial class EntityInstanceDetailValue
    {
        public long EntityInstanceMappingID { get; set; }
        public Nullable<long> BookingExtraSelectionID { get; set; }
        public Nullable<long> BookingID { get; set; }
        public Nullable<long> EntityTypeDetailFieldID { get; set; }
        public string DetailValue { get; set; }
        public Nullable<long> ValueInt { get; set; }
        public Nullable<decimal> ValueMoney { get; set; }
        public byte[] WhenCreated { get; set; }
        public virtual EntityTypeDetailField EntityTypeDetailField { get; set; }
    }
}
