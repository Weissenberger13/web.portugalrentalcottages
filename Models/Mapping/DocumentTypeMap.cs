using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BootstrapVillas.Models.Mapping
{
    public class DocumentTypeMap : EntityTypeConfiguration<DocumentType>
    {
        public DocumentTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.DocumentTypeID);

            // Properties
            // Table & Column Mappings
            this.ToTable("DocumentType");
            this.Property(t => t.DocumentTypeID).HasColumnName("DocumentTypeID");
            this.Property(t => t.DocumentTypeName).HasColumnName("DocumentTypeName");
            this.Property(t => t.DocumentTypeDescription).HasColumnName("DocumentTypeDescription");
        }
    }
}
