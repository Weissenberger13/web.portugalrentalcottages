using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BootstrapVillas.Models;
using BootstrapVillas.Content.Classes;
using System.Web.SessionState;

namespace BootstrapVillas.Controllers
{
    public class FinalBookingDetailGatheringController : Controller, IRequiresSessionState
    {

        Dictionary<BookingDataCollectionEventType.Types, BookingDataCollectionEventType.PRCDataGatheringControllerActionRedirect> DataThatNeedsGetting;


        protected bool TestForSessionVariable(string variableName)
        {
            if (Session != null)
            {
                var isThereAnythingHere = Session[String.Format("{0}", variableName)];
                if (isThereAnythingHere != null && isThereAnythingHere != "")
                {
                    return true;
                }
            }

            return false;

        }


        public ActionResult DetailGatheringEventChain()
        {
            //create the lists we need 

            Dictionary<BookingDataCollectionEventType.Types, BookingDataCollectionEventType.PRCDataGatheringControllerActionRedirect> DataThatNeedsGetting = CreateListOfFieldsToGatherBasedOnSessionData();

            foreach (var dataRedirect in DataThatNeedsGetting)
            {
                //if it's a booking or an extra, whack it into the session so we know which one we're using and can pull the vars                

                if (dataRedirect.Key == BookingDataCollectionEventType.Types.Booking)
                {

                    Session["CurrentBookingDataGathering"] = new Booking
                    {
                        PropertyID = (long?)dataRedirect.Value.PropertyID,
                        BookingPRCReference = dataRedirect.Value.PRCRef.ToString(),
                        StartDate = dataRedirect.Value.StartDate,
                        EndDate = dataRedirect.Value.EndDate,
                        ExtraLininSet = dataRedirect.Value.NoLinen,
                        SwimmingPoolHeating = dataRedirect.Value.NoSwimmingPool,
                        // SpecialRequests = dataRedirect.Value.SpecialRequests,
                        NoOfTowelsRequested = dataRedirect.Value.NoTowels,
                        BookingPrice = dataRedirect.Value.Price


                    };
                }
                else if (dataRedirect.Key == BookingDataCollectionEventType.Types.BookingExtraSelection)
                {
                    Session["CurrentBookingExtraDataGathering"] = new BookingExtraSelection
                    {
                        BookingExtraPRCReference = dataRedirect.Value.PRCRef.ToString(),
                        ExtraRentalDate = dataRedirect.Value.StartDate,
                        ExtraReturnDate = dataRedirect.Value.EndDate,
                        BookingExtraID = (long)dataRedirect.Value.ExtraTypeID,
                        BESPrice = dataRedirect.Value.Price
                    };

                }

                return RedirectToAction(dataRedirect.Value.Action, dataRedirect.Value.Controller);
            }



            //populate the session items with those lists






            if (DataThatNeedsGetting.Count == 0)
            //we're done
            {
                return View();
            }
            //remove all items from our list that need gathering

            //end


            return RedirectToAction("DetailGatheringEventChain");


        }

