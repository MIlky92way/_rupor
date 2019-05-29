using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Rupor.Auth.Manager;
using Rupor.Domain.Entities.User;
using Rupor.Logik.File;
using Rupor.Logik.Profile;
using Rupor.Public.Infrastructure.FileTools;
using Rupor.Public.Infrastructure.ProfileTools;
using Rupor.Public.Models.User;
using Rupor.Services.Core.File;
using Rupor.Services.Core.Profile;
using System.Web;
using System.Web.Mvc;

namespace Rupor.Public.Controllers
{
    public class BaseController : Controller
    {
        public const string ErrorMessage = "Error";
        public const string Success = "Success";

        protected IUserProfileService<ProfileEntity> ProfileService { get; private set; }
        protected IFileService FileService { get; }
        protected ImageTools ImageTools { get; }

        protected BaseController()
        {
            ProfileService = new UserProfileService();
            FileService = new FileService();
            ImageTools = new ImageTools();
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            CurrentUser = InitialCurrentUser();

            base.OnActionExecuting(filterContext);
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

        protected ProfileWeb CurrentUser { get; set; }

        protected ProfileWeb InitialCurrentUser(UserEntity current = null)
        {
            ProfileWeb profileWeb = null;
            UserEntity identityUser = null;
            if (!User.Identity.IsAuthenticated)
            {
                return new ProfileWeb();
            }

            RuporUser model = new RuporUser();
            string userEmail = User.Identity.Name;

            if (current != null)
                identityUser = current;
            else
                identityUser = UserManager.FindByName(userEmail);

            profileWeb = new ProfileWeb(ProfileService, identityUser, ImageTools);

            return profileWeb;
        }
    }
}