using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BootstrapVillas.Models.Mapping
{
    public class PropertyOwnerRepresentativeMap : EntityTypeConfiguration<PropertyOwnerRepresentative>
    {
        public PropertyOwnerRepresentativeMap()
        {
            // Primary Key
            this.HasKey(t => t.PropertyOwnerRepresentativeID);

            // Properties
            this.Property(t => t.PropertyOwnerRepresentativeName)
                .HasMaxLength(100);

            this.Property(t => t.PropertyOwnerRepresentativeContactNumberLandline)
                .HasMaxLength(50);

            this.Property(t => t.PropertyOwnerRepresentativeContactNumberMobile)
                .HasMaxLength(50);

            this.Property(t => t.PropertyOwnerRepresentativeContactEmail)
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("PropertyOwnerRepresentative");
            this.Property(t => t.PropertyOwnerRepresentativeID).HasColumnName("PropertyOwnerRepresentativeID");
            this.Property(t => t.PropertyOwnerRepresentativeName).HasColumnName("PropertyOwnerRepresentativeName");
            this.Property(t => t.PropertyOwnerRepresentativeContactNumberLandline).HasColumnName("PropertyOwnerRepresentativeContactNumberLandline");
            this.Property(t => t.PropertyOwnerRepresentativeContactNumberMobile).HasColumnName("PropertyOwnerRepresentativeContactNumberMobile");
            this.Property(t => t.PropertyOwnerRepresentativeContactEmail).HasColumnName("PropertyOwnerRepresentativeContactEmail");
            this.Property(t => t.WhenCreated).HasColumnName("WhenCreated");
        }
    }
}