        /// <summary>
        /// SRP: Creates list of objects for this Customer based on what's in session
        /// - DO we have a customer
        /// - Do we have bank details
        /// - Collect All Booking Details and Extra Details
        /// </summary>
        /// <returns></returns>
        public Dictionary<BookingDataCollectionEventType.Types, BookingDataCollectionEventType.PRCDataGatheringControllerActionRedirect> CreateListOfFieldsToGatherBasedOnSessionData()
        {

            try
            {


                List<BookingDataCollectionEventType.Types> whatDataNeedsToBeGathered = new List<BookingDataCollectionEventType.Types>();
                Dictionary<BookingDataCollectionEventType.Types, BookingDataCollectionEventType.PRCDataGatheringControllerActionRedirect> DataThatNeedsGetting = new Dictionary<BookingDataCollectionEventType.Types, BookingDataCollectionEventType.PRCDataGatheringControllerActionRedirect>();

                //pull customer data if exists, else  corresponding  dataset object
                if (!TestForSessionVariable("prc_customer"))
                {  //there's nothing, add customer to the list
                    DataThatNeedsGetting.Add(BookingDataCollectionEventType.Types.Customer, new BookingDataCollectionEventType.PRCDataGatheringControllerActionRedirect { Action = "CreateCustomer", Controller = "FinalBookingDetailGathering" });
                }

                //pull customer banking details
                if (!TestForSessionVariable("prc_customerBankingDetail"))
                {
                    DataThatNeedsGetting.Add(BookingDataCollectionEventType.Types.Payment, new BookingDataCollectionEventType.PRCDataGatheringControllerActionRedirect { Action = "CreateCustomerBankDetail", Controller = "FinalBookingDetailGathering" });
                }

                //pull all property bookings from the session and generate corresponding  dataset objects   

                if (Session["Cart_PropertyBookings"] != null)
                {

                    List<Booking> theBookings = (List<Booking>)Session["Cart_PropertyBookings"];
                    //foreach booking generate a list of things to grab, then add it to the Dictionary                

                    //test if we have a data gathering object in the session matching every booking - if so, ignore, else add

                    foreach (var booking in theBookings)
                    {
                        //test if we have a data gathering object in the session matching every booking - if so, ignore, else add
                        //  if (Session[String.Format("booking_{0}_{1}_{2}_{3}", booking.PropertyID.ToString(), booking.BookingPRCReference, booking.StartDate.ToString(), booking.EndDate.ToString())] == null)
                        //generate customer data collection attributes and add to the set
                        // {
                        //    Session[String.Format("booking_{0}_{1}_{2}_{3}", booking.PropertyID.ToString(), booking.BookingPRCReference, booking.StartDate.ToString(), booking.EndDate.ToString())] = booking;
                        BookingDataCollectionEventType.PRCDataGatheringControllerActionRedirect aRedirect = new BookingDataCollectionEventType.PRCDataGatheringControllerActionRedirect { Action = "CreateBooking", Controller = "FinalBookingDetailGathering", CompositeBookingID = booking.PropertyID + "_" + booking.BookingPRCReference + "_" + booking.StartDate + "_" + booking.StartDate, PRCRef = booking.BookingPRCReference, PropertyID = (long?)booking.PropertyID, StartDate = booking.StartDate, EndDate = booking.EndDate, NoLinen = booking.ExtraLininSet, NoSwimmingPool = booking.SwimmingPoolHeating, NoTowels = booking.NoOfTowelsRequested, Price = booking.BookingPrice };
                        if (booking.SpecialRequests != null)
                        { aRedirect.SpecialRequests = booking.SpecialRequests; }
                        else aRedirect.SpecialRequests = "";


                        DataThatNeedsGetting.Add(BookingDataCollectionEventType.Types.Booking, aRedirect);
                        // }
                    }

                }

                //pull all extra bookings from the session and corresponding  dataset objects

                if (Session["Cart_ExtraBookings"] != null)
                {
                    List<BookingExtraSelection> theExtraBookings = (List<BookingExtraSelection>)Session["Cart_ExtraBookings"];

                    foreach (var bookingExtra in theExtraBookings)
                    {


                        BookingDataCollectionEventType.PRCDataGatheringControllerActionRedirect aRedirect = new BookingDataCollectionEventType.PRCDataGatheringControllerActionRedirect { Action = "CreateBookingExtraSelection", Controller = "FinalBookingDetailGathering", CompositeBookingID = bookingExtra.BookingExtraPRCReference + "_" + bookingExtra.ExtraRentalDate + "_" + bookingExtra.ExtraReturnDate, PRCRef = bookingExtra.BookingExtraPRCReference, StartDate = bookingExtra.ExtraRentalDate, EndDate = bookingExtra.ExtraReturnDate, ExtraTypeID = bookingExtra.BookingExtraID, Price = bookingExtra.BESPrice };
                        if (bookingExtra.BESSpecialRequests != null)
                        { aRedirect.SpecialRequests = bookingExtra.BESSpecialRequests; }
                        else aRedirect.SpecialRequests = "";

                        DataThatNeedsGetting.Add(BookingDataCollectionEventType.Types.BookingExtraSelection, aRedirect);
                    }

                }



                return DataThatNeedsGetting;


            }
            catch (Exception ex)
            {

                throw ex;
            }



        }


