using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BootstrapVillas.Models;
using System.Data.Entity;

namespace BootstrapVillas.Models
{

    public partial class PropertyVacationType
    {

        /////////////////////////////
        //// STATIC METHODS
        /////////////////////////////

        public static PropertyVacationType GetVacationTypeByProperty(Property aProperty)
        {
            PortugalVillasContext _db = new PortugalVillasContext();

            PropertyVacationType aPropertyVacationType =
                _db.PropertyVacationTypes
                .Where(x => x.PropertyVacationTypeID == aProperty.PropertyVacationTypeID)
                .First();

            return aPropertyVacationType;
        }


        //return linklist of types
        public static LinkedList<PropertyVacationType> GetPropertyVacationTypes()
        {
            PortugalVillasContext _db = new PortugalVillasContext();
            LinkedList<PropertyVacationType> vacationTypeList = new LinkedList<PropertyVacationType>();

            var vType = (from vacType in _db.PropertyVacationTypes
                         orderby vacType.PropertyVacationTypeID descending
                         select vacType);


            foreach (var propertyVacationType in vType)
            {
                //add the latest key to the earliest element so every time you add a new one, it becomes the earliest, 
                //ending up with the first being the first
                vacationTypeList.AddFirst(propertyVacationType);

            }

            return vacationTypeList;
        }


    }
}