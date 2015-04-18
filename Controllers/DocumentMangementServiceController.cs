using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BootstrapVillas.Content.Classes;
using BootstrapVillas.Models;

namespace BootstrapVillas.Controllers
{
    public class DocumentMangementServiceController : Controller
    {
        //
        // GET: /DocumentMangementService/

    

        public ActionResult CreateDocumentAndSaveToLocation()
        {
            var db = new PortugalVillasContext();
            var parentContainer = new BookingParentContainer();

            //dependenceies
            var dc = new DocumentGenerationController();
            var customer = db.Customers.Find(1);
            var booking = db.Bookings.Find(4);
            var type = PRCDocument.PRCDocumentType.UK_WineTasting;

            //create a document with all parsed variables
            var document = dc.CreateDocumentToFileSystem(customer, type, booking);

      /*      db.Documents.Add(new Document
            {
                CustomerID = customer.CustomerID,
                DocumentBLOB = document,
                EventID = 2
                
            });

            db.SaveChanges();*/

            //save it to the DB or the FileSystem
            return RedirectToAction("Dashboard", "Admin");
        }




    }
}