        ///////////////////////////////////////////
        //Customers
        /// <summary>
        /// Creates a new customer at checkout time
        /// </summary>
        /// <returns></returns>

        public ActionResult CreateCustomer()
        {
            return View();
        }

        //
        // POST: /FinalBookingDetailGathering/Create


        public bool CreateCustomerAsPartOfSignUp(Customer customer)
        {

            PortugalVillasContext db = new PortugalVillasContext();

            if (ModelState.IsValid)
            {

                //CHECK IF THERE'S ALREADY A CUSTOMER WITH THIS EMAIL ADDRESS FIRST!!!

                //end check

                //CHECK we ain't got anything for this customer in the Sessions already

                //end check


                //if not, add customer            

                //set up fields

                customer.CreationDate = DateTime.Now;
                if (customer.Country.ToLower() == "united kingdom")
                {
                    customer.PreferredCurrency = "GBP";
                    customer.PreferredCurrencySymbol = "£";
                }
                else
                {
                    customer.PreferredCurrency = "EUR";
                    customer.PreferredCurrencySymbol = "€";
                }
                //end

                db.Customers.Add(customer);

                if (db.SaveChanges() > 0)
                {
                    //customer now has an ID if the save has worked
                    System.Web.HttpContext.Current.Session["prc_customer"] = customer;

                    return true;

                }



            }
            return false;
        }



        [HttpPost]
        public Customer CreateCustomer(Customer customer, PortugalVillasContext db)
        {




            //CHECK IF THERE'S ALREADY A CUSTOMER WITH THIS EMAIL ADDRESS FIRST!!!

            //end check

            //CHECK we ain't got anything for this customer in the Sessions already

            //end check


            //if not, add customer            

            //set up fields
            customer.CreationDate = DateTime.Now;
            customer.Test = false;



            //end

            db.Customers.Add(customer);

            if (db.SaveChanges() > 0)
            {
                //customer now has an ID if the save has worked
                return customer;

            }

            throw new Exception("Cannot create customer");

        }


        /// <summary>
        /// Edit and existing customer
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult EditCustomer(decimal id = 0)
        {
            PortugalVillasContext db = new PortugalVillasContext();

            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }

            db.Dispose();
            return RedirectToAction("DetailGatheringEventChain");


        }

