using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BootstrapVillas.Models.Mapping
{
    public class PaymentMap : EntityTypeConfiguration<Payment>
    {
        public PaymentMap()
        {
            // Primary Key
            this.HasKey(t => t.PaymentID);

            // Properties
            this.Property(t => t.WhenModified)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("Payment");
            this.Property(t => t.BookingID).HasColumnName("BookingID");
            this.Property(t => t.PaymentID).HasColumnName("PaymentID");
            this.Property(t => t.WhenCreated).HasColumnName("WhenCreated");
            this.Property(t => t.PaymentTypeID).HasColumnName("PaymentTypeID");
            this.Property(t => t.WhenModified).HasColumnName("WhenModified");

            // Relationships
            this.HasRequired(t => t.Booking)
                .WithMany(t => t.Payments)
                .HasForeignKey(d => d.BookingID);
            this.HasRequired(t => t.PaymentType)
                .WithMany(t => t.Payments)
                .HasForeignKey(d => d.PaymentTypeID);

        }
    }
}
