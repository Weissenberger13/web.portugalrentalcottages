using System;
using System.Collections.Generic;

namespace BootstrapVillas.Models
{
    public partial class PropertyStaffTaskAssignment
    {
        public long PropertyStaffTaskAssignmentID { get; set; }
        public Nullable<long> PropertyStaffID { get; set; }
        public Nullable<long> PropertyID { get; set; }
        public long PropertyTaskTypeID { get; set; }
        public virtual Property Property { get; set; }
        public virtual PropertyStaff PropertyStaff { get; set; }
        public virtual PropertyTaskType PropertyTaskType { get; set; }
    }
}
