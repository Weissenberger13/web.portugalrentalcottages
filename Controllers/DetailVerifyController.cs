using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Text;
using System.Net;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;
using System.Media;
using System.Web.Mvc;
using System.Web.UI;
using Aspose.Email.Exchange.Schema;
using Aspose.Words;
using BootstrapVillas.Content.Classes;
using BootstrapVillas.Content.Classes.EventCommands;
using BootstrapVillas.Content.Classes.ProcessEvents;
using BootstrapVillas.Content.Classes.ProcessEvents.Services;
using BootstrapVillas.Content.Interfaces;
using BootstrapVillas.Interfaces;
using BootstrapVillas.Models;
using BootstrapVillas.Content.EmailTemplates;
using BootstrapVillas.Models.Events;
using BootstrapVillas.Models.ViewModels;
using DotNetOpenAuth.Messaging;
using Microsoft.Ajax.Utilities;
using Document = BootstrapVillas.Models.Document;
using System.Data.Objects;
using Aspose.Email.Mail;

namespace BootstrapVillas.Controllers
{
    public class DetailVerifyController : Controller
    {
        //
        // GET: /DetailVerify/
        private PortugalVillasContext db = new PortugalVillasContext();

        public ActionResult GetBookingDetails()
        {
            Customer customer = new Customer();

            var testCus = Session["prc_customer"];

            if (testCus != null)
            {
                customer = (Customer)Session["prc_customer"];
            }

            CustomerBankDetail customerBankDetail = new CustomerBankDetail();
            var testBank = Session["prc_customerBankingDetail"];
            if (testBank != null)
            {
                customerBankDetail = (CustomerBankDetail)Session["prc_customerBankingDetail"];
            }


            var model = new FinalBookingDetailViewModel();
            model.Customer = customer;
            model.CustomerBankDetail = customerBankDetail;


            //test if any booking in the cart, if so pass them into the model
            List<Booking> bookings = new List<Booking>();


            if (Session["Cart_PropertyBookings"] != null)
            {
                bookings = (List<Booking>)Session["Cart_PropertyBookings"];
                model.Bookings = bookings;
                foreach (var booking in bookings)
                {
                    booking.Property = db.Properties.Find(booking.PropertyID);
                }
            }


            List<BookingExtraSelection> bes = new List<BookingExtraSelection>();

            if (Session["Cart_ExtraBookings"] != null)
            {
                bes = (List<BookingExtraSelection>)Session["Cart_ExtraBookings"];
                model.BookingExtraSelections = bes;
                foreach (var b in bes)
                {
                    b.BookingExtra = db.BookingExtras.Find(b.BookingExtraID);
                }
            }




            //test if any bes in the cart, if so pass them into the model

            List<AirportDestination>
                apd = AirportDestination.GetAllAirportDestinations().ToList();
            List<SelectListItem>
                airportDestinations = new List<SelectListItem>
                    ();

            /*   
               foreach (var airportDestination in apd)
               {
                   airportDestinations.Add(new SelectListItem { Text = @airportDestination.AirportPickupLocationName, Value = @airportDestination.AirportPickupLocationID.ToString() })
                       ;
               }*/


            airportDestinations.Add(new SelectListItem { Selected = true, Value = "1", Text="BoggleWoggle"});

            ViewBag.AirportDest = airportDestinations;
            ViewBag.Keywords = "cheap all inclusive holidays to portugal,holiday villa portugal,holiday apartments in portugal,portugal rental cottages,cheap package holidays to portugal, cheap holidays to portugal all inclusive";
            ViewBag.Title = "Search for villas townhouses, apartments in rural, seaside or city break settings. Book holidays in portugal's silver coast, in Foz do Arelho, Salir do Porto, Olho Marinho or Formigal. Book car rental, wine tours, site seeing tours with Portugal Rental Cottages. Rent Hyundai I20, Mitsubishi Lancer, Mitsubishi Grandis, go siteseeing with Lisbon City Tours or Sintra Palace Tours, get 9 Seater Van transport from the airport and have full Cleaning Service, Breakfast and Swimming Towels provided! Visit EXPO 98 & Ocenarium on your Portugal Holiday Rental!";
            return View(model);
        }



