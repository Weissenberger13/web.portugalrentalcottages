using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using BootstrapVillas.Models.ViewModels;
using Microsoft.Data.OData.Metadata;

namespace BootstrapVillas.Controllers
{
    public class BookingSyncController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public HttpResponseMessage Post([FromBody]BookingExternalSyncRequest bookingExternal)
        {

            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            else
            {
                if (HttpContext.Current.Request.Url.ToString() == "")
                {
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                //check it's legit

                //add to queue

               

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