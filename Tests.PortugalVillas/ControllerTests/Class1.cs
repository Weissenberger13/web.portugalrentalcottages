using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using BootstrapVillas.Controllers;
using NUnit.Framework;
using Should;
using SpecsFor;


namespace Tests.PortugalVillas
{

    public class HomeControllerSpecs
    {
        public class when_testing_HomeController : SpecsFor<HomeController>
        {
            private ActionResult _result;

            protected override void When()
            {
                _result = SUT.Contact();
            }

            [Test]
            public void should_return_Viewresult()
            {
                _result.ShouldBeType<ViewResult>();

            }


        }
    }
}
