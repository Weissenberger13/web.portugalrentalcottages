using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BootstrapVillas.Models.Mapping
{
    public class BookingExtraVendorMap : EntityTypeConfiguration<BookingExtraVendor>
    {
        public BookingExtraVendorMap()
        {
            // Primary Key
            this.HasKey(t => t.BookingExtraVendorID);

            // Properties
            this.Property(t => t.VendorName)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.VendorPrimaryTelephone)
                .HasMaxLength(50);

            this.Property(t => t.VendorSecondaryTelephone)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("BookingExtraVendor");
            this.Property(t => t.BookingExtraVendorID).HasColumnName("BookingExtraVendorID");
            this.Property(t => t.VendorName).HasColumnName("VendorName");
            this.Property(t => t.VendorPrimaryTelephone).HasColumnName("VendorPrimaryTelephone");
            this.Property(t => t.VendorSecondaryTelephone).HasColumnName("VendorSecondaryTelephone");
        }
    }
}
