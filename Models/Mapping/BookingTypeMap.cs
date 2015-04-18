using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BootstrapVillas.Models.Mapping
{
    public class BookingTypeMap : EntityTypeConfiguration<BookingType>
    {
        public BookingTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.BookingTypeID);

            // Properties
            // Table & Column Mappings
            this.ToTable("BookingType");
            this.Property(t => t.BookingTypeID).HasColumnName("BookingTypeID");
            this.Property(t => t.BookingType1).HasColumnName("BookingType");
            this.Property(t => t.BookingTypeDescription).HasColumnName("BookingTypeDescription");
        }
    }
}
