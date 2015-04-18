using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BootstrapVillas.Content.Interfaces
{
    public interface IContactable
    {
         string ContactFirstName { get; set; }
         string ContactLastName { get; set; }
         string ContactEmailAddress { get; set; }
    }
}
