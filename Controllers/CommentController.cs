using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BootstrapVillas.Models;
using Microsoft.Ajax.Utilities;

namespace BootstrapVillas.Controllers
{
    public class CommentController : Controller
    {
        private PortugalVillasContext db = new PortugalVillasContext();

        //
        // GET: /Comment/

        public ActionResult Index()
        {
            var comments = db.Comments.Include(c => c.Property);
            return View(comments.ToList());
        }

        //
        // GET: /Comment/Details/5

        public ActionResult Details(long id = 0)
        {
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        //
        // GET: /Comment/Create
      
        public ActionResult Create()
        {
            
            ViewBag.PropertyID = new SelectList(db.Properties, "PropertyID", "LegacyReference");
            return View(new Comment());
        }

        //
        // POST: /Comment/Create

     
        [HttpPost]      
        public void Create(FormCollection theFormCollection)
        {

            Comment comment = new Comment();

            comment.WhenCreated = DateTime.Now;
            comment.Username = theFormCollection["Username"];
            comment.StarRating = 1;
            comment.PropertyID = Convert.ToInt64(theFormCollection["PropertyID"]);

            try
            {
                var date = DateTime.ParseExact(theFormCollection["StartdateOfStay"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                comment.StartdateOfStay = date;
            }
            catch (Exception ex)
            {
                
                
            }
            if (theFormCollection["StarRating"].ToString() != "")
            {
                comment.StarRating = Convert.ToInt32(theFormCollection["StarRating"]);
            }
            
            comment.Text = theFormCollection["Text"];


            try
            {
                if (ModelState.IsValid)
                {
                    comment.Approved = true;
                    //comment.WhenCreated = DateTime.Now;

                    db.Comments.Add(comment);
                    db.SaveChanges();


                }
            }
            catch (DbEntityValidationException dataValidationException)
            {
                //do nothing                
            }
            catch (Exception ex)
            {
                throw ex;
            }

            string RootURL = Request.Url.Authority;
            RootURL += "/Home/FullPropertyResult?PropertyID=" + comment.PropertyID;

            Response.Redirect("http://" + RootURL);
           
        }

        //
        // GET: /Comment/Edit/5

        public ActionResult Edit(long id = 0)
        {
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            ViewBag.PropertyID = new SelectList(db.Properties, "PropertyID", "LegacyReference", comment.PropertyID);
            return View(comment);
        }

        //
        // POST: /Comment/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Comment comment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(comment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PropertyID = new SelectList(db.Properties, "PropertyID", "LegacyReference", comment.PropertyID);
            return View(comment);
        }

        //
        // GET: /Comment/Delete/5

        public ActionResult Delete(long id = 0)
        {
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        //
        // POST: /Comment/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Comment comment = db.Comments.Find(id);
            db.Comments.Remove(comment);
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