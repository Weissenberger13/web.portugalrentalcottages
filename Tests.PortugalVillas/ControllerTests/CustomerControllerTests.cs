using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using BootstrapVillas.Controllers;
using BootstrapVillas.Models;
using Moq;
using NUnit.Framework;
using Should;
using SpecsFor;
using ExpectedObjects;
using System.Web;
using System.Web.Routing;
using SpecsFor.Helpers.Web;
using SpecsFor.Helpers.Web.Mvc;
using Microsoft.Web.Mvc;

namespace Tests.PortugalVillas.ControllerTests
{
    class CustomerControllerSpec
    {

        public class when_creating_a_new_customer : SpecsFor<CustomerController>
        {
            private ActionResult result;
            private Customer customer;


            protected override void Given()
            {
                base.Given();
            }


            protected override void When()
            {
                customer = new Customer
                {
                    EmailAddress = "testserson",
                    FirstName = "test",
                    LastName = "test",
                    Country = "test"
                };

                result = SUT.Create(customer);

            }


            [Test]
            public void then_redirects_to_action()
            {

                var redirect = (RedirectToRouteResult) result;
                var test = redirect.RouteValues["action"];

                test.ShouldEqual("Edit");

            }

          




        }

    }
}
