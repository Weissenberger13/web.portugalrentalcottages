using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BootstrapVillas.Models.Mapping
{
    public class CustomerLoginMap : EntityTypeConfiguration<CustomerLogin>
    {
        public CustomerLoginMap()
        {
            // Primary Key
            this.HasKey(t => t.CustomerLoginID);

            // Properties
            this.Property(t => t.LoginName)
                .HasMaxLength(300);

            this.Property(t => t.Password)
                .HasMaxLength(300);

            // Table & Column Mappings
            this.ToTable("CustomerLogin");
            this.Property(t => t.CustomerLoginID).HasColumnName("CustomerLoginID");
            this.Property(t => t.CustomerID).HasColumnName("CustomerID");
            this.Property(t => t.LoginName).HasColumnName("LoginName");
            this.Property(t => t.Password).HasColumnName("Password");
            this.Property(t => t.LastLogin).HasColumnName("LastLogin");
            this.Property(t => t.WhenCreated).HasColumnName("WhenCreated");

            // Relationships
            this.HasRequired(t => t.Customer)
                .WithMany(t => t.CustomerLogins)
                .HasForeignKey(d => d.CustomerID);

        }
    }
}
