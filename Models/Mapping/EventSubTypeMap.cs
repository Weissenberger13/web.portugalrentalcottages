using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BootstrapVillas.Models.Mapping
{
    public class EventSubTypeMap : EntityTypeConfiguration<EventSubType>
    {
        public EventSubTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.EventSubTypeID);

            // Properties
            this.Property(t => t.EventSubTypeName)
                .IsRequired()
                .HasMaxLength(300);

            // Table & Column Mappings
            this.ToTable("EventSubType");
            this.Property(t => t.EventSubTypeID).HasColumnName("EventSubTypeID");
            this.Property(t => t.EventSubTypeName).HasColumnName("EventSubTypeName");
        }
    }
}
