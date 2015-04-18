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
    public class BookingExternalController : Controller
    {
        private PortVillasContext db = new PortVillasContext();

        //
        // GET: /BookingExternal/

        public ActionResult Index()
        {
            var bookingexternals = db.BookingExternals.Include(b => b.Property);
            return View(bookingexternals.ToList());
        }

        //
        // GET: /BookingExternal/Details/5

        public ActionResult Details(long id = 0)
        {
            BookingExternal bookingexternal = db.BookingExternals.Find(id);
            if (bookingexternal == null)
            {
                return HttpNotFound();
            }
            return View(bookingexternal);
        }

        //
        // GET: /BookingExternal/Create

        public ActionResult Create()
        {
            ViewBag.PropertyID = new SelectList(db.Properties.OrderBy(x=>x.PropertyID), "PropertyID", "LegacyReference").OrderBy(x=>x.Text);
            return View();
        }

        //
        // POST: /BookingExternal/Create

        [HttpPost]   
        public ActionResult Create(BookingExternal bookingexternal)
        {
            if (ModelState.IsValid)
            {
                db.BookingExternals.Add(bookingexternal);
                db.SaveChanges();
                return RedirectToAction("Create");
            }

            ViewBag.PropertyID = new SelectList(db.Properties, "PropertyID", "LegacyReference", bookingexternal.PropertyID);
            return View(bookingexternal);
        }

        //
        // GET: /BookingExternal/Edit/5

        public ActionResult Edit(long id = 0)
        {
            BookingExternal bookingexternal = db.BookingExternals.Find(id);
            if (bookingexternal == null)
            {
                return HttpNotFound();
            }
            ViewBag.PropertyID = new SelectList(db.Properties, "PropertyID", "LegacyReference", bookingexternal.PropertyID);
            return View(bookingexternal);
        }

        //
        // POST: /BookingExternal/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BookingExternal bookingexternal)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bookingexternal).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PropertyID = new SelectList(db.Properties, "PropertyID", "LegacyReference", bookingexternal.PropertyID);
            return View(bookingexternal);
        }

        //
        // GET: /BookingExternal/Delete/5

        public ActionResult Delete(long id = 0)
        {
            BookingExternal bookingexternal = db.BookingExternals.Find(id);
            if (bookingexternal == null)
            {
                return HttpNotFound();
            }
            return View(bookingexternal);
        }

        //
        // POST: /BookingExternal/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            BookingExternal bookingexternal = db.BookingExternals.Find(id);
            db.BookingExternals.Remove(bookingexternal);
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