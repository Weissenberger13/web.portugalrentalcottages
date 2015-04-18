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
    public class CustomerBankDetailController : Controller
    {
        private PortugalVillasContext db = new PortugalVillasContext();

        //
        // GET: /CustomerBankDetail/

        public ActionResult Index(long customerId)
        {
            return View(db.CustomerBankDetails.Where(c => c.CustomerID == customerId).ToList());
        }

        //
        // GET: /CustomerBankDetail/Details/5

        public ActionResult Details(long id = 0)
        {
            CustomerBankDetail customerbankdetail = db.CustomerBankDetails.Find(id);
            if (customerbankdetail == null)
            {
                return HttpNotFound();
            }
            return View(customerbankdetail);
        }

        //
        // GET: /CustomerBankDetail/Create

        public ActionResult Create()
        {
            var customer = (Customer)Session["currentCustomer"];
            ViewBag.customerID = customer.CustomerID;

            return View();
        }

        //
        // POST: /CustomerBankDetail/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CustomerBankDetail customerbankdetail)
        {
            if (ModelState.IsValid)
            {
                db.CustomerBankDetails.Add(customerbankdetail);
                db.SaveChanges();

                return RedirectToAction("Index", "CustomerBankDetail", new { customerId = customerbankdetail.CustomerID });
            }

            return View(customerbankdetail);
        }

        //
        // GET: /CustomerBankDetail/Edit/5

        public ActionResult Edit(long id = 0)
        {
            CustomerBankDetail customerbankdetail = db.CustomerBankDetails.Find(id);
            if (customerbankdetail == null)
            {
                return HttpNotFound();
            }
            return View(customerbankdetail);
        }

        //
        // POST: /CustomerBankDetail/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CustomerBankDetail customerbankdetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customerbankdetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "CustomerBankDetail", new { customerId = customerbankdetail.CustomerID });
            }
            return View(customerbankdetail);
        }

        //
        // GET: /CustomerBankDetail/Delete/5

        public ActionResult Delete(long id = 0)
        {
            CustomerBankDetail customerbankdetail = db.CustomerBankDetails.Find(id);
            if (customerbankdetail == null)
            {
                return HttpNotFound();
            }
            db.CustomerBankDetails.Remove(customerbankdetail);
            db.SaveChanges();
            return RedirectToAction("Index", "CustomerBankDetail", new { customerId = customerbankdetail.CustomerID });
        }

        //
        // POST: /CustomerBankDetail/Delete/5
        /*
                [HttpPost, ActionName("Delete")]
                [ValidateAntiForgeryToken]
                public ActionResult DeleteConfirmed(long id)
                {
                    CustomerBankDetail customerbankdetail = db.CustomerBankDetails.Find(id);
                    db.CustomerBankDetails.Remove(customerbankdetail);
                    db.SaveChanges();
                    return RedirectToAction("Edit", "Customer");
                }*/

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}