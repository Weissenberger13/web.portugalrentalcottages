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
    public class PropertyOwnerRepresentativeController : Controller
    {
        private PortugalVillasContext db = new PortugalVillasContext();

        //
        // GET: /PropertyOwnerRepresentative/

        public ActionResult Index()
        {
            return View(db.PropertyOwnerRepresentatives.ToList());
        }

        //
        // GET: /PropertyOwnerRepresentative/Details/5

        public ActionResult Details(long id = 0)
        {
            PropertyOwnerRepresentative propertyownerrepresentative = db.PropertyOwnerRepresentatives.Find(id);
            if (propertyownerrepresentative == null)
            {
                return HttpNotFound();
            }
            return View(propertyownerrepresentative);
        }

        //
        // GET: /PropertyOwnerRepresentative/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /PropertyOwnerRepresentative/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PropertyOwnerRepresentative propertyownerrepresentative)
        {
            if (ModelState.IsValid)
            {
                db.PropertyOwnerRepresentatives.Add(propertyownerrepresentative);
                db.SaveChanges();
                return RedirectToAction("Edit", new { id = propertyownerrepresentative.PropertyOwnerRepresentativeID });
            }

            return View(propertyownerrepresentative);
        }

        //
        // GET: /PropertyOwnerRepresentative/Edit/5

        public ActionResult Edit(long id = 0)
        {
            PropertyOwnerRepresentative propertyownerrepresentative = db.PropertyOwnerRepresentatives.Find(id);
            if (propertyownerrepresentative == null)
            {
                return HttpNotFound();
            }
            return View(propertyownerrepresentative);
        }

        //
        // POST: /PropertyOwnerRepresentative/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PropertyOwnerRepresentative propertyownerrepresentative)
        {
            if (ModelState.IsValid)
            {
                using (var _db = new PortugalVillasContext())
                {
                    _db.PropertyOwnerRepresentatives.Attach(propertyownerrepresentative);
                    _db.Entry(propertyownerrepresentative).State = EntityState.Modified;
                    _db.SaveChanges();
                    return RedirectToAction("Edit", new {id = propertyownerrepresentative.PropertyOwnerRepresentativeID});
                }
            }
            return View(propertyownerrepresentative);
        }

        //
        // GET: /PropertyOwnerRepresentative/Delete/5

        public ActionResult Delete(long id = 0)
        {
            PropertyOwnerRepresentative propertyownerrepresentative = db.PropertyOwnerRepresentatives.Find(id);
            if (propertyownerrepresentative == null)
            {
                return HttpNotFound();
            }
            return View(propertyownerrepresentative);
        }

        //
        // POST: /PropertyOwnerRepresentative/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            PropertyOwnerRepresentative propertyownerrepresentative = db.PropertyOwnerRepresentatives.Find(id);
            db.PropertyOwnerRepresentatives.Remove(propertyownerrepresentative);
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