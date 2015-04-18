using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BootstrapVillas.Content.Classes;
using BootstrapVillas.Models;

namespace BootstrapVillas.Controllers
{

    [Authorize]
    public class AdminController : Controller
    {
        //
        // GET: /Admin/

        /// <summary>
        /// Log On
        /// </summary>
        /// <returns></returns>

        [Authorize]
        public ActionResult Dashboard()
        {



            return View();

        }

        public ActionResult BlankDashboard()
        {


            return View();

        }


        public ActionResult TestDash()
        {


            return View();

        }



    }
}
