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
    public class PropertyRegionController : Controller
    {
        private PortugalVillasContext db = new PortugalVillasContext();

        //
        // GET: /PropertyRegion/

        public ActionResult Index()
        {
            return View(db.PropertyRegions.ToList());
        }

        //
        // GET: /PropertyRegion/Details/5

        public ActionResult Details(long id = 0)
        {
            PropertyRegion propertyregion = db.PropertyRegions.Find(id);
            if (propertyregion == null)
            {
                return HttpNotFound();
            }
            return View(propertyregion);
        }

        //
        // GET: /PropertyRegion/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /PropertyRegion/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PropertyRegion propertyregion)
        {
            if (ModelState.IsValid)
            {
                db.PropertyRegions.Add(propertyregion);
                db.SaveChanges();
                return RedirectToAction("Edit", new{ id = propertyregion.PropertyRegionID});
            }

            return View(propertyregion);
        }

        //
        // GET: /PropertyRegion/Edit/5

        public ActionResult Edit(long id = 0)
        {
            PropertyRegion propertyregion = db.PropertyRegions.Find(id);
            if (propertyregion == null)
            {
                return HttpNotFound();
            }
            return View(propertyregion);
        }

        //
        // POST: /PropertyRegion/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PropertyRegion propertyregion)
        {
            if (ModelState.IsValid)
            {
                using (var _db = new PortugalVillasContext())
                {
                    _db.PropertyRegions.Attach(propertyregion);
                    _db.Entry(propertyregion).State = EntityState.Modified;
                    _db.SaveChanges();
                    return RedirectToAction("Edit", new { id = propertyregion.PropertyRegionID });    
                }

                
            }
            return View(propertyregion);
        }

        //
        // GET: /PropertyRegion/Delete/5

        public ActionResult Delete(long id = 0)
        {
            PropertyRegion propertyregion = db.PropertyRegions.Find(id);
            if (propertyregion == null)
            {
                return HttpNotFound();
            }
            return View(propertyregion);
        }

        //
        // POST: /PropertyRegion/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            PropertyRegion propertyregion = db.PropertyRegions.Find(id);
            db.PropertyRegions.Remove(propertyregion);
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