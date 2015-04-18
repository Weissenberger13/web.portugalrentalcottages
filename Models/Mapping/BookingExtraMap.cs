using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BootstrapVillas.Models.Mapping
{
    public class BookingExtraMap : EntityTypeConfiguration<BookingExtra>
    {
        public BookingExtraMap()
        {
            // Primary Key
            this.HasKey(t => t.BookingExtraID);

            // Properties
            this.Property(t => t.LegacyReference)
                .HasMaxLength(50);

            this.Property(t => t.WhenModified)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            this.Property(t => t.PriceTimespan)
                .HasMaxLength(50);

            this.Property(t => t.PriceSymbol)
                .HasMaxLength(5);

            this.Property(t => t.PickupDriverName)
                .HasMaxLength(150);

            this.Property(t => t.PickupDriverTel)
                .HasMaxLength(25);

            this.Property(t => t.TourPickupTime)
                .HasMaxLength(20);

            this.Property(t => t.TourDropoffTime)
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("BookingExtra");
            this.Property(t => t.BookingExtraID).HasColumnName("BookingExtraID");
            this.Property(t => t.BookingExtraTypeID).HasColumnName("BookingExtraTypeID");
            this.Property(t => t.BookingExtraSubTypeID).HasColumnName("BookingExtraSubTypeID");
            this.Property(t => t.LegacyReference).HasColumnName("LegacyReference");
            this.Property(t => t.Active).HasColumnName("Active");
            this.Property(t => t.TopLevelItem).HasColumnName("TopLevelItem");
            this.Property(t => t.MaxPersons).HasColumnName("MaxPersons");
            this.Property(t => t.BookingExtraName).HasColumnName("BookingExtraName");
            this.Property(t => t.BookingExtraDescription).HasColumnName("BookingExtraDescription");
            this.Property(t => t.FullSynopsis1).HasColumnName("FullSynopsis1");
            this.Property(t => t.FullSynopsis2).HasColumnName("FullSynopsis2");
            this.Property(t => t.FullSynopsis3).HasColumnName("FullSynopsis3");
            this.Property(t => t.HTMLContent).HasColumnName("HTMLContent");
            this.Property(t => t.PriceSynopsis).HasColumnName("PriceSynopsis");
            this.Property(t => t.WhenCreated).HasColumnName("WhenCreated");
            this.Property(t => t.WhenModified).HasColumnName("WhenModified");
            this.Property(t => t.Price).HasColumnName("Price");
            this.Property(t => t.PriceTimespan).HasColumnName("PriceTimespan");
            this.Property(t => t.PriceSymbol).HasColumnName("PriceSymbol");
            this.Property(t => t.LowSeasonPrice).HasColumnName("LowSeasonPrice");
            this.Property(t => t.MidSeasonPrice).HasColumnName("MidSeasonPrice");
            this.Property(t => t.HighSeasonPrice).HasColumnName("HighSeasonPrice");
            this.Property(t => t.PeakSeasonPrice).HasColumnName("PeakSeasonPrice");
            this.Property(t => t.CarSeatPrice).HasColumnName("CarSeatPrice");
            this.Property(t => t.ExtraLuggageCostPerPiece).HasColumnName("ExtraLuggageCostPerPiece");
            this.Property(t => t.DetourCost).HasColumnName("DetourCost");
            this.Property(t => t.isHalfDayTour).HasColumnName("isHalfDayTour");
            this.Property(t => t.ExtraMapUrl).HasColumnName("ExtraMapUrl");
            this.Property(t => t.ExtraMapNote).HasColumnName("ExtraMapNote");
            this.Property(t => t.PickupDriverName).HasColumnName("PickupDriverName");
            this.Property(t => t.PickupDriverTel).HasColumnName("PickupDriverTel");
            this.Property(t => t.InsuranceExcessPerDay).HasColumnName("InsuranceExcessPerDay");
            this.Property(t => t.CDWInsurance).HasColumnName("CDWInsurance");
            this.Property(t => t.AirportPickupLocationID).HasColumnName("AirportPickupLocationID");
            this.Property(t => t.TourPickupTime).HasColumnName("TourPickupTime");
            this.Property(t => t.TourDropoffTime).HasColumnName("TourDropoffTime");
            this.Property(t => t.DepositExcessEURO).HasColumnName("DepositExcessEURO");

            // Relationships
            this.HasOptional(t => t.AirportDestination)
                .WithMany(t => t.BookingExtras)
                .HasForeignKey(d => d.AirportPickupLocationID);
            this.HasOptional(t => t.BookingExtraType)
                .WithMany(t => t.BookingExtras)
                .HasForeignKey(d => d.BookingExtraTypeID);

        }
    }
}
