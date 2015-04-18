using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BootstrapVillas.Models.Mapping
{
    public class FlightBookingMap : EntityTypeConfiguration<FlightBooking>
    {
        public FlightBookingMap()
        {
            // Primary Key
            this.HasKey(t => t.FlightBookingID);

            // Properties
            // Table & Column Mappings
            this.ToTable("FlightBooking");
            this.Property(t => t.FlightBookingID).HasColumnName("FlightBookingID");
            this.Property(t => t.FlightID).HasColumnName("FlightID");
            this.Property(t => t.BookingID).HasColumnName("BookingID");
        }
    }
}
