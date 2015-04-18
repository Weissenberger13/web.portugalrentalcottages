using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BootstrapVillas.Models.Mapping
{
    public class DocumentFieldTemplateMappingMap : EntityTypeConfiguration<DocumentFieldTemplateMapping>
    {
        public DocumentFieldTemplateMappingMap()
        {
            // Primary Key
            this.HasKey(t => t.DocumentFieldTemplateMappingID);

            // Properties
            this.Property(t => t.DocumentFieldTemplateMappingID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("DocumentFieldTemplateMapping");
            this.Property(t => t.DocumentFieldTemplateMappingID).HasColumnName("DocumentFieldTemplateMappingID");
            this.Property(t => t.DocumentFieldID).HasColumnName("DocumentFieldID");
            this.Property(t => t.DocumentTemplateID).HasColumnName("DocumentTemplateID");

            // Relationships
            this.HasRequired(t => t.DocumentField)
                .WithMany(t => t.DocumentFieldTemplateMappings)
                .HasForeignKey(d => d.DocumentFieldID);
            this.HasRequired(t => t.DocumentTemplate)
                .WithMany(t => t.DocumentFieldTemplateMappings)
                .HasForeignKey(d => d.DocumentTemplateID);

        }
    }
}
