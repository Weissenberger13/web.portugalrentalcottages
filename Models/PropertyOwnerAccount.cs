using System;
using System.Collections.Generic;

namespace BootstrapVillas.Models
{
    public partial class PropertyOwnerAccount
    {
        public PropertyOwnerAccount()
        {
            this.AccountTransactions = new List<AccountTransaction>();
        }

        public long AccountID { get; set; }
        public Nullable<decimal> AccountBalance { get; set; }
        public Nullable<long> PropertyOwnerID { get; set; }
        public Nullable<long> ThirdPartyServiceID { get; set; }
        public string BankName { get; set; }
        public string BankAddress1 { get; set; }
        public string BankAddress2 { get; set; }
        public string SortCode { get; set; }
        public string IBAN { get; set; }
        public string WhoCreated { get; set; }
        public Nullable<DateTime> WhenCreated { get; set; }
        public Nullable<DateTime> LastUpdated { get; set; }
        public string WhoUpdated { get; set; }
        public virtual ICollection<AccountTransaction> AccountTransactions { get; set; }
        public virtual PropertyOwner PropertyOwner { get; set; }
        public virtual ThirdPartyService ThirdPartyService { get; set; }
    }
}
