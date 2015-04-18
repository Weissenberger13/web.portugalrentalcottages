using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BootstrapVillas.Models.Mapping
{
    public class InvoiceTypeMap : EntityTypeConfiguration<InvoiceType>
    {
        public InvoiceTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.InvoiceTypeID);

            // Properties
            // Table & Column Mappings
            this.ToTable("InvoiceType");
            this.Property(t => t.InvoiceTypeID).HasColumnName("InvoiceTypeID");
            this.Property(t => t.CreationDate).HasColumnName("CreationDate");
        }
    }
}
