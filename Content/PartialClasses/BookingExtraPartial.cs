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
    public partial class BookingExtra
    {


        public static BookingExtra GetBookingExtraByID(long? ID)
        {
            PortugalVillasContext _db = new PortugalVillasContext();

            BookingExtra bookingExtra = _db.BookingExtras
                .Where(x => x.BookingExtraID == ID)
                .First();


            return bookingExtra;
        }


        public static long GetBookingExtraIDByPRCReference(string prcRef)
        {
            PortugalVillasContext _db = new PortugalVillasContext();

            long theID = _db.BookingExtras
                .Where(x => x.LegacyReference == prcRef.Trim())
                .Select(x => x.BookingExtraID).First();


            return theID;
        }



        //get all the booking extra types
        public static List<BookingExtraType> GetBookingExtraTypes()
        {
            List<BookingExtraType> theList = new List<BookingExtraType>();
            PortugalVillasContext _db = new PortugalVillasContext();

            return theList = _db.BookingExtraTypes.ToList();
        }


        //get all the booking extras - return list
        public static List<BookingExtra> GetBookingExtras()
        {
            List<BookingExtra> theList = new List<BookingExtra>();
            PortugalVillasContext _db = new PortugalVillasContext();

            return theList = _db.BookingExtras
                .Where(x => x.TopLevelItem == true)
                .ToList();
        }



        //get all the booking extras of a specific extra type - passed a BookingExtraTypeID
        public static List<BookingExtra> GetBookingExtrasOfType(long? BookingExtraTypeID)
        {

            List<BookingExtra> theList = new List<BookingExtra>();
            PortugalVillasContext _db = new PortugalVillasContext();

            theList = _db.BookingExtras.Where(x => x.BookingExtraTypeID == BookingExtraTypeID)
                .Where(x => x.TopLevelItem == true)

                .ToList();

            return theList;

        }

        //no parameter, just get all BookingExtras
        public static List<BookingExtra> GetBookingExtrasOfType()
        {

            List<BookingExtra> theList = new List<BookingExtra>();
            PortugalVillasContext _db = new PortugalVillasContext();

            theList = _db.BookingExtras
                .Where(x => x.TopLevelItem == true).ToList();

            return theList;

        }



        //overload 1 - take type not long?
        public static List<BookingExtra> GetBookingExtrasOfType(BookingExtraType aBookingExtraType)
        {

            List<BookingExtra> theList = new List<BookingExtra>();
            PortugalVillasContext _db = new PortugalVillasContext();

            return theList = _db.BookingExtras.Where(x => x.BookingExtraTypeID == aBookingExtraType.BookingExtraTypeID)
                .Where(x => x.TopLevelItem == true).ToList();

        }



        //get all extra attributes for an extra ID

        public static List<BookingExtraAttribute> GetBookingExtraAttributesByBookingExtra(BookingExtra aBookingExtra)
        {
            List<BookingExtraAttribute> theListOfAttributes = new List<BookingExtraAttribute>();
            PortugalVillasContext _db = new PortugalVillasContext();

            return theListOfAttributes = _db.BookingExtraAttributes.Where(x => x.BookingExtraID == aBookingExtra.BookingExtraID)
              .ToList();

        }

        public static List<BookingExtraAttribute> GetBookingExtraAttributesByBookingExtraID(long? bookingExtraID)
        {
            List<BookingExtraAttribute> theListOfAttributes = new List<BookingExtraAttribute>();
            PortugalVillasContext _db = new PortugalVillasContext();

            return theListOfAttributes = _db.BookingExtraAttributes.Where(x => x.BookingExtraID == bookingExtraID).ToList();

        }


      //this retrieves all attribute for an extra
       
      public void GetAllBookingExtraAttributes()
        {       if (this.BookingExtraAttributes.Count == 0)
            {

                // List<BookingExtraAttribute> theListOfAttributes = new List<BookingExtraAttribute>();
                PortugalVillasContext _db = new PortugalVillasContext();


                //assign to this instance's collection
                this.BookingExtraAttributes =
                    _db.BookingExtraAttributes.Where(x => x.BookingExtraID == this.BookingExtraID).ToList();
            }
        }

      public void GetBookingExtraAttributesWithoutDescriptions()
      {
          if (this.BookingExtraAttributes.Count == 0)
          {

              // List<BookingExtraAttribute> theListOfAttributes = new List<BookingExtraAttribute>();
              PortugalVillasContext _db = new PortugalVillasContext();


              //assign to this instance's collection
              this.BookingExtraAttributes =
                  _db.BookingExtraAttributes
                  .Where(x => x.AttributeName != "Description")
                  .Where(x => x.BookingExtraID == this.BookingExtraID).ToList();
          }
      }

      public void GetBookingExtraAttributesDescriptionsOnly()
      {
          if (this.BookingExtraAttributes.Count == 0)
          {

              // List<BookingExtraAttribute> theListOfAttributes = new List<BookingExtraAttribute>();
              PortugalVillasContext _db = new PortugalVillasContext();


              //assign to this instance's collection
              this.BookingExtraAttributes =
                  _db.BookingExtraAttributes
                  .Where(x => x.AttributeName == "Description")
                  .Where(x => x.BookingExtraID == this.BookingExtraID).ToList();
          }
      }


     




    }
}