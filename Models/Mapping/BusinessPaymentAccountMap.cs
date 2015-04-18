using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BootstrapVillas.Models.Mapping
{
    public class BusinessPaymentAccountMap : EntityTypeConfiguration<BusinessPaymentAccount>
    {
        public BusinessPaymentAccountMap()
        {
            // Primary Key
            this.HasKey(t => t.BusinessPaymentAccountID);

            // Properties
            // Table & Column Mappings
            this.ToTable("BusinessPaymentAccount");
            this.Property(t => t.BusinessPaymentAccountID).HasColumnName("BusinessPaymentAccountID");
        }
    }
}
