using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BootstrapVillas.Models.Mapping
{
    public class TitleMap : EntityTypeConfiguration<Title>
    {
        public TitleMap()
        {
            // Primary Key
            this.HasKey(t => t.TitleID);

            // Properties
            this.Property(t => t.Title1)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Title");
            this.Property(t => t.TitleID).HasColumnName("TitleID");
            this.Property(t => t.Title1).HasColumnName("Title");
        }
    }
}
