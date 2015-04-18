using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BootstrapVillas.Models.Mapping
{
    public class DocumentFieldMap : EntityTypeConfiguration<DocumentField>
    {
        public DocumentFieldMap()
        {
            // Primary Key
            this.HasKey(t => t.DocumentFieldID);

            // Properties
            this.Property(t => t.FieldName)
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("DocumentField");
            this.Property(t => t.DocumentFieldID).HasColumnName("DocumentFieldID");
            this.Property(t => t.DocumentFieldTypeID).HasColumnName("DocumentFieldTypeID");
            this.Property(t => t.DocumentFieldName).HasColumnName("DocumentFieldName");
            this.Property(t => t.LookupListID).HasColumnName("LookupListID");
            this.Property(t => t.ValidationTypeID).HasColumnName("ValidationTypeID");
            this.Property(t => t.FieldName).HasColumnName("FieldName");
            this.Property(t => t.Enabled).HasColumnName("Enabled");
            this.Property(t => t.WhenCreated).HasColumnName("WhenCreated");
            this.Property(t => t.WhenModified).HasColumnName("WhenModified");

            // Relationships
            this.HasRequired(t => t.DocumentFieldType)
                .WithMany(t => t.DocumentFields)
                .HasForeignKey(d => d.DocumentFieldTypeID);

        }
    }
}
