using Microsoft.AspNet.Identity.Owin;
using Rupor.Auth.Manager;
using Rupor.Domain.Entities.User;
using Rupor.Logik.Profile;
using Rupor.Public.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Rupor.Logik.File;

namespace Rupor.Public.Controllers
{
    public class BaseController : Controller
    {
        
        
        protected IUserProfileService<ProfileEntity> ProfileService { get; private set; }
        
        protected BaseController()
        {
            ProfileService = new UserProfileService();
        }
        

        protected AppUserManager UserManager
        {
            get
            {
                var userManager = HttpContext.GetOwinContext().GetUserManager<AppUserManager>();

                return userManager;
            }
        }

        protected AppRoleManager RoleManager
        {
            get
            {
                return HttpContext.GetOwinContext().Get<AppRoleManager>();
            }
        }

        protected ProfileEntity CurrentUser => InitialCurrentUser();

        private ProfileEntity InitialCurrentUser()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return null;
            }

            RuporUser model = new RuporUser();
            string userEmail = User.Identity.Name;
            return ProfileService[userEmail];
        }
    }
}