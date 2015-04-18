using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BootstrapVillas.Models;
using System.Data.Entity;

namespace BootstrapVillas.Models
{
    public partial class PropertyType
    {

        /////////////////////////////
        //// STATIC METHODS
        /////////////////////////////



        public static List<PropertyType> GetPropertyTypes()
        {
            PortugalVillasContext _db = new PortugalVillasContext();
            List<PropertyType> vacationTypeList;


            vacationTypeList = (from types in _db.PropertyTypes
                          select types).ToList();


            return vacationTypeList;
        }

        public static PropertyType GetPropertyTypeByProperty(Property aProperty)
        {
            PortugalVillasContext _db = new PortugalVillasContext();
            PropertyType theType
                = _db.PropertyTypes
                    .Where(x => x.PropertyTypeID == aProperty.PropertyTypeID)
                    .First();



            return theType;
        }

    }
}