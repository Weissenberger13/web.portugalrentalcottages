using System;
using System.Web.Routing;
using BootstrapVillas.Data.QueriesSQL;
using BootstrapVillas.Data.SQLCommands;
using BootstrapVillas.Data.DataSets;
using BootstrapVillas;
using BootstrapVillas.Controllers;
using BootstrapVillas.Models.ViewModels;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Web;
using System.Web.Mvc;
using BootstrapVillas.Models;
using System.Data.Entity;
using BootstrapVillas.Content.Classes;
using Aspose.Words;
using Aspose.Email;
using System.Data;
using System.Data.SqlClient;
using System.Data.Sql;
using BootstrapVillas.Data.DataConnections;
using BootstrapVillas.Data.DataAdapters;

namespace BootstrapVillas.Controllers
{
    public class PropertyViewController : Controller
    {
        //
        // GET: /PropertyView/
        PortugalVillasContext db = new PortugalVillasContext();

      

        /// <summary>
        /// This is used by the FullPropertyResult
        /// page - POST        /// </summary>
        /// <param name="theBooking"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddPropertyBookingToSession(FormCollection theBooking)
        {
            //takes a form - puts the dates and the property into a provisional booking object and adds this booking into the list of
            //bookings
            try
            {
                if(theBooking["bookingModalPropertyID"].ToString() != "0")
                {
                    var sessionProp = (Property)Session["theCurrentProperty"];
                    theBooking["bookingModalPropertyID"] = sessionProp.PropertyID.ToString();
                }


                Booking CartBooking = ConvertPropertyFormBookingToCartBooking(theBooking);
                //get the price
                
                Property prop = db.Properties.Find(CartBooking.PropertyID);
                CartBooking.Property = prop;
                

                try
                {
                  
                    //init currency converter


                    CartBooking.NumberOfNights = GeneralStaticHelperMethods.CalculateNoofNights(CartBooking.StartDate,
                        CartBooking.EndDate);
                    CartBooking.CalculateBookingPricingForAPropertyBooking(db);
                    CartBooking.CalculateExtrasPriceForAPropertyBooking(prop, CartBooking, db);

                    var currencyService = new CurrencyConverterController();
                    currencyService.InitialiseDefaultCurrencyForObject(CartBooking);

                }
                catch (Exception ex)
                {
                    /*Response.Redirect("http://" + Request.Url.Authority + "/Error/PropertyErrorSelection");*/
                    //return RedirectToAction("PropertyErrorSelection", "Error", new { propID = CartBooking.PropertyID });                    
                }                



                //1. Test if there are any bookings in the session currently.
                if (AreThereAnyBookingsInTheSession() > 0)
                {
                    //there are bookings in the viewbag, take the list, add another, put back in the bag
                    List<Booking> theSessionBookings = (List<Booking>)Session["Cart_PropertyBookings"];
                    theSessionBookings.Add(CartBooking);
                    Session["Cart_PropertyBookings"] = theSessionBookings;

                }
                else
                {
                    //there are no bookings in the viewbag - add a list of bookings with this CartBooking as the first booking
                    List<Booking> theSessionBookings = new List<Booking>();
                    theSessionBookings.Add(CartBooking);
                    Session["Cart_PropertyBookings"] = theSessionBookings;
                }

                string RootURL = Request.Url.Authority;
                RootURL += "/Home/BookCarRental";

                Response.Redirect("http://" + RootURL);

                return RedirectToAction("BookCarRental", "Home");

            }
            catch (Exception ex)
            {

                return RedirectToAction("SearchProperties", "Home");
                throw ex;


                ///production
                return RedirectToAction("SearchProperties", "Home");

            }
        }




