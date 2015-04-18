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
    public class NewsletterController : Controller
    {
        private PortVillasContext db = new PortVillasContext();

        //
        // GET: /Newsletter/

        public ActionResult Index()
        {
            return View(db.NewsletterParticipants.ToList());
        }

        //
        // GET: /Newsletter/Details/5

        public ActionResult Details(int id = 0)
        {
            NewsletterParticipant newsletterparticipant = db.NewsletterParticipants.Find(id);
            if (newsletterparticipant == null)
            {
                return HttpNotFound();
            }
            return View(newsletterparticipant);
        }

        //
        // GET: /Newsletter/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Newsletter/Create

        [HttpPost]
        public ActionResult Create(string newsletterparticipant)
        {
            var np = new NewsletterParticipant
            {
                NewsletterParticipantEmail = newsletterparticipant
            };
           
                db.NewsletterParticipants.Add(np);
                db.SaveChanges();
                return View();
           
        }

        //
        // GET: /Newsletter/Edit/5

        public ActionResult Edit(int id = 0)
        {
            NewsletterParticipant newsletterparticipant = db.NewsletterParticipants.Find(id);
            if (newsletterparticipant == null)
            {
                return HttpNotFound();
            }
            return View(newsletterparticipant);
        }

        //
        // POST: /Newsletter/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(NewsletterParticipant newsletterparticipant)
        {
            if (ModelState.IsValid)
            {
                db.Entry(newsletterparticipant).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(newsletterparticipant);
        }

        //
        // GET: /Newsletter/Delete/5

        public ActionResult Delete(int id = 0)
        {
            NewsletterParticipant newsletterparticipant = db.NewsletterParticipants.Find(id);
            if (newsletterparticipant == null)
            {
                return HttpNotFound();
            }
            return View(newsletterparticipant);
        }

        //
        // POST: /Newsletter/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NewsletterParticipant newsletterparticipant = db.NewsletterParticipants.Find(id);
            db.NewsletterParticipants.Remove(newsletterparticipant);
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