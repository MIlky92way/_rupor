using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using Rupor.Auth.Manager;
using Rupor.Domain.Entities.User;

[assembly: OwinStartup(typeof(Rupor.Public.Startup))]

namespace Rupor.Public
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext<AppUserManager>(AppUserManager.GetInstance);
            app.CreatePerOwinContext<AppRoleManager>(AppRoleManager.Create);
            app.CreatePerOwinContext<AppSignInManager>(AppSignInManager.CreateSignInManager);

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                CookieName = "rc",
                LoginPath = new PathString("/Account/Login"),
                ExpireTimeSpan = new TimeSpan(8, 0, 0),
                Provider = new CookieAuthenticationProvider
                {
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<AppUserManager, UserEntity>
                (
                    validateInterval: TimeSpan.FromMinutes(35),
                    regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager)
                )
                }
            });
        }
    }
}
