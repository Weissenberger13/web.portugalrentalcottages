using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using BootstrapVillas.Models;

namespace BootstrapVillas.Controllers
{
   
    public class BookingExtraAttributeController : Controller
    {
        private PortugalVillasContext db = new PortugalVillasContext();

        //
        // GET: /BookingExtraAttribute/


        public ActionResult BookingExtraAttributeListIndex(long bookingExtraId)
        {
            var bookingextraattributes = db.BookingExtraAttributes.Include(b => b.BookingExtra).Where(s => s.BookingExtraID == bookingExtraId);

            return View(bookingextraattributes.ToList());
        }


        public ActionResult Index()
        {
            var bookingextraattributes = db.BookingExtraAttributes.Include(b => b.BookingExtra);
            return View(bookingextraattributes.ToList());
        }

        //
        // GET: /BookingExtraAttribute/Details/5

        public ActionResult Details(long id = 0)
        {
            BookingExtraAttribute bookingextraattribute = db.BookingExtraAttributes.Find(id);
            if (bookingextraattribute == null)
            {
                return HttpNotFound();
            }
            return View(bookingextraattribute);
        }

        //
        // GET: /BookingExtraAttribute/Create

        public ActionResult Create()
        {
            ViewBag.BookingExtraID = new SelectList(db.BookingExtras, "BookingExtraID", "LegacyReference");
            ViewBag.BookingExtraTypeID = new SelectList(db.BookingExtraTypes, "BookingExtraTypeID", "ExtraTypeName");
            return View();
        }

        //
        // POST: /BookingExtraAttribute/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BookingExtraAttribute bookingextraattribute)
        {
            if (ModelState.IsValid)
            {
                db.BookingExtraAttributes.Add(bookingextraattribute);
                db.SaveChanges();
                ViewBag.BookingExtraID = new SelectList(db.BookingExtras, "BookingExtraID", "LegacyReference", bookingextraattribute.BookingExtraID);
                ViewBag.BookingExtraTypeID = new SelectList(db.BookingExtraTypes, "BookingExtraTypeID", "ExtraTypeName");
                return RedirectToAction("BookingExtraAttributeListIndex", "BookingExtraAttribute", new { bookingExtraId = bookingextraattribute.BookingExtraID });

            }

            ViewBag.BookingExtraID = new SelectList(db.BookingExtras, "BookingExtraID", "LegacyReference", bookingextraattribute.BookingExtraID);
            ViewBag.BookingExtraTypeID = new SelectList(db.BookingExtraTypes, "BookingExtraTypeID", "ExtraTypeName");
            return View(bookingextraattribute);
        }

        //
        // GET: /BookingExtraAttribute/Edit/5

        public ActionResult Edit(long id = 0)
        {
            BookingExtraAttribute bookingextraattribute = db.BookingExtraAttributes.Find(id);
            if (bookingextraattribute == null)
            {
                return HttpNotFound();
            }
            ViewBag.BookingExtraID = new SelectList(db.BookingExtras, "BookingExtraID", "LegacyReference", bookingextraattribute.BookingExtraID);
            ViewBag.BookingExtraTypeID = new SelectList(db.BookingExtraTypes, "BookingExtraTypeID", "ExtraTypeName");
            return View(bookingextraattribute);
        }

        //
        // POST: /BookingExtraAttribute/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BookingExtraAttribute bookingextraattribute)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bookingextraattribute).State = EntityState.Modified;
                db.SaveChanges();
                ViewBag.BookingExtraID = new SelectList(db.BookingExtras, "BookingExtraID", "LegacyReference", bookingextraattribute.BookingExtraID);
                ViewBag.BookingExtraTypeID = new SelectList(db.BookingExtraTypes, "BookingExtraTypeID", "ExtraTypeName");
                return RedirectToAction("BookingExtraAttributeListIndex", "BookingExtraAttribute", new { bookingExtraId = bookingextraattribute.BookingExtraID} );
            }
            ViewBag.BookingExtraID = new SelectList(db.BookingExtras, "BookingExtraID", "LegacyReference", bookingextraattribute.BookingExtraID);
            ViewBag.BookingExtraTypeID = new SelectList(db.BookingExtraTypes, "BookingExtraTypeID", "ExtraTypeName");
            return View(bookingextraattribute);
        }

        //
        // GET: /BookingExtraAttribute/Delete/5

        public ActionResult Delete(long id = 0)
        {
            
            BookingExtraAttribute bookingextraattribute = db.BookingExtraAttributes.Find(id);
            if (bookingextraattribute == null)
            {
                return HttpNotFound();
            }
            db.BookingExtraAttributes.Remove(bookingextraattribute);
            db.SaveChanges();

            return RedirectToAction("BookingExtraAttributeListIndex", "BookingExtraAttribute", new { bookingExtraId = id });
        }

        //
        // POST: /BookingExtraAttribute/Delete/5
/*
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            BookingExtraAttribute bookingextraattribute = db.BookingExtraAttributes.Find(id);
            db.BookingExtraAttributes.Remove(bookingextraattribute);
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