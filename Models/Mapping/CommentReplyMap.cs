using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BootstrapVillas.Models.Mapping
{
    public class CommentReplyMap : EntityTypeConfiguration<CommentReply>
    {
        public CommentReplyMap()
        {
            // Primary Key
            this.HasKey(t => t.CommentReplyID);

            // Properties
            this.Property(t => t.Username)
                .HasMaxLength(400);

            // Table & Column Mappings
            this.ToTable("CommentReply");
            this.Property(t => t.CommentReplyID).HasColumnName("CommentReplyID");
            this.Property(t => t.CommentID).HasColumnName("CommentID");
            this.Property(t => t.Username).HasColumnName("Username");
            this.Property(t => t.Text).HasColumnName("Text");
            this.Property(t => t.Approved).HasColumnName("Approved");

            // Relationships
            this.HasRequired(t => t.Comment)
                .WithMany(t => t.CommentReplies)
                .HasForeignKey(d => d.CommentID);

        }
    }
}
