using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BootstrapVillas.Models.Mapping
{
    public class PropertySecurityItemTypeMap : EntityTypeConfiguration<PropertySecurityItemType>
    {
        public PropertySecurityItemTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.PropertySecurityItemTypeID);

            // Properties
            this.Property(t => t.PropertySecurityItemTypeName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("PropertySecurityItemType");
            this.Property(t => t.PropertySecurityItemTypeID).HasColumnName("PropertySecurityItemTypeID");
            this.Property(t => t.PropertySecurityItemTypeName).HasColumnName("PropertySecurityItemTypeName");
        }
    }
}
