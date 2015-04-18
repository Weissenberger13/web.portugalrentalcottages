using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BootstrapVillas.Models.Mapping
{
    public class PRCInformationMap : EntityTypeConfiguration<PRCInformation>
    {
        public PRCInformationMap()
        {
            // Primary Key
            this.HasKey(t => t.PRCInformationID);

            // Properties
            this.Property(t => t.PRCCompanyName)
                .HasMaxLength(150);

            this.Property(t => t.PRCAddress)
                .HasMaxLength(300);

            this.Property(t => t.PRCCountry)
                .HasMaxLength(50);

            this.Property(t => t.PRCOfficePhone)
                .HasMaxLength(50);

            this.Property(t => t.PRCFax)
                .HasMaxLength(50);

            this.Property(t => t.PRCMobile1)
                .HasMaxLength(50);

            this.Property(t => t.PRCMobile2)
                .HasMaxLength(50);

            this.Property(t => t.PRCEmail1)
                .HasMaxLength(300);

            this.Property(t => t.PRCEmail2)
                .HasMaxLength(300);

            this.Property(t => t.PRCEmail3)
                .HasMaxLength(300);

            this.Property(t => t.PRCURL1)
                .HasMaxLength(500);

            this.Property(t => t.PRCURL2)
                .HasMaxLength(500);

            this.Property(t => t.PRCNameOnAccount)
                .HasMaxLength(300);

            this.Property(t => t.PRCIBAN)
                .HasMaxLength(300);

            this.Property(t => t.PRCSwiftCode_BIC)
                .HasMaxLength(300);

            this.Property(t => t.PRCBankName)
                .HasMaxLength(300);

            this.Property(t => t.PRCBankAddressLine1)
                .HasMaxLength(300);

            this.Property(t => t.PRCBankAddressLine2)
                .HasMaxLength(300);

            this.Property(t => t.PRCNotificationEmailAddress)
                .HasMaxLength(300);

            // Table & Column Mappings
            this.ToTable("PRCInformation");
            this.Property(t => t.PRCInformationID).HasColumnName("PRCInformationID");
            this.Property(t => t.PRCCompanyName).HasColumnName("PRCCompanyName");
            this.Property(t => t.PRCAddress).HasColumnName("PRCAddress");
            this.Property(t => t.PRCCountry).HasColumnName("PRCCountry");
            this.Property(t => t.PRCOfficePhone).HasColumnName("PRCOfficePhone");
            this.Property(t => t.PRCFax).HasColumnName("PRCFax");
            this.Property(t => t.PRCMobile1).HasColumnName("PRCMobile1");
            this.Property(t => t.PRCMobile2).HasColumnName("PRCMobile2");
            this.Property(t => t.PRCEmail1).HasColumnName("PRCEmail1");
            this.Property(t => t.PRCEmail2).HasColumnName("PRCEmail2");
            this.Property(t => t.PRCEmail3).HasColumnName("PRCEmail3");
            this.Property(t => t.PRCURL1).HasColumnName("PRCURL1");
            this.Property(t => t.PRCURL2).HasColumnName("PRCURL2");
            this.Property(t => t.PRCNameOnAccount).HasColumnName("PRCNameOnAccount");
            this.Property(t => t.PRCIBAN).HasColumnName("PRCIBAN");
            this.Property(t => t.PRCSwiftCode_BIC).HasColumnName("PRCSwiftCode_BIC");
            this.Property(t => t.PRCBankName).HasColumnName("PRCBankName");
            this.Property(t => t.PRCBankAddressLine1).HasColumnName("PRCBankAddressLine1");
            this.Property(t => t.PRCBankAddressLine2).HasColumnName("PRCBankAddressLine2");
            this.Property(t => t.PRCNotificationEmailAddress).HasColumnName("PRCNotificationEmailAddress");
        }
    }
}
