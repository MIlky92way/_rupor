using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Rupor.Public.Areas.Cab.Controllers
{
    [Authorize]
    public class ProfileDashboardController : Controller
    {
        // GET: Cab/ProfileDashboard
        public ActionResult Index()
        {
            return View();
        }
    }
}