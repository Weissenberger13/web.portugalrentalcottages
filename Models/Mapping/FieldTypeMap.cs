using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BootstrapVillas.Models.Mapping
{
    public class FieldTypeMap : EntityTypeConfiguration<FieldType>
    {
        public FieldTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.FieldTypeID);

            // Properties
            this.Property(t => t.FieldTypeID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.FieldTypeName)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("FieldType");
            this.Property(t => t.FieldTypeID).HasColumnName("FieldTypeID");
            this.Property(t => t.FieldTypeName).HasColumnName("FieldTypeName");
        }
    }
}
