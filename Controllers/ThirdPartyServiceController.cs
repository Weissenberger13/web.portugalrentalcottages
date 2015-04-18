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
    public class ThirdPartyServiceController : Controller
    {
        private PortugalVillasContext db = new PortugalVillasContext();

        //
        // GET: /ThirdPartyService/

        public ActionResult Index()
        {
            return View(db.ThirdPartyServices.ToList());
        }

        //
        // GET: /ThirdPartyService/Details/5

        public ActionResult Details(long id = 0)
        {
            ThirdPartyService thirdpartyservice = db.ThirdPartyServices.Find(id);
            if (thirdpartyservice == null)
            {
                return HttpNotFound();
            }
            return View(thirdpartyservice);
        }

        //
        // GET: /ThirdPartyService/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /ThirdPartyService/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ThirdPartyService thirdpartyservice)
        {
            if (ModelState.IsValid)
            {
                db.ThirdPartyServices.Add(thirdpartyservice);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(thirdpartyservice);
        }

        //
        // GET: /ThirdPartyService/Edit/5

        public ActionResult Edit(long id = 0)
        {
            ThirdPartyService thirdpartyservice = db.ThirdPartyServices.Find(id);
            if (thirdpartyservice == null)
            {
                return HttpNotFound();
            }
            return View(thirdpartyservice);
        }

        //
        // POST: /ThirdPartyService/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ThirdPartyService thirdpartyservice)
        {
            if (ModelState.IsValid)
            {
                db.Entry(thirdpartyservice).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(thirdpartyservice);
        }

        //
        // GET: /ThirdPartyService/Delete/5

        public ActionResult Delete(long id = 0)
        {
            ThirdPartyService thirdpartyservice = db.ThirdPartyServices.Find(id);
            if (thirdpartyservice == null)
            {
                return HttpNotFound();
            }
            return View(thirdpartyservice);
        }

        //
        // POST: /ThirdPartyService/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            ThirdPartyService thirdpartyservice = db.ThirdPartyServices.Find(id);
            db.ThirdPartyServices.Remove(thirdpartyservice);
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