using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BootstrapVillas.Models.Mapping
{
    public class PropertyRegionMap : EntityTypeConfiguration<PropertyRegion>
    {
        public PropertyRegionMap()
        {
            // Primary Key
            this.HasKey(t => t.PropertyRegionID);

            // Properties
            this.Property(t => t.RegionName)
                .HasMaxLength(200);

            this.Property(t => t.RegionCode)
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("PropertyRegion");
            this.Property(t => t.PropertyRegionID).HasColumnName("PropertyRegionID");
            this.Property(t => t.RegionName).HasColumnName("RegionName");
            this.Property(t => t.RegionCode).HasColumnName("RegionCode");
        }
    }
}
