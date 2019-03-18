using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.DataProtection;
using Rupor.Domain.Context;
using Rupor.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rupor.Auth.Manager
{
    public class AppUserManager: UserManager<UserEntity>
    {
        public AppUserManager(IUserStore<UserEntity> store) : base(store)
        {

        }

        public static AppUserManager GetInstance(IdentityFactoryOptions<AppUserManager> option, IOwinContext owinContext)
        {

            var dbContext = new RuporDbContext();
            var usermgr = new AppUserManager(new UserStore<UserEntity>(dbContext));

            usermgr.PasswordValidator = new PasswordValidator
            {
                RequireDigit = true,
                RequiredLength = 8,
                RequireUppercase = true,
                RequireLowercase = true,
            };

            usermgr.UserValidator = new UserValidator<UserEntity>(usermgr)
            {
                RequireUniqueEmail = true,
                AllowOnlyAlphanumericUserNames = true
            };
            var tokenProvider = new DpapiDataProtectionProvider("therupor");
            usermgr.UserTokenProvider = new DataProtectorTokenProvider<UserEntity>(tokenProvider.Create("EmailConfirmation"));
        
            return usermgr;
        }
    }
}
