using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BootstrapVillas.Models.Mapping
{
    public class PropertyTaskTypeMap : EntityTypeConfiguration<PropertyTaskType>
    {
        public PropertyTaskTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.PropertyTaskTypeID);

            // Properties
            // Table & Column Mappings
            this.ToTable("PropertyTaskType");
            this.Property(t => t.PropertyTaskTypeID).HasColumnName("PropertyTaskTypeID");
        }
    }
}
