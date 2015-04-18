using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BootstrapVillas.Models.Mapping
{
    public class PropertyMap : EntityTypeConfiguration<Property>
    {
        public PropertyMap()
        {
            // Primary Key
            this.HasKey(t => t.PropertyID);

            // Properties
            this.Property(t => t.LegacyReference)
                .HasMaxLength(50);

            this.Property(t => t.HomeawayReference)
                .HasMaxLength(50);

            this.Property(t => t.PropertyName)
                .HasMaxLength(50);

            this.Property(t => t.ChangeOverDay)
                .HasMaxLength(200);

            this.Property(t => t.Pets)
                .HasMaxLength(200);

            this.Property(t => t.Parking)
                .HasMaxLength(200);

            this.Property(t => t.DisabledAccess)
                .HasMaxLength(200);

            this.Property(t => t.Smoking)
                .HasMaxLength(200);

            this.Property(t => t.SwimmingPool)
                .HasMaxLength(200);

            this.Property(t => t.SwimmingPoolType)
                .HasMaxLength(200);

            this.Property(t => t.HeatedPool)
                .HasMaxLength(200);

            this.Property(t => t.Heating)
                .HasMaxLength(200);

            this.Property(t => t.AirConditioning)
                .HasMaxLength(200);

            this.Property(t => t.InternetAccess)
                .HasMaxLength(200);

            this.Property(t => t.HiFi)
                .HasMaxLength(200);

            this.Property(t => t.TV)
                .HasMaxLength(200);

            this.Property(t => t.HouseName)
                .HasMaxLength(50);

            this.Property(t => t.HouseNumber)
                .HasMaxLength(50);

            this.Property(t => t.AddressLine1)
                .HasMaxLength(200);

            this.Property(t => t.AddressLine2)
                .HasMaxLength(200);

            this.Property(t => t.Town)
                .HasMaxLength(200);

            this.Property(t => t.Postcode)
                .HasMaxLength(60);

            this.Property(t => t.County)
                .HasMaxLength(200);

            this.Property(t => t.Country)
                .HasMaxLength(200);

            this.Property(t => t.PropertyTelephone)
                .HasMaxLength(50);

            this.Property(t => t.WhenUpdated)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            this.Property(t => t.Alarm)
                .HasMaxLength(200);

            this.Property(t => t.BBQ)
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("Property");
            this.Property(t => t.PropertyID).HasColumnName("PropertyID");
            this.Property(t => t.LegacyReference).HasColumnName("LegacyReference");
            this.Property(t => t.PropertyOwnerID).HasColumnName("PropertyOwnerID");
            this.Property(t => t.PropertyOwnerRepresentativeID).HasColumnName("PropertyOwnerRepresentativeID");
            this.Property(t => t.PropertyTypeID).HasColumnName("PropertyTypeID");
            this.Property(t => t.PropertyTownID).HasColumnName("PropertyTownID");
            this.Property(t => t.PropertyVacationTypeID).HasColumnName("PropertyVacationTypeID");
            this.Property(t => t.HomeawayReference).HasColumnName("HomeawayReference");
            this.Property(t => t.PropertyName).HasColumnName("PropertyName");
            this.Property(t => t.FullDescription).HasColumnName("FullDescription");
            this.Property(t => t.ChangeOverDay).HasColumnName("ChangeOverDay");
            this.Property(t => t.ChangeoverNotes).HasColumnName("ChangeoverNotes");
            this.Property(t => t.Pets).HasColumnName("Pets");
            this.Property(t => t.Parking).HasColumnName("Parking");
            this.Property(t => t.DisabledAccess).HasColumnName("DisabledAccess");
            this.Property(t => t.Smoking).HasColumnName("Smoking");
            this.Property(t => t.SwimmingPool).HasColumnName("SwimmingPool");
            this.Property(t => t.SwimmingPoolType).HasColumnName("SwimmingPoolType");
            this.Property(t => t.HeatedPool).HasColumnName("HeatedPool");
            this.Property(t => t.Heating).HasColumnName("Heating");
            this.Property(t => t.AirConditioning).HasColumnName("AirConditioning");
            this.Property(t => t.InternetAccess).HasColumnName("InternetAccess");
            this.Property(t => t.HiFi).HasColumnName("HiFi");
            this.Property(t => t.TV).HasColumnName("TV");
            this.Property(t => t.Bedrooms).HasColumnName("Bedrooms");
            this.Property(t => t.Bathrooms).HasColumnName("Bathrooms");
            this.Property(t => t.MaxGuests).HasColumnName("MaxGuests");
            this.Property(t => t.SofaBeds).HasColumnName("SofaBeds");
            this.Property(t => t.ExtraBeds).HasColumnName("ExtraBeds");
            this.Property(t => t.Cots).HasColumnName("Cots");
            this.Property(t => t.HighChair).HasColumnName("HighChair");
            this.Property(t => t.Other).HasColumnName("Other");
            this.Property(t => t.MapURL).HasColumnName("MapURL");
            this.Property(t => t.GPSCoordinatesLongitude).HasColumnName("GPSCoordinatesLongitude");
            this.Property(t => t.GPSCoordinatesLatitude).HasColumnName("GPSCoordinatesLatitude");
            this.Property(t => t.LegacySynopsis1).HasColumnName("LegacySynopsis1");
            this.Property(t => t.LegacySynopsis2).HasColumnName("LegacySynopsis2");
            this.Property(t => t.LegacySynopsis3).HasColumnName("LegacySynopsis3");
            this.Property(t => t.PriceSynopsis).HasColumnName("PriceSynopsis");
            this.Property(t => t.Exclusive).HasColumnName("Exclusive");
            this.Property(t => t.FullAddress).HasColumnName("FullAddress");
            this.Property(t => t.HouseName).HasColumnName("HouseName");
            this.Property(t => t.HouseNumber).HasColumnName("HouseNumber");
            this.Property(t => t.AddressLine1).HasColumnName("AddressLine1");
            this.Property(t => t.AddressLine2).HasColumnName("AddressLine2");
            this.Property(t => t.Town).HasColumnName("Town");
            this.Property(t => t.Postcode).HasColumnName("Postcode");
            this.Property(t => t.County).HasColumnName("County");
            this.Property(t => t.Country).HasColumnName("Country");
            this.Property(t => t.PropertyTelephone).HasColumnName("PropertyTelephone");
            this.Property(t => t.WhenCreated).HasColumnName("WhenCreated");
            this.Property(t => t.WhenUpdated).HasColumnName("WhenUpdated");
            this.Property(t => t.Active).HasColumnName("Active");
            this.Property(t => t.Alarm).HasColumnName("Alarm");
            this.Property(t => t.BBQ).HasColumnName("BBQ");
            this.Property(t => t.DirectionsFromAirport).HasColumnName("DirectionsFromAirport");
            this.Property(t => t.WhoUpdated).HasColumnName("WhoUpdated");
            this.Property(t => t.IsFeatured).HasColumnName("IsFeatured");
            this.Property(t => t.IsSale).HasColumnName("IsSale");
            this.Property(t => t.Deposit_GBP_).HasColumnName("Deposit(GBP)");
            this.Property(t => t.BreakageDeposit_GBP_).HasColumnName("BreakageDeposit(GBP)");

            // Relationships
            this.HasOptional(t => t.PropertyOwnerRepresentative)
                .WithMany(t => t.Properties)
                .HasForeignKey(d => d.PropertyOwnerRepresentativeID);
            this.HasOptional(t => t.PropertyOwner)
                .WithMany(t => t.Properties)
                .HasForeignKey(d => d.PropertyOwnerID);
            this.HasOptional(t => t.PropertyTown)
                .WithMany(t => t.Properties)
                .HasForeignKey(d => d.PropertyTownID);
            this.HasOptional(t => t.PropertyType)
                .WithMany(t => t.Properties)
                .HasForeignKey(d => d.PropertyTypeID);
            this.HasOptional(t => t.PropertyVacationType)
                .WithMany(t => t.Properties)
                .HasForeignKey(d => d.PropertyVacationTypeID);

        }
    }
}
