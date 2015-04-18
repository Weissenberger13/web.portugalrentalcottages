using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BootstrapVillas.Models.Mapping
{
    public class PropertyPricingTypeMap : EntityTypeConfiguration<PropertyPricingType>
    {
        public PropertyPricingTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.PropertyPricingTypeID);

            // Properties
            this.Property(t => t.PropertyPricingSeasonName)
                .HasMaxLength(200);

            this.Property(t => t.PropertyPricingSeasonDescription)
                .HasMaxLength(200);

            this.Property(t => t.WhenUpdated)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("PropertyPricingType");
            this.Property(t => t.PropertyPricingTypeID).HasColumnName("PropertyPricingTypeID");
            this.Property(t => t.PropertyPricingSeasonName).HasColumnName("PropertyPricingSeasonName");
            this.Property(t => t.PropertyPricingSeasonDescription).HasColumnName("PropertyPricingSeasonDescription");
            this.Property(t => t.PropertyPricingCommissionRate).HasColumnName("PropertyPricingCommissionRate");
            this.Property(t => t.WhenUpdated).HasColumnName("WhenUpdated");
            this.Property(t => t.WhenCreated).HasColumnName("WhenCreated");
        }
    }
}
