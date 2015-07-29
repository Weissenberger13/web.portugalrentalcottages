using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using BootstrapVillas.Models;
using BootstrapVillas.Models.ViewModels;
using Microsoft.Data.OData.Metadata;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BootstrapVillas.Controllers
{
    public class BookingSyncController : ApiController
    {
        PortugalVillasContext db = new PortugalVillasContext();

   

        // GET api/<controller>/5
        public IEnumerable<BookingExternalSyncRequest> Get()
        {
            var bookingsthisSystem = Booking.ToBookingExternalSyncRequests(db.Bookings.Where(x => x.Confirmed == true && x.Cancelled == false && x.StartDate > (DateTime)DateTime.Now).ToList()) as List<BookingExternalSyncRequest>;


            bookingsthisSystem.AddRange(
                BookingExternal.ToBookingExternalSyncRequests(db.BookingExternals.Where(x=>(DateTime)x.StartDate > (DateTime)DateTime.Now).ToList())               
               );

            return bookingsthisSystem;
        }

        // POST api/<controller>
        public HttpResponseMessage Post([FromBody]string value)
        {

            var test = value;
     

            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            else
            {

               /* var systems = db.PortugalSystem.ToList();
                foreach (var system in systems)
                {
                    if (HttpContext.Current.Request.Url.ToString().Trim().ToLower() == system.URL.Trim().ToLower())
                    {
                        var prop = Property.GetPropertyByLegacyReference(bookingExternal.PropertyReference);
                        //add the external booking

                        if (prop != null)
                        {
                            db.BookingExternals.Add(new BookingExternal
                            {
                                StartDate = bookingExternal.StartDate,
                                EndDate = bookingExternal.EndDate,
                                PortugalSystem = system,
                                Property = prop,
                                Notes = "Created by the system at "+ DateTime.Now + " from" + system.URL
                            });

                            return Request.CreateResponse(HttpStatusCode.OK);

                        }
                     
                    }
                }
                
                //check it's legit

                //add to queue

               */

            }

            return Request.CreateResponse(HttpStatusCode.BadRequest);


        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}