using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FullStackTest.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult About()
        {
            ViewBag.Message = "This application utilizes AJAX in order to update the database and DOM in real-time.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "I'm sure my address is found somewhere else on the internet.";

            return View();
        }
    }
}