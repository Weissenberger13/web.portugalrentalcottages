using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BootstrapVillas.Models.Mapping
{
    public class DepositTypeMap : EntityTypeConfiguration<DepositType>
    {
        public DepositTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.DepositTypeID);

            // Properties
            this.Property(t => t.DespositTypeName)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("DepositType");
            this.Property(t => t.DepositTypeID).HasColumnName("DepositTypeID");
            this.Property(t => t.DespositTypeName).HasColumnName("DespositTypeName");
        }
    }
}
