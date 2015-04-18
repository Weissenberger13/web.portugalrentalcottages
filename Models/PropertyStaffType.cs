using System;
using System.Collections.Generic;

namespace BootstrapVillas.Models
{
    public partial class PropertyStaffType
    {
        public PropertyStaffType()
        {
            this.PropertyStaffs = new List<PropertyStaff>();
        }

        public long PropertyStaffTypeID { get; set; }
        public string PropertyStaffTypeDescription { get; set; }
        public virtual ICollection<PropertyStaff> PropertyStaffs { get; set; }
    }
}
