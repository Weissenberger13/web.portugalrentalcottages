using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BootstrapVillas.Models.Mapping
{
    public class BookingParticipantMap : EntityTypeConfiguration<BookingParticipant>
    {
        public BookingParticipantMap()
        {
            // Primary Key
            this.HasKey(t => t.BookingParticipantID);

            // Properties
            this.Property(t => t.BookingParticipantTitle)
                .HasMaxLength(50);

            this.Property(t => t.BookingParticipantFirstName)
                .HasMaxLength(100);

            this.Property(t => t.BookingParticipantMiddleName)
                .HasMaxLength(100);

            this.Property(t => t.BookingParticipantLastName)
                .HasMaxLength(100);

            this.Property(t => t.BookingParticipantAge)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("BookingParticipant");
            this.Property(t => t.BookingParticipantID).HasColumnName("BookingParticipantID");
            this.Property(t => t.BookingID).HasColumnName("BookingID");
            this.Property(t => t.BookingParticipantTitle).HasColumnName("BookingParticipantTitle");
            this.Property(t => t.BookingParticipantFirstName).HasColumnName("BookingParticipantFirstName");
            this.Property(t => t.BookingParticipantMiddleName).HasColumnName("BookingParticipantMiddleName");
            this.Property(t => t.BookingParticipantLastName).HasColumnName("BookingParticipantLastName");
            this.Property(t => t.BookingParticipantDOB).HasColumnName("BookingParticipantDOB");
            this.Property(t => t.BookingParticipantChild).HasColumnName("BookingParticipantChild");
            this.Property(t => t.BookingParticipantInfant).HasColumnName("BookingParticipantInfant");
            this.Property(t => t.BookingParticipantWhenCreated).HasColumnName("BookingParticipantWhenCreated");
            this.Property(t => t.BookingParticipantAge).HasColumnName("BookingParticipantAge");
            this.Property(t => t.BookingParticipantNumber).HasColumnName("BookingParticipantNumber");

            // Relationships
            this.HasRequired(t => t.Booking)
                .WithMany(t => t.BookingParticipants)
                .HasForeignKey(d => d.BookingID);

        }
    }
}
