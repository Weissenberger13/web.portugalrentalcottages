using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BootstrapVillas.Models.Mapping
{
    public class BookingExtraPackageMappingMap : EntityTypeConfiguration<BookingExtraPackageMapping>
    {
        public BookingExtraPackageMappingMap()
        {
            // Primary Key
            this.HasKey(t => t.BookingExtraPackageMappingID);

            // Properties
            // Table & Column Mappings
            this.ToTable("BookingExtraPackageMapping");
            this.Property(t => t.BookingExtraPackageMappingID).HasColumnName("BookingExtraPackageMappingID");
            this.Property(t => t.BookingExtraID).HasColumnName("BookingExtraID");
            this.Property(t => t.PackageID).HasColumnName("PackageID");

            // Relationships
            this.HasRequired(t => t.BookingExtra)
                .WithMany(t => t.BookingExtraPackageMappings)
                .HasForeignKey(d => d.BookingExtraID);

        }
    }
}
