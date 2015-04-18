using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BootstrapVillas.Models.Mapping
{
    public class PropertyEntityTypeMap : EntityTypeConfiguration<PropertyEntityType>
    {
        public PropertyEntityTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.PropertyEntityTypeID);

            // Properties
            this.Property(t => t.PropertyEntityTypeName)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.WhenUpdated)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("PropertyEntityType");
            this.Property(t => t.PropertyEntityTypeID).HasColumnName("PropertyEntityTypeID");
            this.Property(t => t.PropertyEntityTypeName).HasColumnName("PropertyEntityTypeName");
            this.Property(t => t.WhenCreated).HasColumnName("WhenCreated");
            this.Property(t => t.WhenUpdated).HasColumnName("WhenUpdated");
        }
    }
}
