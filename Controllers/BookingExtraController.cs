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
    public class BookingExtraController : Controller
    {
        private PortugalVillasContext db = new PortugalVillasContext();

        //
        // GET: /BookingExtra/

        public ActionResult SingleBookingExtraIndex(long bookingExtraID)
        {
            BookingExtra bookingextra = db.BookingExtras.Find(bookingExtraID);
            if (bookingextra == null)
            {
                return HttpNotFound();
            }
            ViewBag.BookingExtraTypeID = new SelectList(db.BookingExtraTypes, "BookingExtraTypeID", "ExtraTypeName", bookingextra.BookingExtraTypeID);
           
            return View(bookingextra);
        }



        public ActionResult Index()
        {
            var bookingextras = db.BookingExtras.Include(b => b.BookingExtraType);
            return View(bookingextras.ToList());
        }

        //
        // GET: /BookingExtra/Details/5

        public ActionResult Details(long id = 0)
        {
            BookingExtra bookingextra = db.BookingExtras.Find(id);
            if (bookingextra == null)
            {
                return HttpNotFound();
            }
            return View(bookingextra);
        }

        //
        // GET: /BookingExtra/Create

        public ActionResult Create()
        {
            ViewBag.BookingExtraTypeID = new SelectList(db.BookingExtraTypes, "BookingExtraTypeID", "ExtraTypeName");
            return View();
        }

        //
        // POST: /BookingExtra/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BookingExtra bookingextra)
        {
            bookingextra.WhenCreated = DateTime.Now;

            if (ModelState.IsValid)
            {
                db.BookingExtras.Add(bookingextra);
                db.SaveChanges();
                ViewBag.BookingExtraTypeID = new SelectList(db.BookingExtraTypes, "BookingExtraTypeID", "ExtraTypeName", bookingextra.BookingExtraTypeID);
                return RedirectToAction("SingleBookingExtraIndex", "BookingExtra", new { bookingextraID = bookingextra.BookingExtraID });
            }

            ViewBag.BookingExtraTypeID = new SelectList(db.BookingExtraTypes, "BookingExtraTypeID", "ExtraTypeName", bookingextra.BookingExtraTypeID);
            return View(bookingextra);
        }

        //
        // GET: /BookingExtra/Edit/5

        public ActionResult Edit(long id = 0)
        {
            BookingExtra bookingextra = db.BookingExtras.Find(id);
            if (bookingextra == null)
            {
                return HttpNotFound();
            }
            ViewBag.BookingExtraTypeID = new SelectList(db.BookingExtraTypes, "BookingExtraTypeID", "ExtraTypeName", bookingextra.BookingExtraTypeID);
            return View(bookingextra);
        }

        //
        // POST: /BookingExtra/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BookingExtra bookingextra)
        {
            var oldExtra = db.BookingExtras.Where(x => x.BookingExtraID == bookingextra.BookingExtraID).First();
            bookingextra.WhenCreated = oldExtra.WhenCreated;
            bookingextra.WhenModified = oldExtra.WhenModified;

            if (ModelState.IsValid)
            {
                using (var _db = new PortugalVillasContext())
                {
                    _db.BookingExtras.Attach(bookingextra);
                    _db.Entry(bookingextra).State = EntityState.Modified;
                    _db.SaveChanges();
                    ViewBag.BookingExtraTypeID = new SelectList(db.BookingExtraTypes, "BookingExtraTypeID",
                        "ExtraTypeName", bookingextra.BookingExtraTypeID);
                    return RedirectToAction("SingleBookingExtraIndex", "BookingExtra",
                        new {bookingextraID = bookingextra.BookingExtraID});
                }
            }
            ViewBag.BookingExtraTypeID = new SelectList(db.BookingExtraTypes, "BookingExtraTypeID", "ExtraTypeName", bookingextra.BookingExtraTypeID);
            return View(bookingextra);
        }

        //
        // GET: /BookingExtra/Delete/5

        public ActionResult Delete(long id = 0)
        {
            BookingExtra bookingextra = db.BookingExtras.Find(id);
            if (bookingextra == null)
            {
                return HttpNotFound();
            }
            return View(bookingextra);
        }

        //
        // POST: /BookingExtra/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            BookingExtra bookingextra = db.BookingExtras.Find(id);
            db.BookingExtras.Remove(bookingextra);
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