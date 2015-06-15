using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Text;
using System.Net;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;
using System.Media;
using System.Web.Mvc;
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

namespace BootstrapVillas.Controllers
{
    public class TestController : Controller
    {
        public ActionResult TestHtmlHelper()
        {

            return View();

        }

        public ActionResult TestAutoComplete()
        {

            return View();
        }


        public ActionResult TestPOSTCustomer()
        {

            return View();
        }


        public ActionResult TestBookingCalendar()
        {

            return View();
        }


        public ActionResult TestBookingSteps()
        {
            ViewBag.aBooking = Booking.GetBookingByID(4);
            ViewBag.aCustomer = Customer.GetSingleCustomer(1);
            ViewBag.aBes = BookingExtraSelection.GetSingleBookingExtraSelection(3);
            ViewBag.BankDetail = CustomerBankDetail.GetCustomerBankDetailFromCustomer((Customer)ViewBag.aCustomer);

            var model = new FinalBookingDetailViewModel();
            model.Customer = Customer.GetSingleCustomer(1);
            model.CustomerBankDetail = CustomerBankDetail.GetCustomerBankDetailFromCustomer((Customer)ViewBag.aCustomer);
            model.Bookings.Add(Booking.GetBookingByID(4));
            /*model.Bookings.Add(Booking.GetBookingByID(4));*/

            /*model.BookingExtraSelections.Add(BookingExtraSelection.GetSingleBookingExtraSelection(3));*/
            model.BookingExtraSelections.Add(BookingExtraSelection.GetSingleBookingExtraSelection(3));

            return View(model);
        }


        [HttpPost]
        public ActionResult TestBookingSteps(Customer cus, CustomerBankDetail bank, List<Booking> bookings = null, List<BookingExtraSelection> bookingExtraSelections = null, List<BookingParticipant> bookingParticipants = null)
        {
            Session["prc_customer"] = new Customer();


            using (var db = new PortugalVillasContext())
            {
                var eventService = new EventController();
                var bookingRepo = new FinalBookingDetailGatheringController();
                //CUSTOMER
                //does customer have ID? if not create new Customer
                if (cus.CustomerID.Equals(0))
                {
                    bookingRepo.CreateCustomer(cus, db);
                }
                else
                {
                    //update customer with new details
                    /*cus.BookingExtraSelections = null;
                    cus.Bookings = null;
                    cus.CreationDate = cus.CreationDate;

                    db.Customers.Attach(cus);
                    db.Entry(cus).State = EntityState.Modified;

                    try
                    {
                        db.SaveChanges();
                    }
                    catch (OptimisticConcurrencyException)
                    {
                       
                    }
                    */
                }

                //BANK DETAIL
                if (bank.CustomerBankDetailID.Equals(0))
                {
                    bookingRepo.CreateCustomerBankDetail(bank, cus, db);
                }
                else
                {
                    //update customer with new details
                    db.CustomerBankDetails.Attach(bank);
                    db.Entry(bank).State = EntityState.Modified;
                    db.SaveChanges();
                }




                //////////////////
                //BOOKING
                //create a parent booking thingy and link it
                BookingParentContainer parentContainer = new BookingParentContainer();
                parentContainer.CustomerID = cus.CustomerID;

                parentContainer = bookingRepo.CreateBookingParentContainer(parentContainer, db);


                foreach (var booking in bookings)
                {   //link to parent booking
                    booking.BookingParentContainerID = parentContainer.BookingParentContainerID;
                    //fill em out and push them to the DB


                  /*  bookingRepo.CreateBooking(booking, cus, db);*/

                    
                    var participantsThisRound =
                        bookingParticipants.Where(x => x.StepNo.Equals(bookingParticipants.Min(y => y.StepNo))).ToList();
                    //create the participants

                    foreach (var bookingParticipant in participantsThisRound)
                    {
                        if (bookingParticipant.BookingParticipantFirstName != "" && bookingParticipant.BookingParticipantFirstName != null 
                            && bookingParticipant.BookingParticipantLastName != "" &&
                             bookingParticipant.BookingParticipantLastName != null)
                        {
                            bookingRepo.CreateBookingParticipant(participantsThisRound, booking, db);
                        }        
                    }
                    


                    foreach (var bookingParticipant in participantsThisRound)
                    {
                        bookingParticipants.Remove(bookingParticipant);               
                    }
         

                    


                    //now check which booking form to send depending on location
                    string EventTypeID;
                    if (cus.Country.ToUpper() == "GB")
                    {
                        EventTypeID = "17";
                    }
                    else
                    {
                        EventTypeID = "20";
                    }

                    //email all docs
                    var form = new FormCollection();
                    form.Add("BookingID", booking.BookingID.ToString());
                    form.Add("EventTypeID", EventTypeID);

                    eventService.AddBookingEvent(form);

                    //email the user of the system 

                }


                foreach (var bes in bookingExtraSelections)
                {
                    //link to parent booking

                }
             

                //wipe cars = booking and bes
                Session["Cart_PropertyBookings"] = null;
                Session["Cart_ExtraBookings"] = null;


                return RedirectToAction("EndOfBookingProcess", "EndOfProcess");
            }

        }


