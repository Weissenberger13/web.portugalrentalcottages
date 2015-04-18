using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BootstrapVillas.Models.Mapping
{
    public class ProcessEventMap : EntityTypeConfiguration<ProcessEvent>
    {
        public ProcessEventMap()
        {
            // Primary Key
            this.HasKey(t => t.ProcessEventID);

            // Properties
            this.Property(t => t.WhenCreated)
                .IsFixedLength()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("ProcessEvent");
            this.Property(t => t.ProcessEventID).HasColumnName("ProcessEventID");
            this.Property(t => t.WhenCreated).HasColumnName("WhenCreated");
            this.Property(t => t.EventTypeID).HasColumnName("EventTypeID");
            this.Property(t => t.BookingID).HasColumnName("BookingID");

            // Relationships
            this.HasRequired(t => t.Booking)
                .WithMany(t => t.ProcessEvents)
                .HasForeignKey(d => d.BookingID);
            this.HasRequired(t => t.EventType)
                .WithMany(t => t.ProcessEvents)
                .HasForeignKey(d => d.EventTypeID);

        }
    }
}
