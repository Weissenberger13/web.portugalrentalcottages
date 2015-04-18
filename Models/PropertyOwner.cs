using System;
using System.Collections.Generic;

namespace BootstrapVillas.Models
{
    public partial class PropertyOwner
    {
        public PropertyOwner()
        {
            this.Events = new List<Event>();
            this.Properties = new List<Property>();
            this.PropertyOwnerAccounts = new List<PropertyOwnerAccount>();
        }

        public long PropertyOwnerID { get; set; }
        public string PropertyOwnerEmailAddress { get; set; }
        public string PropertyOwnerEmailAddress2 { get; set; }
        public string PropertyOwnerEmailAddress3 { get; set; }
        public string OwnerFirstName { get; set; }
        public string OwnerLastName { get; set; }
        public string PropertyOwnerHouseName { get; set; }
        public string PropertyOwnerHouseNumber { get; set; }
        public string PropertyOwnerAddressLine1 { get; set; }
        public string PropertyOwnerAddressLine2 { get; set; }
        public string PropertyOwnerTown { get; set; }
        public string PropertyOwnerPostcode { get; set; }
        public string PropertyOwnerCounty { get; set; }
        public string PropertyOwnerCountry { get; set; }
        public string PropertyOwnerHomeTelephone { get; set; }
        public string PropertyOwnerMobileTelephone { get; set; }
        public System.DateTime WhenCreated { get; set; }
        public byte[] WhenUpdated { get; set; }
        public string PropertyOwnerNotes { get; set; }
        public string PropertyOwnerBankDetails { get; set; }
        public string PropertyOwnerBankDetailsNotes { get; set; }
        public virtual ICollection<Event> Events { get; set; }
        public virtual ICollection<Property> Properties { get; set; }
        public virtual ICollection<PropertyOwnerAccount> PropertyOwnerAccounts { get; set; }
    }
}
