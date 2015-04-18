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
    public class PropertyOwnerController : Controller
    {
        private PortugalVillasContext db = new PortugalVillasContext();

        //
        // GET: /PropertyOwner/

        public ActionResult Index()
        {
            return View(db.PropertyOwners.ToList());
        }

        //
        // GET: /PropertyOwner/Details/5

        public ActionResult Details(long id = 0)
        {
            PropertyOwner propertyowner = db.PropertyOwners.Find(id);
            if (propertyowner == null)
            {
                return HttpNotFound();
            }
            return View(propertyowner);
        }

        //
        // GET: /PropertyOwner/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /PropertyOwner/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PropertyOwner propertyowner)
        {
            propertyowner.WhenCreated = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.PropertyOwners.Add(propertyowner);
                db.SaveChanges();
                return RedirectToAction("Edit", new { id = propertyowner.PropertyOwnerID });    
            }

            return View(propertyowner);
        }

        //
        // GET: /PropertyOwner/Edit/5

        public ActionResult Edit(long id = 0)
        {
            PropertyOwner propertyowner = db.PropertyOwners.Find(id);
            if (propertyowner == null)
            {
                return HttpNotFound();
            }
            return View(propertyowner);
        }

        //
        // POST: /PropertyOwner/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PropertyOwner propertyowner)
        {
            propertyowner.WhenCreated = DateTime.Now;
            propertyowner.WhenUpdated =
                db.PropertyOwners.Where(x => x.PropertyOwnerID == propertyowner.PropertyOwnerID).First().WhenUpdated;


            if (ModelState.IsValid)
            {
                using (var _db = new PortugalVillasContext())
                {
                    _db.PropertyOwners.Attach(propertyowner);
                    _db.Entry(propertyowner).State = EntityState.Modified;
                    _db.SaveChanges();
                    return RedirectToAction("Edit", new { id = propertyowner.PropertyOwnerID });    
                }
                
                
            }
            return View(propertyowner);
        }

        //
        // GET: /PropertyOwner/Delete/5

        public ActionResult Delete(long id = 0)
        {
            PropertyOwner propertyowner = db.PropertyOwners.Find(id);
            if (propertyowner == null)
            {
                return HttpNotFound();
            }
            return View(propertyowner);
        }

        //
        // POST: /PropertyOwner/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            PropertyOwner propertyowner = db.PropertyOwners.Find(id);
            db.PropertyOwners.Remove(propertyowner);
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