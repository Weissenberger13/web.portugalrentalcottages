using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BootstrapVillas.Models.Mapping
{
    public class PropertyStaffMap : EntityTypeConfiguration<PropertyStaff>
    {
        public PropertyStaffMap()
        {
            // Primary Key
            this.HasKey(t => t.PropertyStaffID);

            // Properties
            this.Property(t => t.HouseName)
                .HasMaxLength(50);

            this.Property(t => t.HouseNumber)
                .HasMaxLength(50);

            this.Property(t => t.AddressLine1)
                .HasMaxLength(200);

            this.Property(t => t.AddressLine2)
                .HasMaxLength(200);

            this.Property(t => t.Town)
                .HasMaxLength(200);

            this.Property(t => t.Postcode)
                .IsFixedLength()
                .HasMaxLength(50);

            this.Property(t => t.County)
                .HasMaxLength(200);

            this.Property(t => t.Country)
                .HasMaxLength(200);

            this.Property(t => t.StaffHomeTelephone)
                .HasMaxLength(50);

            this.Property(t => t.StaffMobileTelephone)
                .HasMaxLength(50);

            this.Property(t => t.WhenUpdated)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("PropertyStaff");
            this.Property(t => t.PropertyStaffID).HasColumnName("PropertyStaffID");
            this.Property(t => t.HouseName).HasColumnName("HouseName");
            this.Property(t => t.HouseNumber).HasColumnName("HouseNumber");
            this.Property(t => t.AddressLine1).HasColumnName("AddressLine1");
            this.Property(t => t.AddressLine2).HasColumnName("AddressLine2");
            this.Property(t => t.Town).HasColumnName("Town");
            this.Property(t => t.Postcode).HasColumnName("Postcode");
            this.Property(t => t.County).HasColumnName("County");
            this.Property(t => t.Country).HasColumnName("Country");
            this.Property(t => t.StaffHomeTelephone).HasColumnName("StaffHomeTelephone");
            this.Property(t => t.StaffMobileTelephone).HasColumnName("StaffMobileTelephone");
            this.Property(t => t.WhenCreated).HasColumnName("WhenCreated");
            this.Property(t => t.WhenUpdated).HasColumnName("WhenUpdated");
            this.Property(t => t.PropertyStaffTypeID).HasColumnName("PropertyStaffTypeID");

            // Relationships
            this.HasOptional(t => t.PropertyStaffType)
                .WithMany(t => t.PropertyStaffs)
                .HasForeignKey(d => d.PropertyStaffTypeID);

        }
    }
}
