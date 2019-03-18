using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Rupor.Domain.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rupor.Auth.Manager
{
    public class AppRoleManager: RoleManager<IdentityRole>
    {
        public AppRoleManager(RoleStore<IdentityRole> store) : base(store)
        {

        }

        public static AppRoleManager Create(IdentityFactoryOptions<AppRoleManager> options, IOwinContext context)
        {
            var dbContext = new RuporDbContext();
            var roleManager =
                new AppRoleManager(new RoleStore<IdentityRole>(dbContext));

            return roleManager;
        }
    }
}
