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
    public class CustomerController : Controller
    {
        private PortugalVillasContext db = new PortugalVillasContext();


        public ActionResult ShowAllRelatedBookingsAndExtras(long customerID)
        {
            ViewBag.Bookings =
                db.Bookings.Where(x => x.CustomerID.Equals(customerID)).OrderByDescending(W => W.CreationDate).ToList();
            ViewBag.Bes = db.BookingExtraSelections.Where(x => x.CustomerID.Equals(customerID)).OrderByDescending(W => W.WhenCreated).ToList();

            return View();
        }

        //
        // GET: /Customer/

        public ActionResult Index()
        {
            return View(db.Customers.ToList());
        }

        //
        // GET: /Customer/Details/5

        public ActionResult Details(long id = 0)
        {
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        //
        // GET: /Customer/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Customer/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Customer customer)
        {
            customer.CreationDate = DateTime.Now;
            if (customer.Country.ToLower() == "united kingdom")
            {
                customer.PreferredCurrency = "GBP";
                customer.PreferredCurrencySymbol = "£";
            }
            else
            {
                customer.PreferredCurrency = "EUR";
                customer.PreferredCurrencySymbol = "€";
            }

            db.Customers.Add(customer);
            db.SaveChanges();
            return RedirectToAction("Edit", customer);


            return View(customer);
        }

        //
        // GET: /Customer/Edit/5

        public ActionResult Edit(long customerId = 0)
        {
            Customer customer = db.Customers.Find(customerId);
            Session["currentCustomer"] = customer;

            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        //
        // POST: /Customer/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Customer customer)
        {


            var oldCust = db.Customers.Where(x => x.CustomerID == customer.CustomerID).First();
            customer.CreationDate = customer.CreationDate;


            if (ModelState.IsValid)
            {
                using (var _db = new PortugalVillasContext())
                {
                    _db.Customers.Attach(customer);
                    _db.Entry(customer).State = EntityState.Modified;
                    _db.SaveChanges();
                    Session["currentCustomer"] = customer;
                    return RedirectToAction("Edit", customer);
                }

            }
            return View(customer);
        }

        //
        // GET: /Customer/Delete/5

        public ActionResult Delete(long id = 0)
        {
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        //
        // POST: /Customer/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
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