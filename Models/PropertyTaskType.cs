using System;
using System.Collections.Generic;

namespace BootstrapVillas.Models
{
    public partial class PropertyTaskType
    {
        public PropertyTaskType()
        {
            this.PropertyStaffTaskAssignments = new List<PropertyStaffTaskAssignment>();
        }

        public long PropertyTaskTypeID { get; set; }
        public virtual ICollection<PropertyStaffTaskAssignment> PropertyStaffTaskAssignments { get; set; }
    }
}
