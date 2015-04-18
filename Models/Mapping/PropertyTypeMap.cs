using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BootstrapVillas.Models.Mapping
{
    public class PropertyTypeMap : EntityTypeConfiguration<PropertyType>
    {
        public PropertyTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.PropertyTypeID);

            // Properties
            this.Property(t => t.PropertyTypeName)
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("PropertyType");
            this.Property(t => t.PropertyTypeID).HasColumnName("PropertyTypeID");
            this.Property(t => t.PropertyTypeName).HasColumnName("PropertyTypeName");
            this.Property(t => t.WhenCreated).HasColumnName("WhenCreated");
        }
    }
}
