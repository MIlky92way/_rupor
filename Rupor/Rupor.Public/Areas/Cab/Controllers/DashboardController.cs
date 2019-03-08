using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Rupor.Public.Areas.Cab.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Cab/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}