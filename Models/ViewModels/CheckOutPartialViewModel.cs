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


namespace BootstrapVillas.Models.ViewModels
{
    #region checkoutPartialViewModels

    public static class checkoutPartialViewModels
    {
        //delete an object from the the session / shoppingcart
        //match on the startdate and the reference - if both match, delete that item
        public static HttpContext DeleteItemFromSession(string callingURL, string VariableToDeleteFrom, string StartDate, string ReferenceToDelete, object TypeToCastTo, HttpContext theHttpContextTheSessionIsIn)
        {
            //depending on the type, we know which list to delete from, then redirect to the calling url
            if (TypeToCastTo.GetType().FullName == "") //it's a property booking
            {
                List<Booking> thePropertyBookings = (List<Booking>)theHttpContextTheSessionIsIn.Session["Cart_PropertyBookings"];
                foreach (var booking in thePropertyBookings)
                {
                    
                    if (booking.StartDate.Equals(StartDate) && booking.BookingPRCReference.Equals(ReferenceToDelete))
                    {
                        thePropertyBookings.Remove(booking);
                    }

                }

            }
            if (TypeToCastTo.GetType().FullName == "") //it's an extra booking 
            { }


            //do I need to return the sesssion or not? PRobs not
            return theHttpContextTheSessionIsIn;
        }

    }


 
    /////////////////////////////////////////////////
    //these are all used by the checkoutPartialView
    /// <summary>
    /// /////////////////////////////////////////////
    /// </summary>
    public class Property_BookingViewModel
    {
        PRCImageCollection thePropertyBookingImages { get; set; }
        public string PRCRef { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int positionInList { get; set; } //To remove from the session/viewbag 
        public decimal price { get; set; }

        //the Checkout view model for any property
        public Property_BookingViewModel(HttpServerUtilityBase Server, string PRCRef)
        {
            
            //needs a propertyID 
            thePropertyBookingImages = new PRCImageCollection(Server, PRCRef);
        }

        public Property_BookingViewModel() {
        
        }


    }

    public class BookingExtra_BookingExtraSelectionCompositeObject
    {
        string BookingExtraSelectionName;
        DateTime theBookingSelectionStartDate;
        DateTime theBookingSelectionEndDate;
        int positionInList = 0;//To remove from the session/viewbag 

        public BookingExtra_BookingExtraSelectionCompositeObject()
        {

        }

    }

    //extend the view models based on the views and what they have

    


   


     

       #endregion

}