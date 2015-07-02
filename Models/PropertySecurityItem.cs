using System;
using System.Collections.Generic;

namespace BootstrapVillas.Models
{
    public partial class PropertySecurityItem
    {
        public long PropertySecurityItemID { get; set; }
        public long PropertySecurityItemTypeID { get; set; }
        public string PropertySecurityItemName { get; set; }
        public string PropertySecurityItemDescription { get; set; }
        public string PropertySecurityItemCode { get; set; }
        public string PropertySecurityItemNote { get; set; }
        public DateTime WhenCreated { get; set; }
        public long PropertyID { get; set; }
        public byte[] WhenUpdated { get; set; }
        public virtual Property Property { get; set; }
        public virtual PropertySecurityItemType PropertySecurityItemType { get; set; }
    }
}
