using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BootstrapVillas.Models.Mapping
{
    public class BookingDocumentMap : EntityTypeConfiguration<BookingDocument>
    {
        public BookingDocumentMap()
        {
            // Primary Key
            this.HasKey(t => t.BookingDocumentID);

            // Properties
            // Table & Column Mappings
            this.ToTable("BookingDocument");
            this.Property(t => t.BookingDocumentID).HasColumnName("BookingDocumentID");
            this.Property(t => t.WhenCreated).HasColumnName("WhenCreated");
            this.Property(t => t.EmailTo).HasColumnName("EmailTo");
            this.Property(t => t.EmailCC).HasColumnName("EmailCC");
            this.Property(t => t.BookingID).HasColumnName("BookingID");
            this.Property(t => t.WhoUpdated).HasColumnName("WhoUpdated");
        }
    }
}
