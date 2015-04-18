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
    public class PropertyController : Controller
    {
        private PortugalVillasContext db = new PortugalVillasContext();

        //
        // GET: /Property/

        public ActionResult SinglePropertyIndex(long propertyID)
        {
            var property = db.Properties.Include(p => p.PropertyOwnerRepresentative).Include(p => p.PropertyOwner).Include(p => p.PropertyTown).Include(p => p.PropertyType).Include(p => p.PropertyVacationType).Include(p => p.PropertyPricingSeasonalInstances).Where(p=>p.PropertyID.Equals(propertyID)).FirstOrDefault();

            //from edit
            if (property == null)
            {
                return HttpNotFound();
            }
            ViewBag.PropertyOwnerRepresentativeID = new SelectList(db.PropertyOwnerRepresentatives, "PropertyOwnerRepresentativeID", "PropertyOwnerRepresentativeName", property.PropertyOwnerRepresentativeID);
            ViewBag.PropertyOwnerID = new SelectList(db.PropertyOwners, "PropertyOwnerID", "PropertyOwnerEmailAddress", property.PropertyOwnerID);
            ViewBag.PropertyTownID = new SelectList(db.PropertyTowns, "PropertyTownID", "TownName", property.PropertyTownID);
            ViewBag.PropertyTypeID = new SelectList(db.PropertyTypes, "PropertyTypeID", "PropertyTypeName", property.PropertyTypeID);
            ViewBag.PropertyVacationTypeID = new SelectList(db.PropertyVacationTypes, "PropertyVacationTypeID", "PropertyVacationTypeDescription", property.PropertyVacationTypeID);

            //pricing
            ViewBag.PropertyPricing = property.PropertyPricingSeasonalInstances.ToList();

            return View(property);
        }

        public ActionResult Index()
        {
            var properties = db.Properties.Include(p => p.PropertyOwnerRepresentative).Include(p => p.PropertyOwner).Include(p => p.PropertyTown).Include(p => p.PropertyType).Include(p => p.PropertyVacationType).Include(p=>p.PropertyPricingSeasonalInstances);
            return View(properties.ToList());
        }

        //
        // GET: /Property/Details/5

        public ActionResult Details(long id = 0)
        {
            Property property = db.Properties.Find(id);
            if (property == null)
            {
                return HttpNotFound();
            }
            return View(property);
        }

        //
        // GET: /Property/Create

        public ActionResult Create()
        {
            ViewBag.PropertyOwnerRepresentativeID = new SelectList(db.PropertyOwnerRepresentatives, "PropertyOwnerRepresentativeID", "PropertyOwnerRepresentativeName");
            ViewBag.PropertyOwnerID = new SelectList(db.PropertyOwners, "PropertyOwnerID", "PropertyOwnerEmailAddress");
            ViewBag.PropertyTownID = new SelectList(db.PropertyTowns, "PropertyTownID", "TownName");
            ViewBag.PropertyTypeID = new SelectList(db.PropertyTypes, "PropertyTypeID", "PropertyTypeName");
            ViewBag.PropertyVacationTypeID = new SelectList(db.PropertyVacationTypes, "PropertyVacationTypeID", "PropertyVacationTypeDescription");
            return View();
        }

        //
        // POST: /Property/Create

        [HttpPost, ValidateInput(false)]
        public ActionResult Create(Property property)
        {
            property.WhenCreated = DateTime.Now;

           
                db.Properties.Add(property);
                db.SaveChanges();
                ViewBag.PropertyOwnerRepresentativeID = new SelectList(db.PropertyOwnerRepresentatives, "PropertyOwnerRepresentativeID", "PropertyOwnerRepresentativeName", property.PropertyOwnerRepresentativeID);
                ViewBag.PropertyOwnerID = new SelectList(db.PropertyOwners, "PropertyOwnerID", "PropertyOwnerEmailAddress", property.PropertyOwnerID);
                ViewBag.PropertyTownID = new SelectList(db.PropertyTowns, "PropertyTownID", "TownName", property.PropertyTownID);
                ViewBag.PropertyTypeID = new SelectList(db.PropertyTypes, "PropertyTypeID", "PropertyTypeName", property.PropertyTypeID);
                ViewBag.PropertyVacationTypeID = new SelectList(db.PropertyVacationTypes, "PropertyVacationTypeID", "PropertyVacationTypeDescription", property.PropertyVacationTypeID);
                return RedirectToAction("BlankDashboard", "Admin");
                  
        }

        //
        // GET: /Property/Edit/5

        public ActionResult Edit(long id = 0)
        {
            Property property = db.Properties.Find(id);
            if (property == null)
            {
                return HttpNotFound();
            }
            ViewBag.PropertyOwnerRepresentativeID = new SelectList(db.PropertyOwnerRepresentatives, "PropertyOwnerRepresentativeID", "PropertyOwnerRepresentativeName", property.PropertyOwnerRepresentativeID);
            ViewBag.PropertyOwnerID = new SelectList(db.PropertyOwners, "PropertyOwnerID", "PropertyOwnerEmailAddress", property.PropertyOwnerID);
            ViewBag.PropertyTownID = new SelectList(db.PropertyTowns, "PropertyTownID", "TownName", property.PropertyTownID);
            ViewBag.PropertyTypeID = new SelectList(db.PropertyTypes, "PropertyTypeID", "PropertyTypeName", property.PropertyTypeID);
            ViewBag.PropertyVacationTypeID = new SelectList(db.PropertyVacationTypes, "PropertyVacationTypeID", "PropertyVacationTypeDescription", property.PropertyVacationTypeID);
            return View(property);
        }

        //
        // POST: /Property/Edit/5

    
        [HttpPost, ValidateInput(false)]
        public ActionResult Edit(Property property)
        {
            var oldProp = db.Properties.Where(x => x.PropertyID == property.PropertyID).FirstOrDefault();

            property.WhenCreated = oldProp.WhenCreated;
            property.WhenUpdated = oldProp.WhenUpdated;

            if (ModelState.IsValid)
            {

                using (var _db = new PortugalVillasContext())
                {
                    _db.Properties.Attach(property);
                    _db.Entry(property).State = EntityState.Modified;
                    _db.SaveChanges();
                    ViewBag.PropertyOwnerRepresentativeID = new SelectList(db.PropertyOwnerRepresentatives, "PropertyOwnerRepresentativeID", "PropertyOwnerRepresentativeName", property.PropertyOwnerRepresentativeID);
                    ViewBag.PropertyOwnerID = new SelectList(db.PropertyOwners, "PropertyOwnerID", "PropertyOwnerEmailAddress", property.PropertyOwnerID);
                    ViewBag.PropertyTownID = new SelectList(db.PropertyTowns, "PropertyTownID", "TownName", property.PropertyTownID);
                    ViewBag.PropertyTypeID = new SelectList(db.PropertyTypes, "PropertyTypeID", "PropertyTypeName", property.PropertyTypeID);
                    ViewBag.PropertyVacationTypeID = new SelectList(db.PropertyVacationTypes, "PropertyVacationTypeID", "PropertyVacationTypeDescription", property.PropertyVacationTypeID);
                    return View("SinglePropertyIndex", property);
                }
            }
            ViewBag.PropertyOwnerRepresentativeID = new SelectList(db.PropertyOwnerRepresentatives, "PropertyOwnerRepresentativeID", "PropertyOwnerRepresentativeName", property.PropertyOwnerRepresentativeID);
            ViewBag.PropertyOwnerID = new SelectList(db.PropertyOwners, "PropertyOwnerID", "PropertyOwnerEmailAddress", property.PropertyOwnerID);
            ViewBag.PropertyTownID = new SelectList(db.PropertyTowns, "PropertyTownID", "TownName", property.PropertyTownID);
            ViewBag.PropertyTypeID = new SelectList(db.PropertyTypes, "PropertyTypeID", "PropertyTypeName", property.PropertyTypeID);
            ViewBag.PropertyVacationTypeID = new SelectList(db.PropertyVacationTypes, "PropertyVacationTypeID", "PropertyVacationTypeDescription", property.PropertyVacationTypeID);
            return View("SinglePropertyIndex", property);
        }

        //
        // GET: /Property/Delete/5

        public ActionResult Delete(long id = 0)
        {
            Property property = db.Properties.Find(id);
            if (property == null)
            {
                return HttpNotFound();
            }
            return View(property);
        }

        //
        // POST: /Property/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Property property = db.Properties.Find(id);
            db.Properties.Remove(property);
            db.SaveChanges();
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}