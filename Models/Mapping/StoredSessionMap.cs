using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BootstrapVillas.Models.Mapping
{
    public class StoredSessionMap : EntityTypeConfiguration<StoredSession>
    {
        public StoredSessionMap()
        {
            // Primary Key
            this.HasKey(t => t.StoredSessionID);

            // Properties
            // Table & Column Mappings
            this.ToTable("StoredSession");
            this.Property(t => t.StoredSessionID).HasColumnName("StoredSessionID");
            this.Property(t => t.NETSessionID).HasColumnName("NETSessionID");
            this.Property(t => t.WhenCreated).HasColumnName("WhenCreated");
        }
    }
}
