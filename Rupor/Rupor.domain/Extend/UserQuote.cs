using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Rupor.Domain.Context;
using Rupor.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rupor.Domain.Extend
{
    public static class UserQuote
    {
        public static bool CanEditSections(this UserEntity current)
        {
            RoleManager<RoleEntity> roleManager = 
                new RoleManager<RoleEntity>(new RoleStore<RoleEntity>(new RuporDbContext()));

            var roles = new string[] { "ROOT", "ADMINISTRATOR" };
           var roleIds = roleManager.Roles
                .Where(r => roles.Any(rn => rn.Contains(r.Name)))
                .Select(r => r.Id);

            return current.Roles.Any(r => roleIds.Any(id => r.RoleId == id));
        }
    }
}
