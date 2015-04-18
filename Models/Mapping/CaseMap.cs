using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BootstrapVillas.Models.Mapping
{
    public class CaseMap : EntityTypeConfiguration<Case>
    {
        public CaseMap()
        {
            // Primary Key
            this.HasKey(t => t.CaseID);

            // Properties
            // Table & Column Mappings
            this.ToTable("Case");
            this.Property(t => t.CaseID).HasColumnName("CaseID");
            this.Property(t => t.CustomerID).HasColumnName("CustomerID");
            this.Property(t => t.WhenCreated).HasColumnName("WhenCreated");

            // Relationships
            this.HasOptional(t => t.Customer)
                .WithMany(t => t.Cases)
                .HasForeignKey(d => d.CustomerID);

        }
    }
}
