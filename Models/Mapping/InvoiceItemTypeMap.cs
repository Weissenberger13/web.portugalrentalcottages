using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BootstrapVillas.Models.Mapping
{
    public class InvoiceItemTypeMap : EntityTypeConfiguration<InvoiceItemType>
    {
        public InvoiceItemTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.InvoiceItemTypeID);

            // Properties
            this.Property(t => t.InvoiceItemName)
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("InvoiceItemType");
            this.Property(t => t.InvoiceItemTypeID).HasColumnName("InvoiceItemTypeID");
            this.Property(t => t.InvoiceItemName).HasColumnName("InvoiceItemName");
        }
    }
}
