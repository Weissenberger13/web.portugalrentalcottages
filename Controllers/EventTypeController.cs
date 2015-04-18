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
    public class EventTypeController : Controller
    {
        private PortugalVillasContext db = new PortugalVillasContext();

        //
        // GET: /EventType/

        public ActionResult Index()
        {
            var eventtypes = db.EventTypes.Include(e => e.EventSchemeType).Include(e => e.EventSubType);
            return View(eventtypes.ToList());
        }

        //
        // GET: /EventType/Details/5

        public ActionResult Details(long id = 0)
        {
            EventType eventtype = db.EventTypes.Find(id);
            if (eventtype == null)
            {
                return HttpNotFound();
            }
            return View(eventtype);
        }

        //
        // GET: /EventType/Create

        public ActionResult Create()
        {
            ViewBag.EventSchemeTypeID = new SelectList(db.EventSchemeTypes, "EventSchemeTypeID", "EventSchemeTypeName");
            ViewBag.EventSubTypeID = new SelectList(db.EventSubTypes, "EventSubTypeID", "EventSubTypeName");
            return View();
        }

        //
        // POST: /EventType/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EventType eventtype)
        {
            if (ModelState.IsValid)
            {
                db.EventTypes.Add(eventtype);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EventSchemeTypeID = new SelectList(db.EventSchemeTypes, "EventSchemeTypeID", "EventSchemeTypeName", eventtype.EventSchemeTypeID);
            ViewBag.EventSubTypeID = new SelectList(db.EventSubTypes, "EventSubTypeID", "EventSubTypeName", eventtype.EventSubTypeID);
            return View(eventtype);
        }

        //
        // GET: /EventType/Edit/5

        public ActionResult Edit(long id = 0)
        {
            EventType eventtype = db.EventTypes.Find(id);
            if (eventtype == null)
            {
                return HttpNotFound();
            }
            ViewBag.EventSchemeTypeID = new SelectList(db.EventSchemeTypes, "EventSchemeTypeID", "EventSchemeTypeName", eventtype.EventSchemeTypeID);
            ViewBag.EventSubTypeID = new SelectList(db.EventSubTypes, "EventSubTypeID", "EventSubTypeName", eventtype.EventSubTypeID);
            return View(eventtype);
        }

        //
        // POST: /EventType/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EventType eventtype)
        {
            if (ModelState.IsValid)
            {
                db.Entry(eventtype).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EventSchemeTypeID = new SelectList(db.EventSchemeTypes, "EventSchemeTypeID", "EventSchemeTypeName", eventtype.EventSchemeTypeID);
            ViewBag.EventSubTypeID = new SelectList(db.EventSubTypes, "EventSubTypeID", "EventSubTypeName", eventtype.EventSubTypeID);
            return View(eventtype);
        }

        //
        // GET: /EventType/Delete/5

        public ActionResult Delete(long id = 0)
        {
            EventType eventtype = db.EventTypes.Find(id);
            if (eventtype == null)
            {
                return HttpNotFound();
            }
            return View(eventtype);
        }

        //
        // POST: /EventType/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            EventType eventtype = db.EventTypes.Find(id);
            db.EventTypes.Remove(eventtype);
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