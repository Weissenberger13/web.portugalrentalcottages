using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BootstrapVillas.Models.Mapping
{
    public class PropertySecurityItemMap : EntityTypeConfiguration<PropertySecurityItem>
    {
        public PropertySecurityItemMap()
        {
            // Primary Key
            this.HasKey(t => t.PropertySecurityItemID);

            // Properties
            this.Property(t => t.PropertySecurityItemName)
                .HasMaxLength(50);

            this.Property(t => t.PropertySecurityItemDescription)
                .HasMaxLength(50);

            this.Property(t => t.PropertySecurityItemCode)
                .HasMaxLength(50);

            this.Property(t => t.PropertySecurityItemNote)
                .HasMaxLength(300);

            this.Property(t => t.WhenUpdated)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("PropertySecurityItem");
            this.Property(t => t.PropertySecurityItemID).HasColumnName("PropertySecurityItemID");
            this.Property(t => t.PropertySecurityItemTypeID).HasColumnName("PropertySecurityItemTypeID");
            this.Property(t => t.PropertySecurityItemName).HasColumnName("PropertySecurityItemName");
            this.Property(t => t.PropertySecurityItemDescription).HasColumnName("PropertySecurityItemDescription");
            this.Property(t => t.PropertySecurityItemCode).HasColumnName("PropertySecurityItemCode");
            this.Property(t => t.PropertySecurityItemNote).HasColumnName("PropertySecurityItemNote");
            this.Property(t => t.WhenCreated).HasColumnName("WhenCreated");
            this.Property(t => t.PropertyID).HasColumnName("PropertyID");
            this.Property(t => t.WhenUpdated).HasColumnName("WhenUpdated");

            // Relationships
            this.HasRequired(t => t.Property)
                .WithMany(t => t.PropertySecurityItems)
                .HasForeignKey(d => d.PropertyID);
            this.HasRequired(t => t.PropertySecurityItemType)
                .WithMany(t => t.PropertySecurityItems)
                .HasForeignKey(d => d.PropertySecurityItemTypeID);

        }
    }
}
