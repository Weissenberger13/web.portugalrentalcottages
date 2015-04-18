using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BootstrapVillas.Models.Mapping
{
    public class StandardSeasonMap : EntityTypeConfiguration<StandardSeason>
    {
        public StandardSeasonMap()
        {
            // Primary Key
            this.HasKey(t => t.StandardSeasonID);

            // Properties
            this.Property(t => t.SeasonName)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("StandardSeasons");
            this.Property(t => t.StandardSeasonID).HasColumnName("StandardSeasonID");
            this.Property(t => t.SeasonName).HasColumnName("SeasonName");
            this.Property(t => t.SeasonStartDate).HasColumnName("SeasonStartDate");
            this.Property(t => t.SeasonEndDate).HasColumnName("SeasonEndDate");
        }
    }
}
