using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BootstrapVillas.Models.Mapping
{
    public class EntityTypeDetailFieldMap : EntityTypeConfiguration<EntityTypeDetailField>
    {
        public EntityTypeDetailFieldMap()
        {
            // Primary Key
            this.HasKey(t => t.EntityTypeDetailFieldID);

            // Properties
            this.Property(t => t.EntityTypeDetailFieldName)
                .IsRequired()
                .HasMaxLength(300);

            this.Property(t => t.EntityTypeDetailFieldCurrency)
                .IsFixedLength()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("EntityTypeDetailField");
            this.Property(t => t.EntityTypeDetailFieldID).HasColumnName("EntityTypeDetailFieldID");
            this.Property(t => t.EntityTypeID).HasColumnName("EntityTypeID");
            this.Property(t => t.EntityTypeDetailFieldName).HasColumnName("EntityTypeDetailFieldName");
            this.Property(t => t.EntityTypeDetailFieldDescription).HasColumnName("EntityTypeDetailFieldDescription");
            this.Property(t => t.EntityTypeDetailFieldNotes).HasColumnName("EntityTypeDetailFieldNotes");
            this.Property(t => t.EntityTypeDetailFieldTypeID).HasColumnName("EntityTypeDetailFieldTypeID");
            this.Property(t => t.EntityTypeDetailFieldPrice).HasColumnName("EntityTypeDetailFieldPrice");
            this.Property(t => t.EntityTypeDetailFieldCurrency).HasColumnName("EntityTypeDetailFieldCurrency");

            // Relationships
            this.HasOptional(t => t.EntityType)
                .WithMany(t => t.EntityTypeDetailFields)
                .HasForeignKey(d => d.EntityTypeID);

        }
    }
}
