using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BootstrapVillas.Models.Mapping
{
    public class PaymentTypeMap : EntityTypeConfiguration<PaymentType>
    {
        public PaymentTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.PaymentTypeID);

            // Properties
            // Table & Column Mappings
            this.ToTable("PaymentType");
            this.Property(t => t.PaymentTypeID).HasColumnName("PaymentTypeID");
        }
    }
}
