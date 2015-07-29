using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using BootstrapVillas.Controllers;
using BootstrapVillas.Models.ViewModels;
using NUnit.Framework;
using Should;
using TechTalk.SpecFlow.BindingSkeletons;

namespace Tests.PortugalVillas.ControllerTests.APITests
{


    [TestFixture]
    public class BookingSyncTests
    {
        private BookingSyncController SUT;



        [SetUp]
        public void Setup()
        {
            SUT = new BookingSyncController();

        }

        public void ShouldAddNewBookingExternal()
        {
            var bookingRequest = new BookingExternalSyncRequest
            {
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(-5),
                };

            /*var result = SUT.Post(bookingRequest);
           result.StatusCode.ShouldEqual(HttpStatusCode.Created);*/

            
        }








    }
}
