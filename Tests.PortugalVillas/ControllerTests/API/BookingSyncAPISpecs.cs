using BootstrapVillas.Controllers;
using BootstrapVillas.Models;
using NUnit.Core;
using NUnit.Framework;
using SpecsFor;
using Should;

namespace Tests.PortugalVillas.ControllerTests.API
{
    public class BookingSyncAPISpecs
    {
        [TestFixture]
        public class when_creating_new_booking
        {
            private PortVillasContext db;
            private 

            [SetUp]
            protected void TestSetup()
            {
                db = new PortVillasContext();

            }

            [Test]
            protected  void ShouldAddNewBooking()
            {
                

                
            }
        }
    }
}