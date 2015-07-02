using System;
using System.Collections.Generic;

namespace BootstrapVillas.Models
{
    public partial class Customer
    {
        public Customer()
        {
            this.Bookings = new List<Booking>();
            this.BookingExtraSelections = new List<BookingExtraSelection>();
            this.BookingParentContainers = new List<BookingParentContainer>();
            this.Cases = new List<Case>();
            this.CustomerLogins = new List<CustomerLogin>();
        }

        public long CustomerID { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public Nullable<DateTime> DOB { get; set; }
        public string EmailAddress { get; set; }
        public string AltEmailAddress { get; set; }
        public string DayTimeTelephone { get; set; }
        public string HomeTelephone { get; set; }
        public string MobileTelephone { get; set; }
        public string AltTelephone { get; set; }
        public string CompanyTelephone { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Town { get; set; }
        public string Postcode { get; set; }
        public string County { get; set; }
        public string Country { get; set; }
        public Nullable<bool> Test { get; set; }
        public Nullable<int> WhoUpdated { get; set; }
        public Nullable<DateTime> CreationDate { get; set; }
        public string PreferredCurrency { get; set; }
        public string PreferredCurrencySymbol { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; }
        public virtual ICollection<BookingExtraSelection> BookingExtraSelections { get; set; }
        public virtual ICollection<BookingParentContainer> BookingParentContainers { get; set; }
        public virtual ICollection<Case> Cases { get; set; }
        public virtual ICollection<CustomerLogin> CustomerLogins { get; set; }
    }
}
