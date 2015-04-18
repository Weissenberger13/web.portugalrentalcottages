using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BootstrapVillas.Models.Mapping
{
    public class BookingExtraTypeMap : EntityTypeConfiguration<BookingExtraType>
    {
        public BookingExtraTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.BookingExtraTypeID);

            // Properties
            this.Property(t => t.ExtraTypeName)
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("BookingExtraType");
            this.Property(t => t.BookingExtraTypeID).HasColumnName("BookingExtraTypeID");
            this.Property(t => t.ExtraTypeName).HasColumnName("ExtraTypeName");
            this.Property(t => t.BookingExtraVendorID).HasColumnName("BookingExtraVendorID");
            this.Property(t => t.ExtraTypeDescription).HasColumnName("ExtraTypeDescription");

            // Relationships
            this.HasOptional(t => t.BookingExtraVendor)
                .WithMany(t => t.BookingExtraTypes)
                .HasForeignKey(d => d.BookingExtraVendorID);

        }
    }
}
