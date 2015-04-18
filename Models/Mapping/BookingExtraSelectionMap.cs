using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BootstrapVillas.Models.Mapping
{
    public class BookingExtraSelectionMap : EntityTypeConfiguration<BookingExtraSelection>
    {
        public BookingExtraSelectionMap()
        {
            // Primary Key
            this.HasKey(t => t.BookingExtraSelectionID);

            // Properties
            this.Property(t => t.BookingExtraPRCReference)
                .HasMaxLength(100);

            this.Property(t => t.PassportNumber)
                .HasMaxLength(250);

            this.Property(t => t.DrivingLicenceNumber)
                .HasMaxLength(250);

            this.Property(t => t.DrivingLicenceNumber2)
                .HasMaxLength(250);

            this.Property(t => t.PickupLocation)
                .HasMaxLength(250);

            this.Property(t => t.DropoffLocation)
                .HasMaxLength(250);

            this.Property(t => t.FlightArrivalTime)
                .HasMaxLength(150);

            this.Property(t => t.FlightDepartureTime)
                .HasMaxLength(150);

            this.Property(t => t.FlightArrivalTerminalNumber)
                .HasMaxLength(150);

            this.Property(t => t.FlightDepartureTeminalNumber)
                .HasMaxLength(150);

            this.Property(t => t.BESSpecialRequests)
                .HasMaxLength(500);

            this.Property(t => t.FlightNumberArrival)
                .HasMaxLength(150);

            this.Property(t => t.FlightNumberDeparture)
                .HasMaxLength(150);

            this.Property(t => t.AcceptTC)
                .HasMaxLength(50);

            this.Property(t => t.BESPreferredCurrency)
                .HasMaxLength(50);

            this.Property(t => t.BESCurrencyConversionSymbol)
                .HasMaxLength(20);

            this.Property(t => t.LeadPassengerName)
                .HasMaxLength(250);

            // Table & Column Mappings
            this.ToTable("BookingExtraSelection");
            this.Property(t => t.BookingExtraSelectionID).HasColumnName("BookingExtraSelectionID");
            this.Property(t => t.ExtraRentalDate).HasColumnName("ExtraRentalDate");
            this.Property(t => t.ExtraReturnDate).HasColumnName("ExtraReturnDate");
            this.Property(t => t.BookingExtraID).HasColumnName("BookingExtraID");
            this.Property(t => t.BookingParentContainerID).HasColumnName("BookingParentContainerID");
            this.Property(t => t.WhenCreated).HasColumnName("WhenCreated");
            this.Property(t => t.Test).HasColumnName("Test");
            this.Property(t => t.CustomerID).HasColumnName("CustomerID");
            this.Property(t => t.NumberOfDays).HasColumnName("NumberOfDays");
            this.Property(t => t.Cancelled).HasColumnName("Cancelled");
            this.Property(t => t.Confirmed).HasColumnName("Confirmed");
            this.Property(t => t.BookingExtraPRCReference).HasColumnName("BookingExtraPRCReference");
            this.Property(t => t.NumberOfAdults).HasColumnName("NumberOfAdults");
            this.Property(t => t.NumberOfGuests).HasColumnName("NumberOfGuests");
            this.Property(t => t.NumberOfChildren).HasColumnName("NumberOfChildren");
            this.Property(t => t.NumberOfInfants).HasColumnName("NumberOfInfants");
            this.Property(t => t.NumberOfChildseats).HasColumnName("NumberOfChildseats");
            this.Property(t => t.PassportNumber).HasColumnName("PassportNumber");
            this.Property(t => t.DrivingLicenceNumber).HasColumnName("DrivingLicenceNumber");
            this.Property(t => t.DrivingLicenceNumber2).HasColumnName("DrivingLicenceNumber2");
            this.Property(t => t.PickupLocation).HasColumnName("PickupLocation");
            this.Property(t => t.DropoffLocation).HasColumnName("DropoffLocation");
            this.Property(t => t.FlightArrivalTime).HasColumnName("FlightArrivalTime");
            this.Property(t => t.FlightDepartureTime).HasColumnName("FlightDepartureTime");
            this.Property(t => t.FlightArrivalTerminalNumber).HasColumnName("FlightArrivalTerminalNumber");
            this.Property(t => t.FlightDepartureTeminalNumber).HasColumnName("FlightDepartureTeminalNumber");
            this.Property(t => t.Detours).HasColumnName("Detours");
            this.Property(t => t.PiecesOfLuggage).HasColumnName("PiecesOfLuggage");
            this.Property(t => t.BESSpecialRequests).HasColumnName("BESSpecialRequests");
            this.Property(t => t.FlightNumberArrival).HasColumnName("FlightNumberArrival");
            this.Property(t => t.FlightNumberDeparture).HasColumnName("FlightNumberDeparture");
            this.Property(t => t.AcceptTC).HasColumnName("AcceptTC");
            this.Property(t => t.BESPreferredCurrency).HasColumnName("BESPreferredCurrency");
            this.Property(t => t.BESPrice).HasColumnName("BESPrice");
            this.Property(t => t.BESCurrencyConversionPrice).HasColumnName("BESCurrencyConversionPrice");
            this.Property(t => t.BESCurrencyConversionSymbol).HasColumnName("BESCurrencyConversionSymbol");
            this.Property(t => t.BESTotalServicesPrice).HasColumnName("BESTotalServicesPrice");
            this.Property(t => t.BESCurrencyExchangeRate).HasColumnName("BESCurrencyExchangeRate");
            this.Property(t => t.CaseID).HasColumnName("CaseID");
            this.Property(t => t.EventID).HasColumnName("EventID");
            this.Property(t => t.isDeleted).HasColumnName("isDeleted");
            this.Property(t => t.TotalNoOfVehicles).HasColumnName("TotalNoOfVehicles");
            this.Property(t => t.LeadPassengerName).HasColumnName("LeadPassengerName");
            this.Property(t => t.StandardPiecesOfLuggage).HasColumnName("StandardPiecesOfLuggage");
            this.Property(t => t.BESExtraServicesPrice).HasColumnName("BESExtraServicesPrice");
            this.Property(t => t.BESExtraServicesCurrencyConversionPrice).HasColumnName("BESExtraServicesCurrencyConversionPrice");
            this.Property(t => t.AirportPickupLocationID).HasColumnName("AirportPickupLocationID");
            this.Property(t => t.ExtraSummaryDescription).HasColumnName("ExtraSummaryDescription");

            // Relationships
            this.HasOptional(t => t.AirportDestination)
                .WithMany(t => t.BookingExtraSelections)
                .HasForeignKey(d => d.AirportPickupLocationID);
            this.HasRequired(t => t.BookingExtra)
                .WithMany(t => t.BookingExtraSelections)
                .HasForeignKey(d => d.BookingExtraID);
            this.HasOptional(t => t.BookingParentContainer)
                .WithMany(t => t.BookingExtraSelections)
                .HasForeignKey(d => d.BookingParentContainerID);
            this.HasRequired(t => t.Customer)
                .WithMany(t => t.BookingExtraSelections)
                .HasForeignKey(d => d.CustomerID);

        }
    }
}
