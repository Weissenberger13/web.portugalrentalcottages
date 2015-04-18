using System;
using System.Collections.Generic;

namespace BootstrapVillas.Models
{
    public partial class PropertyStaff
    {
        public PropertyStaff()
        {
            this.PropertyStaffTaskAssignments = new List<PropertyStaffTaskAssignment>();
        }

        public long PropertyStaffID { get; set; }
        public string HouseName { get; set; }
        public string HouseNumber { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string Town { get; set; }
        public string Postcode { get; set; }
        public string County { get; set; }
        public string Country { get; set; }
        public string StaffHomeTelephone { get; set; }
        public string StaffMobileTelephone { get; set; }
        public System.DateTime WhenCreated { get; set; }
        public byte[] WhenUpdated { get; set; }
        public Nullable<long> PropertyStaffTypeID { get; set; }
        public virtual PropertyStaffType PropertyStaffType { get; set; }
        public virtual ICollection<PropertyStaffTaskAssignment> PropertyStaffTaskAssignments { get; set; }
    }
}
