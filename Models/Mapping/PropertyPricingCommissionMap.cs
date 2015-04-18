using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BootstrapVillas.Models.Mapping
{
    public class PropertyPricingCommissionMap : EntityTypeConfiguration<PropertyPricingCommission>
    {
        public PropertyPricingCommissionMap()
        {
            // Primary Key
            this.HasKey(t => t.PropertyPricingComissionID);

            // Properties
            // Table & Column Mappings
            this.ToTable("PropertyPricingCommission");
            this.Property(t => t.PropertyPricingComissionID).HasColumnName("PropertyPricingComissionID");
            this.Property(t => t.PropertyPricingCommissionRate).HasColumnName("PropertyPricingCommissionRate");
        }
    }
}
