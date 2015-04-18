using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BootstrapVillas.Models.Mapping
{
    public class PropertyPricingSeasonalInstanceMap : EntityTypeConfiguration<PropertyPricingSeasonalInstance>
    {
        public PropertyPricingSeasonalInstanceMap()
        {
            // Primary Key
            this.HasKey(t => t.PropertyPricingSeasonalInstanceID);

            // Properties
            // Table & Column Mappings
            this.ToTable("PropertyPricingSeasonalInstance");
            this.Property(t => t.PropertyPricingSeasonalInstanceID).HasColumnName("PropertyPricingSeasonalInstanceID");
            this.Property(t => t.PropertyPricingSeasonID).HasColumnName("PropertyPricingSeasonID");
            this.Property(t => t.PropertyID).HasColumnName("PropertyID");
            this.Property(t => t.Price).HasColumnName("Price");

            // Relationships
            this.HasRequired(t => t.Property)
                .WithMany(t => t.PropertyPricingSeasonalInstances)
                .HasForeignKey(d => d.PropertyID);
            this.HasRequired(t => t.PropertyPricingSeason)
                .WithMany(t => t.PropertyPricingSeasonalInstances)
                .HasForeignKey(d => d.PropertyPricingSeasonID);

        }
    }
}
