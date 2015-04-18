using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BootstrapVillas.Models.Mapping
{
    public class EventCommandMap : EntityTypeConfiguration<EventCommand>
    {
        public EventCommandMap()
        {
            // Primary Key
            this.HasKey(t => t.EventCommandID);

            // Properties
            // Table & Column Mappings
            this.ToTable("EventCommand");
            this.Property(t => t.EventCommandID).HasColumnName("EventCommandID");
            this.Property(t => t.EventID).HasColumnName("EventID");
            this.Property(t => t.CommandName).HasColumnName("CommandName");

            // Relationships
            this.HasOptional(t => t.Event)
                .WithMany(t => t.EventCommands)
                .HasForeignKey(d => d.EventID);

        }
    }
}