        public ActionResult TestDataTableRender()
        {
            using (var db = new PortugalVillasContext())
            {
                return View(db.Customers.Take(100).ToList());
            }



        }


        public void TestMailMergeRegions()
        {
            //Test WITH PARENT
            var db = new PortugalVillasContext();

            //TEST WITH BOOKING
            Booking booking = db.Bookings.Where(x => x.BookingID.Equals(4)).FirstOrDefault();
            booking.PropertyID = 221;
            /*           Booking booking = null;*/



            //TEST WITH CUSTOMER
            Customer aCustomer = db.Customers.Where(x => x.CustomerID == 1).FirstOrDefault();
            //TEST WITH BES
            var bes = db.BookingExtraSelections.Where(x => x.BookingExtraSelectionID == 3).First();
            bes.BookingExtraID = 1;

            //set up vars


            var type = PRCDocument.PRCDocumentType.UK_WineTasting;

        }


        public void TestSaveAsPDFASpose()
        {
            Aspose.Words.Document theDoc = new Aspose.Words.Document(@"h:\test\BungalowAdvert.docx");
            theDoc.Save(@"h:\test\test.pdf", SaveFormat.Pdf);


        }


        public void TestFullDocumentGenerateStack()
        {

            var db = new PortugalVillasContext();

            //Test WITH PARENT

            //TEST WITH BOOKING
            Booking booking = db.Bookings.Where(x => x.BookingID.Equals(4)).FirstOrDefault();
            booking.PropertyID = 221;
            /*           Booking booking = null;*/



            //TEST WITH CUSTOMER
            Customer aCustomer = db.Customers.Where(x => x.CustomerID == 1).FirstOrDefault();
            //TEST WITH BES
            var bes = db.BookingExtraSelections.Where(x => x.BookingExtraSelectionID == 3).First();
            bes.BookingExtraID = 1;

            //set up vars


            var type = PRCDocument.PRCDocumentType.UK_WineTasting;




            //create event  - need to give it type thus name
            var anEvent = new Event();
            anEvent.Documents = new List<Document>();
            //TEST ADD ID - PLEASE REMOVE
            anEvent.EventID = 2;
            //END TEST CODE

            anEvent.WhenCreated = DateTime.Now;
            var commandsAndResultsToLog = new List<EventCommand>();


            //create correct command
            //doc
            //email out with doc

            EventCommandCreateDocument createDocCommand;
            EventCommandSendEmail sendEmail;
            //HOW DO WE DECIDE WHAT TYPE OF EVENT WE WANT??? depends on type??

            if (booking != null)
            {
                createDocCommand = new EventCommandCreateDocument(anEvent, aCustomer, type, booking);
                /* sendEmail = new EventCommandSendEmail(anEvent, aCustomer, booking);*/

            }
            else
            {
                createDocCommand = new EventCommandCreateDocument(anEvent, aCustomer, type, null, bes);
                /*/sendEmail = new EventCommandSendEmail(anEvent, aCustomer, null, bes);*/
            }



            //create all commands
            anEvent.EventCommands.Add(createDocCommand);
            /*       anEvent.EventCommands.Add(sendEmail);*/


            //create document using executes
            var result = new EventCommandResult();
            foreach (var command in anEvent.EventCommands)
            {
                command.EventCommandResults.Add(command.ExecuteCommand());
                commandsAndResultsToLog.Add(command);
            }

            //render

            //save to DB with all correct commands etc

            var EventCommandLogger = new CommandLogger(db);
            var eventLogger = new EventLogger(db, EventCommandLogger);

            //save event
            eventLogger.LogEvent(anEvent);
            //save eventcoomand and result


            foreach (var commandAndResult in commandsAndResultsToLog)
            {
                EventCommandLogger.Log(anEvent.EventID, commandAndResult);
            }

            //save document generated

            //NEED TO ADD CORRECT PARAMS (which are what??)

            //if there's any docs, write them to the DB
            foreach (var doc in anEvent.Documents)
            {
                doc.EventID = anEvent.EventID;
                doc.DocumentDescription = type.ToString();
                doc.EmailTo = aCustomer.EmailAddress;
                doc.CustomerID = aCustomer.CustomerID;

                db.Documents.Add(doc);
                db.SaveChanges();
            }





        }



