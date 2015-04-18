using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BootstrapVillas.Models.Mapping
{
    public class InvoiceItemMap : EntityTypeConfiguration<InvoiceItem>
    {
        public InvoiceItemMap()
        {
            // Primary Key
            this.HasKey(t => t.InvoiceItemID);

            // Properties
            this.Property(t => t.InvoiceItemName)
                .IsFixedLength()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("InvoiceItem");
            this.Property(t => t.InvoiceItemID).HasColumnName("InvoiceItemID");
            this.Property(t => t.InvoiceID).HasColumnName("InvoiceID");
            this.Property(t => t.InvoiceItemTypeID).HasColumnName("InvoiceItemTypeID");
            this.Property(t => t.InvoiceItemName).HasColumnName("InvoiceItemName");

            // Relationships
            this.HasRequired(t => t.Invoice)
                .WithMany(t => t.InvoiceItems)
                .HasForeignKey(d => d.InvoiceID);
            this.HasRequired(t => t.InvoiceItemType)
                .WithMany(t => t.InvoiceItems)
                .HasForeignKey(d => d.InvoiceItemTypeID);

        }
    }
}
