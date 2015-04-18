using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BootstrapVillas.Models.Mapping
{
    public class PropertyStaffTypeMap : EntityTypeConfiguration<PropertyStaffType>
    {
        public PropertyStaffTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.PropertyStaffTypeID);

            // Properties
            this.Property(t => t.PropertyStaffTypeDescription)
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("PropertyStaffType");
            this.Property(t => t.PropertyStaffTypeID).HasColumnName("PropertyStaffTypeID");
            this.Property(t => t.PropertyStaffTypeDescription).HasColumnName("PropertyStaffTypeDescription");
        }
    }
}
