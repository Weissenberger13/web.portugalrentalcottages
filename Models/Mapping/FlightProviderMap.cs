using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BootstrapVillas.Models.Mapping
{
    public class FlightProviderMap : EntityTypeConfiguration<FlightProvider>
    {
        public FlightProviderMap()
        {
            // Primary Key
            this.HasKey(t => t.FlightProviderID);

            // Properties
            this.Property(t => t.FlightProviderID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.FlightProviderName)
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("FlightProvider");
            this.Property(t => t.FlightProviderID).HasColumnName("FlightProviderID");
            this.Property(t => t.FlightProviderName).HasColumnName("FlightProviderName");
        }
    }
}
