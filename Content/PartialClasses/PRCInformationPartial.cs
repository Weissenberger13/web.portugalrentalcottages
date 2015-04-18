using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BootstrapVillas.Models;

namespace BootstrapVillas.Models
{
    public partial class PRCInformation
    {
        public static PRCInformation GetPRCInformation()
        {
            using (var db = new PortugalVillasContext())
            {
                return db.PRCInformations.FirstOrDefault();
            }
            


        }


    }
}