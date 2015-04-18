using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BootstrapVillas.Models.Mapping
{
    public class BookingExtraAttributeMap : EntityTypeConfiguration<BookingExtraAttribute>
    {
        public BookingExtraAttributeMap()
        {
            // Primary Key
            this.HasKey(t => t.BookingExtraAttributeID);

            // Properties
            this.Property(t => t.AttributeName)
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("BookingExtraAttribute");
            this.Property(t => t.BookingExtraAttributeID).HasColumnName("BookingExtraAttributeID");
            this.Property(t => t.BookingExtraID).HasColumnName("BookingExtraID");
            this.Property(t => t.AttributeName).HasColumnName("AttributeName");
            this.Property(t => t.AttributeContent).HasColumnName("AttributeContent");
            this.Property(t => t.AttributeOrder).HasColumnName("AttributeOrder");

            // Relationships
            this.HasOptional(t => t.BookingExtra)
                .WithMany(t => t.BookingExtraAttributes)
                .HasForeignKey(d => d.BookingExtraID);

        }
    }
}
