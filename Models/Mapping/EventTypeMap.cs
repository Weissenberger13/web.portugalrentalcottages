using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BootstrapVillas.Models.Mapping
{
    public class EventTypeMap : EntityTypeConfiguration<EventType>
    {
        public EventTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.EventTypeID);

            // Properties
            this.Property(t => t.EventTypeName)
                .HasMaxLength(300);

            this.Property(t => t.EventTypeDescription)
                .HasMaxLength(300);

            // Table & Column Mappings
            this.ToTable("EventType");
            this.Property(t => t.EventTypeID).HasColumnName("EventTypeID");
            this.Property(t => t.EventTypeName).HasColumnName("EventTypeName");
            this.Property(t => t.EventTypeDescription).HasColumnName("EventTypeDescription");
            this.Property(t => t.EventSubTypeID).HasColumnName("EventSubTypeID");
            this.Property(t => t.EventSchemeTypeID).HasColumnName("EventSchemeTypeID");
            this.Property(t => t.ManuallyAvailable).HasColumnName("ManuallyAvailable");
            this.Property(t => t.DocumentTypeID).HasColumnName("DocumentTypeID");
            this.Property(t => t.DocumentEnumID).HasColumnName("DocumentEnumID");
            this.Property(t => t.EmailEnumID).HasColumnName("EmailEnumID");
            this.Property(t => t.EmailTemplateId).HasColumnName("EmailTemplateId");

            // Relationships
            this.HasOptional(t => t.DocumentType)
                .WithMany(t => t.EventTypes)
                .HasForeignKey(d => d.DocumentTypeID);
            this.HasOptional(t => t.EmailTemplate)
                .WithMany(t => t.EventTypes)
                .HasForeignKey(d => d.EmailTemplateId);
            this.HasOptional(t => t.EventSchemeType)
                .WithMany(t => t.EventTypes)
                .HasForeignKey(d => d.EventSchemeTypeID);
            this.HasRequired(t => t.EventSubType)
                .WithMany(t => t.EventTypes)
                .HasForeignKey(d => d.EventSubTypeID);

        }
    }
}
