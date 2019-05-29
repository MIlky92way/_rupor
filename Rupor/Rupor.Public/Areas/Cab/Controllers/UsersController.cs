using Rupor.Public.Controllers;
using Rupor.Public.Infrastructure.Attributes;
using Rupor.Tools.Consts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Rupor.Public.Areas.Cab.Controllers
{
    [CustomAuth(roles: new[] { Role._ROOT, Role._ADMINISTRATOR })]
    public class UsersController : BaseController
    {
        // GET: Cab/Users
        public ActionResult Index()
        {

            return View();
        }
    }
}