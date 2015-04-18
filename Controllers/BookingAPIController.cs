using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using BootstrapVillas.Models;

namespace BootstrapVillas.Controllers
{
    public class BookingAPIController : ApiController
    {
        private PortugalVillasContext db = new PortugalVillasContext();

        // GET api/BookingAPI

     


        public Booking SetBookingPrices(Booking booking)
        {
            var prop = db.Properties.Find(booking.PropertyID);             
            booking.Property = prop;

            // new FinalBookingDetailGatheringController().CreateBooking(booking, prop, db);

            return booking;
        }
        

  
        public IEnumerable<Booking> GetBookings()
        {
            var bookings = db.Bookings.Include(b => b.BookingParentContainer).Include(b => b.Case).Include(b => b.Customer).Include(b => b.Property);
            return bookings.AsEnumerable();
        }

        // GET api/BookingAPI/5
        public Booking GetBooking(long id)
        {
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return booking;
        }

        // PUT api/BookingAPI/5
        public HttpResponseMessage PutBooking(long id, Booking booking)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != booking.BookingID)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(booking).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // POST api/BookingAPI
        public HttpResponseMessage PostBooking(Booking booking)
        {
            if (ModelState.IsValid)
            {
                db.Bookings.Add(booking);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, booking);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = booking.BookingID }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/BookingAPI/5
        public HttpResponseMessage DeleteBooking(long id)
        {
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.Bookings.Remove(booking);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, booking);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}