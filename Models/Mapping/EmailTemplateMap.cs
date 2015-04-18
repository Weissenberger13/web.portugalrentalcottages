using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BootstrapVillas.Models.Mapping
{
    public class EmailTemplateMap : EntityTypeConfiguration<EmailTemplate>
    {
        public EmailTemplateMap()
        {
            // Primary Key
            this.HasKey(t => t.EmailTemplateId);

            // Properties
            // Table & Column Mappings
            this.ToTable("EmailTemplates");
            this.Property(t => t.EmailTemplateId).HasColumnName("EmailTemplateId");
            this.Property(t => t.EmailTemplateName).HasColumnName("EmailTemplateName");
            this.Property(t => t.EmailTemplateBodyHTML).HasColumnName("EmailTemplateBodyHTML");
            this.Property(t => t.EmailSubject).HasColumnName("EmailSubject");
        }
    }
}
