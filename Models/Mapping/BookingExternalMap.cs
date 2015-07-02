using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BootstrapVillas.Models.Mapping
{
    public class BookingExternalMap : EntityTypeConfiguration<BookingExternal>
    {
        public BookingExternalMap()
        {
            // Primary Key
            this.HasKey(t => t.BookingExternalID);

            // Properties
            // Table & Column Mappings
            this.ToTable("BookingExternal");
            this.Property(t => t.BookingExternalID).HasColumnName("BookingExternalID");
            this.Property(t => t.StartDate).HasColumnName("StartDate");
            this.Property(t => t.EndDate).HasColumnName("EndDate");
            this.Property(t => t.PropertyID).HasColumnName("PropertyID");
            this.Property(t => t.Notes).HasColumnName("Notes");

            // Relationships
          

          

        }
    }
}
