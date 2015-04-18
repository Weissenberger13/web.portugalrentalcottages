using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BootstrapVillas.Models.Mapping
{
    public class CustomerBankDetailMap : EntityTypeConfiguration<CustomerBankDetail>
    {
        public CustomerBankDetailMap()
        {
            // Primary Key
            this.HasKey(t => t.CustomerBankDetailID);

            // Properties
            this.Property(t => t.NameOnAccount)
                .HasMaxLength(300);

            this.Property(t => t.IBAN)
                .HasMaxLength(300);

            this.Property(t => t.SwiftCode_BIC)
                .HasMaxLength(300);

            this.Property(t => t.BankName)
                .HasMaxLength(300);

            this.Property(t => t.BankAddressLine1)
                .HasMaxLength(300);

            this.Property(t => t.BankAddressLine2)
                .HasMaxLength(300);

            this.Property(t => t.SortCode)
                .HasMaxLength(50);

            this.Property(t => t.Account)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("CustomerBankDetail");
            this.Property(t => t.CustomerBankDetailID).HasColumnName("CustomerBankDetailID");
            this.Property(t => t.NameOnAccount).HasColumnName("NameOnAccount");
            this.Property(t => t.IBAN).HasColumnName("IBAN");
            this.Property(t => t.SwiftCode_BIC).HasColumnName("SwiftCode_BIC");
            this.Property(t => t.BankName).HasColumnName("BankName");
            this.Property(t => t.BankAddressLine1).HasColumnName("BankAddressLine1");
            this.Property(t => t.BankAddressLine2).HasColumnName("BankAddressLine2");
            this.Property(t => t.CustomerID).HasColumnName("CustomerID");
            this.Property(t => t.SortCode).HasColumnName("SortCode");
            this.Property(t => t.Account).HasColumnName("Account");
        }
    }
}
