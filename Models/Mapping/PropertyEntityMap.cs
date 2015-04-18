using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BootstrapVillas.Models.Mapping
{
    public class PropertyEntityMap : EntityTypeConfiguration<PropertyEntity>
    {
        public PropertyEntityMap()
        {
            // Primary Key
            this.HasKey(t => t.PropertyEntityID);

            // Properties
            this.Property(t => t.PropertyEntityName)
                .HasMaxLength(100);

            this.Property(t => t.WhenUpdated)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("PropertyEntity");
            this.Property(t => t.PropertyEntityID).HasColumnName("PropertyEntityID");
            this.Property(t => t.PropertyEntityTypeID).HasColumnName("PropertyEntityTypeID");
            this.Property(t => t.PropertyID).HasColumnName("PropertyID");
            this.Property(t => t.PropertyEntitySummaryDescription).HasColumnName("PropertyEntitySummaryDescription");
            this.Property(t => t.PropertyEntityName).HasColumnName("PropertyEntityName");
            this.Property(t => t.PropertyEntityDescription1).HasColumnName("PropertyEntityDescription1");
            this.Property(t => t.PropertyEntityDescription2).HasColumnName("PropertyEntityDescription2");
            this.Property(t => t.PropertyEntityDescription3).HasColumnName("PropertyEntityDescription3");
            this.Property(t => t.PropertyEntityDescription4).HasColumnName("PropertyEntityDescription4");
            this.Property(t => t.PropertyEntityDescription5).HasColumnName("PropertyEntityDescription5");
            this.Property(t => t.PropertyEntityDescription6).HasColumnName("PropertyEntityDescription6");
            this.Property(t => t.PropertyEntityDescription7).HasColumnName("PropertyEntityDescription7");
            this.Property(t => t.PropertyEntityDescription8).HasColumnName("PropertyEntityDescription8");
            this.Property(t => t.PropertyEntityDescription9).HasColumnName("PropertyEntityDescription9");
            this.Property(t => t.PropertyEntityDescription10).HasColumnName("PropertyEntityDescription10");
            this.Property(t => t.PropertyEntityDescription11).HasColumnName("PropertyEntityDescription11");
            this.Property(t => t.PropertyEntityDescription12).HasColumnName("PropertyEntityDescription12");
            this.Property(t => t.PropertyEntityDescription13).HasColumnName("PropertyEntityDescription13");
            this.Property(t => t.PropertyEntityDescription14).HasColumnName("PropertyEntityDescription14");
            this.Property(t => t.PropertyEntityDescription15).HasColumnName("PropertyEntityDescription15");
            this.Property(t => t.WhenCreated).HasColumnName("WhenCreated");
            this.Property(t => t.WhenUpdated).HasColumnName("WhenUpdated");

            // Relationships
            this.HasRequired(t => t.Property)
                .WithMany(t => t.PropertyEntities)
                .HasForeignKey(d => d.PropertyID);
            this.HasRequired(t => t.PropertyEntityType)
                .WithMany(t => t.PropertyEntities)
                .HasForeignKey(d => d.PropertyEntityTypeID);

        }
    }
}
