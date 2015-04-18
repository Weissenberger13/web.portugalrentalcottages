using BootstrapVillas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BootstrapVillas.Controllers
{
    public class CustomerAPIController : ApiController
    {
        private PortugalVillasContext db = new PortugalVillasContext();


        [HttpGet]
        public List<EmailAddress> GetAllCustomerAndLoginEmailAddresses()
        {
            List<EmailAddress> emailList = new List<EmailAddress>();

            var customers = db.Customers.Select(x => x.EmailAddress).ToList();

            using (var userdb = new UsersContext())
            {
                var users = userdb.UserProfiles.Select(x => x.UserName).ToList();

                foreach (var item in users)
                {
                    emailList.Add(new EmailAddress
                    {
                        Email = item.ToLower().Trim()
                    });
                }

                foreach (var item in customers)
                {

                    if (item != null)
                    {
                        emailList.Add(new EmailAddress
                        {
                            Email = item.ToLower().Trim()
                        });
                    }
                }
            }


            return emailList;

        }


    }
}
