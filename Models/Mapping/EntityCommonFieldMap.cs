using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BootstrapVillas.Models.Mapping
{
    public class EntityCommonFieldMap : EntityTypeConfiguration<EntityCommonField>
    {
        public EntityCommonFieldMap()
        {
            // Primary Key
            this.HasKey(t => t.EntityCommonFieldID);

            // Properties
            this.Property(t => t.EntityCommonFieldName)
                .HasMaxLength(300);

            this.Property(t => t.EntityCommonFieldValue)
                .HasMaxLength(300);

            // Table & Column Mappings
            this.ToTable("EntityCommonField");
            this.Property(t => t.EntityCommonFieldID).HasColumnName("EntityCommonFieldID");
            this.Property(t => t.EntityCommonFieldName).HasColumnName("EntityCommonFieldName");
            this.Property(t => t.EntityCommonFieldValue).HasColumnName("EntityCommonFieldValue");
            this.Property(t => t.EntityCommonFieldOrder).HasColumnName("EntityCommonFieldOrder");
        }
    }
}
