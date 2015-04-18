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
    public class CommentReplyController : Controller
    {
        private PortugalVillasContext db = new PortugalVillasContext();

        //
        // GET: /CommentReply/

        public ActionResult Index()
        {
            var commentreplies = db.CommentReplies.Include(c => c.Comment);
            return View(commentreplies.ToList());
        }

        //
        // GET: /CommentReply/Details/5

        public ActionResult Details(long id = 0)
        {
            CommentReply commentreply = db.CommentReplies.Find(id);
            if (commentreply == null)
            {
                return HttpNotFound();
            }
            return View(commentreply);
        }

        //
        // GET: /CommentReply/Create
        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public ActionResult Create(int commentID)
        {

            ViewBag.CurrentCommentID = commentID;
            return View();
        }

        //
        // POST: /CommentReply/Create

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public ActionResult Create(CommentReply commentreply)
        {
            if (ModelState.IsValid)
            {
                commentreply.Approved = true;
                db.CommentReplies.Add(commentreply);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }

            ViewBag.CommentID = new SelectList(db.Comments, "CommentID", "Username", commentreply.CommentID);
            return View(commentreply);
        }

        //
        // GET: /CommentReply/Edit/5

        public ActionResult Edit(long id = 0)
        {
            CommentReply commentreply = db.CommentReplies.Find(id);
            if (commentreply == null)
            {
                return HttpNotFound();
            }
            ViewBag.CommentID = new SelectList(db.Comments, "CommentID", "Username", commentreply.CommentID);
            return View(commentreply);
        }

        //
        // POST: /CommentReply/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CommentReply commentreply)
        {
            if (ModelState.IsValid)
            {
                db.Entry(commentreply).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CommentID = new SelectList(db.Comments, "CommentID", "Username", commentreply.CommentID);
            return View(commentreply);
        }

        //
        // GET: /CommentReply/Delete/5

        public ActionResult Delete(long id = 0)
        {
            CommentReply commentreply = db.CommentReplies.Find(id);
            if (commentreply == null)
            {
                return HttpNotFound();
            }
            return View(commentreply);
        }

        //
        // POST: /CommentReply/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            CommentReply commentreply = db.CommentReplies.Find(id);
            db.CommentReplies.Remove(commentreply);
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