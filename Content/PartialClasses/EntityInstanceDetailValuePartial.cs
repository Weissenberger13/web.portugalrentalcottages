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


namespace BootstrapVillas.Content.PartialClasses
{
    public class EntityInstanceDetailValuePartial
    {
        
       
        //inserts necessary rows into EntityInstanceDetailValue
        //depends on the takes the 
        public bool InsertEntityInstanceDetailValuePartial(long anEntityTypeID, FormCollection theFormCollectionToInsert)
        {
            //execute the method to pull back alll the fields of that type

            List<EntityTypeDetailField> theEntityTypeDetailFields  = GetAllEntityDetailFieldsOfEntityType(anEntityTypeID);



            return true;
        }








        //from The EntityTypeTable
        enum EntityTypes {  Property,
                            BookingExtraType,
                            BookingExtraTypeAirportTransfer,
                            BookingExtraTypeWineTastingTour,
                            BookingExtraTypeCarRental,
                            BookingExtraTypeSightseeingtour}

        //Original method - takes long, returns list
        protected List<EntityTypeDetailField> GetAllEntityDetailFieldsOfEntityType(long anEntityTypeID)
        {
            //test for Entity Type
            //add necessary fields to list
            //return lists
            PortugalVillasContext _db = new PortugalVillasContext();
       
            var theEntityTypeDetailFields = _db.EntityTypeDetailFields
                .Where(x => x.EntityTypeID == anEntityTypeID)
                .ToList();
                
            return theEntityTypeDetailFields;
        }


      //Overload 1 - takes EntityType
      protected List<long> GetAllEntityDetailFieldsOfEntityType(EntityType anEntityType)
        {
            //test for Entity Type
            //add necessary fields to list
            //return lists
            PortugalVillasContext _db = new PortugalVillasContext();

            var theEntityTypeDetailFields = _db.EntityTypeDetailFields
                .Where(x => x.EntityTypeID == anEntityType.EntityTypeID)
                .ToList(); 

            List<long> theDetailFieldIDsToReturn = new List<long>();
            return theDetailFieldIDsToReturn;
        }

    }
}