using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Sockets;
using System.Web;
using System.Web.Razor.Parser;
using BootstrapVillas.Content.Classes;
using BootstrapVillas.Content.PartialClasses;

namespace BootstrapVillas.Models
{
    public partial class PropertyOwnerAccount
    {
        private static List<BookingAndRelatedTransactions> ReturnAllBookingsAndTransactions(long AccountID, PortugalVillasContext db)
        {
            var owner = db.PropertyOwnerAccounts.Where(x => x.AccountID == AccountID).First();
            var props = db.Properties.Where(x => x.PropertyOwnerID == owner.PropertyOwnerID).ToList();

            var bookings = new List<Booking>();

            foreach (var property in props)
            {
                bookings.AddRange(db.Bookings.Where(booking => booking.PropertyID == property.PropertyID).Include(x=>x.Property).ToList());
            }

    
            var transactions = db.AccountTransactions.Where(x => x.AccountID == AccountID).Where(x=>x.Voided != true);

            List<BookingAndRelatedTransactions> bookingAndTrans = new List<BookingAndRelatedTransactions>();

            foreach (var booking in bookings)
            {
                bookingAndTrans.Add(new BookingAndRelatedTransactions
                {
                    booking = booking,
                    transactions = transactions.Where(x=>x.BookingID == booking.BookingID).ToList()
                });
            }




            return bookingAndTrans;
        }

        public static List<BookingAndRelatedTransactions> GetBookingsWithPaymentsOutstanding(long AccountID, PortugalVillasContext db)
        {
            var bookingAndTrans = ReturnAllBookingsAndTransactions(AccountID, db);

            List<BookingAndRelatedTransactions> unpaidTrans = new List<BookingAndRelatedTransactions>();

            foreach (var booking in bookingAndTrans)
            {
                var sumOfTransactions = booking.transactions.Sum(x => x.TransactionAmount);

                if (sumOfTransactions < booking.booking.RemittanceAmount)
                {
                    unpaidTrans.Add(booking);
                }
            }
            return unpaidTrans;
        }

        public static List<BookingAndRelatedTransactions> GetBookingsPaid(long AccountID, PortugalVillasContext db)
        {
            var bookingAndTrans = ReturnAllBookingsAndTransactions(AccountID, db);

            List<BookingAndRelatedTransactions> paidTrans = new List<BookingAndRelatedTransactions>();

            foreach (var booking in bookingAndTrans)
            {
                var sumOfTransactions = booking.transactions.Sum(x => x.TransactionAmount);

                if (sumOfTransactions >= booking.booking.RemittanceAmount)
                {
                    paidTrans.Add(booking);
                }
            }
            return paidTrans;
        }


        public static int CreateAccountsForOwnersWithoutAccount(PortugalVillasContext db)
        {
            //get all ownersIDs
            var owners = db.PropertyOwners.ToList();
            //checks they all have an account is accounts
            var accounts = db.PropertyOwnerAccounts.ToList();

            List<PropertyOwnerAccount> accountsToCreate = new List<PropertyOwnerAccount>();

            foreach (var propertyOwner in owners)
            {
                var account = accounts.Where(x => x.PropertyOwnerID == propertyOwner.PropertyOwnerID).FirstOrDefault();
                if (account == null)
                {
                    db.PropertyOwnerAccounts.Add(new PropertyOwnerAccount
                    {
                        AccountBalance = 0.00M,
                        PropertyOwnerID = propertyOwner.PropertyOwnerID
                    });


                }
            }

            

            return db.SaveChanges();

        }


    }
}