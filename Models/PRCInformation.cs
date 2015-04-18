using System;
using System.Collections.Generic;

namespace BootstrapVillas.Models
{
    public partial class PRCInformation
    {
        public long PRCInformationID { get; set; }
        public string PRCCompanyName { get; set; }
        public string PRCAddress { get; set; }
        public string PRCCountry { get; set; }
        public string PRCOfficePhone { get; set; }
        public string PRCFax { get; set; }
        public string PRCMobile1 { get; set; }
        public string PRCMobile2 { get; set; }
        public string PRCEmail1 { get; set; }
        public string PRCEmail2 { get; set; }
        public string PRCEmail3 { get; set; }
        public string PRCURL1 { get; set; }
        public string PRCURL2 { get; set; }
        public string PRCNameOnAccount { get; set; }
        public string PRCIBAN { get; set; }
        public string PRCSwiftCode_BIC { get; set; }
        public string PRCBankName { get; set; }
        public string PRCBankAddressLine1 { get; set; }
        public string PRCBankAddressLine2 { get; set; }
        public string PRCNotificationEmailAddress { get; set; }
    }
}
