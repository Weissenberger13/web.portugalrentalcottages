using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Microsoft.Ajax.Utilities;

namespace BootstrapVillas.Models
{
    public partial class AirportDestination
    {


        public static List<AirportDestination> GetAllAirportDestinations()
        {
            {
                using (var portCon = new PortVillasContext())
                {
                    return portCon.AirportDestinations.ToList();
                }
                
            }
        }
    }


}