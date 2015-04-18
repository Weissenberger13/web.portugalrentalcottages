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
      [Authorize(Roles = "Administrator")]
    public class BookingExtraParticipantController : Controller
    {
        private PortugalVillasContext db = new PortugalVillasContext();

        //
        // GET: /BookingExtraParticipant/

        public ActionResult Index(long BookingExtraSelectionId)
        {
            var bookingextraparticipants = db.BookingExtraParticipants.Include(b => b.BookingExtraSelection).Where(x=>x.BookingExtraSelectionID == BookingExtraSelectionId);
            return View(bookingextraparticipants.ToList());
        }

        //
        // GET: /BookingExtraParticipant/Details/5

        public ActionResult DataTableIndex()
        {
            return View("Index", db.BookingExtraParticipants.ToList());
        }

        public ActionResult Details(long id = 0)
        {
            BookingExtraParticipant bookingextraparticipant = db.BookingExtraParticipants.Find(id);
            if (bookingextraparticipant == null)
            {
                return HttpNotFound();
            }
            return View(bookingextraparticipant);
        }

        //
        // GET: /BookingExtraParticipant/Create

        public ActionResult Create()
        {

            ViewBag.ID = (long)Session["recentBES"];
            return View();
        }

        //
        // POST: /BookingExtraParticipant/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BookingExtraParticipant bookingextraparticipant)
        {
            bookingextraparticipant.BookingExtraParticipantWhenCreated = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.BookingExtraParticipants.Add(bookingextraparticipant);
                db.SaveChanges();
                ViewBag.ID = (long)Session["recentBES"];
                return RedirectToAction("Index", new { BookingExtraSelectionId = bookingextraparticipant.BookingExtraSelectionID });
            }

          
            return View(bookingextraparticipant);
        }

        //
        // GET: /BookingExtraParticipant/Edit/5

        public ActionResult Edit(long id = 0)
        {
            BookingExtraParticipant bookingextraparticipant = db.BookingExtraParticipants.Find(id);
            if (bookingextraparticipant == null)
            {
                return HttpNotFound();
            }

            ViewBag.ID = (long)Session["recentBES"];
            return View(bookingextraparticipant);
        }

        //
        // POST: /BookingExtraParticipant/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BookingExtraParticipant bookingextraparticipant)
        {
            var old =
                db.BookingExtraParticipants.Where(
                    c => c.BookingExtraSelectionID == bookingextraparticipant.BookingExtraSelectionID).FirstOrDefault();

            bookingextraparticipant.BookingExtraParticipantWhenCreated = old.BookingExtraParticipantWhenCreated;

            if (ModelState.IsValid)
            {
                using (var _db = new PortugalVillasContext())
                {
                    _db.BookingExtraParticipants.Attach(bookingextraparticipant);
                    _db.Entry(bookingextraparticipant).State = EntityState.Modified;
                    _db.SaveChanges();
                    return RedirectToAction("Index",
                        new {BookingExtraSelectionId = bookingextraparticipant.BookingExtraSelectionID});
                }
            }
            ViewBag.ID = (long)Session["recentBES"];
            return View(bookingextraparticipant);
        }

        //
        // GET: /BookingExtraParticipant/Delete/5

        public ActionResult Delete(long id = 0)
        {
            BookingExtraParticipant bookingextraparticipant = db.BookingExtraParticipants.Find(id);
            if (bookingextraparticipant == null)
            {
                return HttpNotFound();
            }
            var besid = bookingextraparticipant.BookingExtraSelectionID;
            db.BookingExtraParticipants.Remove(bookingextraparticipant);
            db.SaveChanges();
            return RedirectToAction("Index", new { BookingExtraSelectionId = besid });            
        }

        //
        // POST: /BookingExtraParticipant/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
           
            BookingExtraParticipant bookingextraparticipant = db.BookingExtraParticipants.Find(id);
            var besid = bookingextraparticipant.BookingExtraSelectionID;
            db.BookingExtraParticipants.Remove(bookingextraparticipant);
            db.SaveChanges();
            return RedirectToAction("Index", new { BookingExtraSelectionId = besid });
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}