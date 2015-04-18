using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BootstrapVillas.Models.Mapping
{
    public class EntityTypeMap : EntityTypeConfiguration<EntityType>
    {
        public EntityTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.EntityTypeID);

            // Properties
            this.Property(t => t.EntityTypeName)
                .IsRequired()
                .HasMaxLength(300);

            // Table & Column Mappings
            this.ToTable("EntityType");
            this.Property(t => t.EntityTypeID).HasColumnName("EntityTypeID");
            this.Property(t => t.EntityTypeName).HasColumnName("EntityTypeName");
        }
    }
}
