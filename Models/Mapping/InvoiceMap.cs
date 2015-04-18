using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BootstrapVillas.Models.Mapping
{
    public class InvoiceMap : EntityTypeConfiguration<Invoice>
    {
        public InvoiceMap()
        {
            // Primary Key
            this.HasKey(t => t.InvoiceID);

            // Properties
            this.Property(t => t.WhenModified)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("Invoice");
            this.Property(t => t.BookingID).HasColumnName("BookingID");
            this.Property(t => t.InvoiceID).HasColumnName("InvoiceID");
            this.Property(t => t.Paid).HasColumnName("Paid");
            this.Property(t => t.SubTotal).HasColumnName("SubTotal");
            this.Property(t => t.VAT).HasColumnName("VAT");
            this.Property(t => t.Total).HasColumnName("Total");
            this.Property(t => t.InvoiceTypeID).HasColumnName("InvoiceTypeID");
            this.Property(t => t.PaidDate).HasColumnName("PaidDate");
            this.Property(t => t.WhenCreated).HasColumnName("WhenCreated");
            this.Property(t => t.WhenModified).HasColumnName("WhenModified");

            // Relationships
            this.HasRequired(t => t.Booking)
                .WithMany(t => t.Invoices)
                .HasForeignKey(d => d.BookingID);
            this.HasRequired(t => t.InvoiceType)
                .WithMany(t => t.Invoices)
                .HasForeignKey(d => d.InvoiceTypeID);

        }
    }
}
