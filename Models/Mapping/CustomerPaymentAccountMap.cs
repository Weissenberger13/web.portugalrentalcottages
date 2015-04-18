using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BootstrapVillas.Models.Mapping
{
    public class CustomerPaymentAccountMap : EntityTypeConfiguration<CustomerPaymentAccount>
    {
        public CustomerPaymentAccountMap()
        {
            // Primary Key
            this.HasKey(t => t.CustomerPaymentAccountID);

            // Properties
            // Table & Column Mappings
            this.ToTable("CustomerPaymentAccount");
            this.Property(t => t.CustomerPaymentAccountID).HasColumnName("CustomerPaymentAccountID");
        }
    }
}