        //
        // GET: /Test/
        public void TestEventCommandLogging()
        {
            var db = new PortugalVillasContext();

            var log = new CommandLogger(db);



            log.Log(35, new EventCommand());


        }




        public void TestEventLogging()
        {
            var an = new Event();
            an.WhenCreated = DateTime.Now;
            an.EventTypeID = 3;


            var log = new EventLogger(new PortugalVillasContext());
            log.LogEvent(an);

        }


        public void TestEventCommandAndLogging()
        {
            var db = new PortugalVillasContext();

            var anEvent = new Event();
            anEvent.WhenCreated = DateTime.Now;



            anEvent.Booking = db.Bookings.Find(4);

            long customerID = anEvent.Booking.CustomerID;

            //get other vars
            Customer aCustomer = db.Customers.Where(x => x.CustomerID == customerID).FirstOrDefault();


            EventCommandCreateDocument docEventCommand = new EventCommandCreateDocument(anEvent, aCustomer, PRCDocument.PRCDocumentType.UK_AirportTransfer, anEvent.Booking);

            anEvent.EventCommands.Add(docEventCommand);

            var result = new EventCommandResult();
            foreach (var command in anEvent.EventCommands)
            {
                result = command.ExecuteCommand();

            }

            //all done, do the logging

            var eventLogger = new EventLogger(db, new CommandLogger(db));

            eventLogger.LogEventAndCommandandCommandResult(anEvent, docEventCommand,
                result);

        }


        public ActionResult TestViewDocument()
        {
            return View();
        }


        public FileContentResult OpenInlineDocument()
        {
            using (var db = new PortugalVillasContext())
            {
                byte[] data = db.Documents.First(c => c.DocumentID == 2).DocumentBLOB; //.DocumentTable.First(p => p.DocumentUID == id);
                return File(data, "application/pdf", "test.pdf");
            }

        }


        public void TestEventCommands()
        {
            List<IEventCommand> cmds = new List<IEventCommand>();

            /*var command = new EventCommandBase();
            cmds.Add(command);

            var newEvent = new Event(cmds);*/



        }



