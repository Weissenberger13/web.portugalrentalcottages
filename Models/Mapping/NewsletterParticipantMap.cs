using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BootstrapVillas.Models.Mapping
{
    public class NewsletterParticipantMap : EntityTypeConfiguration<NewsletterParticipant>
    {
        public NewsletterParticipantMap()
        {
            // Primary Key
            this.HasKey(t => t.NewsletterParticipantID);

            // Properties
            this.Property(t => t.NewsletterParticipantEmail)
                .IsRequired()
                .HasMaxLength(300);

            // Table & Column Mappings
            this.ToTable("NewsletterParticipant");
            this.Property(t => t.NewsletterParticipantID).HasColumnName("NewsletterParticipantID");
            this.Property(t => t.NewsletterParticipantEmail).HasColumnName("NewsletterParticipantEmail");
        }
    }
}
