using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Dynamic;
using System.Web;
using System.Web.Mvc;
using Aspose.Email.Exchange.Schema;
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
using BootstrapVillas.Models.Mapping;
using System.Configuration;
using Microsoft.Ajax.Utilities;
using PagedList;
using WebGrease.Css.Extensions;
using BootstrapVillas.Content.Classes.CustomSorting;
using BootstrapVillas.Content.CustomAttribute;
using System.Reflection;
using System.Diagnostics;


namespace BootstrapVillas.Controllers
{
    public class HomeController : Controller
    {
        //list of items to be searched on 
        List<Property> thePropertyList = new List<Property>();
        PortugalVillasContext _db = new PortugalVillasContext();

        public ActionResult CreateSiteMap()
        {
            List<string> allPageUrls = new List<string>();

            // Find all the MVC Routes
            Debug.Print("*** FINDING ALL MVC ROUTES MARKED FOR INCLUSION IN SITEMAP");
            var allControllers = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.IsSubclassOf(typeof(Controller)));
            Debug.Print("Found" + allControllers.Count().ToString() + "controllers");

            foreach (var controllerType in allControllers)
            {
                var allPublicMethodsOnController = controllerType.GetMethods(BindingFlags.Public | BindingFlags.Instance);
                Debug.Print("Found" + allPublicMethodsOnController.Count() + "public methods on" + controllerType.Name + " controllerType.Name");

                foreach (var publicMethod in allPublicMethodsOnController)
                {
                    var mvcurlattr = publicMethod.GetCustomAttributes(true).OfType<MVCUrlAttribute>().FirstOrDefault();
                    if (mvcurlattr != null)
                    {
                        string url = mvcurlattr.Url;
                        Debug.Print("Found " + controllerType.Name + "." + publicMethod.Name + " <– " + url);
                        allPageUrls.Add(url);
                    }
                }
            }

            return View(allPageUrls);

        }


        //ON LOAD OF SITE
        public HomeController()
        {

            //pull properties into list
            var theProperties = (from props in _db.Properties
                                 select props
                                );

            foreach (var theProperty in theProperties)
            {
                Property aProperty = new Property();
                aProperty = theProperty;
                thePropertyList.Add(aProperty);
            }
            ViewBag.Properties = thePropertyList;




        }


        public ActionResult Robots()
        {
            Response.ContentType = "text/plain";
            return View();
        }


        /*Custom partial views*/
        public ActionResult ForgotUserPassModal()
        {
            return View();
        }

        public ActionResult ReplyMessage()
        {
            return View();
        }


        public ActionResult SignUpRegistration()
        {
            ViewBag.Title = "Sign up to Portugal Holiday Rentals";
            return View();
        }

        public ActionResult EditAccountInfo()
        {

            return View();
        }

        public ActionResult StepWizard()
        {

            return View();
        }

        [HttpPost]
        public bool StorePagerCookie(string value)
        {
            try
            {
                Session["noOfPropertiesToPageInSearchResults"] = value;
                return true;
            }
            catch (Exception exception)
            {

                throw;
            }

        }