        public void TestDocumentReadAndInsert()
        {


            var files = Directory.GetFileSystemEntries(Server.MapPath("~/Documents"), "*.pdf", SearchOption.AllDirectories);

            var bDoc = new Document();
            bDoc.DocumentBLOB = System.IO.File.ReadAllBytes(files[0]);

            using (var db = new PortugalVillasContext())
            {
                db.Documents.Add(bDoc);
                db.SaveChanges();

            }



        }

        public void testGetImages()
        {
            var test = Server.MapPath("~/").ToString();
            string[] entries = Directory.GetFileSystemEntries(Server.MapPath("~/PropertyImages/PRCV28"), "*.jpg", SearchOption.AllDirectories);
            entries.AddRange(Directory.GetFileSystemEntries(Server.MapPath("~/PropertyImages/PRCV28"), "*.png", SearchOption.AllDirectories));

            var pdfFiles = new DirectoryInfo(test + "/PropertyImages/PRCV28").GetFiles("*.jpg");

        }


        //test creating the documents
        public void DocumentBind()
        {






        }


        public void TestNewMailAllTemplates()
        {
            Customer testCust = new Customer();
            testCust.FirstName = "Test";
            testCust.LastName = "Customer";
            testCust.EmailAddress = "nadav.drewe@interresolve.co.uk";

            Booking booking = new Booking();
            booking.StartDate = new DateTime(2014, 01, 04);
            booking.EndDate = new DateTime(2014, 01, 10);
            booking.NumberOfNights = 5;
            booking.PropertyID = 220;
            booking.BookingID = 513;


            BookingExtraSelection bes = new BookingExtraSelection();
            bes.ExtraRentalDate = new DateTime(2014, 01, 04);
            bes.ExtraReturnDate = new DateTime(2014, 01, 09);
            bes.BookingExtraID = 1;
            bes.BookingExtraSelectionID = 1; //primary key





            EmailController ec = new EmailController();
            /*var ok = ec.SendEmailToCustomer(testCust, , booking, bes);
            ok = ec.SendEmailToCustomer(testCust, EmailOutType.EmailOutEmailType.BookingConfirm, booking);
            ok = ec.SendEmailToCustomer(testCust, EmailOutType.EmailOutEmailType.BookingExtraSelectionConfirm, null, bes);*/




        }

        public void TestMaintainanceMailService()
        {
            IMailService mailerService = new MaintainanceMailer();
            CurrencyExchangeService ces = new CurrencyExchangeService(mailerService);

            mailerService.Mail("emailnadz@gmail.com", "Help");
        }

        public void TestCurrencyService()
        {
            //new up everything
            IMailService mailerService = new MaintainanceMailer();
            CurrencyExchangeService ces = new CurrencyExchangeService(mailerService);

            //send a cheeky testmail


            //get all the currencies
            using (var _db = new PortVillasContext())
            {

                var currencies = _db.CurrencyExchanges.ToList();

                foreach (var currency in currencies)
                {
                    ces.UpdateCurrency(currency);
                }

            }

        }

        public void TestMail()
        {

            MaintainanceMailer letsMail = new MaintainanceMailer();
            letsMail.SendEmail();

        }


        public void TestBookingExtraPrices()
        {
            var bse = new BookingExtraSelection()
            {
                AcceptTC = "true",
                BookingExtraID = 1,
                ExtraRentalDate = new DateTime(2014, 03, 26),
                ExtraReturnDate = new DateTime(2014, 04, 02)

            };



           /* bse.GetBookingExtraPriceAndAssignToThisBookingExtraSelection();*/



        }


        public int CurrencyExchange()
        {
            var cc = new CurrencyConverterController();


            Booking book = new Booking();


            book.BookingPreferredCurrency = "GBP";
            book.BookingCurrencyConversionPrice = 1M;
            book.BookingCurrencyConversionSymbol = "£";
            book.BookingCurrencyExchangeRate = 1M;

            var result = cc.ConvertCurrency("GBP", "EUR", 1.00M);


            return 0;
        }

        public ActionResult TestValidation()
        {



            return View();
        }


