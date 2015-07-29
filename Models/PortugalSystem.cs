using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BootstrapVillas.Models.ViewModels;

namespace BootstrapVillas.Models
{
    public class PortugalSystem
    {
        [Key]
        public int PortugalSystemId { get; set; }
        public string name { get; set; }
        public string URL { get; set; }
      


        public ICollection<BookingExternal> BookingExternals { get; set; }
    }
}
