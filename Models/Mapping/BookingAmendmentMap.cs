using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BootstrapVillas.Models.Mapping
{
    public class BookingAmendmentMap : EntityTypeConfiguration<BookingAmendment>
    {
        public BookingAmendmentMap()
        {
            // Primary Key
            this.HasKey(t => t.BookingAmendmentID);

            // Properties
            this.Property(t => t.AmendmentNote)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("BookingAmendment");
            this.Property(t => t.BookingAmendmentID).HasColumnName("BookingAmendmentID");
            this.Property(t => t.WhenCreated).HasColumnName("WhenCreated");
            this.Property(t => t.BookingID).HasColumnName("BookingID");
            this.Property(t => t.AmendmentNote).HasColumnName("AmendmentNote");
            this.Property(t => t.WhoUpdated).HasColumnName("WhoUpdated");

            // Relationships
            this.HasRequired(t => t.Booking)
                .WithMany(t => t.BookingAmendments)
                .HasForeignKey(d => d.BookingID);

        }
    }
}
