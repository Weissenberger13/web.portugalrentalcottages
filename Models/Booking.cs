using System;
using System.Collections.Generic;

namespace BootstrapVillas.Models
{
    public partial class Booking
    {
        public Booking()
        {
            this.AccountTransactions = new List<AccountTransaction>();
            this.BookingAmendments = new List<BookingAmendment>();
            this.BookingParticipants = new List<BookingParticipant>();
            this.Enquiries = new List<Enquiry>();
            this.Events = new List<Event>();
            this.Invoices = new List<Invoice>();
            this.Payments = new List<Payment>();
            this.ProcessEvents = new List<ProcessEvent>();
        }

        public long BookingID { get; set; }
        public Nullable<long> PropertyID { get; set; }
        public long CustomerID { get; set; }
        public Nullable<System.Guid> BookingGUID { get; set; }
        public Nullable<long> BookingParentContainerID { get; set; }
        public string BookingPRCReference { get; set; }
        public string HomeawayReference { get; set; }
        public Nullable<decimal> BookingPrice { get; set; }
        public Nullable<decimal> InitialDeposit { get; set; }
        public Nullable<System.DateTime> InitialDepositDueDate { get; set; }
        public Nullable<System.DateTime> InitialDepositPaidDate { get; set; }
        public Nullable<System.DateTime> BreakageDepositRemittancePaidDate { get; set; }
        public Nullable<System.DateTime> BreakageDepositRemittanceDueDate { get; set; }
        public Nullable<decimal> CommissionAmount { get; set; }
        public Nullable<decimal> RemittanceAmount { get; set; }
        public Nullable<decimal> FinalRentalPayment { get; set; }
        public Nullable<decimal> FinalRentalPaymentIncBreakageDepositLateBooking { get; set; }
        public Nullable<decimal> FinalRentalPaymentNoBreakageDepositLateBooking { get; set; }
        public Nullable<decimal> BreakageDeposit { get; set; }
        public Nullable<System.DateTime> RentalBalanceDueDate { get; set; }
        public Nullable<System.DateTime> BreakageDepositDueDate { get; set; }
        public bool Cancelled { get; set; }
        public bool Confirmed { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public Nullable<System.TimeSpan> CheckInTime { get; set; }
        public Nullable<System.TimeSpan> CheckOutTime { get; set; }
        public Nullable<int> NumberOfNights { get; set; }
        public long BookingTypeID { get; set; }
        public Nullable<int> NumberOfGuests { get; set; }
        public Nullable<int> NumberOfAdults { get; set; }
        public Nullable<int> NumberOfChildren { get; set; }
        public Nullable<int> NumberOfInfants { get; set; }
        public Nullable<int> TotalNumberOfMinors { get; set; }
        public Nullable<System.DateTime> CreationDate { get; set; }
        public byte[] LastUpdated { get; set; }
        public Nullable<int> WhoUpdated { get; set; }
        public Nullable<bool> Test { get; set; }
        public Nullable<long> DocumentID { get; set; }
        public Nullable<long> PackageID { get; set; }
        public string SpecialRequests { get; set; }
        public Nullable<int> NoOfTowelsRequested { get; set; }
        public Nullable<int> MidVactionCleaning { get; set; }
        public Nullable<int> SwimmingPoolHeating { get; set; }
        public Nullable<int> ExtraLininSet { get; set; }
        public string BookingPreferredCurrency { get; set; }
        public Nullable<decimal> BookingCurrencyConversionPrice { get; set; }
        public string BookingCurrencyConversionSymbol { get; set; }
        public Nullable<decimal> BookingCurrencyExchangeRate { get; set; }
        public Nullable<long> CaseID { get; set; }
        public Nullable<bool> isDeleted { get; set; }
        public Nullable<decimal> TowelsPrice { get; set; }
        public Nullable<decimal> MidVactionCleaningPrice { get; set; }
        public Nullable<decimal> SwimmingPoolHeatingPrice { get; set; }
        public Nullable<decimal> ExtraLininSetPrice { get; set; }
        public Nullable<decimal> HeatingPrice { get; set; }
        public Nullable<int> HeatingNoNights { get; set; }
        public Nullable<decimal> CleaningPostVisitPrice { get; set; }
        public string FlightArrivalTime { get; set; }
        public string FlightDepartureTime { get; set; }
        public Nullable<System.DateTime> FlightArrivalDate { get; set; }
        public Nullable<System.DateTime> FlightDepartureDate { get; set; }
        public string FlightNumberArrival { get; set; }
        public string FlightNumberDeparture { get; set; }
        public Nullable<decimal> TotalOnePaymentBookingAmountExcludingBreakageDeposit { get; set; }
        public Nullable<decimal> TotalOnePaymentBookingAmountIncludingBreakageDeposit { get; set; }
        public Nullable<System.DateTime> FinalRentalPaymentPaidDate { get; set; }
        public Nullable<System.DateTime> FinalRentalPaymentDueDate { get; set; }
        public Nullable<decimal> FinalRentalPaymentPlusBreakageDepositMinusInitialDeposit { get; set; }
        public string Remarks { get; set; }
        public Nullable<decimal> StandardBookingRemainingRentalAmountPlusDeposit { get; set; }
        public Nullable<decimal> CommissionAmount_AlternateCurrency { get; set; }
        public string CommissionAmount_AlternateCurrencyType { get; set; }
        public Nullable<decimal> CommissionAmount_ExchangeRate { get; set; }
        public Nullable<decimal> FinalRentalPaymentMinusDeposit { get; set; }
        public Nullable<decimal> HomeownerPaymentAmount { get; set; }
        public Nullable<decimal> HomeownerPaymentAmount_AlternateCurrency { get; set; }
        public Nullable<decimal> HomeownerPaymentAmount_ExchangeRate { get; set; }
        public string HomeownerPaymentAmount_AlternateCurrencyType { get; set; }
        public virtual ICollection<AccountTransaction> AccountTransactions { get; set; }
        public virtual BookingParentContainer BookingParentContainer { get; set; }
        public virtual Case Case { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Property Property { get; set; }
        public virtual ICollection<BookingAmendment> BookingAmendments { get; set; }
        public virtual ICollection<BookingParticipant> BookingParticipants { get; set; }
        public virtual ICollection<Enquiry> Enquiries { get; set; }
        public virtual ICollection<Event> Events { get; set; }
        public virtual ICollection<Invoice> Invoices { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
        public virtual ICollection<ProcessEvent> ProcessEvents { get; set; }
    }
}
