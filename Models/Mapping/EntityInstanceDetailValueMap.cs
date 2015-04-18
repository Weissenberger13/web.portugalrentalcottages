using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BootstrapVillas.Models.Mapping
{
    public class EntityInstanceDetailValueMap : EntityTypeConfiguration<EntityInstanceDetailValue>
    {
        public EntityInstanceDetailValueMap()
        {
            // Primary Key
            this.HasKey(t => t.EntityInstanceMappingID);

            // Properties
            this.Property(t => t.EntityInstanceMappingID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.WhenCreated)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("EntityInstanceDetailValue");
            this.Property(t => t.EntityInstanceMappingID).HasColumnName("EntityInstanceMappingID");
            this.Property(t => t.BookingExtraSelectionID).HasColumnName("BookingExtraSelectionID");
            this.Property(t => t.BookingID).HasColumnName("BookingID");
            this.Property(t => t.EntityTypeDetailFieldID).HasColumnName("EntityTypeDetailFieldID");
            this.Property(t => t.DetailValue).HasColumnName("DetailValue");
            this.Property(t => t.ValueInt).HasColumnName("ValueInt");
            this.Property(t => t.ValueMoney).HasColumnName("ValueMoney");
            this.Property(t => t.WhenCreated).HasColumnName("WhenCreated");

            // Relationships
            this.HasOptional(t => t.EntityTypeDetailField)
                .WithMany(t => t.EntityInstanceDetailValues)
                .HasForeignKey(d => d.EntityTypeDetailFieldID);

        }
    }
}
