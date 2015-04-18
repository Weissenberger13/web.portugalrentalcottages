using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BootstrapVillas.Models;
using WebGrease.Css.Extensions;

namespace BootstrapVillas.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class PropertyOwnerAccountController : Controller
    {
        private PortugalVillasContext db = new PortugalVillasContext();

        //
        // GET: /PropertyOwnerAccount/

        public ActionResult SelectFromList()
        {
            var accounts = db.PropertyOwnerAccounts.Include(x => x.PropertyOwner).ToSafeReadOnlyCollection();
            ViewBag.PropertyOwnerAccount = accounts.Select(option => new SelectListItem
            {
                Text =
                   (option == null
                       ? "None"
                       : ("OwnerID:" + option.PropertyOwnerID + "|| AccountID:" + option.AccountID + "||  Name:" + option.PropertyOwner.OwnerFirstName + " " + option.PropertyOwner.OwnerLastName
                       + "||  Owns (first only):" + option.PropertyOwner.Properties.DefaultIfEmpty(new Property { LegacyReference = "" }).First().LegacyReference

                       )),
                Value = option.AccountID.ToString()
            });

            
            return View();
        }


        [HttpPost]
        public ActionResult SelectFromList(long id)
        {
          var accounts = db.PropertyOwnerAccounts.Include(x => x.PropertyOwner).ToSafeReadOnlyCollection();          
          ViewBag.PropertyOwnerAccount = accounts.Select(option => new SelectListItem
            {
                Text =
                    (option == null
                        ? "None"
                        : ("OwnerID:" + option.PropertyOwnerID + "|| AccountID:" + option.AccountID + "||  Name:" + option.PropertyOwner.OwnerFirstName + " " + option.PropertyOwner.OwnerLastName
                        + "||  Owns (first only):" + option.PropertyOwner.Properties.DefaultIfEmpty(new Property { LegacyReference = "" }).First().LegacyReference
                        
                        )),
                Value = option.AccountID.ToString()
            });

            ViewBag.Transactions = db.AccountTransactions.Where(x => x.AccountID == id).ToSafeReadOnlyCollection();
            ViewBag.BookingsWTrans = PropertyOwnerAccount.GetBookingsWithPaymentsOutstanding(id, db);
            ViewBag.BookingsPaid = PropertyOwnerAccount.GetBookingsPaid(id, db);
            return View(accounts.First(x=>x.AccountID == id));
        }

        public ActionResult Index()
        {
            var propertyowneraccounts = db.PropertyOwnerAccounts.Include(p => p.PropertyOwner).Include(p => p.ThirdPartyService);
            return View(propertyowneraccounts.ToList());
        }


        public ActionResult PropertyOwnerIndex()
        {
            var propertyowneraccounts = db.PropertyOwnerAccounts.Where(x=>!x.PropertyOwnerID.Equals(null)).Include(p => p.PropertyOwner).Include(p => p.ThirdPartyService);
            return View("Index", propertyowneraccounts.ToList());
        }


        public ActionResult ThirdPartyServiceIndex()
        {
            var propertyowneraccounts = db.PropertyOwnerAccounts.Where(x => !x.ThirdPartyServiceID.Equals(null)).Include(p => p.PropertyOwner).Include(p => p.ThirdPartyService);
            return View("Index", propertyowneraccounts.ToList());
        }

        //
        // GET: /PropertyOwnerAccount/Details/5

        public ActionResult Details(long id = 0)
        {
            PropertyOwnerAccount propertyowneraccount = db.PropertyOwnerAccounts.Find(id);
            if (propertyowneraccount == null)
            {
                return HttpNotFound();
            }
            return View(propertyowneraccount);
        }

        //
        // GET: /PropertyOwnerAccount/Create

        public ActionResult Create()
        {
            ViewBag.PropertyOwnerID = new SelectList(db.PropertyOwners, "PropertyOwnerID", "PropertyOwnerEmailAddress");
            ViewBag.ThirdPartyServiceID = new SelectList(db.ThirdPartyServices, "ThirdPartyServiceID", "ThirdPartyServiceName");
            return View();
        }

        //
        // POST: /PropertyOwnerAccount/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PropertyOwnerAccount propertyowneraccount)
        {
            if (ModelState.IsValid)
            {
                db.PropertyOwnerAccounts.Add(propertyowneraccount);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PropertyOwnerID = new SelectList(db.PropertyOwners, "PropertyOwnerID", "PropertyOwnerEmailAddress", propertyowneraccount.PropertyOwnerID);
            ViewBag.ThirdPartyServiceID = new SelectList(db.ThirdPartyServices, "ThirdPartyServiceID", "ThirdPartyServiceName", propertyowneraccount.ThirdPartyServiceID);
            return View(propertyowneraccount);
        }

        //
        // GET: /PropertyOwnerAccount/Edit/5

        public ActionResult Edit(long id = 0)
        {
            PropertyOwnerAccount propertyowneraccount = db.PropertyOwnerAccounts.Find(id);
            if (propertyowneraccount == null)
            {
                return HttpNotFound();
            }
            ViewBag.PropertyOwnerID = new SelectList(db.PropertyOwners, "PropertyOwnerID", "PropertyOwnerEmailAddress", propertyowneraccount.PropertyOwnerID);
            ViewBag.ThirdPartyServiceID = new SelectList(db.ThirdPartyServices, "ThirdPartyServiceID", "ThirdPartyServiceName", propertyowneraccount.ThirdPartyServiceID);
            return View(propertyowneraccount);
        }

        //
        // POST: /PropertyOwnerAccount/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PropertyOwnerAccount propertyowneraccount)
        {
            if (ModelState.IsValid)
            {
                db.Entry(propertyowneraccount).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PropertyOwnerID = new SelectList(db.PropertyOwners, "PropertyOwnerID", "PropertyOwnerEmailAddress", propertyowneraccount.PropertyOwnerID);
            ViewBag.ThirdPartyServiceID = new SelectList(db.ThirdPartyServices, "ThirdPartyServiceID", "ThirdPartyServiceName", propertyowneraccount.ThirdPartyServiceID);
            return View(propertyowneraccount);
        }

        //
        // GET: /PropertyOwnerAccount/Delete/5

        public ActionResult Delete(long id = 0)
        {
            PropertyOwnerAccount propertyowneraccount = db.PropertyOwnerAccounts.Find(id);
            if (propertyowneraccount == null)
            {
                return HttpNotFound();
            }
            return View(propertyowneraccount);
        }

        //
        // POST: /PropertyOwnerAccount/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            PropertyOwnerAccount propertyowneraccount = db.PropertyOwnerAccounts.Find(id);
            db.PropertyOwnerAccounts.Remove(propertyowneraccount);
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