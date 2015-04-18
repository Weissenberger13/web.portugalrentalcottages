using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BootstrapVillas.Models.Mapping
{
    public class BookingExtraParticipantMap : EntityTypeConfiguration<BookingExtraParticipant>
    {
        public BookingExtraParticipantMap()
        {
            // Primary Key
            this.HasKey(t => t.BookingExtraParticipantID);

            // Properties
            this.Property(t => t.BookingExtraParticipantTitle)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.BookingExtraParticipantFirstName)
                .HasMaxLength(150);

            this.Property(t => t.BookingExtraParticipantMiddleName)
                .HasMaxLength(150);

            this.Property(t => t.BookingExtraParticipantLastName)
                .HasMaxLength(150);

            this.Property(t => t.BookingExtraParticipantDriversLicenceNo)
                .HasMaxLength(200);

            this.Property(t => t.BookingExtraParticipantDriversPassportNo)
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("BookingExtraParticipant");
            this.Property(t => t.BookingExtraParticipantID).HasColumnName("BookingExtraParticipantID");
            this.Property(t => t.BookingExtraSelectionID).HasColumnName("BookingExtraSelectionID");
            this.Property(t => t.BookingExtraParticipantTitle).HasColumnName("BookingExtraParticipantTitle");
            this.Property(t => t.BookingExtraParticipantFirstName).HasColumnName("BookingExtraParticipantFirstName");
            this.Property(t => t.BookingExtraParticipantMiddleName).HasColumnName("BookingExtraParticipantMiddleName");
            this.Property(t => t.BookingExtraParticipantLastName).HasColumnName("BookingExtraParticipantLastName");
            this.Property(t => t.BookingExtraParticipantDOB).HasColumnName("BookingExtraParticipantDOB");
            this.Property(t => t.BookingExtraParticipantChild).HasColumnName("BookingExtraParticipantChild");
            this.Property(t => t.BookingExtraParticipantInfant).HasColumnName("BookingExtraParticipantInfant");
            this.Property(t => t.BookingExtraParticipantWhenCreated).HasColumnName("BookingExtraParticipantWhenCreated");
            this.Property(t => t.BookingExtraParticipantDriversLicenceNo).HasColumnName("BookingExtraParticipantDriversLicenceNo");
            this.Property(t => t.BookingExtraParticipantDriversPassportNo).HasColumnName("BookingExtraParticipantDriversPassportNo");
            this.Property(t => t.BookingExtraParticipantAge).HasColumnName("BookingExtraParticipantAge");

            // Relationships
            this.HasRequired(t => t.BookingExtraSelection)
                .WithMany(t => t.BookingExtraParticipants)
                .HasForeignKey(d => d.BookingExtraSelectionID);

        }
    }
}
