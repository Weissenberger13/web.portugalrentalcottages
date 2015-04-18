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
    public class DocumentController : Controller
    {
        private PortugalVillasContext db = new PortugalVillasContext();

        //
        // GET: /Document/

        public FileContentResult ReturnPDFDocument(long id)
        {
            using (var db = new PortugalVillasContext())
            {
                var doc = db.Documents.First(c => c.DocumentID == id); //.DocumentTable.First(p => p.DocumentUID == id);
                byte[] data = doc.DocumentBLOB;
                return File(data, "application/pdf", doc.DocumentName);
            }

        }


        public ActionResult BesIndex(long besID)
        {
            var documents = db.Documents.Include(d => d.BookingExtraSelection).Include(d => d.Case).Include(d => d.Event).Where(x=>x.Event.BookingExtraSelectionID == besID);
            return View("Index", documents.ToList());
        }

        

        public ActionResult BookingIndex(long bookingID)
        {
            var documents = db.Documents.Include(d => d.BookingExtraSelection).Include(d => d.Case).Include(d => d.Event).Where(x => x.Event.BookingID == bookingID);
            return View("Index", documents.ToList());
        }
        //
        // GET: /Document/Details/5

        public ActionResult Details(long id = 0)
        {
            Document document = db.Documents.Find(id);
            if (document == null)
            {
                return HttpNotFound();
            }
            return View(document);
        }

        //
        // GET: /Document/Create

        public ActionResult Create()
        {
            ViewBag.BookingExtraSelectionID = new SelectList(db.BookingExtraSelections, "BookingExtraSelectionID", "Test");
            ViewBag.CaseID = new SelectList(db.Cases, "CaseID", "CaseID");
            ViewBag.EventID = new SelectList(db.Events, "EventID", "EventID");
            return View();
        }

        //
        // POST: /Document/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Document document)
        {
            if (ModelState.IsValid)
            {
                db.Documents.Add(document);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BookingExtraSelectionID = new SelectList(db.BookingExtraSelections, "BookingExtraSelectionID", "Test", document.BookingExtraSelectionID);
            ViewBag.CaseID = new SelectList(db.Cases, "CaseID", "CaseID", document.CaseID);
            ViewBag.EventID = new SelectList(db.Events, "EventID", "EventID", document.EventID);
            return View(document);
        }

        //
        // GET: /Document/Edit/5

        public ActionResult Edit(long id = 0)
        {
            Document document = db.Documents.Find(id);
            if (document == null)
            {
                return HttpNotFound();
            }
            ViewBag.BookingExtraSelectionID = new SelectList(db.BookingExtraSelections, "BookingExtraSelectionID", "Test", document.BookingExtraSelectionID);
            ViewBag.CaseID = new SelectList(db.Cases, "CaseID", "CaseID", document.CaseID);
            ViewBag.EventID = new SelectList(db.Events, "EventID", "EventID", document.EventID);
            return View(document);
        }

        //
        // POST: /Document/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Document document)
        {
            if (ModelState.IsValid)
            {
                db.Entry(document).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BookingExtraSelectionID = new SelectList(db.BookingExtraSelections, "BookingExtraSelectionID", "Test", document.BookingExtraSelectionID);
            ViewBag.CaseID = new SelectList(db.Cases, "CaseID", "CaseID", document.CaseID);
            ViewBag.EventID = new SelectList(db.Events, "EventID", "EventID", document.EventID);
            return View(document);
        }

        //
        // GET: /Document/Delete/5

        public ActionResult Delete(long id = 0)
        {
            Document document = db.Documents.Find(id);
            if (document == null)
            {
                return HttpNotFound();
            }
            return View(document);
        }

        //
        // POST: /Document/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Document document = db.Documents.Find(id);
            db.Documents.Remove(document);
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