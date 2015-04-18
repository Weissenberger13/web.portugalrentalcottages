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
    public class BookingParticipantController : Controller
    {
        private PortugalVillasContext db = new PortugalVillasContext();

        //
        // GET: /BookingParticipant/

        public ActionResult Index(long bookingID)
        {
            var bookingparticipants = db.BookingParticipants.Include(b => b.Booking).Where(x => x.BookingID == bookingID);
            return View("SingleBookingParticipantIndex", bookingparticipants.ToList());
        }


        public ActionResult DataTableIndex()
        {
            return View("Index", db.BookingParticipants.ToList());
        }

        //
        // GET: /BookingParticipant/Details/5

        public ActionResult Details(long id = 0)
        {
            BookingParticipant bookingparticipant = db.BookingParticipants.Find(id);
            if (bookingparticipant == null)
            {
                return HttpNotFound();
            }
            return View(bookingparticipant);
        }

        //
        // GET: /BookingParticipant/Create

        public ActionResult Create()
        {
            ViewBag.BookingID = new SelectList(db.Bookings, "BookingID", "BookingPRCReference");
            return View();
        }

        //
        // POST: /BookingParticipant/Create

        [HttpPost]
     
        public ActionResult Create(BookingParticipant bookingparticipant)
        {
            bookingparticipant.BookingParticipantWhenCreated = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.BookingParticipants.Add(bookingparticipant);
                db.SaveChanges();
                ViewBag.BookingID = new SelectList(db.Bookings, "BookingID", "BookingPRCReference", bookingparticipant.BookingID);             
                return RedirectToAction("Index", "BookingParticipant", new { bookingID = bookingparticipant.BookingID });
            }

            ViewBag.BookingID = new SelectList(db.Bookings, "BookingID", "BookingPRCReference", bookingparticipant.BookingID);

            bookingparticipant =
                db.BookingParticipants.Where(s => s.BookingParticipantID == bookingparticipant.BookingParticipantID)
                    .First();

            return View("SingleBookingParticipantIndex", db.BookingParticipants.Where(x=>x.BookingID == bookingparticipant.BookingID).ToList());
        }

        //
        // GET: /BookingParticipant/Edit/5

        public ActionResult Edit(long id = 0)
        {
            BookingParticipant bookingparticipant = db.BookingParticipants.Find(id);
            if (bookingparticipant == null)
            {
                return HttpNotFound();
            }
            ViewBag.BookingID = new SelectList(db.Bookings, "BookingID", "BookingPRCReference", bookingparticipant.BookingID);
            return View(bookingparticipant);
        }

        //
        // POST: /BookingParticipant/Edit/5

        [HttpPost]
    
        public ActionResult Edit(BookingParticipant bookingparticipant)
        {
            var oldPart = db.BookingParticipants.Where(x => x.BookingParticipantID == bookingparticipant.BookingParticipantID).FirstOrDefault();

            bookingparticipant.BookingParticipantWhenCreated = oldPart.BookingParticipantWhenCreated;
            if (ModelState.IsValid)
            {
                using (var _db = new PortugalVillasContext())
                {
                    _db.BookingParticipants.Attach(bookingparticipant);
                    _db.Entry(bookingparticipant).State = EntityState.Modified;
                    _db.SaveChanges();
                    return RedirectToAction("Index", "BookingParticipant", new { bookingID = bookingparticipant.BookingID });
                }
            }
            ViewBag.BookingID = new SelectList(db.Bookings, "BookingID", "BookingPRCReference", bookingparticipant.BookingID);
            return View(bookingparticipant);
        }

        //
        // GET: /BookingParticipant/Delete/5

        public ActionResult Delete(long id = 0)
        {
            BookingParticipant bookingparticipant = db.BookingParticipants.Find(id);           
            db.BookingParticipants.Remove(bookingparticipant);
            db.SaveChanges();
            return RedirectToAction("Index", "BookingParticipant", new { bookingID = bookingparticipant.BookingID });
        }

        //
        // POST: /BookingParticipant/Delete/5

      /*  [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            BookingParticipant bookingparticipant = db.BookingParticipants.Find(id);
            db.BookingParticipants.Remove(bookingparticipant);
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