        public void TestPricingMethods()
        {

            var db = new PortugalVillasContext();

            Booking testBooks = new Booking { PropertyID = 220, StartDate = new DateTime(2014, 01, 01), EndDate = new DateTime(2014, 01, 08) };

            var price = testBooks.CalculateBookingPricingForAPropertyBooking(db);

            //test for every day in a booking (e.g. range)
            var test = 0;

        }




        public void TestDataGatheringMethods()
        {

            var theBookingsInSession = (List<Booking>)Session["Cart_PropertyBookings"];

            //  var testSession = new PropertyViewController().AreThereAnyBookingsInTheSession();
            //   var testSEssionExtraBooking = new PropertyViewController().AreThereAnyExtraBookingsInTheSession();
            //instance method execute on propertyview controller
            var result = new FinalBookingDetailGatheringController().CreateListOfFieldsToGatherBasedOnSessionData();


        }



        public void TestNewClassesAndInterfaces()
        {
            ///create a new concrete implementation of the 
            ///
            Customer testCusti = Customer.GetSingleCustomer(1);

            Customer blankCust = new Customer();
            CustomerBankDetail aBankDetail = CustomerBankDetail.GetCustomerBankDetailFromCustomer(testCusti);

            testCusti.EmailAddress = "test@custi.com";
            testCusti.Town = "london";


            ICustomerSet testMeCustomer = new CustomerSet(testCusti);

            //right, so do we have the right events dere eh?

            var theEvents = testMeCustomer.theEventsChainToComplete.CheckRemainingEvents();
            var isItFalse = testMeCustomer.theEventsChainToComplete.CheckIfBookingComplete();
            var nextEvent = testMeCustomer.theEventsChainToComplete.CheckNextEvent();

            testMeCustomer.theEventsChainToComplete.EventComplete_RemoveFromRemainingEvents(nextEvent);


            var howManyEventsWeGot = testMeCustomer.theEventsChainToComplete.CheckRemainingEvents().Count();


        }


        public ActionResult TestTranslator()
        {

            string token = System.Web.HttpContext.Current.Application["TranslateToken"].ToString();


            return View();
        }


        private static void TranslateMethod(string authToken)
        {

            string text = "Use pixels to express measurements for padding and margins.";
            string from = "en";
            string to = "fr";

            string uri = "http://api.microsofttranslator.com/v2/Http.svc/Translate?text=" + System.Web.HttpUtility.UrlEncode(text) + "&from=" + from + "&to=" + to;

            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(uri);
            httpWebRequest.Headers.Add("Authorization", authToken);
            WebResponse response = null;
            try
            {
                response = httpWebRequest.GetResponse();
                using (Stream stream = response.GetResponseStream())
                {
                    System.Runtime.Serialization.DataContractSerializer dcs = new System.Runtime.Serialization.DataContractSerializer(Type.GetType("System.String"));
                    string translation = (string)dcs.ReadObject(stream);
                    Console.WriteLine("Translation for source text '{0}' from {1} to {2} is", text, "en", "de");
                    Console.WriteLine(translation);

                }
                //     Console.WriteLine("Press any key to continue...");
                //     Console.ReadKey(true);
            }
            catch
            {
                throw;
            }
            finally
            {
                if (response != null)
                {
                    response.Close();
                    response = null;
                }
            }
        }
        private static void ProcessWebException(WebException e)
        {
            Console.WriteLine("{0}", e.ToString());
            // Obtain detailed error information
            string strResponse = string.Empty;
            using (HttpWebResponse response = (HttpWebResponse)e.Response)
            {
                using (Stream responseStream = response.GetResponseStream())
                {
                    using (StreamReader sr = new StreamReader(responseStream, System.Text.Encoding.ASCII))
                    {
                        strResponse = sr.ReadToEnd();
                    }
                }
            }
            Console.WriteLine("Http status code={0}, error message={1}", e.Status, strResponse);
        }
    }


}

