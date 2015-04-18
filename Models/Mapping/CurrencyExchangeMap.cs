using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BootstrapVillas.Models.Mapping
{
    public class CurrencyExchangeMap : EntityTypeConfiguration<CurrencyExchange>
    {
        public CurrencyExchangeMap()
        {
            // Primary Key
            this.HasKey(t => t.CurrencyExchangeID);

            // Properties
            this.Property(t => t.CurrencyExchangeSymbol)
                .HasMaxLength(5);

            // Table & Column Mappings
            this.ToTable("CurrencyExchanges");
            this.Property(t => t.CurrencyExchangeID).HasColumnName("CurrencyExchangeID");
            this.Property(t => t.CurrencyExchangeName).HasColumnName("CurrencyExchangeName");
            this.Property(t => t.CurrencyExchangeRate).HasColumnName("CurrencyExchangeRate");
            this.Property(t => t.LastUpdated).HasColumnName("LastUpdated");
            this.Property(t => t.CurrencyExchangeProperName).HasColumnName("CurrencyExchangeProperName");
            this.Property(t => t.CurrencyExchangeSymbol).HasColumnName("CurrencyExchangeSymbol");
        }
    }
}
