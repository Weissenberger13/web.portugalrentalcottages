using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BootstrapVillas.Models.ViewModels
{
    public class BookingExternalSyncRequest
    {

        public string SystemId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string PropertyReference { get; set; }

    }
}
