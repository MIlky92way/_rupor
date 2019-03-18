using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Rupor.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Rupor.Auth.Manager
{
    public class AppSignInManager: SignInManager<UserEntity, string>
    {
        public AppSignInManager(AppUserManager userManager, IAuthenticationManager authManager) : base(userManager, authManager)
        {

        }
        public ClaimsIdentity CreateUserIdentity(UserEntity user)
        {
            return user.GenerateUserIdentityAsync((AppUserManager)UserManager).Result;
        }

        public static AppSignInManager CreateSignInManager(IdentityFactoryOptions<AppSignInManager> options, IOwinContext context)
        {
            return new AppSignInManager(context.GetUserManager<AppUserManager>(), context.Authentication);
        }
    }
}
