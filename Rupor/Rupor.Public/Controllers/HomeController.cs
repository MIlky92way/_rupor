using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rupor.Domain.Entities.User;
using Rupor.Logik.Profile;

namespace Rupor.Public.Controllers
{
    public class HomeController : BaseController
    {
                
        // GET: Home
        public ActionResult Index()
        {
            ViewBag.Title = CurrentUser.IsGuest ? CurrentUser.GivenName : CurrentUser.ToString();
            return View();
        }
    }
}