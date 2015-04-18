using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BootstrapVillas.Models.Mapping
{
    public class CustomerMap : EntityTypeConfiguration<Customer>
    {
        public CustomerMap()
        {
            // Primary Key
            this.HasKey(t => t.CustomerID);

            // Properties
            this.Property(t => t.Title)
                .HasMaxLength(50);

            this.Property(t => t.FirstName)
                .HasMaxLength(200);

            this.Property(t => t.MiddleName)
                .HasMaxLength(200);

            this.Property(t => t.LastName)
                .HasMaxLength(200);

            this.Property(t => t.EmailAddress)
                .HasMaxLength(255);

            this.Property(t => t.AltEmailAddress)
                .HasMaxLength(255);

            this.Property(t => t.DayTimeTelephone)
                .HasMaxLength(50);

            this.Property(t => t.HomeTelephone)
                .HasMaxLength(50);

            this.Property(t => t.MobileTelephone)
                .HasMaxLength(50);

            this.Property(t => t.AltTelephone)
                .HasMaxLength(50);

            this.Property(t => t.CompanyTelephone)
                .HasMaxLength(50);

            this.Property(t => t.Address1)
                .HasMaxLength(200);

            this.Property(t => t.Address2)
                .HasMaxLength(200);

            this.Property(t => t.Town)
                .HasMaxLength(200);

            this.Property(t => t.Postcode)
                .HasMaxLength(50);

            this.Property(t => t.County)
                .HasMaxLength(200);

            this.Property(t => t.Country)
                .HasMaxLength(200);

            this.Property(t => t.PreferredCurrency)
                .HasMaxLength(10);

            this.Property(t => t.PreferredCurrencySymbol)
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("Customer");
            this.Property(t => t.CustomerID).HasColumnName("CustomerID");
            this.Property(t => t.Title).HasColumnName("Title");
            this.Property(t => t.FirstName).HasColumnName("FirstName");
            this.Property(t => t.MiddleName).HasColumnName("MiddleName");
            this.Property(t => t.LastName).HasColumnName("LastName");
            this.Property(t => t.DOB).HasColumnName("DOB");
            this.Property(t => t.EmailAddress).HasColumnName("EmailAddress");
            this.Property(t => t.AltEmailAddress).HasColumnName("AltEmailAddress");
            this.Property(t => t.DayTimeTelephone).HasColumnName("DayTimeTelephone");
            this.Property(t => t.HomeTelephone).HasColumnName("HomeTelephone");
            this.Property(t => t.MobileTelephone).HasColumnName("MobileTelephone");
            this.Property(t => t.AltTelephone).HasColumnName("AltTelephone");
            this.Property(t => t.CompanyTelephone).HasColumnName("CompanyTelephone");
            this.Property(t => t.Address1).HasColumnName("Address1");
            this.Property(t => t.Address2).HasColumnName("Address2");
            this.Property(t => t.Town).HasColumnName("Town");
            this.Property(t => t.Postcode).HasColumnName("Postcode");
            this.Property(t => t.County).HasColumnName("County");
            this.Property(t => t.Country).HasColumnName("Country");
            this.Property(t => t.Test).HasColumnName("Test");
            this.Property(t => t.WhoUpdated).HasColumnName("WhoUpdated");
            this.Property(t => t.CreationDate).HasColumnName("CreationDate");
            this.Property(t => t.PreferredCurrency).HasColumnName("PreferredCurrency");
            this.Property(t => t.PreferredCurrencySymbol).HasColumnName("PreferredCurrencySymbol");
        }
    }
}
