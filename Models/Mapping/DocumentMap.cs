using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BootstrapVillas.Models.Mapping
{
    public class DocumentMap : EntityTypeConfiguration<Document>
    {
        public DocumentMap()
        {
            // Primary Key
            this.HasKey(t => t.DocumentID);

            // Properties
            this.Property(t => t.Filename)
                .HasMaxLength(200);

            this.Property(t => t.Encoding)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Document");
            this.Property(t => t.DocumentID).HasColumnName("DocumentID");
            this.Property(t => t.DocumentTemplateID).HasColumnName("DocumentTemplateID");
            this.Property(t => t.CustomerID).HasColumnName("CustomerID");
            this.Property(t => t.DocumentName).HasColumnName("DocumentName");
            this.Property(t => t.DocumentDescription).HasColumnName("DocumentDescription");
            this.Property(t => t.Filename).HasColumnName("Filename");
            this.Property(t => t.Sent).HasColumnName("Sent");
            this.Property(t => t.WhenCreated).HasColumnName("WhenCreated");
            this.Property(t => t.WhoCreated).HasColumnName("WhoCreated");
            this.Property(t => t.DocumentBLOB).HasColumnName("DocumentBLOB");
            this.Property(t => t.EmailBLOB).HasColumnName("EmailBLOB");
            this.Property(t => t.DocumentFormat).HasColumnName("DocumentFormat");
            this.Property(t => t.EmailHTML).HasColumnName("EmailHTML");
            this.Property(t => t.EmailFrom).HasColumnName("EmailFrom");
            this.Property(t => t.EmailTo).HasColumnName("EmailTo");
            this.Property(t => t.EmailCC).HasColumnName("EmailCC");
            this.Property(t => t.EmailBCC).HasColumnName("EmailBCC");
            this.Property(t => t.Encoding).HasColumnName("Encoding");
            this.Property(t => t.DocumentSize).HasColumnName("DocumentSize");
            this.Property(t => t.EmailSize).HasColumnName("EmailSize");
            this.Property(t => t.BookingExtraSelectionID).HasColumnName("BookingExtraSelectionID");
            this.Property(t => t.CaseID).HasColumnName("CaseID");
            this.Property(t => t.EventID).HasColumnName("EventID");

            // Relationships
            this.HasOptional(t => t.BookingExtraSelection)
                .WithMany(t => t.Documents)
                .HasForeignKey(d => d.BookingExtraSelectionID);
            this.HasOptional(t => t.Case)
                .WithMany(t => t.Documents)
                .HasForeignKey(d => d.CaseID);
            this.HasOptional(t => t.Event)
                .WithMany(t => t.Documents)
                .HasForeignKey(d => d.EventID);

        }
    }
}
