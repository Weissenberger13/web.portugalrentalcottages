using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BootstrapVillas.Models.Mapping
{
    public class EventSchemeTypeMap : EntityTypeConfiguration<EventSchemeType>
    {
        public EventSchemeTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.EventSchemeTypeID);

            // Properties
            this.Property(t => t.EventSchemeTypeName)
                .IsRequired()
                .HasMaxLength(300);

            // Table & Column Mappings
            this.ToTable("EventSchemeType");
            this.Property(t => t.EventSchemeTypeID).HasColumnName("EventSchemeTypeID");
            this.Property(t => t.EventSchemeTypeName).HasColumnName("EventSchemeTypeName");
        }
    }
}
