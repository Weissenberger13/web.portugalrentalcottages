using System;
using System.Collections.Generic;

namespace BootstrapVillas.Models
{
    public partial class ThirdPartyService
    {
        public ThirdPartyService()
        {
            this.PropertyOwnerAccounts = new List<PropertyOwnerAccount>();
        }

        public long ThirdPartyServiceID { get; set; }
        public string ThirdPartyServiceName { get; set; }
        public virtual ICollection<PropertyOwnerAccount> PropertyOwnerAccounts { get; set; }
    }
}
