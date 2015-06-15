using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using BootstrapVillas.Controllers;
using BootstrapVillas.Interfaces;
using BootstrapVillas.Models;
using Moq;
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
            private Mock<ICurrencyConvertable> mock; 

            protected override void Given()
            {
                mock = GetMockFor<ICurrencyConvertable>();
                mock.Setup(x => x.BookingCurrencyExchangeRate).Returns(2.00M);
            }

            protected override void When()
            {
                _result = SUT.Contact();
                
            }

            [Test]
            public void should_return_Viewresult()
            {
                _result.ShouldBeType<ViewResult>();

            }

            [Test]
            public void should_return_2()
            {
                Assert.That(mock.Object.BookingCurrencyExchangeRate, Is.EqualTo(2.00M));

            }

           }

       
    }
}
