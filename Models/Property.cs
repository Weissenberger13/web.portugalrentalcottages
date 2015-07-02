using System;
using System.Collections.Generic;

namespace BootstrapVillas.Models
{
    public partial class Property
    {
 

        public long PropertyID { get; set; }
        public string LegacyReference { get; set; }
        public Nullable<long> PropertyOwnerID { get; set; }
        public Nullable<long> PropertyOwnerRepresentativeID { get; set; }
        public Nullable<long> PropertyTypeID { get; set; }
        public Nullable<long> PropertyTownID { get; set; }
        public Nullable<long> PropertyVacationTypeID { get; set; }
        public string HomeawayReference { get; set; }
        public string PropertyName { get; set; }
        public string FullDescription { get; set; }
        public string ChangeOverDay { get; set; }
        public string ChangeoverNotes { get; set; }
        public string Pets { get; set; }
        public string Parking { get; set; }
        public string DisabledAccess { get; set; }
        public string Smoking { get; set; }
        public string SwimmingPool { get; set; }
        public string SwimmingPoolType { get; set; }
        public string HeatedPool { get; set; }
        public string Heating { get; set; }
        public string AirConditioning { get; set; }
        public string InternetAccess { get; set; }
        public string HiFi { get; set; }
        public string TV { get; set; }
        public Nullable<int> Bedrooms { get; set; }
        public Nullable<int> Bathrooms { get; set; }
        public Nullable<int> MaxGuests { get; set; }
        public Nullable<int> SofaBeds { get; set; }
        public Nullable<int> ExtraBeds { get; set; }
        public Nullable<int> Cots { get; set; }
        public Nullable<int> HighChair { get; set; }
        public string Other { get; set; }
        public string MapURL { get; set; }
        public Nullable<float> GPSCoordinatesLongitude { get; set; }
        public Nullable<float> GPSCoordinatesLatitude { get; set; }
        public string LegacySynopsis1 { get; set; }
        public string LegacySynopsis2 { get; set; }
        public string LegacySynopsis3 { get; set; }
        public string PriceSynopsis { get; set; }
        public bool Exclusive { get; set; }
        public string FullAddress { get; set; }
        public string HouseName { get; set; }
        public string HouseNumber { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string Town { get; set; }
        public string Postcode { get; set; }
        public string County { get; set; }
        public string Country { get; set; }
        public string PropertyTelephone { get; set; }
        public Nullable<DateTime> WhenCreated { get; set; }
        public byte[] WhenUpdated { get; set; }
        public Nullable<bool> Active { get; set; }
        public string Alarm { get; set; }
        public string BBQ { get; set; }
        public string DirectionsFromAirport { get; set; }
        public Nullable<int> WhoUpdated { get; set; }
        public Nullable<bool> IsFeatured { get; set; }
        public Nullable<bool> IsSale { get; set; }
        public Nullable<decimal> Deposit_GBP_ { get; set; }
        public Nullable<decimal> BreakageDeposit_GBP_ { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; }
        public virtual ICollection<BookingExternal> BookingExternals { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Package> Packages { get; set; }
        public virtual PropertyOwnerRepresentative PropertyOwnerRepresentative { get; set; }
        public virtual PropertyOwner PropertyOwner { get; set; }
        public virtual PropertyTown PropertyTown { get; set; }
        public virtual PropertyType PropertyType { get; set; }
        public virtual PropertyVacationType PropertyVacationType { get; set; }
        public virtual ICollection<PropertyEntity> PropertyEntities { get; set; }
        public virtual ICollection<PropertyPricingSeasonalInstance> PropertyPricingSeasonalInstances { get; set; }
        public virtual ICollection<PropertySecurityItem> PropertySecurityItems { get; set; }
        public virtual ICollection<PropertyStaffTaskAssignment> PropertyStaffTaskAssignments { get; set; }
    }
}
