﻿using Rupor.Public.Infrastructure.Attributes;
using Rupor.Tools.Consts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Rupor.Public.Areas.Cab.Controllers
{
    [CustomAuth(roles: new[] { Role._ADMINISTRATOR, Role._ROOT })]
    public class ProfileDashboardController : Controller
    {
        // GET: Cab/ProfileDashboard
        public ActionResult Index()
        {
            return View();
        }
    }
}