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

    public class AccountTransactionController : Controller
    {
        private PortugalVillasContext db = new PortugalVillasContext();

        //
        // GET: /AccountTransaction/

        [HttpGet]
        public ActionResult VoidTransaction(long id = 0)
        {
            var transaction = db.AccountTransactions.Find(id);

            transaction.Voided = true;
            transaction.WhoVoided = User.Identity.Name;
            transaction.VoidedDate = DateTime.Now;

            return RedirectToAction("PropertyAccountTransactionIndex");
        }

        public ActionResult Index()
        {
            var accounttransactions = db.AccountTransactions.Where(x => !x.BookingID.Equals(null)).Include(a => a.Booking).Include(a => a.BookingExtraSelection).Include(x => x.PropertyOwnerAccount.PropertyOwner.Properties);
            return View("Index", accounttransactions.ToList());
        }

        public ActionResult PropertyAccountTransactionIndex()
        {
            var created = PropertyOwnerAccount.CreateAccountsForOwnersWithoutAccount(db);

            var accounttransactions = db.AccountTransactions.Where(x => !x.BookingID.Equals(null)).Include(a => a.Booking).Include(a => a.BookingExtraSelection).Include(x => x.PropertyOwnerAccount.PropertyOwner.Properties);
            return View("Index", accounttransactions.ToList());
        }

        public ActionResult ThirdPartyAccountTransactionIndex()
        {
            var accounttransactions = db.AccountTransactions.Where(x => !x.BookingExtraSelectionID.Equals(null)).Include(a => a.Booking).Include(a => a.BookingExtraSelection).Include(x => x.PropertyOwnerAccount.PropertyOwner.Properties);
            return View("Index", accounttransactions.ToList());
        }

        //
        // GET: /AccountTransaction/Details/5

        public ActionResult Details(long id = 0)
        {
            AccountTransaction accounttransaction = db.AccountTransactions.Find(id);
            if (accounttransaction == null)
            {
                return HttpNotFound();
            }
            return View(accounttransaction);
        }

        //
        // GET: /AccountTransaction/Create

        public ActionResult Create()
        {
           

            var accounts = db.PropertyOwnerAccounts.Include(x => x.PropertyOwner).ToSafeReadOnlyCollection();
           

            ViewBag.PropertyOwnerAccount = accounts.Select(option => new SelectListItem
            {
                Text =
                    (option == null
                        ? "None"
                        : ("OwnerID:" + option.PropertyOwnerID + "||  Name:" + option.PropertyOwner.OwnerFirstName + " " + option.PropertyOwner.OwnerLastName
                        + "||  Owns (first only):" + option.PropertyOwner.Properties.DefaultIfEmpty(new Property { LegacyReference = "" }).First().LegacyReference
                        
                        )),
                Value = option.AccountID.ToString()
            });

            ViewBag.BookingID = new SelectList(db.Bookings, "BookingID", "BookingPRCReference");
            ViewBag.BookingExtraSelectionID = new SelectList(db.BookingExtraSelections, "BookingExtraSelectionID", "BookingExtraPRCReference");
            return View();
        }

        //
        // POST: /AccountTransaction/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AccountTransaction accounttransaction)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var created = PropertyOwnerAccount.CreateAccountsForOwnersWithoutAccount(db);

                    var booking = db.Bookings.Find(accounttransaction.BookingID);
                    var property =
                        db.Properties.Include(x => x.PropertyOwner.PropertyOwnerAccounts)
                            .Where(x => x.PropertyID == booking.PropertyID).FirstOrDefault();


                    accounttransaction.AccountID = property.PropertyOwner.PropertyOwnerAccounts.First().AccountID;
                    accounttransaction.WhenCreated = DateTime.Now;
                    accounttransaction.WhoCreated = User.Identity.Name;



                    db.AccountTransactions.Add(accounttransaction);
                    db.SaveChanges();
                    ViewBag.BookingID = new SelectList(db.Bookings, "BookingID", "BookingPRCReference", accounttransaction.BookingID);
                    ViewBag.BookingExtraSelectionID = new SelectList(db.BookingExtraSelections, "BookingExtraSelectionID", "BookingExtraPRCReference", accounttransaction.BookingExtraSelectionID);
                    return RedirectToAction("Create");
                }
                catch (Exception ex)
                {
                    
                    throw new Exception("Can't add a transaction!", ex);
                }
            }

            ViewBag.BookingID = new SelectList(db.Bookings, "BookingID", "BookingPRCReference", accounttransaction.BookingID);
            ViewBag.BookingExtraSelectionID = new SelectList(db.BookingExtraSelections, "BookingExtraSelectionID", "BookingExtraPRCReference", accounttransaction.BookingExtraSelectionID);
            return View(accounttransaction);
        }

        //
        // GET: /AccountTransaction/Edit/5

        public ActionResult Edit(long id = 0)
        {
            AccountTransaction accounttransaction = db.AccountTransactions.Find(id);
            if (accounttransaction == null)
            {
                return HttpNotFound();
            }

         
            var accounts = db.PropertyOwnerAccounts.Include(x => x.PropertyOwner).ToSafeReadOnlyCollection();


            ViewBag.PropertyOwnerAccount = accounts.Select(option => new SelectListItem
            {
                Text =
                    (option == null
                        ? "None"
                        : ("OwnerID:" + option.PropertyOwnerID + "||  Name:" + option.PropertyOwner.OwnerFirstName + " " + option.PropertyOwner.OwnerLastName
                        + "||  Owns (first only):" + option.PropertyOwner.Properties.DefaultIfEmpty(new Property { LegacyReference = "" }).First().LegacyReference

                        )),
                Value = option.AccountID.ToString()
            });

            ViewBag.BookingID = new SelectList(db.Bookings, "BookingID", "BookingPRCReference", accounttransaction.BookingID);
            ViewBag.BookingExtraSelectionID = new SelectList(db.BookingExtraSelections, "BookingExtraSelectionID", "BookingExtraPRCReference", accounttransaction.BookingExtraSelectionID);
            return View(accounttransaction);
        }

        //
        // POST: /AccountTransaction/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AccountTransaction accounttransaction)
        {
            if (ModelState.IsValid)
            {
                db.Entry(accounttransaction).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BookingID = new SelectList(db.Bookings, "BookingID", "BookingPRCReference", accounttransaction.BookingID);
            ViewBag.BookingExtraSelectionID = new SelectList(db.BookingExtraSelections, "BookingExtraSelectionID", "BookingExtraPRCReference", accounttransaction.BookingExtraSelectionID);
            return View(accounttransaction);
        }

        //
        // GET: /AccountTransaction/Delete/5

        public ActionResult Delete(long id = 0)
        {
            AccountTransaction accounttransaction = db.AccountTransactions.Find(id);
            if (accounttransaction == null)
            {
                return HttpNotFound();
            }
            return View(accounttransaction);
        }

        //
        // POST: /AccountTransaction/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            AccountTransaction accounttransaction = db.AccountTransactions.Find(id);
            db.AccountTransactions.Remove(accounttransaction);
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