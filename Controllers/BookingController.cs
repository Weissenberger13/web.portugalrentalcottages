using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BootstrapVillas.Models;
using Microsoft.Ajax.Utilities;
using BootstrapVillas.Content.Classes;

namespace BootstrapVillas.Controllers
{
      [Authorize(Roles = "Administrator")]
    public class BookingController : Controller
    {
        private PortugalVillasContext db = new PortugalVillasContext();

        //
        // GET: /Booking/

        public ActionResult BookingCalendar()
        {

            return View();

        }


        [HttpPost]
        public ActionResult BookingCalendar(long propertyID)
        {
            var calendar = new BookingCalendar(propertyID);
            calendar.GetAllBookingDatesAndAddToThisCalendar();


            return View(calendar);
        }

        public bool CancelBooking(long bookingID)
        {
            Booking booking = db.Bookings.Include(x => x.Customer).First(x => x.BookingID == bookingID);
            booking.Cancelled = true;

            if (ModelState.IsValid)
            {
                db.Entry(booking).State = EntityState.Modified;
                db.SaveChanges();
                return true;
            }

            return false;
        }


        public ActionResult SingleBookingIndex(long bookingID)
        {
            Booking booking = db.Bookings.Include(x => x.Customer).First(x => x.BookingID == bookingID);
            if (booking == null)
            {
                return HttpNotFound();
            }
            ViewBag.BookingParentContainerID = new SelectList(db.BookingParentContainers, "BookingParentContainerID", "OverallBookingReference", booking.BookingParentContainerID);
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "Title", booking.CustomerID);
            ViewBag.PropertyID = new SelectList(db.Properties, "PropertyID", "LegacyReference", booking.PropertyID);

            return View(booking);
        }


        public ActionResult Index()
        {
            var bookings = db.Bookings.Include(b => b.BookingParentContainer).Include(b => b.Customer).Include(b => b.Property);
            return View(bookings.ToList());
        }

        //
        // GET: /Booking/Details/5

        public ActionResult Details(long id = 0)
        {
            Booking booking = db.Bookings.Include(x => x.Customer).First(x => x.BookingID == id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        //
        // GET: /Booking/Create

        public ActionResult Create()
        {
            ViewBag.BookingParentContainerID = new SelectList(db.BookingParentContainers, "BookingParentContainerID", "OverallBookingReference");
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "CustomerID");
            ViewBag.PropertyID = new SelectList(db.Properties, "PropertyID", "LegacyReference");
            return View("QuickCreateBookingWrapper");
        }

        //
        // POST: /Booking/Create

        public ActionResult QuickCreateBooking()
        {
            //ViewBag.BookingParentContainerID = new SelectList(db.BookingParentContainers, "BookingParentContainerID", "OverallBookingReference", booking.BookingParentContainerID);
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "CustomerID");
            ViewBag.PropertyID = new SelectList(db.Properties, "PropertyID", "LegacyReference");
            return View("QuickCreateBookingWrapper");

        }

        //
        // GET: /Booking/Edit/5


        [HttpPost]
        public ActionResult QuickCreateBooking(Booking booking, List<BookingParticipant> bookingParticipants)
        {
            //setup
            

            booking.Test = false;

            var bookingRepo = new FinalBookingDetailGatheringController();

            BookingParentContainer parentContainer = new BookingParentContainer();
            parentContainer.CustomerID = booking.CustomerID;


            parentContainer = bookingRepo.CreateBookingParentContainer(parentContainer, db);
            db.BookingParentContainers.Add(parentContainer);

            //create booking
            var property = db.Properties.Find(booking.PropertyID);
            var customer = db.Customers.Find(booking.CustomerID);
            booking.Property = property;
            booking.BookingParentContainer = parentContainer;
            booking.BookingCurrencyConversionSymbol = customer.PreferredCurrencySymbol;
            booking.BookingPreferredCurrency = customer.PreferredCurrency;
            
            booking.BookingParticipants = null;
            bookingRepo.CreateBooking(booking, property, customer, db);
            //create parts           
            booking.Cancelled = false;
            booking.Confirmed = true;
            booking.Test = false;

            db.Bookings.Attach(booking);
            db.Entry(booking).State = EntityState.Modified;
            db.SaveChanges();



            var partstoAdd = new List<BookingParticipant>();
            if (bookingParticipants != null)
            {

                foreach (var bookingParticipant in bookingParticipants)
                {
                    if (bookingParticipant.BookingParticipantFirstName != "" &&
                        bookingParticipant.BookingParticipantFirstName != null
                        && bookingParticipant.BookingParticipantLastName != "" &&
                        bookingParticipant.BookingParticipantLastName != null &&
                        bookingParticipant.BookingParticipantAge != "")
                    {
                        bookingParticipant.BookingID = booking.BookingID;
                        partstoAdd.Add(bookingParticipant);
                    }

                }
                if (partstoAdd.Count > 0)
                {
                    bookingRepo.CreateBookingParticipant(partstoAdd, booking, db); //create all ones we need
                }
            }



            return View("QuickCreateBookingWrapper");
        }


