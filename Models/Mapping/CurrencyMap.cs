using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BootstrapVillas.Models.Mapping
{
    public class CurrencyMap : EntityTypeConfiguration<Currency>
    {
        public CurrencyMap()
        {
            // Primary Key
            this.HasKey(t => t.CurrencyID);

            // Properties
            this.Property(t => t.CurrencyCode)
                .HasMaxLength(10);

            this.Property(t => t.CurrencySymbol)
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("Currency");
            this.Property(t => t.CurrencyID).HasColumnName("CurrencyID");
            this.Property(t => t.CurrencyCode).HasColumnName("CurrencyCode");
            this.Property(t => t.CurrencySymbol).HasColumnName("CurrencySymbol");
            this.Property(t => t.CurrencyInflationFromDBPriceEquation).HasColumnName("CurrencyInflationFromDBPriceEquation");
        }
    }
}
