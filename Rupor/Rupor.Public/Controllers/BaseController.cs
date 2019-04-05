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
using Rupor.Public.Infrastructure.ProfileTools;
using Rupor.Public.Infrastructure.FileTools;
using Rupor.Services.Core.Profile;
using Rupor.Services.Core.File;

namespace Rupor.Public.Controllers
{
    public class BaseController : Controller
    {
        
        
        protected IUserProfileService<ProfileEntity> ProfileService { get; private set; }
        protected IFileService FileService { get; }
        protected ImageTools ImageTools { get; }
        
        protected BaseController()
        {
            ProfileService = new UserProfileService();
            FileService = new FileService();
            ImageTools = new ImageTools();
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

        protected ProfileWeb CurrentUser => InitialCurrentUser();

        private ProfileWeb InitialCurrentUser()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return null;
            }

            RuporUser model = new RuporUser();
            string userEmail = User.Identity.Name;
            var profile = ProfileService[userEmail];
           
            ProfileWeb profileWeb = new ProfileWeb(profile, ImageTools);

            return profileWeb;
        }
    }
}