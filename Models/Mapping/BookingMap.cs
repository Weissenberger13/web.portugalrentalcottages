using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BootstrapVillas.Models.Mapping
{
    public class BookingMap : EntityTypeConfiguration<Booking>
    {
        public BookingMap()
        {
            // Primary Key
            this.HasKey(t => t.BookingID);

            // Properties
            this.Property(t => t.BookingPRCReference)
                .HasMaxLength(150);

            this.Property(t => t.HomeawayReference)
                .HasMaxLength(150);

            this.Property(t => t.LastUpdated)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            this.Property(t => t.BookingPreferredCurrency)
                .HasMaxLength(20);

            this.Property(t => t.BookingCurrencyConversionSymbol)
                .HasMaxLength(20);

            this.Property(t => t.FlightArrivalTime)
                .HasMaxLength(50);

            this.Property(t => t.FlightDepartureTime)
                .HasMaxLength(50);

            this.Property(t => t.FlightNumberArrival)
                .HasMaxLength(200);

            this.Property(t => t.FlightNumberDeparture)
                .HasMaxLength(200);

            this.Property(t => t.Remarks)
                .HasMaxLength(250);

            // Table & Column Mappings
            this.ToTable("Booking");
            this.Property(t => t.BookingID).HasColumnName("BookingID");
            this.Property(t => t.PropertyID).HasColumnName("PropertyID");
            this.Property(t => t.CustomerID).HasColumnName("CustomerID");
            this.Property(t => t.BookingGUID).HasColumnName("BookingGUID");
            this.Property(t => t.BookingParentContainerID).HasColumnName("BookingParentContainerID");
            this.Property(t => t.BookingPRCReference).HasColumnName("BookingPRCReference");
            this.Property(t => t.HomeawayReference).HasColumnName("HomeawayReference");
            this.Property(t => t.BookingPrice).HasColumnName("BookingPrice");
            this.Property(t => t.InitialDeposit).HasColumnName("InitialDeposit");
            this.Property(t => t.InitialDepositDueDate).HasColumnName("InitialDepositDueDate");
            this.Property(t => t.InitialDepositPaidDate).HasColumnName("InitialDepositPaidDate");
            this.Property(t => t.BreakageDepositRemittancePaidDate).HasColumnName("BreakageDepositRemittancePaidDate");
            this.Property(t => t.BreakageDepositRemittanceDueDate).HasColumnName("BreakageDepositRemittanceDueDate");
            this.Property(t => t.CommissionAmount).HasColumnName("CommissionAmount");
            this.Property(t => t.RemittanceAmount).HasColumnName("RemittanceAmount");
            this.Property(t => t.FinalRentalPayment).HasColumnName("FinalRentalPayment");
            this.Property(t => t.FinalRentalPaymentIncBreakageDepositLateBooking).HasColumnName("FinalRentalPaymentIncBreakageDepositLateBooking");
            this.Property(t => t.FinalRentalPaymentNoBreakageDepositLateBooking).HasColumnName("FinalRentalPaymentNoBreakageDepositLateBooking");
            this.Property(t => t.BreakageDeposit).HasColumnName("BreakageDeposit");
            this.Property(t => t.RentalBalanceDueDate).HasColumnName("RentalBalanceDueDate");
            this.Property(t => t.BreakageDepositDueDate).HasColumnName("BreakageDepositDueDate");
            this.Property(t => t.Cancelled).HasColumnName("Cancelled");
            this.Property(t => t.Confirmed).HasColumnName("Confirmed");
            this.Property(t => t.StartDate).HasColumnName("StartDate");
            this.Property(t => t.EndDate).HasColumnName("EndDate");
            this.Property(t => t.CheckInTime).HasColumnName("CheckInTime");
            this.Property(t => t.CheckOutTime).HasColumnName("CheckOutTime");
            this.Property(t => t.NumberOfNights).HasColumnName("NumberOfNights");
            this.Property(t => t.BookingTypeID).HasColumnName("BookingTypeID");
            this.Property(t => t.NumberOfGuests).HasColumnName("NumberOfGuests");
            this.Property(t => t.NumberOfAdults).HasColumnName("NumberOfAdults");
            this.Property(t => t.NumberOfChildren).HasColumnName("NumberOfChildren");
            this.Property(t => t.NumberOfInfants).HasColumnName("NumberOfInfants");
            this.Property(t => t.TotalNumberOfMinors).HasColumnName("TotalNumberOfMinors");
            this.Property(t => t.CreationDate).HasColumnName("CreationDate");
            this.Property(t => t.LastUpdated).HasColumnName("LastUpdated");
            this.Property(t => t.WhoUpdated).HasColumnName("WhoUpdated");
            this.Property(t => t.Test).HasColumnName("Test");
            this.Property(t => t.DocumentID).HasColumnName("DocumentID");
            this.Property(t => t.PackageID).HasColumnName("PackageID");
            this.Property(t => t.SpecialRequests).HasColumnName("SpecialRequests");
            this.Property(t => t.NoOfTowelsRequested).HasColumnName("NoOfTowelsRequested");
            this.Property(t => t.MidVactionCleaning).HasColumnName("MidVactionCleaning");
            this.Property(t => t.SwimmingPoolHeating).HasColumnName("SwimmingPoolHeating");
            this.Property(t => t.ExtraLininSet).HasColumnName("ExtraLininSet");
            this.Property(t => t.BookingPreferredCurrency).HasColumnName("BookingPreferredCurrency");
            this.Property(t => t.BookingCurrencyConversionPrice).HasColumnName("BookingCurrencyConversionPrice");
            this.Property(t => t.BookingCurrencyConversionSymbol).HasColumnName("BookingCurrencyConversionSymbol");
            this.Property(t => t.BookingCurrencyExchangeRate).HasColumnName("BookingCurrencyExchangeRate");
            this.Property(t => t.CaseID).HasColumnName("CaseID");
            this.Property(t => t.isDeleted).HasColumnName("isDeleted");
            this.Property(t => t.TowelsPrice).HasColumnName("TowelsPrice");
            this.Property(t => t.MidVactionCleaningPrice).HasColumnName("MidVactionCleaningPrice");
            this.Property(t => t.SwimmingPoolHeatingPrice).HasColumnName("SwimmingPoolHeatingPrice");
            this.Property(t => t.ExtraLininSetPrice).HasColumnName("ExtraLininSetPrice");
            this.Property(t => t.HeatingPrice).HasColumnName("HeatingPrice");
            this.Property(t => t.HeatingNoNights).HasColumnName("HeatingNoNights");
            this.Property(t => t.CleaningPostVisitPrice).HasColumnName("CleaningPostVisitPrice");
            this.Property(t => t.FlightArrivalTime).HasColumnName("FlightArrivalTime");
            this.Property(t => t.FlightDepartureTime).HasColumnName("FlightDepartureTime");
            this.Property(t => t.FlightArrivalDate).HasColumnName("FlightArrivalDate");
            this.Property(t => t.FlightDepartureDate).HasColumnName("FlightDepartureDate");
            this.Property(t => t.FlightNumberArrival).HasColumnName("FlightNumberArrival");
            this.Property(t => t.FlightNumberDeparture).HasColumnName("FlightNumberDeparture");
            this.Property(t => t.TotalOnePaymentBookingAmountExcludingBreakageDeposit).HasColumnName("TotalOnePaymentBookingAmountExcludingBreakageDeposit");
            this.Property(t => t.TotalOnePaymentBookingAmountIncludingBreakageDeposit).HasColumnName("TotalOnePaymentBookingAmountIncludingBreakageDeposit");
            this.Property(t => t.FinalRentalPaymentPaidDate).HasColumnName("FinalRentalPaymentPaidDate");
            this.Property(t => t.FinalRentalPaymentDueDate).HasColumnName("FinalRentalPaymentDueDate");
            this.Property(t => t.FinalRentalPaymentPlusBreakageDepositMinusInitialDeposit).HasColumnName("FinalRentalPaymentPlusBreakageDepositMinusInitialDeposit");
            this.Property(t => t.Remarks).HasColumnName("Remarks");
            this.Property(t => t.StandardBookingRemainingRentalAmountPlusDeposit).HasColumnName("StandardBookingRemainingRentalAmountPlusDeposit");
            this.Property(t => t.CommissionAmount_AlternateCurrency).HasColumnName("CommissionAmount_AlternateCurrency");
            this.Property(t => t.CommissionAmount_AlternateCurrencyType).HasColumnName("CommissionAmount_AlternateCurrencyType");
            this.Property(t => t.CommissionAmount_ExchangeRate).HasColumnName("CommissionAmount_ExchangeRate");
            this.Property(t => t.FinalRentalPaymentMinusDeposit).HasColumnName("FinalRentalPaymentMinusDeposit");
            this.Property(t => t.HomeownerPaymentAmount).HasColumnName("HomeownerPaymentAmount");
            this.Property(t => t.HomeownerPaymentAmount_AlternateCurrency).HasColumnName("HomeownerPaymentAmount_AlternateCurrency");
            this.Property(t => t.HomeownerPaymentAmount_ExchangeRate).HasColumnName("HomeownerPaymentAmount_ExchangeRate");
            this.Property(t => t.HomeownerPaymentAmount_AlternateCurrencyType).HasColumnName("HomeownerPaymentAmount_AlternateCurrencyType");

            // Relationships
            this.HasOptional(t => t.BookingParentContainer)
                .WithMany(t => t.Bookings)
                .HasForeignKey(d => d.BookingParentContainerID);
            this.HasOptional(t => t.Case)
                .WithMany(t => t.Bookings)
                .HasForeignKey(d => d.CaseID);
            this.HasRequired(t => t.Customer)
                .WithMany(t => t.Bookings)
                .HasForeignKey(d => d.CustomerID);
            this.HasOptional(t => t.Property)
                .WithMany(t => t.Bookings)
                .HasForeignKey(d => d.PropertyID);

        }
    }
}
