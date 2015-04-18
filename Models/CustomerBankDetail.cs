using System;
using System.Collections.Generic;

namespace BootstrapVillas.Models
{
    public partial class CustomerBankDetail
    {
        public long CustomerBankDetailID { get; set; }
        public string NameOnAccount { get; set; }
        public string IBAN { get; set; }
        public string SwiftCode_BIC { get; set; }
        public string BankName { get; set; }
        public string BankAddressLine1 { get; set; }
        public string BankAddressLine2 { get; set; }
        public long CustomerID { get; set; }
        public string SortCode { get; set; }
        public string Account { get; set; }
    }
}
