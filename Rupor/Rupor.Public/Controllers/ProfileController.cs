using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rupor.Domain.Entities.User;
using Rupor.Logik.Profile;
using Rupor.Public.Models.Profile;

namespace Rupor.Public.Controllers
{
    [Authorize]
    public class ProfileController : BaseController
    {
        // GET: Profile
        public ActionResult Index()
        {
            return View();
        }


        public PartialViewResult UserInfo()
        {
            return PartialView();
        }

        [AllowAnonymous]
        public PartialViewResult AuthUserWidget()
        {
            var model = new AuthUserWidgetModel(User.Identity.IsAuthenticated,CurrentUser);

            return PartialView(model);
        }
    }
}