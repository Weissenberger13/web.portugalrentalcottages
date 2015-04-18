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
    public class PropertyPricingSeasonalInstanceController : Controller
    {
        private PortugalVillasContext db = new PortugalVillasContext();

        //
        // GET: /PropertyPricingSeasonalInstance/

        public ActionResult Index(long propertyID)
        {
            var propertypricingseasonalinstances = db.PropertyPricingSeasonalInstances.Include(p => p.Property).Include(p => p.PropertyPricingSeason).Where(x => x.PropertyID == propertyID);
            return View(propertypricingseasonalinstances.ToList());
        }

        public ActionResult FullIndex()
        {
            var propertypricingseasonalinstances = db.PropertyPricingSeasonalInstances.Include(p => p.Property).Include(p => p.PropertyPricingSeason);
            return View(propertypricingseasonalinstances.ToList());
        }


        //
        // GET: /PropertyPricingSeasonalInstance/Details/5

        public ActionResult Details(int id = 0)
        {
            PropertyPricingSeasonalInstance propertypricingseasonalinstance = db.PropertyPricingSeasonalInstances.Find(id);
            if (propertypricingseasonalinstance == null)
            {
                return HttpNotFound();
            }
            return View(propertypricingseasonalinstance);
        }

        //
        // GET: /PropertyPricingSeasonalInstance/Create

        public ActionResult Create()
        {
            ViewBag.PropertyID = new SelectList(db.Properties, "PropertyID", "LegacyReference").OrderBy(x => x.Text);
            ViewBag.PropertyPricingSeasonID = new SelectList(db.PropertyPricingSeasons, "PropertyPricingSeasonID", "Season_Name");
            return View();
        }

        //
        // POST: /PropertyPricingSeasonalInstance/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PropertyPricingSeasonalInstance propertypricingseasonalinstance)
        {
            if (ModelState.IsValid)
            {
                db.PropertyPricingSeasonalInstances.Add(propertypricingseasonalinstance);
                db.SaveChanges();


                var propertypricingseasonalinstances = db.PropertyPricingSeasonalInstances.Include(p => p.Property).Include(p => p.PropertyPricingSeason).Where(x => x.PropertyID == propertypricingseasonalinstance.PropertyID);
                ViewBag.PropertyID = new SelectList(db.Properties, "PropertyID", "LegacyReference", propertypricingseasonalinstance.PropertyID);
                ViewBag.PropertyPricingSeasonID = new SelectList(db.PropertyPricingSeasons, "PropertyPricingSeasonID", "Season_Name", propertypricingseasonalinstance.PropertyPricingSeasonID);
               
                return RedirectToAction("Index", propertypricingseasonalinstance);
            }

            ViewBag.PropertyID = new SelectList(db.Properties, "PropertyID", "LegacyReference", propertypricingseasonalinstance.PropertyID);
            ViewBag.PropertyPricingSeasonID = new SelectList(db.PropertyPricingSeasons, "PropertyPricingSeasonID", "Season_Name", propertypricingseasonalinstance.PropertyPricingSeasonID);
            return View(propertypricingseasonalinstance);
        }

        //
        // GET: /PropertyPricingSeasonalInstance/Edit/5

        public ActionResult Edit(int id = 0)
        {
            PropertyPricingSeasonalInstance propertypricingseasonalinstance = db.PropertyPricingSeasonalInstances.Find(id);
            if (propertypricingseasonalinstance == null)
            {
                return HttpNotFound();
            }
            ViewBag.PropertyID = new SelectList(db.Properties, "PropertyID", "LegacyReference", propertypricingseasonalinstance.PropertyID);
            ViewBag.PropertyPricingSeasonID = new SelectList(db.PropertyPricingSeasons, "PropertyPricingSeasonID", "Season_Name", propertypricingseasonalinstance.PropertyPricingSeasonID);
            return View(propertypricingseasonalinstance);
        }

        //
        // POST: /PropertyPricingSeasonalInstance/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PropertyPricingSeasonalInstance propertypricingseasonalinstance)
        {
            if (ModelState.IsValid)
            {
                db.Entry(propertypricingseasonalinstance).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", propertypricingseasonalinstance.PropertyID);
            }
            ViewBag.PropertyID = new SelectList(db.Properties, "PropertyID", "LegacyReference", propertypricingseasonalinstance.PropertyID);
            ViewBag.PropertyPricingSeasonID = new SelectList(db.PropertyPricingSeasons, "PropertyPricingSeasonID", "Season_Name", propertypricingseasonalinstance.PropertyPricingSeasonID);
            return View(propertypricingseasonalinstance);
        }

        //
        // GET: /PropertyPricingSeasonalInstance/Delete/5

        public ActionResult Delete(int id = 0)
        {
            PropertyPricingSeasonalInstance propertypricingseasonalinstance = db.PropertyPricingSeasonalInstances.Find(id);
            if (propertypricingseasonalinstance == null)
            {
                return HttpNotFound();
            }
            
            db.PropertyPricingSeasonalInstances.Remove(propertypricingseasonalinstance);
            db.SaveChanges();
            var propertypricingseasonalinstances = db.PropertyPricingSeasonalInstances.Include(p => p.Property).Include(p => p.PropertyPricingSeason).Where(x => x.PropertyID == id).ToList();
            return View("Index", propertypricingseasonalinstances);
        }

        //
        // POST: /PropertyPricingSeasonalInstance/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PropertyPricingSeasonalInstance propertypricingseasonalinstance = db.PropertyPricingSeasonalInstances.Find(id);
            db.PropertyPricingSeasonalInstances.Remove(propertypricingseasonalinstance);
            db.SaveChanges();

            var propertypricingseasonalinstances = db.PropertyPricingSeasonalInstances.Include(p => p.Property).Include(p => p.PropertyPricingSeason).Where(x => x.PropertyID == id).ToList();
            return View("Index", propertypricingseasonalinstances);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}