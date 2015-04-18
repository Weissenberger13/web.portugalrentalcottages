using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BootstrapVillas.Models.Mapping
{
    public class DocumentTemplateMap : EntityTypeConfiguration<DocumentTemplate>
    {
        public DocumentTemplateMap()
        {
            // Primary Key
            this.HasKey(t => t.DocumentTemplateID);

            // Properties
            this.Property(t => t.DocumentTemplateTitle)
                .HasMaxLength(200);

            this.Property(t => t.DocumentTemplateLegacyReference)
                .HasMaxLength(200);

            this.Property(t => t.DocumentTemplateEmailSubject)
                .HasMaxLength(300);

            this.Property(t => t.DocumentTemplateOutputFormat)
                .HasMaxLength(20);

            this.Property(t => t.WhenUpdated)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("DocumentTemplate");
            this.Property(t => t.DocumentTemplateID).HasColumnName("DocumentTemplateID");
            this.Property(t => t.DocumentTypeID).HasColumnName("DocumentTypeID");
            this.Property(t => t.DocumentTemplateTitle).HasColumnName("DocumentTemplateTitle");
            this.Property(t => t.DocumentTemplateDescription).HasColumnName("DocumentTemplateDescription");
            this.Property(t => t.DocumentTemplateLegacyReference).HasColumnName("DocumentTemplateLegacyReference");
            this.Property(t => t.DocumentTemplateEmailSubject).HasColumnName("DocumentTemplateEmailSubject");
            this.Property(t => t.DocumentTemplateEmailBodyHTML).HasColumnName("DocumentTemplateEmailBodyHTML");
            this.Property(t => t.DocumentTemplateOutputFormat).HasColumnName("DocumentTemplateOutputFormat");
            this.Property(t => t.Enabled).HasColumnName("Enabled");
            this.Property(t => t.WhenCreated).HasColumnName("WhenCreated");
            this.Property(t => t.WhenUpdated).HasColumnName("WhenUpdated");
            this.Property(t => t.WhoUpdated).HasColumnName("WhoUpdated");
            this.Property(t => t.DocumentTemplateBLOB).HasColumnName("DocumentTemplateBLOB");
            this.Property(t => t.DocumentEmailTemplateBLOB).HasColumnName("DocumentEmailTemplateBLOB");
            this.Property(t => t.DocumentTemplateMergeTabels).HasColumnName("DocumentTemplateMergeTabels");
            this.Property(t => t.DocumentEmailMergeTabels).HasColumnName("DocumentEmailMergeTabels");

            // Relationships
            this.HasOptional(t => t.DocumentType)
                .WithMany(t => t.DocumentTemplates)
                .HasForeignKey(d => d.DocumentTypeID);

        }
    }
}
