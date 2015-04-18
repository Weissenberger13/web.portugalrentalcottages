using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BootstrapVillas.Models.Mapping
{
    public class PropertyOwnerAccountMap : EntityTypeConfiguration<PropertyOwnerAccount>
    {
        public PropertyOwnerAccountMap()
        {
            // Primary Key
            this.HasKey(t => t.AccountID);

            // Properties
            this.Property(t => t.BankName)
                .HasMaxLength(200);

            this.Property(t => t.BankAddress1)
                .HasMaxLength(800);

            this.Property(t => t.SortCode)
                .HasMaxLength(200);

            this.Property(t => t.IBAN)
                .HasMaxLength(200);

            this.Property(t => t.WhoCreated)
                .HasMaxLength(500);

            this.Property(t => t.WhoUpdated)
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("PropertyOwnerAccount");
            this.Property(t => t.AccountID).HasColumnName("AccountID");
            this.Property(t => t.AccountBalance).HasColumnName("AccountBalance");
            this.Property(t => t.PropertyOwnerID).HasColumnName("PropertyOwnerID");
            this.Property(t => t.ThirdPartyServiceID).HasColumnName("ThirdPartyServiceID");
            this.Property(t => t.BankName).HasColumnName("BankName");
            this.Property(t => t.BankAddress1).HasColumnName("BankAddress1");
            this.Property(t => t.BankAddress2).HasColumnName("BankAddress2");
            this.Property(t => t.SortCode).HasColumnName("SortCode");
            this.Property(t => t.IBAN).HasColumnName("IBAN");
            this.Property(t => t.WhoCreated).HasColumnName("WhoCreated");
            this.Property(t => t.WhenCreated).HasColumnName("WhenCreated");
            this.Property(t => t.LastUpdated).HasColumnName("LastUpdated");
            this.Property(t => t.WhoUpdated).HasColumnName("WhoUpdated");

            // Relationships
            this.HasOptional(t => t.PropertyOwner)
                .WithMany(t => t.PropertyOwnerAccounts)
                .HasForeignKey(d => d.PropertyOwnerID);
            this.HasOptional(t => t.ThirdPartyService)
                .WithMany(t => t.PropertyOwnerAccounts)
                .HasForeignKey(d => d.ThirdPartyServiceID);

        }
    }
}
