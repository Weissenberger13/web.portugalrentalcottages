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
    public class PRCInformationController : Controller
    {
        private PortugalVillasContext db = new PortugalVillasContext();

        //
        // GET: /PRCInformation/

        public ActionResult Index()
        {
            return View(db.PRCInformations.ToList());
        }

        //
        // GET: /PRCInformation/Details/5

        public ActionResult Details(long id = 0)
        {
            PRCInformation prcinformation = db.PRCInformations.Find(id);
            if (prcinformation == null)
            {
                return HttpNotFound();
            }
            return View(prcinformation);
        }

        //
        // GET: /PRCInformation/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /PRCInformation/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PRCInformation prcinformation)
        {
            if (ModelState.IsValid)
            {
                db.PRCInformations.Add(prcinformation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(prcinformation);
        }

        //
        // GET: /PRCInformation/Edit/5

        public ActionResult Edit(long id = 0)
        {
            PRCInformation prcinformation = db.PRCInformations.Find(id);
            if (prcinformation == null)
            {
                return HttpNotFound();
            }
            return View(prcinformation);
        }

        //
        // POST: /PRCInformation/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PRCInformation prcinformation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(prcinformation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(prcinformation);
        }

        //
        // GET: /PRCInformation/Delete/5

        public ActionResult Delete(long id = 0)
        {
            PRCInformation prcinformation = db.PRCInformations.Find(id);
            if (prcinformation == null)
            {
                return HttpNotFound();
            }
            return View(prcinformation);
        }

        //
        // POST: /PRCInformation/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            PRCInformation prcinformation = db.PRCInformations.Find(id);
            db.PRCInformations.Remove(prcinformation);
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