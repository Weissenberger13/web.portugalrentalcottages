using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BootstrapVillas.Models.Mapping
{
    public class PropertyStaffTaskAssignmentMap : EntityTypeConfiguration<PropertyStaffTaskAssignment>
    {
        public PropertyStaffTaskAssignmentMap()
        {
            // Primary Key
            this.HasKey(t => t.PropertyStaffTaskAssignmentID);

            // Properties
            // Table & Column Mappings
            this.ToTable("PropertyStaffTaskAssignment");
            this.Property(t => t.PropertyStaffTaskAssignmentID).HasColumnName("PropertyStaffTaskAssignmentID");
            this.Property(t => t.PropertyStaffID).HasColumnName("PropertyStaffID");
            this.Property(t => t.PropertyID).HasColumnName("PropertyID");
            this.Property(t => t.PropertyTaskTypeID).HasColumnName("PropertyTaskTypeID");

            // Relationships
            this.HasOptional(t => t.Property)
                .WithMany(t => t.PropertyStaffTaskAssignments)
                .HasForeignKey(d => d.PropertyID);
            this.HasOptional(t => t.PropertyStaff)
                .WithMany(t => t.PropertyStaffTaskAssignments)
                .HasForeignKey(d => d.PropertyStaffID);
            this.HasRequired(t => t.PropertyTaskType)
                .WithMany(t => t.PropertyStaffTaskAssignments)
                .HasForeignKey(d => d.PropertyTaskTypeID);

        }
    }
}