        [HttpPost]
        public void AddExtraFormBookingToSession(BookingExtraSelection theExtraSelection)
        {
            if(theExtraSelection.BookingExtraID == 0)
            {
                //get the value from the session instead
                  if(Session["theCurrentExtraID"].ToString() != "")
                  {
                      theExtraSelection.BookingExtraID = (int)Session["theCurrentExtraID"];
                  }
                
            }


            //take the ExtraBooking and put it in the session
            try
            {

                //get the booking extra and extra type from the PRC Ref / Type
                /*var test = theExtraSelectionForm["ExtraRentalDate"].ToString();

                ///////////////////
                //// GENERAL ATTRIBUTES - COMMON
                ///////////////////

                //all

                theExtraSelection.ExtraRentalDate = DateTime.ParseExact(theExtraSelectionForm["ExtraRentalDate"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                theExtraSelection.ExtraReturnDate = DateTime.ParseExact(theExtraSelectionForm["ExtraReturnDate"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                
                theExtraSelection.NumberOfChildseats = Convert.ToInt16(theExtraSelectionForm["SpecialRequests"]);


                //cars
                theExtraSelection.SpecialRequests = theExtraSelectionForm["NumberOfChildseats"];
                

                //wine tourcs
                //theExtraSelection.

                //airport transfers
*/

                //tours


                //get any remaining fields
                BookingExtra theExtraType = db.BookingExtras.Find(theExtraSelection.BookingExtraID);
                theExtraSelection.BookingExtra = theExtraType;
                    //need to get PRCRef                             

                //calculate main and addition pricings and bes setup
                try
                {
                    theExtraSelection.BESPrice = BookingExtraSelection.GetBookingExtraPrice(theExtraSelection, db);
                

                    if (theExtraSelection.ExtraReturnDate != null)
                    {
                        theExtraSelection.NumberOfDays =
                            GeneralStaticHelperMethods.CalculateNoofDays(theExtraSelection.ExtraRentalDate,
                                theExtraSelection.ExtraReturnDate);
                    }
                    else
                    {
                        theExtraSelection.NumberOfDays = 1;
                    }

                    //this happens AFTER number of days has been calculated
                    theExtraSelection.BESExtraServicesPrice =
                        BookingExtraSelection.CalculateBookingExtraAdditionalCostsAndAssignToThisBooking(
                            theExtraSelection, db);    
                    
                
                }
                catch (Exception)
                {
                    
                    
                }

                //assign a guid for cart deletion
                theExtraSelection.BookingGuid = new Guid();
                theExtraSelection.BookingGuid = System.Guid.NewGuid();


                //1. Test if there are any bookings in the viewbag currently.
                if (AreThereAnyExtraBookingsInTheSession() > 0)
                {
                    //there are bookings in the viewbag, take the list, add another, put back in the bag
                    List<BookingExtraSelection> theSessionBookings = (List<BookingExtraSelection>)Session["Cart_ExtraBookings"];
                    theSessionBookings.Add(theExtraSelection);
                    Session["Cart_ExtraBookings"] = theSessionBookings;

                }
                else
                {
                    //there are no bookings in the viewbag - add a list of bookings with this CartBooking as the first booking
                    List<BookingExtraSelection> theSessionBookings = new List<BookingExtraSelection>();
                    theSessionBookings.Add(theExtraSelection);
                    Session["Cart_ExtraBookings"] = theSessionBookings;
                }



                string RootURL = Request.Url.Authority;
                RootURL += "/Home/BookCarRental";

                Response.Redirect("http://"+RootURL);



                /////////////
                //then put the extra selection into the ViewBag for later
                /////////////

            }
            catch (Exception ex)
            {
                throw ex;
            }




        }



        public void AddExtraFormBookingToSession()
        {

            string RootURL = Request.Url.Authority;
            RootURL += "/Home/BookCarRental";

            Response.Redirect("http://" + RootURL);

        }


        public Booking ConvertPropertyFormBookingToCartBooking(FormCollection theBooking)
        {
            try
            {
                Booking theCartBooking = new Booking();

                //change the form into a BookingObject
                if (theBooking["bookingModalStartDatePicker"] != "" && theBooking["bookingModalStartDatePicker"] != null)
                {
                    theCartBooking.StartDate = DateTime.ParseExact(theBooking["bookingModalStartDatePicker"], "dd-MM-yyyy", System.Globalization.CultureInfo.CreateSpecificCulture("en-GB"));
                }
                if (theBooking["bookingModalEndDatePicker"] != "" && theBooking["bookingModalEndDatePicker"] != null)
                {
                    theCartBooking.EndDate = DateTime.ParseExact(theBooking["bookingModalEndDatePicker"], "dd-MM-yyyy", System.Globalization.CultureInfo.CreateSpecificCulture("en-GB"));
                }
                if (Int32.Parse(theBooking["bookingModalPropertyID"]) != 0 && (theBooking["bookingModalPropertyID"] != null))
                {
                    theCartBooking.PropertyID = Int32.Parse(theBooking["bookingModalPropertyID"]);
                }
                if (theBooking["prcReference"] != "" && theBooking["prcReference"] != null)
                {
                    theCartBooking.BookingPRCReference = theBooking["prcReference"].ToString();
                }


                if (theBooking["Booking_Towels"] != "" && theBooking["Booking_Towels"] != null)
                {
                    theCartBooking.NoOfTowelsRequested = Convert.ToInt32(theBooking["Booking_Towels"].ToString());
                }
                if (theBooking["Booking_MidVacationCleaning"] != "" && theBooking["Booking_MidVacationCleaning"] != null)
                {
                    theCartBooking.MidVactionCleaning = Convert.ToInt32(theBooking["Booking_MidVacationCleaning"].ToString());
                }
                if (theBooking["Booking_SwimmingPoolHeating"] != "" && theBooking["Booking_SwimmingPoolHeating"] != null)
                {
                    theCartBooking.SwimmingPoolHeating= Convert.ToInt32(theBooking["Booking_SwimmingPoolHeating"].ToString());
                }
                if (theBooking["Booking_ExtraLinen"] != "" && theBooking["Booking_ExtraLinen"] != null)
                {
                    theCartBooking.ExtraLininSet = Convert.ToInt32(theBooking["Booking_ExtraLinen"].ToString());
                }
                if (theBooking["Booking_Heating"] != "" && theBooking["Booking_Heating"] != null)
                {
                    theCartBooking.HeatingNoNights = Convert.ToInt32(theBooking["Booking_Heating"].ToString());
                }

                theCartBooking.BookingGUID = Guid.NewGuid();
                

                //takes a cart booking object and converts to a 'proper' booking that can be stored in the Db.
                return theCartBooking;
            }
            catch (Exception ex)
            {
                throw ex;

            }

        }

