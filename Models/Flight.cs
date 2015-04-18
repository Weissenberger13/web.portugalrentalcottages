using System;
using System.Collections.Generic;

namespace BootstrapVillas.Models
{
    public partial class Flight
    {
        public long FlightID { get; set; }
        public string FlightCode { get; set; }
        public string DepartingAirport { get; set; }
        public string DestinationAiport { get; set; }
        public string DepartingCountry { get; set; }
        public string DestinationCountry { get; set; }
    }
}
