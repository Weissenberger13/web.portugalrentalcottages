using System;
using System.Collections.Generic;

namespace BootstrapVillas.Models
{
    public partial class PropertyTypeServicesChargeInstance
    {
        public int PropertyTypeServicesChargeInstanceID { get; set; }
        public Nullable<int> Bedrooms { get; set; }
        public Nullable<decimal> ServicePriceGBP { get; set; }
        public long PropertyTypeID { get; set; }
        public int PropertyTypeServicesID { get; set; }
        public virtual PropertyType PropertyType { get; set; }
        public virtual PropertyTypeService PropertyTypeService { get; set; }
    }
}
