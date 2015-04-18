using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BootstrapVillas.Models.Mapping
{
    public class SeasonMap : EntityTypeConfiguration<Season>
    {
        public SeasonMap()
        {
            // Primary Key
            this.HasKey(t => t.SeasonID);

            // Properties
            this.Property(t => t.SeasonName)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("Season");
            this.Property(t => t.SeasonID).HasColumnName("SeasonID");
            this.Property(t => t.SeasonName).HasColumnName("SeasonName");
            this.Property(t => t.SeasonCommissionPercent).HasColumnName("SeasonCommissionPercent");
        }
    }
}
