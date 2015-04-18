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
    public class PropertyTownController : Controller
    {
        private PortugalVillasContext db = new PortugalVillasContext();

        //
        // GET: /PropertyTown/

        public ActionResult Index()
        {
            var propertytowns = db.PropertyTowns.Include(p => p.PropertyRegion);
            return View(propertytowns.ToList());
        }

        //
        // GET: /PropertyTown/Details/5

        public ActionResult Details(long id = 0)
        {
            PropertyTown propertytown = db.PropertyTowns.Find(id);
            if (propertytown == null)
            {
                return HttpNotFound();
            }
            return View(propertytown);
        }

        //
        // GET: /PropertyTown/Create

        public ActionResult Create()
        {
            ViewBag.PropertyRegionID = new SelectList(db.PropertyRegions, "PropertyRegionID", "RegionName");
            return View();
        }

        //
        // POST: /PropertyTown/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PropertyTown propertytown)
        {
            if (ModelState.IsValid)
            {
                using (var _db = new PortugalVillasContext())
                {                    
                    _db.PropertyTowns.Add(propertytown);
                    _db.SaveChanges();
                    ViewBag.PropertyRegionID = new SelectList(db.PropertyRegions, "PropertyRegionID", "RegionName",
                        propertytown.PropertyRegionID);
                    return RedirectToAction("Edit", new {id = propertytown.PropertyTownID});
                }

            }

            ViewBag.PropertyRegionID = new SelectList(db.PropertyRegions, "PropertyRegionID", "RegionName", propertytown.PropertyRegionID);
            return View(propertytown);
        }

        //
        // GET: /PropertyTown/Edit/5

        public ActionResult Edit(long id = 0)
        {
            PropertyTown propertytown = db.PropertyTowns.Find(id);
            if (propertytown == null)
            {
                return HttpNotFound();
            }
            ViewBag.PropertyRegionID = new SelectList(db.PropertyRegions, "PropertyRegionID", "RegionName", propertytown.PropertyRegionID);
            return View(propertytown);
        }

        //
        // POST: /PropertyTown/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PropertyTown propertytown)
        {
            if (ModelState.IsValid)
            {

                using (var _db = new PortugalVillasContext())
                {
                    _db.PropertyTowns.Attach(propertytown);
                    _db.Entry(propertytown).State = EntityState.Modified;
                    _db.SaveChanges();
                    ViewBag.PropertyRegionID = new SelectList(db.PropertyRegions, "PropertyRegionID", "RegionName",
                        propertytown.PropertyRegionID);
                    return RedirectToAction("Edit", new {id = propertytown.PropertyTownID});
                }
            }
            ViewBag.PropertyRegionID = new SelectList(db.PropertyRegions, "PropertyRegionID", "RegionName", propertytown.PropertyRegionID);
            return View(propertytown);
        }

        //
        // GET: /PropertyTown/Delete/5

        public ActionResult Delete(long id = 0)
        {
            PropertyTown propertytown = db.PropertyTowns.Find(id);
            if (propertytown == null)
            {
                return HttpNotFound();
            }
            return View(propertytown);
        }

        //
        // POST: /PropertyTown/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            PropertyTown propertytown = db.PropertyTowns.Find(id);
            db.PropertyTowns.Remove(propertytown);
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