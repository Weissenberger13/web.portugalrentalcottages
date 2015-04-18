using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BootstrapVillas.Models.Mapping
{
    public class PropertyTypeServicesChargeInstanceMap : EntityTypeConfiguration<PropertyTypeServicesChargeInstance>
    {
        public PropertyTypeServicesChargeInstanceMap()
        {
            // Primary Key
            this.HasKey(t => t.PropertyTypeServicesChargeInstanceID);

            // Properties
            // Table & Column Mappings
            this.ToTable("PropertyTypeServicesChargeInstance");
            this.Property(t => t.PropertyTypeServicesChargeInstanceID).HasColumnName("PropertyTypeServicesChargeInstanceID");
            this.Property(t => t.Bedrooms).HasColumnName("Bedrooms");
            this.Property(t => t.ServicePriceGBP).HasColumnName("ServicePriceGBP");
            this.Property(t => t.PropertyTypeID).HasColumnName("PropertyTypeID");
            this.Property(t => t.PropertyTypeServicesID).HasColumnName("PropertyTypeServicesID");

            // Relationships
            this.HasRequired(t => t.PropertyType)
                .WithMany(t => t.PropertyTypeServicesChargeInstances)
                .HasForeignKey(d => d.PropertyTypeID);
            this.HasRequired(t => t.PropertyTypeService)
                .WithMany(t => t.PropertyTypeServicesChargeInstances)
                .HasForeignKey(d => d.PropertyTypeServicesID);

        }
    }
}
