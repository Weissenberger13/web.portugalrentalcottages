using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BootstrapVillas.Models;
using System.Data.Entity;
using System.Linq.Dynamic;
using BootstrapVillas.Content.Classes;


namespace BootstrapVillas.Models
{
    public partial class PropertyRegion
    {

        public static long GetPropertyRegionIDByProperty(Property aProperty)
        {
            PortugalVillasContext _db = new PortugalVillasContext();

            IQueryable<long> regionID = from towns in _db.PropertyTowns
                                        where towns.PropertyTownID == aProperty.PropertyTownID
                                        select towns.PropertyRegionID;

            //    whereropertyID == aProperty.PropertyID)
            //   .Select(x => x.PropertyTown);


            return regionID.FirstOrDefault();
        }



        public static string GetPropertyRegionbyID(long propertyRegionID)
        {
            PortugalVillasContext _db = new PortugalVillasContext();

            if (propertyRegionID != 0)
            {

                string theRegionName = _db.PropertyRegions
                    .Find(propertyRegionID)
                    .RegionName;

                return theRegionName;
            }

            else return "No region found";

        }


        
        
        //gets town ID from property, gets regionID from townID
        public static PropertyRegion GetPropertyRegionByProperty(Property aProperty)
        {
             PortugalVillasContext _db = new PortugalVillasContext();

            var propertyTown = _db.PropertyTowns.Where(x => x.PropertyTownID == aProperty.PropertyTownID).Select(x => x).FirstOrDefault();

            var propertyRegion =
                _db.PropertyRegions.Where(x => x.PropertyRegionID == propertyTown.PropertyRegionID).FirstOrDefault();

            return propertyRegion;
        }
        
    }



}