        public ActionResult Index()
        {
            //context class

            DirectoryInfo ServerPath = new DirectoryInfo(Server.MapPath(@"~\"));
            ViewBag.theServerPath = ServerPath;

            ViewBag.IdentifyPage = "Home Page";
            ViewBag.Title = "Search for villas townhouses, apartments in rural, seaside or city break settings. Book holidays in portugal's silver coast, in Foz do Arelho, Salir do Porto, Olho Marinho or Formigal. Book car rental, wine tours, site seeing tours with Portugal Rental Cottages. Rent Hyundai I20, Mitsubishi Lancer, Mitsubishi Grandis, go siteseeing with Lisbon City Tours or Sintra Palace Tours, get 9 Seater Van transport from the airport and have full Cleaning Service, Breakfast and Swimming Towels provided! Visit EXPO 98 & Ocenarium on your Portugal Holiday Rental!";
            return View();
        }


        //////////////////////////////////////
        //Begin main search overloaded views
        ////////////////////////////////////
        ////////////////////////////////////

        [MVCUrlAttribute("SearchProperties")]
        //nothing passed - run a search with default values (will bring back all properties) - no PriceRange object
        public ActionResult SearchProperties(string PropertyType = "", string VacationType = "", int page = 1, string propertyResultsSort = "", int propertyResultsAmount = 25)
        {
            ViewBag.IdentifyPage = "Property Search";

            Session["CurrentSearchResultsAmount"] = propertyResultsAmount;
            Session["CurrentSearchResultsSort"] = propertyResultsSort;

            ViewBag.propertyResultsSort = propertyResultsSort;
            ViewBag.propertyResultsAmount = propertyResultsAmount;


            PropertySearch newSearch = new PropertySearch()
            {
                MaxSleeps = 0,
                NoOfAdults = 0,
                NoOfChildren = 0,
                NoOfInfants = 0,
                PoolType = null,
                PropertyTypeID = 0,
                VacationTypeID = 0
            };

            if (VacationType != "")
            {
                newSearch.VacationTypeID = Convert.ToInt16(VacationType);
            }
            if (PropertyType != "")
            {
                newSearch.PropertyTypeID = Convert.ToInt16(PropertyType);
            }

            //run the search - then need to display it in the view
            var MatchingProperties = newSearch.SearchForMatchingProperties();

            //before limiting results
            Session["LastSearchResults"] = MatchingProperties;

            //paging here - then pass to viewbag                                       
            Session["LastPropertyPagerPage"] = page;

            //after sort
            Session["currentPagedPropertySearch"] = MatchingProperties;

            ViewBag.MatchingProperties = PagedList.PagedListExtensions.ToPagedList(MatchingProperties, page, propertyResultsAmount);
            Session["currentPagedPropertySearch"] = PagedList.PagedListExtensions.ToPagedList(MatchingProperties, page, 50000000);

            ViewBag.Title = "Book your villa in Obidos, Nazare, Foz do Arelho or A-dos-Negros for a seaside holiday or rural townhouse with swimming pool and Internet. All types of villa, apartment are catered for in regions like Foz do Arelho, Obidos, Alfeizerao - Sao Martinho and Reguengo Grande. Stay in the lovely Salir do Porto and visit the Mafra Palace & Obidos during your vacation. With Portugal Rental Cottages, you can be up directly from the airport with our rental extras. While you stay, go on wine tasting tours to the beautiful Sanguinhal Estate and get picked up from the airport with our airport to villa transfer and return.";
            ViewBag.Keywords = "villa rental portugal, portugal silver coast, portugal cheap holidays, cheap holidays to portugal,silver coast portugal, portugal rental cottages, cheap all inclusive holidays to portugal";
            return View();
        }


        public ActionResult SortProperties(string propertyResultsSort = "", int propertyResultsAmount = 25)
        {
            Session["CurrentSearchResultsAmount"] = propertyResultsAmount;
            Session["CurrentSearchResultsSort"] = propertyResultsSort;

            ViewBag.propertyResultsSort = propertyResultsSort;
            ViewBag.propertyResultsAmount = propertyResultsAmount;
            //get paged list out of session

            //  var props = (List<Property>)Session["LastSearchResults"];
            var filteredResults = (List<Property>)Session["LastSearchResults"];
            //run through filtering
            var sorter = new PropertySorter();

            if (propertyResultsSort != "")
            {
                var returnList = sorter.SortProperty(filteredResults, propertyResultsSort);
            }
            //put filtered results back in session

            
            Session["LastSearchResults"] = PagedList.PagedListExtensions.ToPagedList(filteredResults, 1, propertyResultsAmount);



            //which page are we on of the results - reset
            Session["LastPropertyPagerPage"] = 1; //every sort starts on page one, no page


            //pass to view
            ViewBag.MatchingProperties = PagedList.PagedListExtensions.ToPagedList(filteredResults, 1, propertyResultsAmount);

            Session["currentPagedPropertySearch"] = PagedList.PagedListExtensions.ToPagedList(filteredResults, 1, 500000);

            return View("SearchProperties");
        }

        //takes the posted form - if there is variable passed from the paging link we know it's a page request, if not, we know 
        [HttpPost]
        public ActionResult SearchProperties(FormCollection formCollection, int page = 1, string propertyResultsSort = "", int propertyResultsAmount = 25)
        {

            ViewBag.IdentifyPage = "Property Search";

            Session["CurrentSearchResultsAmount"] = propertyResultsAmount;
            Session["CurrentSearchResultsSort"] = propertyResultsSort;

            //stick values into viewback for DDL update
            ViewBag.propertyResultsSort = propertyResultsSort;
            ViewBag.propertyResultsAmount = propertyResultsAmount;

            //retrive values from post
            //based on whether there are dates create either a booking search or property search
            //create new object and populate - then run dynamic instance query on that object

            int propertyTypeID = 0;
            int vacationTypeID = 0;
            string poolType = null;
            int maxSleeps = 0;
            int noOfAdults = 0;
            int noOfChildren = 0;
            int noOfInfants = 0;

            DateTime? startDate = null;
            DateTime? endDate = null; ;

            try
            {
                //parse dates
                if (formCollection["StartDate"] != "" && formCollection["StartDate"] != null)
                {
                    startDate = DateTime.ParseExact(formCollection["StartDate"], "dd/MM/yyyy", CultureInfo.InvariantCulture);
                }
                if (formCollection["EndDate"] != "" && formCollection["EndDate"] != null)
                {
                    endDate = DateTime.ParseExact(formCollection["EndDate"], "dd/MM/yyyy", CultureInfo.InvariantCulture);
                }
                if (formCollection["StartDate1"] != "" && formCollection["StartDate1"] != null)
                {
                    startDate = DateTime.ParseExact(formCollection["StartDate1"], "dd/MM/yyyy", CultureInfo.InvariantCulture);
                }
                if (formCollection["EndDate1"] != "" && formCollection["EndDate1"] != null)
                {
                    endDate = DateTime.ParseExact(formCollection["EndDate1"], "dd/MM/yyyy", CultureInfo.InvariantCulture);
                }

                //parse property Type ID
                if (Int32.Parse(formCollection["PropertyType"]) != 0)
                {
                    propertyTypeID = Int32.Parse(formCollection["PropertyType"]);
                }
                //parse region type
                if (Int32.Parse(formCollection["RegionType"]) != 0)
                {
                    vacationTypeID = Int32.Parse(formCollection["RegionType"]);
                }
                //parse poolType
                if (formCollection["PoolType"] != "" && formCollection["PoolType"] != null && poolType != "0")
                {
                    poolType = formCollection["PoolType"];
                }
                //parse max sleeps
                if (Int32.Parse(formCollection["MaxSleeps"]) != 0)
                {
                    maxSleeps = Int32.Parse(formCollection["MaxSleeps"]);
                }
                //parse no of adults
                if (Int32.Parse(formCollection["NoOfAdults"]) != 0)
                {
                    maxSleeps = Int32.Parse(formCollection["NoOfAdults"]);
                }
                //parse no of children
                if (Int32.Parse(formCollection["NoOfChildren"]) != 0)
                {
                    maxSleeps = Int32.Parse(formCollection["NoOfChildren"]);
                }
                //parse no of infants
                if (Int32.Parse(formCollection["NoOfInfants"]) != 0)
                {
                    maxSleeps = Int32.Parse(formCollection["NoOfInfants"]);
                }
                //parse price range 

                //jsut to account for if we haven't be passed a price
                String[] splitPrice = "1-5000".Split('-');

                if (formCollection["PriceRange"] != "" && formCollection["PriceRange"] != null)
                {
                    splitPrice = null;
                    splitPrice = formCollection["PriceRange"].Split('-');
                }

                LinkedList<decimal> thePrices = new LinkedList<decimal>();



                foreach (var thePrice in splitPrice)
                {
                    int TryParseResult = 0;
                    //try parse to check it's a number
                    Int32.TryParse(thePrice.Trim().Replace("£", "").Replace("$", "").ToString(), out TryParseResult);


                    var cash = (decimal)TryParseResult;
                    if (TryParseResult != 0)
                    {
                        //IF SYSTEM IS US DOLLARS, NEED TO CONVERT THE NUMBER INTO POUNDS
                        if ((string)ConfigurationManager.AppSettings["defaultCurrency"] != "GBP")
                        {
                           var cc = new CurrencyConverterController();
                            cash = cc.ConvertCurrency((string)ConfigurationManager.AppSettings["defaultCurrency"], "GBP", (decimal)cash);
                        }
                            thePrices.AddLast(cash);
                    }
                }
                //end parse price range


                PriceRange customerQueryPriceRange = null;

                if (thePrices.Count > 0)
                {
                    customerQueryPriceRange = new PriceRange(thePrices.Min(), thePrices.Max());
                }
                else //pass the max range ever
                {
                    customerQueryPriceRange = new PriceRange(0, 100000);
                }

                //END FORM PARSE

                //Now test if there are dates - if yes - create a booking query. If no, create a property query


                //create property query
                PropertySearch newSearch = new PropertySearch()
                {
                    MaxSleeps = maxSleeps,
                    NoOfAdults = noOfAdults,
                    NoOfChildren = noOfChildren,
                    NoOfInfants = noOfInfants,
                    PoolType = poolType,
                    PropertyTypeID = propertyTypeID,
                    TheSearchPriceRange = customerQueryPriceRange,
                    VacationTypeID = vacationTypeID
                };

                //do the search

                var searchResults = newSearch.SearchForMatchingProperties();
                //if there are dates, pass the search results into the BookingSearch

                if (startDate.HasValue && endDate.HasValue)
                {
                    //create booking query                                                                        
                    searchResults = SearchBookingResultsAndReturnPropetiesWithoutAnOverlappingBooking(searchResults, (DateTime)startDate, (DateTime)endDate);


                }



                //for each sort / pager
                Session["LastSearchResults"] = PagedList.PagedListExtensions.ToPagedList(searchResults, page, searchResults.Count);
                //for fresh canvas for 'amount' sort
                Session["InitalCustomerSearchResults"] = PagedList.PagedListExtensions.ToPagedList(searchResults, page, searchResults.Count);
                //which page are we on of the results
                Session["LastPropertyPagerPage"] = page;


                ViewBag.MatchingProperties = PagedList.PagedListExtensions.ToPagedList(searchResults, page, propertyResultsAmount);

                Session["currentPagedPropertySearch"] = PagedList.PagedListExtensions.ToPagedList(searchResults, page, 500000);


                //store the last results set in the viewbag, so we can page it if necessary






            }

            catch (Exception ex)
            {
                Response.Write("Something is wrong in the SearchProperties in the Home Controller when trying to assing the variables " +
                             "from the POST from the search form");
            }








            //return results
            //create a new enquiry
            ViewBag.Keywords = "villa rental portugal, portugal silver coast, portugal cheap holidays, cheap holidays to portugal,silver coast portugal, portugal rental cottages, cheap all inclusive holidays to portugal";
            ViewBag.Title = "Book your villa in Obidos, Nazare, Foz do Arelho or A-dos-Negros for a seaside holiday or rural townhouse with swimming pool and Internet. All types of villa, apartment are catered for in regions like Foz do Arelho, Obidos, Alfeizerao - Sao Martinho and Reguengo Grande. Stay in the lovely Salir do Porto and visit the Mafra Palace & Obidos during your vacation. With Portugal Rental Cottages, you can be up directly from the airport with our rental extras. While you stay, go on wine tasting tours to the beautiful Sanguinhal Estate and get picked up from the airport with our airport to villa transfer and return.";
            return View();
        }

        //end main search 


        /// <summary>
        /// RUNS OFF CURRENT PAGED PROPERTIES SESSION Session["currentPagedPropertySearch"]
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public ActionResult SearchPropertiesPager(int page = 1)
        {
            /*//for other functions
            Session["LastPropertyPagerPage"] = page;

            //where is THIS now stored
            int pagerNo = 25;
            //test is there is anything in the session to say how many props to return - if not, use 25
            if (Session["noOfPropertiesToPageInSearchResults"] != null)
            {
                pagerNo = (int)Session["noOfPropertiesToPageInSearchResults"];
            }

            var thePreviousResults = (List<Property>)Session["currentPagedPropertySearch"];*/

            var propsPagedList = (PagedList<Property>)Session["currentPagedPropertySearch"];

            var test = propsPagedList.PageNumber;

            var test2 = propsPagedList[0];

            var test3 = propsPagedList.LastItemOnPage;

            var test4 = propsPagedList.PageCount;

            var props = propsPagedList.ToList();


            //ddls for view to remember state
            var CurrentSearchResultsAmount = (int)Session["CurrentSearchResultsAmount"];
            var CurrentSearchResultsSort = (string)Session["CurrentSearchResultsSort"];

            ViewBag.propertyResultsAmount = CurrentSearchResultsAmount;
            ViewBag.propertyResultsSort = CurrentSearchResultsSort;


            ViewBag.MatchingProperties = PagedList.PagedListExtensions.ToPagedList(props, page, CurrentSearchResultsAmount);


            ViewBag.Title = "Book your villa in Obidos, Nazare, Foz do Arelho or A-dos-Negros for a seaside holiday or rural townhouse with swimming pool and Internet. All types of villa, apartment are catered for in regions like Foz do Arelho, Obidos, Alfeizerao - Sao Martinho and Reguengo Grande. Stay in the lovely Salir do Porto and visit the Mafra Palace & Obidos during your vacation. With Portugal Rental Cottages, you can be up directly from the airport with our rental extras. While you stay, go on wine tasting tours to the beautiful Sanguinhal Estate and get picked up from the airport with our airport to villa transfer and return.";
            ViewBag.Keywords = "villa rental portugal, portugal silver coast, portugal cheap holidays, cheap holidays to portugal,silver coast portugal, portugal rental cottages, cheap all inclusive holidays to portugal";
            return View("SearchProperties");
        }




        public List<Property> SearchBookingResultsAndReturnPropetiesWithoutAnOverlappingBooking(List<Property> PropertyList, DateTime startDate, DateTime endDate)
        {
            List<Property> duplicatePassedInListOfProperties = PropertyList.ToList();

            foreach (var property in PropertyList)
            {
                try
                {
                    //if even one booking is returned, we know there are overlapping dates, so remove that property
                    if (Booking.CheckConfirmedBookingDatesOverlap(property, startDate, endDate).Count > 0)
                    {
                        duplicatePassedInListOfProperties.Remove(property);
                    }

                }
                catch (Exception ex)
                {

                    //do nothing, we want this to return even on error
                }

            }

            return duplicatePassedInListOfProperties;
        }

        [HttpGet]
        public ActionResult FullPropertyResult(string propertyID)
        {

            #region FullPropertyResult_CatchNoValue

            int aTest;
            if (propertyID == null || propertyID == "" || !(Int32.TryParse(propertyID, out aTest)))
            {
                return Redirect("~/Home/SearchProperties");
            }

            #endregion

            Property theProperty = Property.GetPropertyByID(Int32.Parse(propertyID));
            ViewBag.FullPropertyResultProperty = theProperty;

            PRCImageCollection ImageCollection = new PRCImageCollection(Server, theProperty.LegacyReference);
            ViewBag.ImageCollection = ImageCollection;

            //booking calendar population
            BookingCalendar TestBookingCalendar = new BookingCalendar(Int32.Parse(propertyID));
            TestBookingCalendar.GetAllBookingDatesAndAddToThisCalendar();
            ViewBag.BookingCalDates = TestBookingCalendar;

            //Comments
            List<BootstrapVillas.Models.Comment> theComments = BootstrapVillas.Models.Comment.GetComments(theProperty);

            //Comment replies
            ViewBag.Keywords = "portugal rental cottages, silver coast portugal, cheap holidays in portugal, cheap all inclusive holidays to portugal,holiday villa portugal";
            ViewBag.IdentifyPage = "FullPropertyResult";
            ViewBag.Title = "Here is the information, photos, facilities and features for this Villa, Townhouse, Apartment or Hotel holiday rentals in Portugal. A selective but extensive range of quality holiday  properties to choose from, where you can visit Candieeros National Park and take in the sights of Tomar and Fatima on one of our sightseeing tours or wine tours should you wish. Holiday Villas in Olho Marinho, Reguengo Grande, Salir de Matos and Avarela all with luxury extras like Swimming Pools, Pool Heating, Mid Vacation Cleaning and Internet use (please check property). Visit the Loridos Vineyards and stay in one of our  classic Portugal Rental Cottages!";
            return View();
        }

        //takes a comment, attempts to save it to the DB.
        //[HttpPost]
        //public bool ParseComment(FormCollection formCollection)
        //{
        //    if(formCollection.Count > 0) //there's something in the form 
        //    {
        //        string theCommentString = formCollection["Comment"].ToString();
        //        if(theCommentString != "") //comment is not blank
        //          {

        //            try
        //            {

        //                if (theCommentString.Contains("\r")) //we need to split into multiple commentSections
        //                    {
        //                        int countCarriageReturns = theCommentString.Count(f => f == '\r');

        //                    }
        //                else //one comment section is fine
        //                    {


        //                    }




        //                long PropertyIDHolder = 0;
        //                Int64.TryParse(formCollection[""], out PropertyIDHolder);

        //                /*
        //                BootstrapVillas.Models.Comment theComment = new Models.Comment             
        //                {


        //                    Approved = false,
        //                    CommentUsername = formCollection[""],
        //                    StartdateOfStay = formCollection[""],
        //                    EnddateOfStay = formCollection[""],
        //                    StarRating = formCollection[""],
        //                    PropertyID = PropertyIDHolder


        //                    //test for how many sections there are by parsing /r /mreturns

        //                };
        //                 */

        //            }

        //        catch(Exception ex)
        //        {


        //        }

        //      }
        //        return false;
        //    }

        //    return true;
        //}


     

        public ActionResult Contact()
        {


            return View();
        }





        public ActionResult TestingPage()
        {

            int customerID = 1;
            int bookingID = 4;

            /*AUTOMAILER CODE*/
            //       MaintainanceMailer letsMail = new MaintainanceMailer();
            //letsMail.SendEmail();

            //Merge with Regions
            DataTable MyTable = new DataTable();
            MyTable.TableName = "MyTable";
            ConnectionStringSettings connection = ConfigurationManager.ConnectionStrings["PortugalVillasContext"];
            using (SqlConnection conn = new SqlConnection(connection.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetDataForBookingExtraParticipantMailMerge", conn))
                {
                    using (SqlDataAdapter da = new SqlDataAdapter("GetDataForBookingExtraParticipantMailMerge", conn))
                    {

                        // cmd.Parameters.AddWithValue("@BookingExtraSelectionID", 3);                
                        //da.SelectCommand = cmd;
                        da.SelectCommand.CommandText = cmd.CommandText;
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;

                        da.SelectCommand.Parameters.AddWithValue("@BookingExtraSelectionID", 3);


                        conn.Open();
                        da.Fill(MyTable);
                    }
                }
            }

            ////Main Merge
            //DataTable MainMergeTable = new DataTable();
            //MainMergeTable.TableName = "MainMergeTable";
            //ConnectionStringSettings connection = ConfigurationManager.ConnectionStrings["PortugalVillasContext"];
            //using (SqlConnection conn = new SqlConnection(connection.ConnectionString))
            //{
            //    using (SqlCommand cmd = new SqlCommand("GetDataForBookingMailMerge", conn))
            //{
            //    using (SqlDataAdapter da = new SqlDataAdapter("GetDataForBookingMailMerge", conn))
            //    {

            //       // cmd.Parameters.AddWithValue("@BookingExtraSelectionID", 3);                
            //        //da.SelectCommand = cmd;
            //        da.SelectCommand.CommandText = cmd.CommandText;
            //        da.SelectCommand.CommandType = CommandType.StoredProcedure;

            //        da.SelectCommand.Parameters.AddWithValue("@CustomerID", 1);                
            //        da.SelectCommand.Parameters.AddWithValue("@BookingID", 4);                

            //    conn.Open();
            //    da.Fill(MyTable);                
            //}
            //}
            //}

            /*MAIL MERGE CODE*/
            PRCDocumentData theDocumentDataInstance = new PRCDocumentData();
            //call the methods to bring back the necessary rows

            DataTable PRCInformation = theDocumentDataInstance.GetPRCDocumentData(PRCDocumentData.PRCReturnDataTableWrapperTypes.StandardPRCInformation, 1); //NOT WORKING
            DataTable Customers = theDocumentDataInstance.GetPRCDocumentData(PRCDocumentData.PRCReturnDataTableWrapperTypes.CustomerByCustomerID, 1); //works
            DataTable Booking = theDocumentDataInstance.GetPRCDocumentData(PRCDocumentData.PRCReturnDataTableWrapperTypes.BookingByBookingID, 4); //works

            DataTable BookingParticipant = theDocumentDataInstance.GetPRCDocumentData(PRCDocumentData.PRCReturnDataTableWrapperTypes.BookingParticipantByBookingID, 4);

            DataTable BookingExtraSelectionByCustomerID = theDocumentDataInstance.GetPRCDocumentData(PRCDocumentData.PRCReturnDataTableWrapperTypes.BookingExtraSelectionByCustomerID, 1);

            DataTable StandardBookingExtraSelection = theDocumentDataInstance.GetPRCDocumentData(PRCDocumentData.PRCReturnDataTableWrapperTypes.BookingExtraSelectionByBookingExtraSelectionID, 3);

            DataTable StandardPropertyQuery = theDocumentDataInstance.GetPRCDocumentData(PRCDocumentData.PRCReturnDataTableWrapperTypes.PropertyByPropertyID, 221);

            DataTable StandardPropertyRegionQuery = theDocumentDataInstance.GetPRCDocumentData(PRCDocumentData.PRCReturnDataTableWrapperTypes.PropertyRegionByPropertyRegionID, 1);

            DataTable StandardBookingExtraQuery = theDocumentDataInstance.GetPRCDocumentData(PRCDocumentData.PRCReturnDataTableWrapperTypes.BookingExtraByBookingExtraID, 1);

            DataTable BookingExtraParticipantByBookingExtraSelectionID = theDocumentDataInstance.GetPRCDocumentData(PRCDocumentData.PRCReturnDataTableWrapperTypes.BookingExtraParticipantByBookingExtraSelectionID, 3);

            DataTable CustomAttributes = new DataTable();
            CustomAttributes.Columns.Add("CurrentDate").DefaultValue = DateTime.Now.ToShortDateString();

            DataRow newDateRow = CustomAttributes.NewRow();
            newDateRow["CurrentDate"] = DateTime.Now.ToShortDateString();
            CustomAttributes.Rows.Add(newDateRow);


            //pass property ID
            DataTable StandardPropertyOwnerQuery = theDocumentDataInstance.GetPRCDocumentData(PRCDocumentData.PRCReturnDataTableWrapperTypes.StandardPropertyOwner, 221);
            //attempt a merge

            string ApplicationPath = HttpContext.Server.MapPath("~/DocumentTemplates/BookingConfirmationVoucherTemplate.docx");

            string SecndApplicationPath = HttpContext.Server.MapPath("~/DocumentTemplates/BCTTemplate.rtf");


            string BookingRequestFormEU = HttpContext.Server.MapPath("~/DocumentTemplates/BOOKING_EU_EFTABookingRequestFormTemplate.doc");
            string BookingRequestFormUK = HttpContext.Server.MapPath("~/DocumentTemplates/UKROWBookingRequestFormTemplate.doc");
            string SecurityDepositTemplate = HttpContext.Server.MapPath("~/DocumentTemplates/SecurityDepositTemplate.docx");
            string RentalRemittanceAdviceTemplate = HttpContext.Server.MapPath("~/DocumentTemplates/RemTemplate.doc");
            string TestTemplate = HttpContext.Server.MapPath("~/DocumentTemplates/TESTMERGEREGIONS.doc");


            string SavePath = HttpContext.Server.MapPath("~/DocumentGenerated/");
            // string FullDocumentPath = ApplicationPath + "~/DocumentTemplates/BookingConfirmationVoucherTemplate.docx";
            Aspose.Words.Document theDoc = new Aspose.Words.Document(TestTemplate);


            //do all the table merges



            // theDoc.MailMerge.Execute(MyMergeTable);


            theDoc.MailMerge.ExecuteWithRegions(MyTable);
            theDoc.Save(@"c:\test\testdoc1.doc");

            /*
            PRCImageCollection ImageCollection = new PRCImageCollection(Server, "PRCV29");

            BookingCalendar TestBookingCalendar = new BookingCalendar(1);
            TestBookingCalendar.GetAllBookingDatesAndAddToThisCalendar();
            ViewBag.BookingCalDates = TestBookingCalendar;


            Session.Add("test", Property.GetPropertyByID(76));
            */

            return View();
        }

        [HttpGet]
        public ActionResult BookCarRental(string ExtraType = "0", string priceSort = "", string noOfResults = "0")
        {
            //save vars so can remember them
            ViewBag.ExtraType = ExtraType;
            ViewBag.priceSort = priceSort;
            ViewBag.noOfResults = noOfResults;


            int theType = Int32.Parse(ExtraType);
            List<BookingExtra> CarBookingExtras = new List<BookingExtra>();

            switch (theType)
            {
                case 0:
                    CarBookingExtras = BookingExtra.GetBookingExtrasOfType();
                    break;
                default:
                    CarBookingExtras = BookingExtra.GetBookingExtrasOfType(theType);
                    break;

            }


            //do additional sorts depending on vars
            //price
            if (priceSort.Equals("LH"))
            {
                CarBookingExtras = CarBookingExtras.OrderBy(x => x.Price).ToList();
            }
            else if (priceSort.Equals("HL"))
            {
                CarBookingExtras = CarBookingExtras.OrderByDescending(x => x.Price).ToList();
            }

            //no of results
            if (noOfResults != "0")
            {
                //pager

            }


            //assign attributes
            foreach (var cbe in CarBookingExtras)
            {
                cbe.GetAllBookingExtraAttributes();
            }

            //get all the images for all the extras
            //List<ExtrasImage> allTheExtraImages = new List<ExtrasImage>();

            ViewBag.CarBookingExtras = CarBookingExtras;


            using (var _db = new PortugalVillasContext())
            {
                var listOfAllTypes = _db.BookingExtraTypes.Where(x => x.BookingExtraTypeID != 11).ToList();
                listOfAllTypes.Add(new BookingExtraType { BookingExtraTypeID = 0, ExtraTypeName = "Show All" });
                listOfAllTypes = listOfAllTypes.OrderBy(x => x.BookingExtraTypeID).ToList();
                //create list and put in viewbag
                ViewBag.ExtraTypes = listOfAllTypes;


            }

            ViewBag.Keywords = "holiday villa portugal, silver coast portugal,portugal rental cottages,silver coast villas,cheap holidays to portugal, tourist attractions in portugal";
            ViewBag.Title = "A quality selection of cars that includes the Hyundai I20,Mitsubishi Colt, Mitsubishi Colt Diesel, Mitsubishi Lancer, Mitsubishi Grandis, you can also book direct airport transfers here to drop you from the airport directly to your villas and add package and sightseeing tours for you and the family, to well loved destinations like Alcobaça, Batalha and Fatima, the Sintra Palace or Obidos and Nazaré. See the EXPO 98 & Ocenarium or stay in Salir do Porto, sunny Formigal or the beautiful A-da-Gorda. With cheap seasonal holiday rentals in Sao Martinho do Porto, Barrantes, Capeleira and Sancheira Pequena you can get quality Portugese accommodation the entire year round.";
            return View(CarBookingExtras);
        }



        public ActionResult BookCarRental()
        {

            //NB - need a method to get all
            //pull back from db the correct extra and pass to viewbag
            List<BookingExtra> CarBookingExtras = BookingExtra.GetBookingExtrasOfType();
            foreach (var cbe in CarBookingExtras)
            {
                cbe.GetAllBookingExtraAttributes();
            }

            //get all the images for all the extras
            List<ExtrasImage> allTheExtraImages = new List<ExtrasImage>();

            ViewBag.CarBookingExtras = CarBookingExtras;

            //list of airports for  transfers

            //drop down lists 
            using (var _db = new PortugalVillasContext())
            {
                var listOfAllTypes = _db.BookingExtraTypes.ToSafeReadOnlyCollection();
                //create list and put in viewbag
                List<SelectListItem> selectListOfAllTypes = new List<SelectListItem>();
                selectListOfAllTypes.Add(new SelectListItem { Text = "Show All", Value = "0" });

                foreach (var type in listOfAllTypes)
                {
                    selectListOfAllTypes.Add(new SelectListItem { Text = type.ExtraTypeName, Value = type.BookingExtraTypeID.ToString() });
                }
                //add a default


                ViewBag.ExtraTypes = selectListOfAllTypes;
            }

            ViewBag.Keywords = "Portugal Villas, portugal rental cottages, cheap holidays to portugal, silver coast villas,cheap portugal holidays,villa hotel rental portugal";
            ViewBag.Title = "A quality selection of cars that includes the Hyundai I20,Mitsubishi Colt, Mitsubishi Colt Diesel, Mitsubishi Lancer, Mitsubishi Grandis, you can also book direct airport transfers here to drop you from the airport directly to your villas and add package and sightseeing tours for you and the family, to well loved destinations like Alcobaça, Batalha and Fatima, the Sintra Palace or Obidos and Nazare. See the EXPO 98 & Ocenarium or stay in Salir do Porto, sunny Formigal or the beautiful A-da-Gorda. With cheap seasonal holiday rentals in Sao Martinho do Porto, Barrantes, Capeleira and Sancheira Pequena you can get quality Portugese accommodation the entire year round.";
            return View();
        }

        public ActionResult BookWineTour()
        {
            ViewBag.WineBookingExtras = BookingExtra.GetBookingExtrasOfType(2);

            return View();
        }

        public ActionResult BookAirTransfer()
        {
            ViewBag.AirTransfer = BookingExtra.GetBookingExtrasOfType(3);

            return View();
        }

        [HttpGet]
        public ActionResult ListExtras(string ExtraType = "1")
        {
            //test
            switch (ExtraType)
            {
                case "1":
                    return View("BookWineTour");
                //break;
                case "2":
                    return View("BookWineTour");
                //break;
                case "3":
                    return View("BookWineTour");
                //break;
                default:
                    return View("BookWineTour");

            }
            //return View("BookWineTour");
        }

        public ActionResult CheckOut()
        {

            /*return  RedirectToAction("GetBookingDetails", "DetailVerify");*/
            ViewBag.IdentifyPage = "checkOut";
            ViewBag.Title = "Here are the portugal holiday rentals you have selected. Please provide as much information as possible to enable us to process your order, so you can enjoy your holiday to the sunny Silver Coast, Foz do Arelho, Formigal, A-da-Gorda, Casais Bica or one of our beach, seaside or city break vacations with Portugal Rental Cottages.";
            return View();
        }

        public ActionResult ReviewBooking()
        {


            return View();
        }

        public ActionResult FinalBookingCheckout()
        {


            return View();
        }


        public ActionResult AboutPortugal()
        {


            return View();
        }

        [HttpPost]
        public void AddPropertyIDToSessionAsCurrentProperty(FormCollection theFormCollection)
        {


            //not performing Ajax
            if (Request.IsAjaxRequest())
            {
                // ajax request handled

                var thisisAjax = "";
            }


            int PropertyID = 0;


            try
            {
                PropertyID = Convert.ToInt32(theFormCollection["PropertyID"]);
                Session["theCurrentProperty"] = Property.GetPropertyByID(PropertyID);
            }
            catch (Exception ex)
            {
                throw;
            }


        }


        public void AddExtraIDToSessionAsCurrentExtra(FormCollection theFormCollection)
        {


            //not performing Ajax
            if (Request.IsAjaxRequest())
            {
                // ajax request handled

                var thisisAjax = "";
            }


            int BookingExtraID = 0;


            try
            {
                BookingExtraID = Convert.ToInt32(theFormCollection["BookingExtraID"]);
                Session["theCurrentExtraID"] = BookingExtraID;
            }
            catch (Exception ex)
            {
                throw;
            }


        }

    }
}
