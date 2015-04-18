using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BootstrapVillas.Models.Mapping
{
    public class DepositMap : EntityTypeConfiguration<Deposit>
    {
        public DepositMap()
        {
            // Primary Key
            this.HasKey(t => t.DepositID);

            // Properties
            this.Property(t => t.Currency)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.CurrencySymbol)
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("Deposit");
            this.Property(t => t.DepositID).HasColumnName("DepositID");
            this.Property(t => t.BookingID).HasColumnName("BookingID");
            this.Property(t => t.BookingExtraID).HasColumnName("BookingExtraID");
            this.Property(t => t.CustomerID).HasColumnName("CustomerID");
            this.Property(t => t.DepositAmount).HasColumnName("DepositAmount");
            this.Property(t => t.DepositTypeID).HasColumnName("DepositTypeID");
            this.Property(t => t.DepositPaid).HasColumnName("DepositPaid");
            this.Property(t => t.DateDepositPaid).HasColumnName("DateDepositPaid");
            this.Property(t => t.Currency).HasColumnName("Currency");
            this.Property(t => t.CurrencySymbol).HasColumnName("CurrencySymbol");
            this.Property(t => t.DateDepositDue).HasColumnName("DateDepositDue");
        }
    }
}
