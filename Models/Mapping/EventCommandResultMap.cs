using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BootstrapVillas.Models.Mapping
{
    public class EventCommandResultMap : EntityTypeConfiguration<EventCommandResult>
    {
        public EventCommandResultMap()
        {
            // Primary Key
            this.HasKey(t => t.EventCommandResultID);

            // Properties
            // Table & Column Mappings
            this.ToTable("EventCommandResult");
            this.Property(t => t.EventCommandResultID).HasColumnName("EventCommandResultID");
            this.Property(t => t.EventCommandID).HasColumnName("EventCommandID");
            this.Property(t => t.CommandExecutedInfo).HasColumnName("CommandExecutedInfo");
            this.Property(t => t.ResultCode).HasColumnName("ResultCode");
            this.Property(t => t.ResultMessage).HasColumnName("ResultMessage");

            // Relationships
            this.HasOptional(t => t.EventCommand)
                .WithMany(t => t.EventCommandResults)
                .HasForeignKey(d => d.EventCommandID);

        }
    }
}
