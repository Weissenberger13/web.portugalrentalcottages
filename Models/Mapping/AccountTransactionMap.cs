using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BootstrapVillas.Models.Mapping
{
    public class AccountTransactionMap : EntityTypeConfiguration<AccountTransaction>
    {
        public AccountTransactionMap()
        {
            // Primary Key
            this.HasKey(t => t.AccountTransactionID);

            // Properties
            this.Property(t => t.TransactionNote)
                .HasMaxLength(1500);

            this.Property(t => t.TransactionReference)
                .HasMaxLength(500);

            this.Property(t => t.WhoCreated)
                .HasMaxLength(500);

            // Table & Column Mappings
            this.ToTable("AccountTransaction");
            this.Property(t => t.AccountTransactionID).HasColumnName("AccountTransactionID");
            this.Property(t => t.BookingID).HasColumnName("BookingID");
            this.Property(t => t.BookingExtraSelectionID).HasColumnName("BookingExtraSelectionID");
            this.Property(t => t.TransactionAmount).HasColumnName("TransactionAmount");
            this.Property(t => t.TransactionNote).HasColumnName("TransactionNote");
            this.Property(t => t.Paid).HasColumnName("Paid");
            this.Property(t => t.AccountID).HasColumnName("AccountID");
            this.Property(t => t.TransactionReference).HasColumnName("TransactionReference");
            this.Property(t => t.TransactionDate).HasColumnName("TransactionDate");
            this.Property(t => t.WhoCreated).HasColumnName("WhoCreated");
            this.Property(t => t.WhenCreated).HasColumnName("WhenCreated");
            this.Property(t => t.Voided).HasColumnName("Voided");
            this.Property(t => t.VoidedDate).HasColumnName("VoidedDate");
            this.Property(t => t.WhoVoided).HasColumnName("WhoVoided");

            // Relationships
            this.HasOptional(t => t.Booking)
                .WithMany(t => t.AccountTransactions)
                .HasForeignKey(d => d.BookingID);
            this.HasOptional(t => t.BookingExtraSelection)
                .WithMany(t => t.AccountTransactions)
                .HasForeignKey(d => d.BookingExtraSelectionID);
            this.HasRequired(t => t.PropertyOwnerAccount)
                .WithMany(t => t.AccountTransactions)
                .HasForeignKey(d => d.AccountID);

        }
    }
}
