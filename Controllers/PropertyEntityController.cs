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
    public class PropertyEntityController : Controller
    {
        private PortugalVillasContext db = new PortugalVillasContext();

        //
        // GET: /PropertyEntity/

        public ActionResult Index()
        {
            var propertyentities = db.PropertyEntities.Include(p => p.Property).Include(p => p.PropertyEntityType);
            return View(propertyentities.ToList());
        }

        //
        // GET: /PropertyEntity/Details/5

        public ActionResult Details(long id = 0)
        {
            PropertyEntity propertyentity = db.PropertyEntities.Find(id);
            if (propertyentity == null)
            {
                return HttpNotFound();
            }
            return View(propertyentity);
        }

        //
        // GET: /PropertyEntity/Create

        public ActionResult Create()
        {
            ViewBag.PropertyID = new SelectList(db.Properties, "PropertyID", "LegacyReference");
            ViewBag.PropertyEntityTypeID = new SelectList(db.PropertyEntityTypes, "PropertyEntityTypeID", "PropertyEntityTypeName");
            return View();
        }

        //
        // POST: /PropertyEntity/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PropertyEntity propertyentity)
        {
            if (ModelState.IsValid)
            {
                db.PropertyEntities.Add(propertyentity);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PropertyID = new SelectList(db.Properties, "PropertyID", "LegacyReference", propertyentity.PropertyID);
            ViewBag.PropertyEntityTypeID = new SelectList(db.PropertyEntityTypes, "PropertyEntityTypeID", "PropertyEntityTypeName", propertyentity.PropertyEntityTypeID);
            return View(propertyentity);
        }

        //
        // GET: /PropertyEntity/Edit/5

        public ActionResult Edit(long id = 0)
        {
            PropertyEntity propertyentity = db.PropertyEntities.Find(id);
            if (propertyentity == null)
            {
                return HttpNotFound();
            }
            ViewBag.PropertyID = new SelectList(db.Properties, "PropertyID", "LegacyReference", propertyentity.PropertyID);
            ViewBag.PropertyEntityTypeID = new SelectList(db.PropertyEntityTypes, "PropertyEntityTypeID", "PropertyEntityTypeName", propertyentity.PropertyEntityTypeID);
            return View(propertyentity);
        }

        //
        // POST: /PropertyEntity/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PropertyEntity propertyentity)
        {
            if (ModelState.IsValid)
            {
                db.Entry(propertyentity).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PropertyID = new SelectList(db.Properties, "PropertyID", "LegacyReference", propertyentity.PropertyID);
            ViewBag.PropertyEntityTypeID = new SelectList(db.PropertyEntityTypes, "PropertyEntityTypeID", "PropertyEntityTypeName", propertyentity.PropertyEntityTypeID);
            return View(propertyentity);
        }

        //
        // GET: /PropertyEntity/Delete/5

        public ActionResult Delete(long id = 0)
        {
            PropertyEntity propertyentity = db.PropertyEntities.Find(id);
            if (propertyentity == null)
            {
                return HttpNotFound();
            }
            return View(propertyentity);
        }

        //
        // POST: /PropertyEntity/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            PropertyEntity propertyentity = db.PropertyEntities.Find(id);
            db.PropertyEntities.Remove(propertyentity);
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