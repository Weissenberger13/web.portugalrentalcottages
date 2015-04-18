using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Web;
using System.Web.Mvc;
using BootstrapVillas.Models;
using System.Data.Entity;
using BootstrapVillas.Content.Classes;

namespace BootstrapVillas.Models

{
    public partial class PropertyEntity
    {
        //count entities so we can fill the adult/child/whatever we need count

        public static int CountEntitiesForProperty(Property aProperty, PropertyEntityType aType)
        {
            PortugalVillasContext _db = new PortugalVillasContext();

            int count = 0;
                
            count =  _db.PropertyEntities
                        .Where(x => x.PropertyEntityTypeID == aType.PropertyEntityTypeID)
                        .Where(x => x.PropertyID == aProperty.PropertyID)
                        .Select(x => x.PropertyEntityID).Count();

            return count;
        }


        //overload 1 
        public static int CountEntitiesForProperty(Property aProperty, long? aPropertyEntityID)
        {
            PortugalVillasContext _db = new PortugalVillasContext();

            int count = 0;

            count = _db.PropertyEntities
                        .Where(x => x.PropertyEntityTypeID == aPropertyEntityID) 
                        .Where(x => x.PropertyID == aProperty.PropertyID)
                        .Select(x => x.PropertyEntityID).Count();

            return count;
        }



    }
}