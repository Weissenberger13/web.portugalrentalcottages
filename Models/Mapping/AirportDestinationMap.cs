using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BootstrapVillas.Models.Mapping
{
    public class AirportDestinationMap : EntityTypeConfiguration<AirportDestination>
    {
        public AirportDestinationMap()
        {
            // Primary Key
            this.HasKey(t => t.AirportPickupLocationID);

            // Properties
            this.Property(t => t.AirportPickupLocationName)
                .HasMaxLength(300);

            // Table & Column Mappings
            this.ToTable("AirportDestination");
            this.Property(t => t.AirportPickupLocationID).HasColumnName("AirportPickupLocationID");
            this.Property(t => t.AirportPickupLocationName).HasColumnName("AirportPickupLocationName");
        }
    }
}
