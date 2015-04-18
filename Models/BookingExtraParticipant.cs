using System;
using System.Collections.Generic;

namespace BootstrapVillas.Models
{
    public partial class BookingExtraParticipant
    {
        public long BookingExtraParticipantID { get; set; }
        public long BookingExtraSelectionID { get; set; }
        public string BookingExtraParticipantTitle { get; set; }
        public string BookingExtraParticipantFirstName { get; set; }
        public string BookingExtraParticipantMiddleName { get; set; }
        public string BookingExtraParticipantLastName { get; set; }
        public Nullable<System.DateTime> BookingExtraParticipantDOB { get; set; }
        public Nullable<bool> BookingExtraParticipantChild { get; set; }
        public Nullable<bool> BookingExtraParticipantInfant { get; set; }
        public Nullable<System.DateTime> BookingExtraParticipantWhenCreated { get; set; }
        public string BookingExtraParticipantDriversLicenceNo { get; set; }
        public string BookingExtraParticipantDriversPassportNo { get; set; }
        public Nullable<int> BookingExtraParticipantAge { get; set; }
        public virtual BookingExtraSelection BookingExtraSelection { get; set; }
    }
}
