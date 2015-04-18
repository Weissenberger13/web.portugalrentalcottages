using System;
using System.Collections.Generic;

namespace BootstrapVillas.Models
{
    public partial class BookingExtraSelection
    {
        public BookingExtraSelection()
        {
            this.AccountTransactions = new List<AccountTransaction>();
            this.BookingExtraParticipants = new List<BookingExtraParticipant>();
            this.Documents = new List<Document>();
            this.Events = new List<Event>();
        }

        public long BookingExtraSelectionID { get; set; }
        public Nullable<System.DateTime> ExtraRentalDate { get; set; }
        public Nullable<System.DateTime> ExtraReturnDate { get; set; }
        public long BookingExtraID { get; set; }
        public Nullable<long> BookingParentContainerID { get; set; }
        public System.DateTime WhenCreated { get; set; }
        public Nullable<bool> Test { get; set; }
        public long CustomerID { get; set; }
        public Nullable<int> NumberOfDays { get; set; }
        public bool Cancelled { get; set; }
        public bool Confirmed { get; set; }
        public string BookingExtraPRCReference { get; set; }
        public Nullable<int> NumberOfAdults { get; set; }
        public Nullable<int> NumberOfGuests { get; set; }
        public Nullable<int> NumberOfChildren { get; set; }
        public Nullable<int> NumberOfInfants { get; set; }
        public Nullable<int> NumberOfChildseats { get; set; }
        public string PassportNumber { get; set; }
        public string DrivingLicenceNumber { get; set; }
        public string DrivingLicenceNumber2 { get; set; }
        public string PickupLocation { get; set; }
        public string DropoffLocation { get; set; }
        public string FlightArrivalTime { get; set; }
        public string FlightDepartureTime { get; set; }
        public string FlightArrivalTerminalNumber { get; set; }
        public string FlightDepartureTeminalNumber { get; set; }
        public Nullable<int> Detours { get; set; }
        public Nullable<int> PiecesOfLuggage { get; set; }
        public string BESSpecialRequests { get; set; }
        public string FlightNumberArrival { get; set; }
        public string FlightNumberDeparture { get; set; }
        public string AcceptTC { get; set; }
        public string BESPreferredCurrency { get; set; }
        public Nullable<decimal> BESPrice { get; set; }
        public Nullable<decimal> BESCurrencyConversionPrice { get; set; }
        public string BESCurrencyConversionSymbol { get; set; }
        public Nullable<decimal> BESTotalServicesPrice { get; set; }
        public Nullable<decimal> BESCurrencyExchangeRate { get; set; }
        public Nullable<long> CaseID { get; set; }
        public Nullable<long> EventID { get; set; }
        public Nullable<bool> isDeleted { get; set; }
        public Nullable<int> TotalNoOfVehicles { get; set; }
        public string LeadPassengerName { get; set; }
        public Nullable<int> StandardPiecesOfLuggage { get; set; }
        public Nullable<decimal> BESExtraServicesPrice { get; set; }
        public Nullable<decimal> BESExtraServicesCurrencyConversionPrice { get; set; }
        public Nullable<int> AirportPickupLocationID { get; set; }
        public string ExtraSummaryDescription { get; set; }
        public virtual ICollection<AccountTransaction> AccountTransactions { get; set; }
        public virtual AirportDestination AirportDestination { get; set; }
        public virtual BookingExtra BookingExtra { get; set; }
        public virtual ICollection<BookingExtraParticipant> BookingExtraParticipants { get; set; }
        public virtual BookingParentContainer BookingParentContainer { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual ICollection<Document> Documents { get; set; }
        public virtual ICollection<Event> Events { get; set; }
    }
}
