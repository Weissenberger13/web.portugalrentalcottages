using System;
using System.Collections.Generic;

namespace BootstrapVillas.Models
{
    public partial class BookingExtra
    {
        public BookingExtra()
        {
            this.BookingExtraAttributes = new List<BookingExtraAttribute>();
            this.BookingExtraPackageMappings = new List<BookingExtraPackageMapping>();
            this.BookingExtraSelections = new List<BookingExtraSelection>();
        }

        public long BookingExtraID { get; set; }
        public Nullable<long> BookingExtraTypeID { get; set; }
        public Nullable<int> BookingExtraSubTypeID { get; set; }
        public string LegacyReference { get; set; }
        public bool Active { get; set; }
        public Nullable<bool> TopLevelItem { get; set; }
        public Nullable<int> MaxPersons { get; set; }
        public string BookingExtraName { get; set; }
        public string BookingExtraDescription { get; set; }
        public string FullSynopsis1 { get; set; }
        public string FullSynopsis2 { get; set; }
        public string FullSynopsis3 { get; set; }
        public string HTMLContent { get; set; }
        public string PriceSynopsis { get; set; }
        public System.DateTime WhenCreated { get; set; }
        public byte[] WhenModified { get; set; }
        public Nullable<decimal> Price { get; set; }
        public string PriceTimespan { get; set; }
        public string PriceSymbol { get; set; }
        public Nullable<decimal> LowSeasonPrice { get; set; }
        public Nullable<decimal> MidSeasonPrice { get; set; }
        public Nullable<decimal> HighSeasonPrice { get; set; }
        public Nullable<decimal> PeakSeasonPrice { get; set; }
        public Nullable<decimal> CarSeatPrice { get; set; }
        public Nullable<decimal> ExtraLuggageCostPerPiece { get; set; }
        public Nullable<decimal> DetourCost { get; set; }
        public Nullable<bool> isHalfDayTour { get; set; }
        public string ExtraMapUrl { get; set; }
        public string ExtraMapNote { get; set; }
        public string PickupDriverName { get; set; }
        public string PickupDriverTel { get; set; }
        public Nullable<decimal> InsuranceExcessPerDay { get; set; }
        public Nullable<decimal> CDWInsurance { get; set; }
        public Nullable<int> AirportPickupLocationID { get; set; }
        public string TourPickupTime { get; set; }
        public string TourDropoffTime { get; set; }
        public Nullable<decimal> DepositExcessEURO { get; set; }
        public virtual AirportDestination AirportDestination { get; set; }
        public virtual BookingExtraType BookingExtraType { get; set; }
        public virtual ICollection<BookingExtraAttribute> BookingExtraAttributes { get; set; }
        public virtual ICollection<BookingExtraPackageMapping> BookingExtraPackageMappings { get; set; }
        public virtual ICollection<BookingExtraSelection> BookingExtraSelections { get; set; }
    }
}
