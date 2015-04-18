using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BootstrapVillas.Models.Mapping
{
    public class PackageMap : EntityTypeConfiguration<Package>
    {
        public PackageMap()
        {
            // Primary Key
            this.HasKey(t => t.PackageID);

            // Properties
            this.Property(t => t.PackageName)
                .HasMaxLength(200);

            this.Property(t => t.WhenCreated)
                .IsFixedLength()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("Package");
            this.Property(t => t.PackageID).HasColumnName("PackageID");
            this.Property(t => t.PackageName).HasColumnName("PackageName");
            this.Property(t => t.PackageDescription).HasColumnName("PackageDescription");
            this.Property(t => t.WhenCreated).HasColumnName("WhenCreated");
            this.Property(t => t.CreationDate).HasColumnName("CreationDate");
            this.Property(t => t.ValidFrom).HasColumnName("ValidFrom");
            this.Property(t => t.ValidUntil).HasColumnName("ValidUntil");
            this.Property(t => t.PropertyID).HasColumnName("PropertyID");
            this.Property(t => t.Price).HasColumnName("Price");
            this.Property(t => t.Enabled).HasColumnName("Enabled");

            // Relationships
            this.HasRequired(t => t.Property)
                .WithMany(t => t.Packages)
                .HasForeignKey(d => d.PropertyID);

        }
    }
}
