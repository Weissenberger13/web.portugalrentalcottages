using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using BootstrapVillas.Models;

namespace BootstrapVillas.Controllers
{
    public class TestAjaxController : Controller
    {
        //
        // GET: /TestAjax/

        public ActionResult Index()
        {
            using (var _db = new PortVillasContext())
            {
                var data = _db.Customers.ToList();

                ViewData["currentCustomer"] = _db.Customers.Where(x => x.CustomerID == 1).FirstOrDefault();


                return View(data);
            }


        }

    
       /* [HttpGet]
        public ActionResult GetACustomer(int CustomerID)
        {
            using (var _db = new PortVillasContext())
            {
                var model = _db.Customers.Where(x => x.CustomerID == CustomerID).FirstOrDefault();
                ViewData["currentCustomer"] = model;

                return PartialView("CustomerEdit", model);
            }

        }*/


        [HttpGet]
        public ActionResult GetACustomer(int CustomerID)
        {
            using (var _db = new PortVillasContext())
            {
                var data = _db.Customers.Where(x => x.CustomerID == CustomerID).FirstOrDefault();
                ViewData["currentCustomer"] = data;

                return PartialView("CustomerEdit", data);
            }
        }
    

       

        public ActionResult TestAjaxView()
        {

            var cus = new Customer
            {
                FirstName = "Test",
                LastName = "test"

            };
            ViewBag.Message = "This is a AJAX form";

             
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Customer customer)
        {

            if (ModelState.IsValid)
            {
                using (var db = new PortVillasContext())
                {
                    db.Entry(customer).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }


        [HttpPost]
        public ActionResult ProcessForm(Customer obj)
        {


            using (var _db = new PortVillasContext())
            {
                var data = _db.Customers.Where(x => x.CustomerID == obj.CustomerID).SingleOrDefault();

                ViewData["currentCustomer"] = data;

                return PartialView("CustomerEdit", data);
            }



        }


        [HttpPost]
        public ActionResult ProcessLink()
        {
            var cus = new Customer
            {
                CustomerID = 10000,
                FirstName = "Zog"
            };

            return View("Index", cus);
        }

    }
}
