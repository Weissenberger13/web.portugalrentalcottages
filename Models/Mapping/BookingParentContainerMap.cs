using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BootstrapVillas.Models.Mapping
{
    public class BookingParentContainerMap : EntityTypeConfiguration<BookingParentContainer>
    {
        public BookingParentContainerMap()
        {
            // Primary Key
            this.HasKey(t => t.BookingParentContainerID);

            // Properties
            this.Property(t => t.OverallBookingReference)
                .HasMaxLength(150);

            this.Property(t => t.BookingParentContainerCurrencyConversionSymbol)
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("BookingParentContainer");
            this.Property(t => t.BookingParentContainerID).HasColumnName("BookingParentContainerID");
            this.Property(t => t.CustomerID).HasColumnName("CustomerID");
            this.Property(t => t.CreationDate).HasColumnName("CreationDate");
            this.Property(t => t.TotalBookingContainerPrice).HasColumnName("TotalBookingContainerPrice");
            this.Property(t => t.OverallBookingReference).HasColumnName("OverallBookingReference");
            this.Property(t => t.BookingParentContainerCurrencyConversionPrice).HasColumnName("BookingParentContainerCurrencyConversionPrice");
            this.Property(t => t.BookingParentContainerCurrencyConversionSymbol).HasColumnName("BookingParentContainerCurrencyConversionSymbol");
            this.Property(t => t.BookingSummary).HasColumnName("BookingSummary");
            this.Property(t => t.BookingExtraSummary).HasColumnName("BookingExtraSummary");
            this.Property(t => t.TotalVehicleCount).HasColumnName("TotalVehicleCount");
            this.Property(t => t.SumCarRentalAndExtras).HasColumnName("SumCarRentalAndExtras");

            // Relationships
            this.HasRequired(t => t.Customer)
                .WithMany(t => t.BookingParentContainers)
                .HasForeignKey(d => d.CustomerID);

        }
    }
}
