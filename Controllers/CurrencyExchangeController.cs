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
    public class CurrencyExchangeController : Controller
    {
        private PortVillasContext db = new PortVillasContext();

        //
        // GET: /CurrencyExchange/

        public ActionResult Index()
        {
            return View(db.CurrencyExchanges.ToList());
        }

        //
        // GET: /CurrencyExchange/Details/5

        public ActionResult Details(int id = 0)
        {
            CurrencyExchange currencyexchange = db.CurrencyExchanges.Find(id);
            if (currencyexchange == null)
            {
                return HttpNotFound();
            }
            return View(currencyexchange);
        }

        //
        // GET: /CurrencyExchange/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /CurrencyExchange/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CurrencyExchange currencyexchange)
        {
            if (ModelState.IsValid)
            {
                db.CurrencyExchanges.Add(currencyexchange);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(currencyexchange);
        }

        //
        // GET: /CurrencyExchange/Edit/5

        public ActionResult Edit(int id = 0)
        {
            CurrencyExchange currencyexchange = db.CurrencyExchanges.Find(id);
            if (currencyexchange == null)
            {
                return HttpNotFound();
            }
            return View(currencyexchange);
        }

        //
        // POST: /CurrencyExchange/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CurrencyExchange currencyexchange)
        {
            if (ModelState.IsValid)
            {
                db.Entry(currencyexchange).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(currencyexchange);
        }

        //
        // GET: /CurrencyExchange/Delete/5

        public ActionResult Delete(int id = 0)
        {
            CurrencyExchange currencyexchange = db.CurrencyExchanges.Find(id);
            if (currencyexchange == null)
            {
                return HttpNotFound();
            }
            return View(currencyexchange);
        }

        //
        // POST: /CurrencyExchange/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CurrencyExchange currencyexchange = db.CurrencyExchanges.Find(id);
            db.CurrencyExchanges.Remove(currencyexchange);
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