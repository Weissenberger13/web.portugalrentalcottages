using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BootstrapVillas.Models.Mapping
{
    public class CommentMap : EntityTypeConfiguration<Comment>
    {
        public CommentMap()
        {
            // Primary Key
            this.HasKey(t => t.CommentID);

            // Properties
            this.Property(t => t.Username)
                .IsRequired()
                .HasMaxLength(400);

            // Table & Column Mappings
            this.ToTable("Comment");
            this.Property(t => t.CommentID).HasColumnName("CommentID");
            this.Property(t => t.PropertyID).HasColumnName("PropertyID");
            this.Property(t => t.Username).HasColumnName("Username");
            this.Property(t => t.StarRating).HasColumnName("StarRating");
            this.Property(t => t.StartdateOfStay).HasColumnName("StartdateOfStay");
            this.Property(t => t.EnddateOfStay).HasColumnName("EnddateOfStay");
            this.Property(t => t.Approved).HasColumnName("Approved");
            this.Property(t => t.Text).HasColumnName("Text");
            this.Property(t => t.WhenCreated).HasColumnName("WhenCreated");

            // Relationships
            this.HasRequired(t => t.Property)
                .WithMany(t => t.Comments)
                .HasForeignKey(d => d.PropertyID);

        }
    }
}
