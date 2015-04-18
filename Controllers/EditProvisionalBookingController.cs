using System;
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
using BootstrapVillas.Data.QueriesSQL;
using BootstrapVillas.Data.SQLCommands;
using BootstrapVillas.Data.DataSets;
using BootstrapVillas;
using System.IO;

namespace BootstrapVillas.Controllers
{
    public class EditProvisionalBookingController : Controller
    {

        PortugalVillasContext db = new PortugalVillasContext();


        //Methods to pull from the session and test for existence


        //Methods to control the event chain (depending on what's in the Session / i.e. what they've picked
        
        //Method to get existing data based on email address e.g. pull the customer's bank details

        
        //PROPERTY EDIT FIELDS

        //EditPropertyBooking
        [HttpGet]
        public ActionResult EditPropertyBooking(string startDate, string endDate, string prcRef)
        {


            //return to the checkout page where we first performed the edit
            return View();
        }




        //BOOKING EXTRA SELECTION EDIT FIELDS

        [HttpGet]
        public ActionResult EditBookingExtraSelection(string startDate,  string endDate, string prcRef)
        {

            List<BookingExtraSelection> BookingExtraSelections = new List<BookingExtraSelection>(); //where type = extras
            BookingExtraSelections = (List<BookingExtraSelection>)Session["Cart_ExtraBookings"];

            //we will pass over a booking extra selection that has not been assigned it's property DB values             
            BookingExtraSelection theBookingToEdit = BookingExtraSelections.Where(x => x.ExtraRentalDate == Convert.ToDateTime(startDate))
                                        .Where(y => y.ExtraReturnDate == Convert.ToDateTime(endDate))
                                        .Where(z => z.BookingExtraPRCReference == prcRef)
                                        .FirstOrDefault();
                                        


            //pass the booking to the Edit page
            return View(theBookingToEdit);
        }


     
 

    }
}
