using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BootstrapVillas.Models.Mapping
{
    public class PropertyTownMap : EntityTypeConfiguration<PropertyTown>
    {
        public PropertyTownMap()
        {
            // Primary Key
            this.HasKey(t => t.PropertyTownID);

            // Properties
            // Table & Column Mappings
            this.ToTable("PropertyTown");
            this.Property(t => t.PropertyTownID).HasColumnName("PropertyTownID");
            this.Property(t => t.PropertyRegionID).HasColumnName("PropertyRegionID");
            this.Property(t => t.TownName).HasColumnName("TownName");

            // Relationships
            this.HasRequired(t => t.PropertyRegion)
                .WithMany(t => t.PropertyTowns)
                .HasForeignKey(d => d.PropertyRegionID);

        }
    }
}
