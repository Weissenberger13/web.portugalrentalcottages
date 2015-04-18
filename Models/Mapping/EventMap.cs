using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BootstrapVillas.Models.Mapping
{
    public class EventMap : EntityTypeConfiguration<Event>
    {
        public EventMap()
        {
            // Primary Key
            this.HasKey(t => t.EventID);

            // Properties
            // Table & Column Mappings
            this.ToTable("Event");
            this.Property(t => t.EventID).HasColumnName("EventID");
            this.Property(t => t.CaseID).HasColumnName("CaseID");
            this.Property(t => t.EventTypeID).HasColumnName("EventTypeID");
            this.Property(t => t.WhenCreated).HasColumnName("WhenCreated");
            this.Property(t => t.BookingID).HasColumnName("BookingID");
            this.Property(t => t.BookingExtraSelectionID).HasColumnName("BookingExtraSelectionID");
            this.Property(t => t.IsDeleted).HasColumnName("IsDeleted");
            this.Property(t => t.PropertyOwnerID).HasColumnName("PropertyOwnerID");

            // Relationships
            this.HasOptional(t => t.Booking)
                .WithMany(t => t.Events)
                .HasForeignKey(d => d.BookingID);
            this.HasOptional(t => t.BookingExtraSelection)
                .WithMany(t => t.Events)
                .HasForeignKey(d => d.BookingExtraSelectionID);
            this.HasOptional(t => t.PropertyOwner)
                .WithMany(t => t.Events)
                .HasForeignKey(d => d.PropertyOwnerID);
            this.HasOptional(t => t.EventType)
                .WithMany(t => t.Events)
                .HasForeignKey(d => d.EventTypeID);

        }
    }
}
