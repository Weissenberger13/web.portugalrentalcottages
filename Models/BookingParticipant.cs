using System;
using System.Collections.Generic;

namespace BootstrapVillas.Models
{
    public partial class BookingParticipant
    {
        public long BookingParticipantID { get; set; }
        public long BookingID { get; set; }
        public string BookingParticipantTitle { get; set; }
        public string BookingParticipantFirstName { get; set; }
        public string BookingParticipantMiddleName { get; set; }
        public string BookingParticipantLastName { get; set; }
        public Nullable<DateTime> BookingParticipantDOB { get; set; }
        public Nullable<bool> BookingParticipantChild { get; set; }
        public Nullable<bool> BookingParticipantInfant { get; set; }
        public Nullable<DateTime> BookingParticipantWhenCreated { get; set; }
        public string BookingParticipantAge { get; set; }
        public Nullable<int> BookingParticipantNumber { get; set; }
        public virtual Booking Booking { get; set; }
    }
}
