using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BootstrapVillas.Models.Mapping
{
    public class PropertyVacationTypeMap : EntityTypeConfiguration<PropertyVacationType>
    {
        public PropertyVacationTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.PropertyVacationTypeID);

            // Properties
            this.Property(t => t.PropertyVacationTypeDescription)
                .HasMaxLength(1000);

            // Table & Column Mappings
            this.ToTable("PropertyVacationType");
            this.Property(t => t.PropertyVacationTypeID).HasColumnName("PropertyVacationTypeID");
            this.Property(t => t.PropertyVacationTypeDescription).HasColumnName("PropertyVacationTypeDescription");
        }
    }
}