        public Booking ConvertCartBookingToPersistenceBooking(Booking theCartBooking)
        {
            Booking thePersistenceBooking = new Booking();

            return thePersistenceBooking;

        }



        public int AreThereAnyBookingsInTheViewBag()
        {


            List<Booking> testForBookings = ViewBag.Cart_PropertyBookings;

            if (testForBookings == null)
                return 0;
            else return testForBookings.Count;


        }

        public int AreThereAnyBookingsInTheSession()
        {
            if (Session["Cart_PropertyBookings"] != null)
            {
                List<Booking> testForBookings = (List<Booking>)Session["Cart_PropertyBookings"];

                if (testForBookings == null)
                    return 0;
                else return testForBookings.Count;
            }
            
            return 0;

        }

        public int AreThereAnyExtraBookingsInTheSession()
        {

            if (Session["Cart_ExtraBookings"] != null)
            {
                List<BookingExtraSelection> testForBookings = (List<BookingExtraSelection>)Session["Cart_ExtraBookings"];

                if (testForBookings == null)
                    return 0;
                else return testForBookings.Count;
            }
            return 0;

        }


        public List<Booking> GetAllBookingsInSession()
        {
            List<Booking> theBookingsInSession = new List<Booking>();

            if (AreThereAnyBookingsInTheSession() > 0)

                theBookingsInSession = (List<Booking>)Session["Cart_PropertyBookings"];

            return theBookingsInSession;
        }


        public List<BookingExtraSelection> GetAllExtraBookingsInSession()
        {
            List<BookingExtraSelection> theExtraBookingsInSession = new List<BookingExtraSelection>();

            if (AreThereAnyExtraBookingsInTheSession() > 0)

                theExtraBookingsInSession = (List<BookingExtraSelection>)Session["Cart_ExtraBookings"];

            return theExtraBookingsInSession;

        }

        [HttpGet]
        public void DeleteItemFromSessionBookingCart(string StartDate, string EndDate, string PRCRef, Guid BookingGUID, string SessionVariableToDeleteFrom = "", string theListType = "")
        {
            try
            {

                //which session variable is it in

                if (SessionVariableToDeleteFrom == "Cart_PropertyBookings") //it's a property booking

                {
                    //pull session into list
                    List<Booking> theSessionList = (List<Booking>)Session["Cart_PropertyBookings"];

                    List<int> theListPlacesToDelete = new List<int>();
                    string format = "dd/MM/yyyy";

                    //iterate through and find item to remove
                    foreach (var item in theSessionList)
                    {
                        DateTime i1 = DateTime.Parse(item.StartDate.ToString());
                        string BookingDateToTestOn = i1.ToString("dd/MM/yyyy");

                        try
                        {
                            if (item.BookingGUID.Equals(BookingGUID))
                            {
                                theListPlacesToDelete.Add(theSessionList.IndexOf(item));
                                break;

                            }
                        }
                        catch (Exception)
                        {
                            if (BookingDateToTestOn == StartDate)
                            {
                                theListPlacesToDelete.Add(theSessionList.IndexOf(item));
                                break;
                            }
                        }
                    }
                    //now delete those entries


                    foreach (int placeToDelete in theListPlacesToDelete)
                    {

                        theSessionList.RemoveAt(placeToDelete);
                        //if there's two items for same prop with same start only delete the first
                        break;
                    }

                    //put cart back into session
                    Session["Cart_PropertyBookings"] = theSessionList;

                }

                else if (SessionVariableToDeleteFrom == "Cart_ExtraBookings")//it's an extra booking
                {
                    List<BookingExtraSelection> theSessionList = (List<BookingExtraSelection>)Session["Cart_ExtraBookings"];
                    List<int> theListPlacesToDelete = new List<int>();
                    foreach (var item in theSessionList)
                    {
                        if (item.BookingGuid.Equals(BookingGUID) )
                        {                            
                                theListPlacesToDelete.Add(theSessionList.IndexOf(item));
                        }
                        //it's the item we want
                    }


                    //now delete those entries
                    foreach (int placeToDelete in theListPlacesToDelete)
                    {

                        theSessionList.RemoveAt(placeToDelete);
                      
                    }
                    //put session back and return true
                    Session["Cart_ExtraBookings"] = theSessionList;


                }
            }
            catch(Exception ex)
            {
                
                throw;
            }//can't identify - throw an exception          

            //put session back and return true

       
           
            //go back to calling page
            Response.Redirect(Request.UrlReferrer.ToString());

        }

    }
}