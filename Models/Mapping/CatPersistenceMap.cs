using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BootstrapVillas.Models.Mapping
{
    public class CatPersistenceMap : EntityTypeConfiguration<CatPersistence>
    {
        public CatPersistenceMap()
        {
            // Primary Key
            this.HasKey(t => new { t.VillaObjectID, t.Object, t.SessionID });

            // Properties
            this.Property(t => t.VillaObjectID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Object)
                .IsRequired();

            this.Property(t => t.SessionID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.VilllaStageProcessID)
                .IsFixedLength()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("CatPersistence");
            this.Property(t => t.CartPersistenceID).HasColumnName("CartPersistenceID");
            this.Property(t => t.VillaObjectID).HasColumnName("VillaObjectID");
            this.Property(t => t.Object).HasColumnName("Object");
            this.Property(t => t.SessionID).HasColumnName("SessionID");
            this.Property(t => t.VilllaStageProcessID).HasColumnName("VilllaStageProcessID");
        }
    }
}
