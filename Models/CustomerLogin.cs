using System;
using System.Collections.Generic;

namespace BootstrapVillas.Models
{
    public partial class CustomerLogin
    {
        public long CustomerLoginID { get; set; }
        public long CustomerID { get; set; }
        public string LoginName { get; set; }
        public string Password { get; set; }
        public Nullable<System.DateTime> LastLogin { get; set; }
        public System.DateTime WhenCreated { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
