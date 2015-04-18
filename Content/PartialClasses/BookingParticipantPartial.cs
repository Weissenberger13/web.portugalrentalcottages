using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BootstrapVillas.Models
{
    public partial class BookingParticipant
    {
        [NotMapped]
        public int StepNo { get; set; }
    }
}