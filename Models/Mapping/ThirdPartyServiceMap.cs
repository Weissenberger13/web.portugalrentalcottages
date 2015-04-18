using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BootstrapVillas.Models.Mapping
{
    public class ThirdPartyServiceMap : EntityTypeConfiguration<ThirdPartyService>
    {
        public ThirdPartyServiceMap()
        {
            // Primary Key
            this.HasKey(t => t.ThirdPartyServiceID);

            // Properties
            // Table & Column Mappings
            this.ToTable("ThirdPartyService");
            this.Property(t => t.ThirdPartyServiceID).HasColumnName("ThirdPartyServiceID");
            this.Property(t => t.ThirdPartyServiceName).HasColumnName("ThirdPartyServiceName");
        }
    }
}
