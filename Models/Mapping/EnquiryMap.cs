using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BootstrapVillas.Models.Mapping
{
    public class EnquiryMap : EntityTypeConfiguration<Enquiry>
    {
        public EnquiryMap()
        {
            // Primary Key
            this.HasKey(t => t.EnquiryID);

            // Properties
            this.Property(t => t.EnquiryEmailAddress)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.EnquiryHomeTelephone)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.EnquiryMobileTelephone)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.EnquiryNumberOfDays)
                .IsFixedLength()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("Enquiry");
            this.Property(t => t.BookingID).HasColumnName("BookingID");
            this.Property(t => t.EnquiryID).HasColumnName("EnquiryID");
            this.Property(t => t.EnquiryStartDate).HasColumnName("EnquiryStartDate");
            this.Property(t => t.EnquiryEndDate).HasColumnName("EnquiryEndDate");
            this.Property(t => t.EnquiryWhenCreated).HasColumnName("EnquiryWhenCreated");
            this.Property(t => t.EnquiryEmailAddress).HasColumnName("EnquiryEmailAddress");
            this.Property(t => t.EnquiryHomeTelephone).HasColumnName("EnquiryHomeTelephone");
            this.Property(t => t.EnquiryMobileTelephone).HasColumnName("EnquiryMobileTelephone");
            this.Property(t => t.EnquiryNumberOfDays).HasColumnName("EnquiryNumberOfDays");

            // Relationships
            this.HasRequired(t => t.Booking)
                .WithMany(t => t.Enquiries)
                .HasForeignKey(d => d.BookingID);

        }
    }
}