        [HttpPost]
        public ActionResult GetBookingDetails(Customer cus, CustomerBankDetail bank, List<Booking> bookings = null, List<BookingExtraSelection> bookingExtraSelections = null, List<BookingParticipant> bookingParticipants = null)
        {
            //set customer preferred currency symbol and currency

            using (var db = new PortugalVillasContext())
            {

                var account = new AccountController();
                var eventService = new EventController();
                var bookingRepo = new FinalBookingDetailGatheringController();
                //CUSTOMER
                //does customer have ID? if not create new Customer
                if (cus.CustomerID.Equals(0))
                {
                    var returnedCus = bookingRepo.CreateCustomer(cus, db);
                    cus = returnedCus;
                    Session["prc_customer"] = returnedCus;
                }
                else
                {
                    //update customer with new details
                    cus.BookingExtraSelections = null;
                    cus.Bookings = null;

                    //Do the UPDATE
                    if (ModelState.IsValid)
                    {
                        db.Entry(cus).State = EntityState.Modified;
                        var objContextCus = ((IObjectContextAdapter)db).ObjectContext;

                        var refreshableObjectsCus =
                            (from entry in objContextCus.ObjectStateManager.GetObjectStateEntries(
                                EntityState.Added
                                | EntityState.Deleted
                                | EntityState.Modified
                                | EntityState.Unchanged)
                             where entry.EntityKey != null
                             select entry.Entity);

                        objContextCus.Refresh(RefreshMode.ClientWins, refreshableObjectsCus);

                        //if it works, do the update for the userContext, else, don't as it failed
                        if (objContextCus.SaveChanges() > 0)
                        {
                            /*_usersContext.Entry(user).State = EntityState.Modified;
                            _usersContext.SaveChanges();*/
                        }



                    }
                }

                //BANK DETAIL
                if (bank.CustomerBankDetailID.Equals(0))
                {
                    var returnedBank = bookingRepo.CreateCustomerBankDetail(bank, cus, db);
                    Session["prc_customerBankingDetail"] = returnedBank;
                }
                else
                {
                    bank.CustomerID = cus.CustomerID;
                    db.Entry(bank).State = EntityState.Modified;

                    var objContext = ((IObjectContextAdapter)db).ObjectContext;

                    var refreshableObjects = (from entry in objContext.ObjectStateManager.GetObjectStateEntries(
                        EntityState.Added
                        | EntityState.Deleted
                        | EntityState.Modified
                        | EntityState.Unchanged)
                                              where entry.EntityKey != null
                                              select entry.Entity);

                    objContext.Refresh(RefreshMode.ClientWins, refreshableObjects);


                    if (ModelState.IsValid)
                    {

                        if (objContext.SaveChanges() > 0) ;
                        {
                            //great it worked
                        }
                    }

                }


                //parent
                BookingParentContainer parentContainer = new BookingParentContainer();
                parentContainer.CustomerID = cus.CustomerID;


                parentContainer = bookingRepo.CreateBookingParentContainer(parentContainer, db);
                //need to add booking reference then propagate to all others below


                //////////////////
                //BOOKING
                //create a parent booking thingy and link i
                if (bookings != null)
                {
                    foreach (var booking in bookings)
                    {
                        booking.Test = false; //it's real, from a customer
                        //convert if neededed

                        //link to parent booking
                        booking.BookingParentContainerID = parentContainer.BookingParentContainerID;
                        //fill em out and push them to the DB

                        var property = db.Properties.Find(booking.PropertyID);

                        booking.Property = property;

                        booking.BookingParticipants = null;
                        bookingRepo.CreateBooking(booking, property, cus, db);



                        var participantsThisRound =
                            bookingParticipants.Where(x => x.StepNo.Equals(bookingParticipants.Min(y => y.StepNo)))
                                .ToList();
                        //create the participants

                        var partstoAdd = new List<BookingParticipant>();
                        foreach (var bookingParticipant in participantsThisRound)
                        {
                            if (bookingParticipant.BookingParticipantFirstName != "" &&
                                bookingParticipant.BookingParticipantFirstName != null
                                && bookingParticipant.BookingParticipantLastName != "" &&
                                bookingParticipant.BookingParticipantLastName != null)
                            {
                                partstoAdd.Add(bookingParticipant);


                            }
                        }



                        foreach (var bookingParticipant in participantsThisRound)
                        {
                            bookingParticipants.Remove(bookingParticipant);
                        }

                        if (partstoAdd.Count > 0)
                        {
                            bookingRepo.CreateBookingParticipant(partstoAdd, booking, db); //create all ones we need
                        }


                        //now check which booking form to send depending on location
                        string EventTypeID;

                        TimeSpan dateRemainder = ((DateTime)booking.StartDate).Subtract(DateTime.Now);
                        int dateRemainderDays = dateRemainder.Days;


                        //late booking form

                        if (cus.Country.ToLower() == "united kingdom" && ConfigurationManager.AppSettings["defaultCurrency"] == "GBP")
                        {
                            if (dateRemainderDays <= 30)
                            {
                                //late uk
                                EventTypeID = "51";
                            }
                            else
                            {
                                //standard UK
                                EventTypeID = "37";
                            }

                        }
                        else
                        {

                            //convert all the bookings to euros                         
                            if (dateRemainderDays <= 30)
                            {
                                //late EU
                                EventTypeID = "52";
                            }
                            else
                            {
                                //standard EU
                                EventTypeID = "20";
                            }

                        }

                        //email all docs
                        var form = new FormCollection();
                        form.Add("BookingID", booking.BookingID.ToString());
                        form.Add("EventTypeID", EventTypeID);

                        eventService.AddBookingEvent(form);

                        //email the user of the system 

                    }
                }



                if (bookingExtraSelections != null)
                {
                    foreach (var bes in bookingExtraSelections)
                    {
                        //?????//convert all BES to EUROS 



                        var extraType = db.BookingExtras.Find((long)bes.BookingExtraID);
                        bes.BookingExtraID = extraType.BookingExtraID;
                        //link to parent booking
                        bes.BookingParentContainerID = parentContainer.BookingParentContainerID;


                        //need
                        //reference
                        //price fully done
                        //all extras on form fully taken care of for each one
                        var createdBes = bookingRepo.CreateBookingExtraSelection(bes, extraType, cus, db);

                        string EventTypeID = "";
                        //car 
                        if (extraType.BookingExtraTypeID == 1)
                        {
                            if (cus.Country.ToLower() == "united kingdom")
                            {
                                EventTypeID = "43";
                            }
                            else
                            {
                                EventTypeID = "62";
                            }
                        }
                        //wine
                        else if (extraType.BookingExtraTypeID == 2)
                        {
                            if (cus.Country.ToLower() == "united kingdom")
                            {
                                EventTypeID = "45";
                            }
                            else
                            {
                                EventTypeID = "60";
                            }
                        }
                        //airport
                        else if (extraType.BookingExtraTypeID == 3)
                        {
                            if (cus.Country.ToLower() == "united kingdom")
                            {
                                EventTypeID = "31";
                            }
                            else
                            {
                                EventTypeID = "61";
                            }
                        }
                        //tour
                        else if (extraType.BookingExtraTypeID == 4)
                        {
                            if (cus.Country.ToLower() == "united kingdom")
                            {
                                EventTypeID = "33";
                            }
                            else
                            {
                                EventTypeID = "63";
                            }
                        }

                        var form = new FormCollection();
                        form.Add("BookingExtraSelectionID", bes.BookingExtraSelectionID.ToString());
                        form.Add("EventTypeID", EventTypeID);

                        eventService.AddBookingExtraSelectionEvent(form);



                    }
                }


                //wipe cars = booking and bes
                Session["Cart_PropertyBookings"] = null;
                Session["Cart_ExtraBookings"] = null;


                //email the main user with the new services and bookings
                var prc = db.PRCInformations.First();

                MaintainanceMailer mail = new MaintainanceMailer();
                mail.theAsposeMessage.Subject = "You have new bookings:";
                mail.theAsposeMessage.HtmlBody =
                    "<h1>Automated email: You have a new booking</h1><br><p>Customer: ID - " + cus.CustomerID + "-" + cus.FirstName + " " +
                    cus.LastName
                    + "</p>";


                if (bookings != null)
                {
                    foreach (var booking in bookings)
                    {
                        mail.theAsposeMessage.HtmlBody += "<p>Booking: ID - " + booking.BookingID + "-" +
                                                          booking.Property.LegacyReference + " for " +
                                                          booking.NumberOfNights + " " + "nights starting " +
                                                          booking.StartDate.ToString().Substring(0, 10)
                                                          + "</p>";
                    }
                }


                if (bookingExtraSelections != null)
                {
                    foreach (var bes in bookingExtraSelections)
                    {
                        mail.theAsposeMessage.HtmlBody += "<p>Booking: ID - " + bes.BookingExtraSelectionID + " - " +
                                                          bes.BookingExtra.BookingExtraName + " for " + bes.NumberOfDays +
                                                          " " + "days starting " +
                                                          bes.ExtraRentalDate.ToString().Substring(0, 10) + " for " +
                                                          bes.NumberOfGuests + " people"
                                                          + "</p>";
                    }
                }

                var addressees = new MailAddressCollection();
                    addressees.Add(prc.PRCNotificationEmailAddress);
                    if (prc.PRCNotificationEmailAddress2 != null)
                        if (prc.PRCNotificationEmailAddress2 != "")
                        {
                            addressees.Add(prc.PRCNotificationEmailAddress2);
                        }
                    if (prc.PRCNotificationEmailAddress3 != null)
                        if (prc.PRCNotificationEmailAddress3 != "")
                        {
                            addressees.Add(prc.PRCNotificationEmailAddress3);
                        }

                    mail.SendEmail();
               


                return RedirectToAction("EndOfBookingProcess", "EndOfProcess");
            }

        }


    }
}
