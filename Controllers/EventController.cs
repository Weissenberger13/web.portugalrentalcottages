using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using BootstrapVillas.Content.Classes;
using BootstrapVillas.Content.Classes.EventCommands;
using BootstrapVillas.Content.Classes.ProcessEvents;
using BootstrapVillas.Content.Classes.ProcessEvents.Services;
using BootstrapVillas.Models;
using BootstrapVillas.Models.Events;
using DotNetOpenAuth.Messaging;
using WebGrease.Css.Extensions;

namespace BootstrapVillas.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class EventController : Controller
    {
        private PortugalVillasContext db = new PortugalVillasContext();




        public ActionResult AddBookingEvent(long id = 0)
        {
            var eventsForDDL = db.EventTypes.Where(x => x.EventSchemeTypeID == 1 || x.EventSchemeTypeID == 2).ToList();

            ViewBag.Bookingid = id;
            ViewBag.EventTypeDDL = eventsForDDL;

            return View();
        }



        public ActionResult AddBookingExtraSelectionEvent(long id = 0)
        {
            var eventsForDDL = db.EventTypes.Where(x => x.EventSchemeTypeID == 1).ToList();

            //get specific events for this type
            eventsForDDL.AddRange(db.EventTypes.Where(x => x.EventSchemeTypeID == 3).ToList());

            ViewBag.BesID = id;
            ViewBag.EventTypeDDL = eventsForDDL;

            return View();
        }





        [HttpPost]
        public ActionResult AddBookingEvent(FormCollection eventAndBooking)
        {


            try
            {

                var bookingId = Convert.ToInt32(eventAndBooking["BookingID"]);
                var eventTypeId = Convert.ToInt32(eventAndBooking["EventTypeID"]);

                //get the booking
                var booking = db.Bookings.Where(x => x.BookingID.Equals(bookingId)).FirstOrDefault();
                var property = booking.Property;

                //get the customer
                var customer = db.Customers.FirstOrDefault(x => x.CustomerID.Equals(booking.CustomerID));
                var documentType = new PRCDocument.PRCDocumentType();



                //create new event and assign the correct typeID HERE, then can make event creation generic                
                var eventToAdd = CreateBookingEventViaFactory(eventTypeId, booking);

                /*var whatAmIReturning = FullyInstantiateAndRunEvent(customer, booking);*/

                //////////GET TYPE OF DOCUMENT//////
                //STORE IN EVENT - DocumentEnum and Email Enum
                if (eventToAdd.EventType.DocumentEnumID != null)
                {
                    documentType = (PRCDocument.PRCDocumentType)eventToAdd.EventType.DocumentEnumID;
                }


                ////BEGIN GENERIC CODE
                /////////////////////////////////////////
                ////////CREATE EVENT DEPENDING ON SUB TYPE
                EventCommandCreateDocument createDocCommand;
                EventCommandSendEmail sendEmail;
                EventCommandDocumentOutDirectoryBundleAndEmail docBundle;

                switch (eventToAdd.EventType.EventSubTypeID)
                {
                    case 1:
                    //does nothing, just add event
                    case 2:
                        //email out                        
                        sendEmail = new EventCommandSendEmail(eventToAdd, (int)eventToAdd.EventType.EmailTemplateId, customer, booking);
                        eventToAdd.EventCommands.Add(sendEmail);
                        break;
                    case 3:
                        createDocCommand = new EventCommandCreateDocument(eventToAdd, customer, documentType, booking);
                        sendEmail = new EventCommandSendEmail(eventToAdd, (int)eventToAdd.EventType.EmailTemplateId, customer, booking);
                        eventToAdd.EventCommands.Add(createDocCommand);
                        eventToAdd.EventCommands.Add(sendEmail);
                        //document out + email that document
                        break;
                    case 4:
                        //email reminder
                        sendEmail = new EventCommandSendEmail(eventToAdd, (int)eventToAdd.EventType.EmailTemplateId, customer, booking);
                        eventToAdd.EventCommands.Add(sendEmail);
                        break;
                    case 5:
                        //create only
                        sendEmail = new EventCommandSendEmail(eventToAdd, (int)eventToAdd.EventType.EmailTemplateId, customer, booking);
                        eventToAdd.EventCommands.Add(sendEmail);
                        break;
                    case 6:
                        //composite bundle and email
                        //get directory for docs
                        var bundleDir = HttpRuntime.AppDomainAppPath + "FinalBookingDocuments\\" + property.LegacyReference.ToLower() + "\\";
                        docBundle = new EventCommandDocumentOutDirectoryBundleAndEmail(bundleDir, eventToAdd, customer, documentType, booking);
                        sendEmail = new EventCommandSendEmail(eventToAdd, (int)eventToAdd.EventType.EmailEnumID, customer, booking);
                        eventToAdd.EventCommands.Add(docBundle);
                        eventToAdd.EventCommands.Add(sendEmail);
                        break;
                }



                var commandsAndResultsToLog = new List<EventCommand>();

                //execute the commands //create document using executes //create emails and bodies //do all sending

                var result = new EventCommandResult();
                foreach (var command in eventToAdd.EventCommands)
                {
                    command.EventCommandResults.Add(command.ExecuteCommand());
                    commandsAndResultsToLog.Add(command);
                }


                /////END CREATE EVENT


                ////////////////////////////
                //DO LOGGING
                //if successful log the event and the commands that ran
                //save event
                EventLogger log = new EventLogger(db);

                //strip out event wiring before loggin 
                eventToAdd.EventCommands = null;
                var doLogging = log.LogEvent(eventToAdd);

                var EventCommandLogger = new CommandLogger(db);
                var eventLogger = new EventLogger(db, EventCommandLogger);



                //save eventcoomand and result
                foreach (var commandAndResult in commandsAndResultsToLog)
                {
                    EventCommandLogger.Log(eventToAdd.EventID, commandAndResult);
                }


                //save document generated
                //if there's any docs, write them to the DB
                foreach (var doc in eventToAdd.Documents)
                {
                    doc.EventID = eventToAdd.EventID;
                    doc.DocumentDescription = documentType.ToString();
                    doc.EmailTo = customer.EmailAddress;
                    doc.CustomerID = customer.CustomerID;

                    db.Documents.Add(doc);
                    db.SaveChanges();
                }


                //for view
                ViewBag.Bookingid = bookingId;

                var eventsForDDL =
                    db.EventTypes.Where(x => x.EventSchemeTypeID == 1 || x.EventSchemeTypeID == 2).ToList();
                ViewBag.EventTypeDDL = eventsForDDL;
            }
            catch (Exception ex)
            {
                MaintainanceMailer mail = new MaintainanceMailer();

                mail.theAsposeMessage.Subject = "Adding Booking Event Failed";

                mail.theAsposeMessage.Body = ex.Message.ToString();
                mail.theAsposeMessage.Body += "----------------------------------";
                mail.theAsposeMessage.Body += ex.InnerException.ToStringDescriptive();

                throw ex;

            }


            return View();
        }

        private object FullyInstantiateAndRunEvent()
        {
            throw new NotImplementedException();
        }


        [HttpPost]
        public ActionResult AddBookingExtraSelectionEvent(FormCollection eventAndBooking)
        {


            try
            {
                var eventsForDDL = db.EventTypes.Where(x => x.EventSchemeTypeID == 1 || x.EventSchemeTypeID == 3).ToList();
                ViewBag.EventTypeDDL = eventsForDDL;

                var besid = Convert.ToInt32(eventAndBooking["BookingExtraSelectionID"]);
                var eventTypeId = Convert.ToInt32(eventAndBooking["EventTypeID"]);

                //create new event and assign the correct typeID HERE, then can make event creation generic


                var bes = db.BookingExtraSelections.Where(x => x.BookingExtraSelectionID.Equals(besid)).FirstOrDefault();

                //get the customer
                var customer = db.Customers.FirstOrDefault(x => x.CustomerID.Equals(bes.CustomerID));
                var documentType = new PRCDocument.PRCDocumentType();



                //create new event and assign the correct typeID HERE, then can make event creation generic                
                var eventToAdd = CreateBESEventViaFactory(eventTypeId, bes);

                /*var whatAmIReturning = FullyInstantiateAndRunEvent(customer, booking);*/

                //////////GET TYPE OF DOCUMENT//////
                //STORE IN EVENT - DocumentEnum and Email Enum
                documentType = (PRCDocument.PRCDocumentType)eventToAdd.EventType.DocumentEnumID;


                ////BEGIN GENERIC CODE
                /////////////////////////////////////////
                ////////CREATE EVENT DEPENDING ON SUB TYPE
                EventCommandCreateDocument createDocCommand;

                EventCommandSendEmail sendEmail;

                createDocCommand = new EventCommandCreateDocument(eventToAdd, customer, documentType, null, bes);
                sendEmail = new EventCommandSendEmail(eventToAdd, (int)eventToAdd.EventType.EmailTemplateId, customer, null, bes);

                switch (eventToAdd.EventType.EventSubTypeID)
                {
                    case 1:
                    //does nothing, just add event
                    case 2:
                        //email out                        
                        eventToAdd.EventCommands.Add(sendEmail);
                        break;
                    case 3:
                        eventToAdd.EventCommands.Add(createDocCommand);
                        eventToAdd.EventCommands.Add(sendEmail);
                        //document out + email that document
                        break;
                    case 4:
                        //email reminder
                        eventToAdd.EventCommands.Add(sendEmail);
                        break;
                    case 5:
                        //email reminder
                        eventToAdd.EventCommands.Add(createDocCommand);
                        break;
                }



                var commandsAndResultsToLog = new List<EventCommand>();

                //execute the commands //create document using executes //create emails and bodies //do all sending

                var result = new EventCommandResult();
                foreach (var command in eventToAdd.EventCommands)
                {
                    command.EventCommandResults.Add(command.ExecuteCommand());
                    commandsAndResultsToLog.Add(command);
                }


                /////END CREATE EVENT


                ////////////////////////////
                //DO LOGGING
                //if successful log the event and the commands that ran
                //save event
                EventLogger log = new EventLogger(db);

                //strip out event wiring before loggin 
                eventToAdd.EventCommands = null;
                var doLogging = log.LogEvent(eventToAdd);

                var EventCommandLogger = new CommandLogger(db);
                var eventLogger = new EventLogger(db, EventCommandLogger);



                //save eventcoomand and result
                foreach (var commandAndResult in commandsAndResultsToLog)
                {
                    EventCommandLogger.Log(eventToAdd.EventID, commandAndResult);
                }


                //save document generated
                //if there's any docs, write them to the DB
                foreach (var doc in eventToAdd.Documents)
                {
                    doc.EventID = eventToAdd.EventID;
                    doc.DocumentDescription = documentType.ToString();
                    doc.EmailTo = customer.EmailAddress;
                    doc.CustomerID = customer.CustomerID;

                    db.Documents.Add(doc);
                    db.SaveChanges();
                }


                ViewBag.BesID = besid;

                ViewBag.EventTypeDDL = eventsForDDL;
            }
            catch (Exception ex)
            {
                MaintainanceMailer mail = new MaintainanceMailer();

                mail.theAsposeMessage.Subject = "Adding Bes Event Failed";
                mail.theAsposeMessage.Body = ex.Message.ToString();
                mail.theAsposeMessage.Body += "----------------------------------";
                mail.theAsposeMessage.Body += ex.InnerException.ToStringDescriptive();

                mail.SendEmail();

                throw ex;
            }

            return View();
        }



        public Event CreateEventViaFactory(long eventTypeID)
        {

            EventFactory eventFactory = new EventFactory(db);

            var type = db.EventTypes.Where(x => x.EventTypeID.Equals(eventTypeID)).First();
            var returnEvent = eventFactory.CreateEvent(type);
            returnEvent.EventType = type;


            return returnEvent;
        }


        public Event CreateBookingEventViaFactory(long eventTypeID, Booking booking)
        {

            var returnEvent = CreateEventViaFactory(eventTypeID);
            returnEvent.BookingID = booking.BookingID;



            return returnEvent;
        }


        public Event CreateBESEventViaFactory(long eventTypeID, BookingExtraSelection bes)
        {

            var returnEvent = CreateEventViaFactory(eventTypeID);
            returnEvent.BookingExtraSelectionID = bes.BookingExtraSelectionID;


            return returnEvent;
        }




        //
        // GET: /Event/

        public ActionResult EventIndexBooking(long bookingID)
        {
            var booking = db.Bookings.Where(x => x.BookingID == bookingID).FirstOrDefault();

            ViewBag.Booking = booking;
            ViewBag.Bookingid = booking.BookingID;

            var events = db.Events.Include(e => e.Booking).Include(e => e.BookingExtraSelection).Include(e => e.EventType).Include(x => x.Documents)
                .Where(x => !x.IsDeleted.Equals(true))
                .Where(c => c.BookingID == bookingID).OrderByDescending(x => x.WhenCreated).ToList();


            return View("Index", events);
        }


        public ActionResult EventIndexBookingExtraSelection(long besID)
        {
            var bes = db.BookingExtraSelections.Where(x => x.BookingExtraSelectionID == besID).FirstOrDefault();

            ViewBag.BookingExtraSelection = bes;


            var events = db.Events.Include(e => e.Booking).Include(e => e.BookingExtraSelection).Include(e => e.EventType)
                .Where(x => !x.IsDeleted.Equals(true))
                .Where(c => c.BookingExtraSelectionID == besID).OrderByDescending(x => x.WhenCreated);
            return View("Index", events.ToList());
        }

        //
        // GET: /Event/Details/5

        public ActionResult Details(long id = 0)
        {
            Event anevent = db.Events.Find(id);
            if (anevent == null)
            {
                return HttpNotFound();
            }
            return View(anevent);
        }

        //
        // GET: /Event/Create


        public ActionResult Index()
        {
            return View("Index", db.Events.ToSafeReadOnlyCollection());

        }

        public ActionResult Create()
        {
            ViewBag.BookingID = new SelectList(db.Bookings, "BookingID", "BookingPRCReference");
            ViewBag.BookingExtraSelectionID = new SelectList(db.BookingExtraSelections, "BookingExtraSelectionID", "Test");
            ViewBag.EventTypeID = new SelectList(db.EventTypes, "EventTypeID", "EventTypeName");
            return View();
        }

        //
        // POST: /Event/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Event anEvent)
        {
            if (ModelState.IsValid)
            {
                db.Events.Add(anEvent);
                db.SaveChanges();
                //            return RedirectToAction("Index");
            }

            ViewBag.BookingID = new SelectList(db.Bookings, "BookingID", "BookingPRCReference", anEvent.BookingID);
            ViewBag.BookingExtraSelectionID = new SelectList(db.BookingExtraSelections, "BookingExtraSelectionID", "Test", anEvent.BookingExtraSelectionID);
            ViewBag.EventTypeID = new SelectList(db.EventTypes, "EventTypeID", "EventTypeName", anEvent.EventTypeID);
            return View(anEvent);
        }

        //
        // GET: /Event/Edit/5

        public ActionResult Edit(long id = 0)
        {
            Event anEvent = db.Events.Find(id);
            if (anEvent == null)
            {
                return HttpNotFound();
            }
            ViewBag.BookingID = new SelectList(db.Bookings, "BookingID", "BookingPRCReference", anEvent.BookingID);
            ViewBag.BookingExtraSelectionID = new SelectList(db.BookingExtraSelections, "BookingExtraSelectionID", "Test", anEvent.BookingExtraSelectionID);
            ViewBag.EventTypeID = new SelectList(db.EventTypes, "EventTypeID", "EventTypeName", anEvent.EventTypeID);
            return View(anEvent);
        }

        //
        // POST: /Event/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Event anEvent)
        {
            if (ModelState.IsValid)
            {
                db.Entry(anEvent).State = EntityState.Modified;
                db.SaveChanges();
                //              return RedirectToAction("Index");
            }
            ViewBag.BookingID = new SelectList(db.Bookings, "BookingID", "BookingPRCReference", anEvent.BookingID);
            ViewBag.BookingExtraSelectionID = new SelectList(db.BookingExtraSelections, "BookingExtraSelectionID", "Test", anEvent.BookingExtraSelectionID);
            ViewBag.EventTypeID = new SelectList(db.EventTypes, "EventTypeID", "EventTypeName", anEvent.EventTypeID);
            return View(anEvent);
        }

        //
        // GET: /Event/Delete/5
        /*
                public ActionResult Delete(long id = 0)
                {
                    Event anEvent = db.Events.Find(id);
                    if (anEvent == null)
                    {
                        return HttpNotFound();
                    }
                    return View(anEvent);
                }*/

        //
        // POST: /Event/Delete/5


        public ActionResult Delete(long id, long bookingID = 0, long besID = 0)
        {

            long book = bookingID;
            Event anEvent = db.Events.Find(id);
            anEvent.IsDeleted = true;
            db.Events.Attach(anEvent);
            db.Entry(anEvent).State = EntityState.Modified;
            db.SaveChanges();

            if (bookingID > 0)
            {
                return RedirectToAction("EventIndexBooking", new { bookingID = bookingID });
            }
            else
            {
                return RedirectToAction("EventIndexBookingExtraSelection", new { besID = besID });
            }
        }




        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}