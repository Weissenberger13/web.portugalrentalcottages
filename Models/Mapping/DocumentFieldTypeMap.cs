using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BootstrapVillas.Models.Mapping
{
    public class DocumentFieldTypeMap : EntityTypeConfiguration<DocumentFieldType>
    {
        public DocumentFieldTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.DocumentFieldTypeID);

            // Properties
            // Table & Column Mappings
            this.ToTable("DocumentFieldType");
            this.Property(t => t.DocumentFieldTypeID).HasColumnName("DocumentFieldTypeID");
            this.Property(t => t.DocumentFieldTypeName).HasColumnName("DocumentFieldTypeName");
            this.Property(t => t.DocumentFieldTypeDescription).HasColumnName("DocumentFieldTypeDescription");
        }
    }
}
