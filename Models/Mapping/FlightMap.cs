using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BootstrapVillas.Models.Mapping
{
    public class FlightMap : EntityTypeConfiguration<Flight>
    {
        public FlightMap()
        {
            // Primary Key
            this.HasKey(t => t.FlightID);

            // Properties
            this.Property(t => t.FlightCode)
                .HasMaxLength(200);

            this.Property(t => t.DepartingAirport)
                .HasMaxLength(200);

            this.Property(t => t.DestinationAiport)
                .HasMaxLength(200);

            this.Property(t => t.DepartingCountry)
                .HasMaxLength(200);

            this.Property(t => t.DestinationCountry)
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("Flight");
            this.Property(t => t.FlightID).HasColumnName("FlightID");
            this.Property(t => t.FlightCode).HasColumnName("FlightCode");
            this.Property(t => t.DepartingAirport).HasColumnName("DepartingAirport");
            this.Property(t => t.DestinationAiport).HasColumnName("DestinationAiport");
            this.Property(t => t.DepartingCountry).HasColumnName("DepartingCountry");
            this.Property(t => t.DestinationCountry).HasColumnName("DestinationCountry");
        }
    }
}
