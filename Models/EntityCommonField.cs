using System;
using System.Collections.Generic;

namespace BootstrapVillas.Models
{
    public partial class EntityCommonField
    {
        public long EntityCommonFieldID { get; set; }
        public string EntityCommonFieldName { get; set; }
        public string EntityCommonFieldValue { get; set; }
        public Nullable<int> EntityCommonFieldOrder { get; set; }
    }
}
