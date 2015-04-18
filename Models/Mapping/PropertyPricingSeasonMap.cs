using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BootstrapVillas.Models.Mapping
{
    public class PropertyPricingSeasonMap : EntityTypeConfiguration<PropertyPricingSeason>
    {
        public PropertyPricingSeasonMap()
        {
            // Primary Key
            this.HasKey(t => t.PropertyPricingSeasonID);

            // Properties
            this.Property(t => t.Season_Name)
                .HasMaxLength(150);

            // Table & Column Mappings
            this.ToTable("PropertyPricingSeason");
            this.Property(t => t.PropertyPricingSeasonID).HasColumnName("PropertyPricingSeasonID");
            this.Property(t => t.SeasonStartDate).HasColumnName("SeasonStartDate");
            this.Property(t => t.SeasonEndDate).HasColumnName("SeasonEndDate");
            this.Property(t => t.Season_Name).HasColumnName("Season Name");
            this.Property(t => t.SeasonOrdinalInYear).HasColumnName("SeasonOrdinalInYear");
            this.Property(t => t.PropertyPricingComissionID).HasColumnName("PropertyPricingComissionID");

            // Relationships
            this.HasOptional(t => t.PropertyPricingCommission)
                .WithMany(t => t.PropertyPricingSeasons)
                .HasForeignKey(d => d.PropertyPricingComissionID);

        }
    }
}
