using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BootstrapVillas.Controllers
{
    public class EndOfProcessController : Controller
    {
        //
        // GET: /EndOfProcess/

        public ActionResult EndOfBookingProcess()
        {

            ViewBag.Keywords = "";
            ViewBag.Title = "Thank you for booking a Portugal holiday with us at Portugal Rental Cottages. We hope you took some time to check our our car rentals, as well as our selection of villas, townhouses apartments and hotels in portugal and across the silver coast. Book your villa in Obidos, Nazaré, Foz do Arelho or A-dos-Negros for a seaside holiday or rural townhouse with swimming pool and Internet. All types of villa, apartment are catered for in regions like Foz do Arelho, Obidos, Alfeizerão - São Martinho and Reguengo Grande. Stay in the lovely Salir do Porto and visit the Mafra Palace & Obidos during your vacation. With Portugal Rental Cottages, you can be up directly from the airport with our rental extras. While you stay, go on wine tasting tours to the beautiful Sanguinhal Estate and get picked up from the airport with our airport to villa transfer and return.";
            return View();
        }

    }
}