        //
        // POST: /Customer/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCustomer(Customer customer)
        {
            PortugalVillasContext db = new PortugalVillasContext();
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customer);
        }







        ///////////////////////////////////////////
        //BOOKINGS
        /// <summary>
        /// Creates a new booking at checkout time
        /// </summary>
        /// <returns></returns>
        public ActionResult CreateBooking()
        {
            Booking currentBookingInSession = (Booking)Session["CurrentBookingDataGathering"];
            return View(currentBookingInSession);
        }

        [HttpPost]
        public Booking CreateBooking(Booking booking, Property property, Customer theCustomer, PortugalVillasContext db)
        {

            //set default currency

            booking.BookingCurrencyConversionSymbol = theCustomer.PreferredCurrencySymbol;
            booking.BookingPreferredCurrency = theCustomer.PreferredCurrency;

            var cc = new CurrencyConverterController();



            //if the booking is not in GBP convert to EUROS



            //NEED TO CONVERT THE CURRENCY BASED ON WHETHER IT NEEDS TO BE EU OR UK





            int adults, kids, infants;

            adults = booking.NumberOfAdults ?? 0;
            kids = booking.NumberOfChildren ?? 0;
            infants = booking.NumberOfInfants ?? 0;


            booking.NumberOfAdults = adults;
            booking.NumberOfChildren = kids;
            booking.NumberOfInfants = infants;



            booking.CustomerID = theCustomer.CustomerID;
            booking.BookingTypeID = 1; //always a property booking

            booking.NumberOfGuests = adults + kids + infants;
            booking.TotalNumberOfMinors = kids + infants;
            booking.NumberOfNights = GeneralStaticHelperMethods.CalculateNoofNights(booking.StartDate, booking.EndDate);


            try
            {
                var exchangeRateFromDB = new CurrencyExchange();
                var baseCurrency = "";
                var newCurrency = "";

                booking.NumberOfNights = GeneralStaticHelperMethods.CalculateNoofNights(booking.StartDate,
                    booking.EndDate);
                booking.CalculateBookingPricingForAPropertyBooking(db);
                booking.CalculateExtrasPriceForAPropertyBooking(property, booking, db);
                //set this now because need to convert it
                booking.SetBreakageDepositDueDate(); //1 month before
                booking.SetBreakageDepositAmount(); //depends on property

                booking.BookingCurrencyLongName = "G.B. Pounds Sterling";
                //NOW CONVERT CURRENCY IF NECESSARY SO OTHER CALCS ARE CORRECT
                //CHANGE THIS!!! IT'S USING HIDDEN EXTERNAL DEPENDENY
                booking.BookingCurrencyConversionSymbol = "£";
                booking.BookingCurrencyExchangeRate = 1.00M;
                booking.BookingPreferredCurrency = "GBP";


                if (theCustomer.Country.ToLower() != "united kingdom" && ConfigurationManager.AppSettings["defaultCurrency"] == "GBP")
                {

                    //euro strategy
                    baseCurrency = "GBP";
                    newCurrency = "EUR";


                    //set exchange rate
                    booking.BookingCurrencyExchangeRate = exchangeRateFromDB.CurrencyExchangeRate;
                    booking.BookingCurrencyLongName = "Euros";
                    booking.BookingCurrencyConversionSymbol = exchangeRateFromDB.CurrencyExchangeSymbol;
                    booking.BookingCurrencyExchangeRate = exchangeRateFromDB.CurrencyExchangeRate;
                    booking.BookingPreferredCurrency = "EUR";

                    try
                    {
                        booking.BookingPrice = cc.ConvertCurrency(baseCurrency, newCurrency, (decimal)booking.BookingPrice);
                        booking.BookingCurrencyConversionPrice = booking.BookingPrice;

                        booking.TowelsPrice = cc.ConvertCurrency(baseCurrency, newCurrency, (decimal)booking.TowelsPrice);
                        booking.MidVactionCleaningPrice = cc.ConvertCurrency(baseCurrency, newCurrency,
                            (decimal)booking.MidVactionCleaningPrice);
                        booking.SwimmingPoolHeatingPrice = cc.ConvertCurrency(baseCurrency, newCurrency,
                            (decimal)booking.SwimmingPoolHeatingPrice);
                        booking.ExtraLininSetPrice = cc.ConvertCurrency(baseCurrency, newCurrency,
                            (decimal)booking.ExtraLininSetPrice);
                        booking.BreakageDeposit = cc.ConvertCurrency(baseCurrency, newCurrency,
                            (decimal)booking.BreakageDeposit);
                        booking.CleaningPostVisitPrice = cc.ConvertCurrency(baseCurrency, newCurrency,
                            (decimal)booking.CleaningPostVisitPrice);
                        booking.HeatingPrice = cc.ConvertCurrency(baseCurrency, newCurrency,
                            (decimal)booking.HeatingPrice);





                    }
                    catch (Exception)
                    {

                        throw;
                    }

                }

                //ALL BOOKINGS FOR US SYSTEM IN USD
                else if (ConfigurationManager.AppSettings["defaultCurrency"] == "USD")
                {
                    baseCurrency = "GBP";
                    newCurrency = "USD";

                    exchangeRateFromDB =
                        db.CurrencyExchanges.First(x => x.CurrencyExchangeName == "GBP-USD");

                    //set exchange rate and currencies
                    booking.BookingCurrencyExchangeRate = exchangeRateFromDB.CurrencyExchangeRate;
                    booking.BookingCurrencyLongName = "U.S. Dollars";
                    booking.BookingCurrencyConversionSymbol = exchangeRateFromDB.CurrencyExchangeSymbol;
                    booking.BookingCurrencyExchangeRate = exchangeRateFromDB.CurrencyExchangeRate;
                    booking.BookingPreferredCurrency = "USD";




                }

                //do all cuurency conversions if not a UK booking
                if (theCustomer.Country.ToLower() != "united kingdom" || !ConfigurationManager.AppSettings["defaultCurrency"].Equals("GBP"))
                {
                    try
                    {
                        booking.BookingPrice = cc.ConvertCurrency(baseCurrency, newCurrency,
                            (decimal)booking.BookingPrice);
                        booking.BookingCurrencyConversionPrice = booking.BookingPrice;

                        booking.TowelsPrice = cc.ConvertCurrency(baseCurrency, newCurrency,
                            (decimal)booking.TowelsPrice);
                        booking.MidVactionCleaningPrice = cc.ConvertCurrency(baseCurrency, newCurrency,
                            (decimal)booking.MidVactionCleaningPrice);
                        booking.SwimmingPoolHeatingPrice = cc.ConvertCurrency(baseCurrency, newCurrency,
                            (decimal)booking.SwimmingPoolHeatingPrice);
                        booking.ExtraLininSetPrice = cc.ConvertCurrency(baseCurrency, newCurrency,
                            (decimal)booking.ExtraLininSetPrice);
                        booking.BreakageDeposit = cc.ConvertCurrency(baseCurrency, newCurrency,
                            (decimal)booking.BreakageDeposit);
                        booking.CleaningPostVisitPrice = cc.ConvertCurrency(baseCurrency, newCurrency,
                            (decimal)booking.CleaningPostVisitPrice);
                        booking.HeatingPrice = cc.ConvertCurrency(baseCurrency, newCurrency,
                            (decimal)booking.HeatingPrice);


                        booking.HeatingUnitPrice = cc.ConvertCurrency(baseCurrency, newCurrency,
                            (decimal)booking.HeatingUnitPrice);
                        booking.CleaningPostVisitUnitPrice = cc.ConvertCurrency(baseCurrency, newCurrency,
                            (decimal)booking.CleaningPostVisitUnitPrice);
                        booking.ExtraLininSetUnitPrice = cc.ConvertCurrency(baseCurrency, newCurrency,
                            (decimal)booking.ExtraLininSetUnitPrice);
                        booking.MidVactionCleaningUnitPrice = cc.ConvertCurrency(baseCurrency, newCurrency,
                            (decimal)booking.MidVactionCleaningUnitPrice);
                        booking.SwimmingPoolHeatingUnitPrice = cc.ConvertCurrency(baseCurrency, newCurrency,
                            (decimal)booking.SwimmingPoolHeatingUnitPrice);
                        booking.TowelsUnitPrice = cc.ConvertCurrency(baseCurrency, newCurrency,
                            (decimal)booking.TowelsUnitPrice);
                        booking.FirewoodUnitPrice = cc.ConvertCurrency(baseCurrency, newCurrency,
                            (decimal)booking.FirewoodUnitPrice);

                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }

            }
            catch (Exception ex)
            {
                /*Response.Redirect("http://" + Request.Url.Authority + "/Error/PropertyErrorSelection");*/
                //return RedirectToAction("PropertyErrorSelection", "Error", new { propID = CartBooking.PropertyID });                    
            }


            //CURRENCY MUST BE CONVERTED BEFORE THESE METHODS ARE CALLED

            //call meths to set dates
            booking.SetInitalDepositDate();
            booking.SetInitialDepositAmount();

            booking.SetRentalBalanceDueDate(); //1 month before
            booking.CalculateFinalRentalPaymentAmount(); //extrasSummedPrice + price - deposit


            booking.SetBreakageDepositRemittanceDate();
            booking.SetBreakageDepositRemittanceAmount();//1 month after trip end?
            //booking.SetFinalRentalPayment(); //price - deposit

            booking.SetHomeownerAndPRCComissionAmount(db);

            booking.CreationDate = DateTime.Now;


            booking.Cancelled = false;
            booking.Confirmed = false; //if they pay by paypal later, we can update;




            var refGenService = new ReferenceGenerationService();
            booking.BookingPRCReference = refGenService.GenerateBookingReference(booking, property);


            //if (ModelState.IsValid)
            //{

            db.Bookings.Add(booking);

            if (db.SaveChanges() > 0)
            {

                if (booking.BookingID > 0)
                {
                    booking.BookingPRCReference = refGenService.GenerateBookingReference(booking, property);

                    db.Entry(booking).State = EntityState.Modified;
                    db.SaveChanges();

                }





            }
            return booking;
        }

        //}






        /// <summary>
        /// Booking Extra Selection
        /// </summary>
        /// <param name="disposing"></param>

        /*  public ActionResult CreateBookingExtraSelection()
          {
              //logic to determine what we do

              Customer theCustomer = (Customer)Session["prc_customer"];
              BookingExtraSelection currentBooking = (BookingExtraSelection)Session["CurrentBookingExtraDataGathering"];
              long? currentBookingTypeID = currentBooking.GetBookingExtraTypeIDFromBookingExtraSelection();

              try
              {



                  currentBooking.Cancelled = false;
                  currentBooking.Confirmed = false;

                  currentBooking.Customer = theCustomer;
                  currentBooking.WhenCreated = DateTime.Now;

                  if (currentBookingTypeID == 1)
                  //car rental - only need driver details
                  {

                      return CreateBookingExtraSelection(currentBooking);
                  }



              }
              catch (Exception)
              {

                  throw;
              }


              return View(currentBooking);
          }
  */


        public BookingParentContainer CreateBookingParentContainer(BookingParentContainer container,
            PortugalVillasContext db)
        {

            db.BookingParentContainers.Add(container);

            db.SaveChanges();

            return container;



        }

        [HttpPost]
        public BookingExtraSelection CreateBookingExtraSelection(BookingExtraSelection bookingExtraSelection, BookingExtra extra, Customer theCustomer, PortugalVillasContext db)
        {
            bookingExtraSelection.BESCurrencyConversionSymbol = theCustomer.PreferredCurrencySymbol;
            bookingExtraSelection.BESPreferredCurrency = theCustomer.PreferredCurrency;

            var cc = new CurrencyConverterController();


            long? currentBookingTypeID = bookingExtraSelection.GetBookingExtraTypeIDFromBookingExtraSelection();
            bookingExtraSelection.CustomerID = theCustomer.CustomerID;

            //the price already needs to be assigned
            bookingExtraSelection.BESPrice = BookingExtraSelection.GetBookingExtraPrice(bookingExtraSelection, db);
            bookingExtraSelection.BESExtraServicesPrice = BookingExtraSelection.CalculateBookingExtraAdditionalCostsAndAssignToThisBooking(bookingExtraSelection, db);

            bookingExtraSelection.BESTotalServicesPrice = BookingExtraSelection.GetBookingExtraTotalServicesPrice(bookingExtraSelection, db);


            bookingExtraSelection.WhenCreated = DateTime.Now;

            //calc number of guests
            if (bookingExtraSelection.NumberOfGuests == null || bookingExtraSelection.NumberOfGuests == 0)
            {
                bookingExtraSelection.CalculateNoOfGuests();
            }

            //if not UK need to do currency conversion
            if (theCustomer.Country.ToLower() != "united kingdom" && ConfigurationManager.AppSettings["defaultCurrency"] == "GBP")
            {
                var baseCurrency = "GBP";
                var newCurrency = "EUR";

                var exchangeRateFromDB =
                    db.CurrencyExchanges.First(x => x.CurrencyExchangeName == "GBP-EUR");

                try
                {
                    bookingExtraSelection.BESPrice = cc.ConvertCurrency(baseCurrency, newCurrency, (decimal)bookingExtraSelection.BESPrice);
                    bookingExtraSelection.BESCurrencyConversionPrice = bookingExtraSelection.BESPrice;
                    bookingExtraSelection.BESExtraServicesPrice = cc.ConvertCurrency(baseCurrency, newCurrency, (decimal)bookingExtraSelection.BESExtraServicesPrice);
                    bookingExtraSelection.BESTotalServicesPrice = cc.ConvertCurrency(baseCurrency, newCurrency, (decimal)bookingExtraSelection.BESTotalServicesPrice);


                    //set exchange rate
                    bookingExtraSelection.BESCurrencyConversionSymbol = exchangeRateFromDB.CurrencyExchangeSymbol;
                    bookingExtraSelection.BESCurrencyExchangeRate = exchangeRateFromDB.CurrencyExchangeRate;
                    bookingExtraSelection.BESPreferredCurrency = "EUR";
                }
                catch (Exception)
                {

                    throw;
                }

            }

            else if (ConfigurationManager.AppSettings["defaultCurrency"] == "USD")
            {
                var baseCurrency = "GBP";
                var newCurrency = "USD";

                var exchangeRateFromDB =
                    db.CurrencyExchanges.First(x => x.CurrencyExchangeName == "GBP-USD");

                try
                {
                    bookingExtraSelection.BESPrice = cc.ConvertCurrency(baseCurrency, newCurrency, (decimal)bookingExtraSelection.BESPrice);
                    bookingExtraSelection.BESCurrencyConversionPrice = bookingExtraSelection.BESPrice;
                    bookingExtraSelection.BESExtraServicesPrice = cc.ConvertCurrency(baseCurrency, newCurrency, (decimal)bookingExtraSelection.BESExtraServicesPrice);
                    bookingExtraSelection.BESTotalServicesPrice = cc.ConvertCurrency(baseCurrency, newCurrency, (decimal)bookingExtraSelection.BESTotalServicesPrice);


                    //set exchange rate
                    bookingExtraSelection.BESCurrencyConversionSymbol = exchangeRateFromDB.CurrencyExchangeSymbol;
                    bookingExtraSelection.BESCurrencyExchangeRate = exchangeRateFromDB.CurrencyExchangeRate;
                    bookingExtraSelection.BESPreferredCurrency = "USD";
                }
                catch (Exception)
                {

                    throw;
                }
            }

            //generate reference
            var refGenService = new ReferenceGenerationService();
            bookingExtraSelection.BookingExtraPRCReference = refGenService.GenerateBESReference(bookingExtraSelection, extra);


            if (ModelState.IsValid)
            {
                db.BookingExtraSelections.Add(bookingExtraSelection);
                if (db.SaveChanges() > 0)
                {
                    //generate reference with ID
                    bookingExtraSelection.BookingExtraPRCReference = refGenService.GenerateBESReference(bookingExtraSelection, extra);

                    db.Entry(bookingExtraSelection).State = EntityState.Modified;
                    db.SaveChanges();
                    return bookingExtraSelection;

                }


            }
            throw new Exception();

        }



        /// <summary>
        /// Payment Details
        /// </summary>
        /// <param name="disposing"></param>


        public ActionResult CreateCustomerBankDetail()
        {
            return View(new CustomerBankDetail());
        }

        [HttpPost]
        public CustomerBankDetail CreateCustomerBankDetail(CustomerBankDetail customerBankDetail, Customer customer, PortugalVillasContext db)
        {
            customer.CustomerID = customer.CustomerID;

            db.CustomerBankDetails.Add(customerBankDetail);
            //whack it in the session
            if (db.SaveChanges() > 0)
            {
                //customer now has an ID if the save has worked

            }



            return customerBankDetail;
        }




        /// <summary>
        /// Booking Extra Particpant
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BookingExtraParticipant> CreateBookingExtraParticipants(BookingExtraSelection theBookingExtraSelection, IEnumerable<BookingExtraParticipant> participants)
        {


            return participants;
        }


        [HttpPost]
        public ActionResult CreateBookingExtraParticipant(BookingExtraParticipant bookingExtraParticipant, string submitBookingExtraPart)
        {
            PortugalVillasContext db = new PortugalVillasContext();

            if (ModelState.IsValid)
            {

                GeneralStaticHelperMethods.IsPersonAdultChildOrInfant(bookingExtraParticipant);
                //need to assign the booking ID
                BookingExtraSelection currentSelection = (BookingExtraSelection)Session["CurrentBookingExtraDataGathering"];
                bookingExtraParticipant.BookingExtraSelectionID = currentSelection.BookingExtraSelectionID;

                db.BookingExtraParticipants.Add(bookingExtraParticipant);
                db.SaveChanges();

                //check they're under 18 or whatever then make them an infant, or child if they're the corresponsing age
                //is infant
                //is child

                if (submitBookingExtraPart.Equals("Add this participant and add another"))
                {
                    return RedirectToAction("CreateBookingExtraParticipant");
                }

            }

            return RedirectToAction("DetailGatheringEventChain");

        }




        /// <summary>
        /// Booking Particpant
        /// </summary>
        /// <returns></returns>



        public ActionResult CreateBookingParticipant()
        {

            return View();
        }





        [HttpPost]
        public List<BookingParticipant> CreateBookingParticipant(List<BookingParticipant> bookingParticipants, Booking booking, PortugalVillasContext db)
        {
            //assign fields

            foreach (var bookingParticipant in bookingParticipants)
            {

                bookingParticipant.BookingID = booking.BookingID;
                bookingParticipant.BookingParticipantWhenCreated = DateTime.Now;

                //GeneralStaticHelperMethods.IsPersonAdultChildOrInfant(bookingParticipant);

                db.BookingParticipants.Add(bookingParticipant);


            }
            db.SaveChanges();

            return bookingParticipants;

        }




        public bool RemoveBookingExtraSelectionFromListInSession(BookingExtraSelection bookingExtraSelection)
        {

            try
            {


                //if the update's worked, delete the booking from the session, and update the currentBooking

                int? indexerToRemoveListOn = null;
                List<BookingExtraSelection> theExtraBookings = (List<BookingExtraSelection>)Session["Cart_ExtraBookings"];
                int? removeAt = null;

                foreach (var bookingToDelete in theExtraBookings)
                {
                    //if it all matchies, remove that booking
                    if (bookingExtraSelection.BookingExtraID == bookingToDelete.BookingExtraID && bookingExtraSelection.ExtraRentalDate == bookingToDelete.ExtraRentalDate)

                        if (
                            (bookingExtraSelection.ExtraReturnDate != null && bookingExtraSelection.BookingExtraID == bookingToDelete.BookingExtraID)
                            || bookingExtraSelection.ExtraReturnDate == null

                            )

                            indexerToRemoveListOn = theExtraBookings.IndexOf(bookingToDelete);

                    if (indexerToRemoveListOn != null)
                    {
                        removeAt = indexerToRemoveListOn;

                    }


                }

                theExtraBookings.RemoveAt((int)removeAt);
                Session["Cart_ExtraBookings"] = theExtraBookings;

                return true;

            }
            catch (Exception)
            {

                throw;
            }




        }




    }
}