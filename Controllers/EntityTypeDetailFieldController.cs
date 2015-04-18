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
    public class EntityTypeDetailFieldController : Controller
    {
        private PortugalVillasContext db = new PortugalVillasContext();

        //
        // GET: /EntityTypeDetailField/

        public ActionResult Index()
        {
            var entitytypedetailfields = db.EntityTypeDetailFields.Include(e => e.EntityType);
            return View(entitytypedetailfields.ToList());
        }

        //
        // GET: /EntityTypeDetailField/Details/5

        public ActionResult Details(long id = 0)
        {
            EntityTypeDetailField entitytypedetailfield = db.EntityTypeDetailFields.Find(id);
            if (entitytypedetailfield == null)
            {
                return HttpNotFound();
            }
            return View(entitytypedetailfield);
        }

        //
        // GET: /EntityTypeDetailField/Create

        public ActionResult Create()
        {
            ViewBag.EntityTypeID = new SelectList(db.EntityTypes, "EntityTypeID", "EntityTypeName");
            return View();
        }

        //
        // POST: /EntityTypeDetailField/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EntityTypeDetailField entitytypedetailfield)
        {
            if (ModelState.IsValid)
            {
                db.EntityTypeDetailFields.Add(entitytypedetailfield);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EntityTypeID = new SelectList(db.EntityTypes, "EntityTypeID", "EntityTypeName", entitytypedetailfield.EntityTypeID);
            return View(entitytypedetailfield);
        }

        //
        // GET: /EntityTypeDetailField/Edit/5

        public ActionResult Edit(long id = 0)
        {
            EntityTypeDetailField entitytypedetailfield = db.EntityTypeDetailFields.Find(id);
            if (entitytypedetailfield == null)
            {
                return HttpNotFound();
            }
            ViewBag.EntityTypeID = new SelectList(db.EntityTypes, "EntityTypeID", "EntityTypeName", entitytypedetailfield.EntityTypeID);
            return View(entitytypedetailfield);
        }

        //
        // POST: /EntityTypeDetailField/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EntityTypeDetailField entitytypedetailfield)
        {
            if (ModelState.IsValid)
            {
                db.Entry(entitytypedetailfield).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EntityTypeID = new SelectList(db.EntityTypes, "EntityTypeID", "EntityTypeName", entitytypedetailfield.EntityTypeID);
            return View(entitytypedetailfield);
        }

        //
        // GET: /EntityTypeDetailField/Delete/5

        public ActionResult Delete(long id = 0)
        {
            EntityTypeDetailField entitytypedetailfield = db.EntityTypeDetailFields.Find(id);
            if (entitytypedetailfield == null)
            {
                return HttpNotFound();
            }
            return View(entitytypedetailfield);
        }

        //
        // POST: /EntityTypeDetailField/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            EntityTypeDetailField entitytypedetailfield = db.EntityTypeDetailFields.Find(id);
            db.EntityTypeDetailFields.Remove(entitytypedetailfield);
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