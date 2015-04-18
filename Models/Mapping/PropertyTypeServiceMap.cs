using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BootstrapVillas.Models.Mapping
{
    public class PropertyTypeServiceMap : EntityTypeConfiguration<PropertyTypeService>
    {
        public PropertyTypeServiceMap()
        {
            // Primary Key
            this.HasKey(t => t.PropertyTypeServicesID);

            // Properties
            // Table & Column Mappings
            this.ToTable("PropertyTypeServices");
            this.Property(t => t.PropertyTypeServicesID).HasColumnName("PropertyTypeServicesID");
            this.Property(t => t.ServiceName).HasColumnName("ServiceName");
            this.Property(t => t.Property1OrExtraType2).HasColumnName("Property1OrExtraType2");
        }
    }
}
