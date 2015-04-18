using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BootstrapVillas.Models;

namespace BootstrapVillas.Controllers
{
      [Authorize(Roles = "Administrator")]
    public class BookingExtraSelectionController : Controller
    {
        private PortugalVillasContext db = new PortugalVillasContext();

        //
        // GET: /BookingExtraSelection/
        public bool CancelBookingExtraSelection(long besID)
        {
            var bes = db.BookingExtraSelections.Find(besID);
            bes.Cancelled = true;

            if (ModelState.IsValid)
            {
                db.Entry(bes).State = EntityState.Modified;
                db.SaveChanges();
                return true;
            }

            return false;
        }


        public ActionResult Index()
        {
            var bookingextraselections = db.BookingExtraSelections.Include(b => b.AirportDestination).Include(b => b.BookingExtra).Include(b => b.BookingParentContainer).Include(b => b.Customer);
            return View(bookingextraselections.ToList());
        }

        //
        // GET: /BookingExtraSelection/Details/5

        public ActionResult Details(long id = 0)
        {
            BookingExtraSelection bookingextraselection = db.BookingExtraSelections.Find(id);
            if (bookingextraselection == null)
            {
                return HttpNotFound();
            }
            return View(bookingextraselection);
        }

        //
        // GET: /BookingExtraSelection/Create

        public ActionResult Create()
        {

            ViewBag.AirportPickupLocationID = new SelectList(db.AirportDestinations, "AirportPickupLocationID", "AirportPickupLocationName");
            ViewBag.BookingExtraID = new SelectList(db.BookingExtras, "BookingExtraID", "LegacyReference");
            ViewBag.BookingParentContainerID = new SelectList(db.BookingParentContainers, "BookingParentContainerID", "OverallBookingReference");
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "Title");
            return View();
        }

        //
        // POST: /BookingExtraSelection/Create

        [HttpPost]
        public ActionResult Create(BookingExtraSelection bookingextraselection)
        {
            bookingextraselection.WhenCreated = DateTime.Now;

            db.BookingExtraSelections.Add(bookingextraselection);
            db.SaveChanges();
            ViewBag.AirportPickupLocationID = new SelectList(db.AirportDestinations, "AirportPickupLocationID", "AirportPickupLocationName", bookingextraselection.AirportPickupLocationID);
            ViewBag.BookingExtraID = new SelectList(db.BookingExtras, "BookingExtraID", "LegacyReference", bookingextraselection.BookingExtraID);
            ViewBag.BookingParentContainerID = new SelectList(db.BookingParentContainers, "BookingParentContainerID", "OverallBookingReference", bookingextraselection.BookingParentContainerID);
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "Title", bookingextraselection.CustomerID);

            Session["recentBES"] = bookingextraselection.BookingExtraSelectionID;
            return RedirectToAction("Edit", "BookingExtraSelection", new { BookingExtraSelectionId = bookingextraselection.BookingExtraSelectionID });


            ViewBag.AirportPickupLocationID = new SelectList(db.AirportDestinations, "AirportPickupLocationID", "AirportPickupLocationName", bookingextraselection.AirportPickupLocationID);
            ViewBag.BookingExtraID = new SelectList(db.BookingExtras, "BookingExtraID", "LegacyReference", bookingextraselection.BookingExtraID);
            ViewBag.BookingParentContainerID = new SelectList(db.BookingParentContainers, "BookingParentContainerID", "OverallBookingReference", bookingextraselection.BookingParentContainerID);
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "Title", bookingextraselection.CustomerID);
            return View(bookingextraselection);
        }

        //
        // GET: /BookingExtraSelection/Edit/5

        public ActionResult Edit(long BookingExtraSelectionId = 0)
        {
            BookingExtraSelection bookingextraselection = db.BookingExtraSelections.Find(BookingExtraSelectionId);
            if (bookingextraselection == null)
            {
                return HttpNotFound();
            }
            ViewBag.AirportPickupLocationID = new SelectList(db.AirportDestinations, "AirportPickupLocationID", "AirportPickupLocationName", bookingextraselection.AirportPickupLocationID);
            ViewBag.BookingExtraID = new SelectList(db.BookingExtras, "BookingExtraID", "LegacyReference", bookingextraselection.BookingExtraID);
            ViewBag.BookingParentContainerID = new SelectList(db.BookingParentContainers, "BookingParentContainerID", "OverallBookingReference", bookingextraselection.BookingParentContainerID);
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "Title", bookingextraselection.CustomerID);
            Session["recentBES"] = bookingextraselection.BookingExtraSelectionID;
            return View(bookingextraselection);
        }

        //
        // POST: /BookingExtraSelection/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BookingExtraSelection bookingextraselection)
        {
            var oldBes =
                db.BookingExtraSelections.Where(
                    x => x.BookingExtraSelectionID == bookingextraselection.BookingExtraSelectionID).FirstOrDefault();
            bookingextraselection.WhenCreated = oldBes.WhenCreated;

            if (ModelState.IsValid)
            {
                using (var _db = new PortugalVillasContext())
                {
                    _db.BookingExtraSelections.Attach(bookingextraselection);
                    _db.Entry(bookingextraselection).State = EntityState.Modified;
                    _db.SaveChanges();
                    Session["recentBES"] = bookingextraselection.BookingExtraSelectionID;
                    return RedirectToAction("Edit", "BookingExtraSelection",
                        new { BookingExtraSelectionId = bookingextraselection.BookingExtraSelectionID });
                }
            }
            ViewBag.AirportPickupLocationID = new SelectList(db.AirportDestinations, "AirportPickupLocationID", "AirportPickupLocationName", bookingextraselection.AirportPickupLocationID);
            ViewBag.BookingExtraID = new SelectList(db.BookingExtras, "BookingExtraID", "LegacyReference", bookingextraselection.BookingExtraID);
            ViewBag.BookingParentContainerID = new SelectList(db.BookingParentContainers, "BookingParentContainerID", "OverallBookingReference", bookingextraselection.BookingParentContainerID);
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "Title", bookingextraselection.CustomerID);
            return View(bookingextraselection);
        }

        //
        // GET: /BookingExtraSelection/Delete/5

        public ActionResult Delete(long id = 0)
        {
            BookingExtraSelection bookingextraselection = db.BookingExtraSelections.Find(id);
            if (bookingextraselection == null)
            {
                return HttpNotFound();
            }
            return View(bookingextraselection);
        }

        //
        // POST: /BookingExtraSelection/Delete/5

        /*  [HttpPost, ActionName("Delete")]
          [ValidateAntiForgeryToken]
          public ActionResult DeleteConfirmed(long id)
          {
              BookingExtraSelection bookingextraselection = db.BookingExtraSelections.Find(id);
              db.BookingExtraSelections.Remove(bookingextraselection);
              db.SaveChanges();
              return RedirectToAction("Index");
          }*/

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}