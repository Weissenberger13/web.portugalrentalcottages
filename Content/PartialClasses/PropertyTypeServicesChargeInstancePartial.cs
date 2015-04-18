using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using BootstrapVillas.Models;


namespace BootstrapVillas.Models
{
    public partial class PropertyTypeServicesChargeInstance
    {
        public static List<PropertyTypeServicesChargeInstance> GetPropertyTypeServicesChargeInstances(Property prop)
        {
            using (var _db = new PortugalVillasContext())
            {

                var thing =
                    _db.PropertyTypeServicesChargeInstances.Where(x => x.PropertyTypeID == prop.PropertyTypeID).ToList();

                return thing;

            }

        }


        public static IEnumerable<PropertyTypeServicesChargeInstance> GetExtraTypeServicesChargeInstances()
        {
            using (var _db = new PortugalVillasContext())
            {

                var extras = _db.PropertyTypeServices.Where(x => x.Property1OrExtraType2 == 2).ToList();
                List<PropertyTypeServicesChargeInstance> instances = _db.PropertyTypeServicesChargeInstances.ToList();

                List<PropertyTypeServicesChargeInstance> instancesToReturn = new List<PropertyTypeServicesChargeInstance>();

                foreach (var propertyTypeService in extras)
                {

                    instancesToReturn.AddRange(instances.Where(x => x.PropertyTypeServicesID == propertyTypeService.PropertyTypeServicesID)
                        .ToList());
                }
                return instancesToReturn;

            }

        }

    }
}