        public ActionResult Edit(long id = 0)
        {
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }

            ViewBag.BookingParentContainerID = new SelectList(db.BookingParentContainers, "BookingParentContainerID", "OverallBookingReference", booking.BookingParentContainerID);
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "CustomerID", booking.CustomerID);
            ViewBag.PropertyID = new SelectList(db.Properties, "PropertyID", "LegacyReference", booking.PropertyID);
            return View(booking);
        }

        //
        // POST: /Booking/Edit/5

        [HttpPost]
        public ActionResult Edit(Booking booking)
        {
            var oldbook = db.Bookings.Where(x => x.BookingID == booking.BookingID).FirstOrDefault();

            booking.CreationDate = oldbook.CreationDate;
            booking.LastUpdated = oldbook.LastUpdated;

            if (ModelState.IsValid)
            {
                using (var _db = new PortugalVillasContext())
                {
                    _db.Bookings.Attach(booking);
                    _db.Entry(booking).State = EntityState.Modified;
                    _db.SaveChanges();
                    ViewBag.BookingParentContainerID = new SelectList(db.BookingParentContainers,
                        "BookingParentContainerID", "OverallBookingReference", booking.BookingParentContainerID);
                    ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "Title", booking.CustomerID);
                    ViewBag.PropertyID = new SelectList(db.Properties, "PropertyID", "LegacyReference",
                        booking.PropertyID);

                    return View("SingleBookingIndex", db.Bookings.Include(x => x.Customer).FirstOrDefault(x => x.BookingID == booking.BookingID));
                }
            }

            ViewBag.BookingParentContainerID = new SelectList(db.BookingParentContainers, "BookingParentContainerID", "OverallBookingReference", booking.BookingParentContainerID);
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "Title", booking.CustomerID);
            ViewBag.PropertyID = new SelectList(db.Properties, "PropertyID", "LegacyReference", booking.PropertyID);
            return View("SingleBookingIndex", db.Bookings.Include(x => x.Customer).FirstOrDefault(x => x.BookingID == booking.BookingID));
        }


        //
        // GET: /Booking/Delete/5

        public ActionResult Delete(long id = 0)
        {
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        //
        // POST: /Booking/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Booking booking = db.Bookings.Find(id);
            var parts = db.BookingParticipants.Where(x => x.BookingID == id).ToList();
            var events = db.Events.Include(x => x.EventCommands).Where(x => x.BookingID == id).ToList();

            var commands = new List<EventCommand>();
            var results = new List<EventCommandResult>();
            var docs = new List<Document>();

            //parts
            foreach (var part in parts)
            {
                db.BookingParticipants.Remove(part);
            }

            //get commands
            foreach (var @event in events)
            {
                commands = db.EventCommands.Where(x => x.EventID == @event.EventID).ToList();
                docs = db.Documents.Where(x => x.EventID == @event.EventID).ToList();
            }

            //get command results
            foreach (var command in commands)
            {
                results = db.EventCommandResults.Where(x => x.EventCommandID == command.EventCommandID).ToList();
            }


            //do the laboured EF 5 deleted
            foreach (var eventCommand in commands)
            {
                db.EventCommands.Remove(eventCommand);
            }

            foreach (var document in docs)
            {
                db.Documents.Remove(document);
            }

            foreach (var result in results)
            {
                db.EventCommandResults.Remove(result);
            }

            db.Bookings.Remove(booking);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}