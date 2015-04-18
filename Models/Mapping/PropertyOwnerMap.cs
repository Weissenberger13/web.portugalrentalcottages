using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BootstrapVillas.Models.Mapping
{
    public class PropertyOwnerMap : EntityTypeConfiguration<PropertyOwner>
    {
        public PropertyOwnerMap()
        {
            // Primary Key
            this.HasKey(t => t.PropertyOwnerID);

            // Properties
            this.Property(t => t.PropertyOwnerEmailAddress)
                .HasMaxLength(200);

            this.Property(t => t.PropertyOwnerEmailAddress2)
                .HasMaxLength(200);

            this.Property(t => t.PropertyOwnerEmailAddress3)
                .HasMaxLength(200);

            this.Property(t => t.OwnerFirstName)
                .HasMaxLength(200);

            this.Property(t => t.OwnerLastName)
                .HasMaxLength(200);

            this.Property(t => t.PropertyOwnerHouseName)
                .HasMaxLength(50);

            this.Property(t => t.PropertyOwnerHouseNumber)
                .HasMaxLength(50);

            this.Property(t => t.PropertyOwnerAddressLine1)
                .HasMaxLength(200);

            this.Property(t => t.PropertyOwnerAddressLine2)
                .HasMaxLength(200);

            this.Property(t => t.PropertyOwnerTown)
                .HasMaxLength(200);

            this.Property(t => t.PropertyOwnerPostcode)
                .IsFixedLength()
                .HasMaxLength(50);

            this.Property(t => t.PropertyOwnerCounty)
                .HasMaxLength(200);

            this.Property(t => t.PropertyOwnerCountry)
                .HasMaxLength(200);

            this.Property(t => t.PropertyOwnerHomeTelephone)
                .HasMaxLength(50);

            this.Property(t => t.PropertyOwnerMobileTelephone)
                .HasMaxLength(50);

            this.Property(t => t.WhenUpdated)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("PropertyOwner");
            this.Property(t => t.PropertyOwnerID).HasColumnName("PropertyOwnerID");
            this.Property(t => t.PropertyOwnerEmailAddress).HasColumnName("PropertyOwnerEmailAddress");
            this.Property(t => t.PropertyOwnerEmailAddress2).HasColumnName("PropertyOwnerEmailAddress2");
            this.Property(t => t.PropertyOwnerEmailAddress3).HasColumnName("PropertyOwnerEmailAddress3");
            this.Property(t => t.OwnerFirstName).HasColumnName("OwnerFirstName");
            this.Property(t => t.OwnerLastName).HasColumnName("OwnerLastName");
            this.Property(t => t.PropertyOwnerHouseName).HasColumnName("PropertyOwnerHouseName");
            this.Property(t => t.PropertyOwnerHouseNumber).HasColumnName("PropertyOwnerHouseNumber");
            this.Property(t => t.PropertyOwnerAddressLine1).HasColumnName("PropertyOwnerAddressLine1");
            this.Property(t => t.PropertyOwnerAddressLine2).HasColumnName("PropertyOwnerAddressLine2");
            this.Property(t => t.PropertyOwnerTown).HasColumnName("PropertyOwnerTown");
            this.Property(t => t.PropertyOwnerPostcode).HasColumnName("PropertyOwnerPostcode");
            this.Property(t => t.PropertyOwnerCounty).HasColumnName("PropertyOwnerCounty");
            this.Property(t => t.PropertyOwnerCountry).HasColumnName("PropertyOwnerCountry");
            this.Property(t => t.PropertyOwnerHomeTelephone).HasColumnName("PropertyOwnerHomeTelephone");
            this.Property(t => t.PropertyOwnerMobileTelephone).HasColumnName("PropertyOwnerMobileTelephone");
            this.Property(t => t.WhenCreated).HasColumnName("WhenCreated");
            this.Property(t => t.WhenUpdated).HasColumnName("WhenUpdated");
            this.Property(t => t.PropertyOwnerNotes).HasColumnName("PropertyOwnerNotes");
            this.Property(t => t.PropertyOwnerBankDetails).HasColumnName("PropertyOwnerBankDetails");
            this.Property(t => t.PropertyOwnerBankDetailsNotes).HasColumnName("PropertyOwnerBankDetailsNotes");
        }
    }
}
