using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Rupor.Domain.Context;
using Rupor.Domain.Entities.User;
using System.Data.Entity.Migrations;
using Rupor.Tools.Consts;

namespace Rupor.Domain.Migrations
{
    public class Configuration : DbMigrationsConfiguration<RuporDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(RuporDbContext context)
        {
            AppRoleInitial(context);
            AppUserInit(context);
            base.Seed(context);
        }


        private void AppRoleInitial(RuporDbContext context)
        {
            RoleManager<RoleEntity> manager =
                new RoleManager<RoleEntity>(new RoleStore<RoleEntity>(context));
            RoleEntity role;

            try
            {
                role = new RoleEntity();
                role.Name = Role._ROOT;
                manager.Create(role);
            }
            catch (Exception ex) { }

            try
            {
                role = new RoleEntity();
                role.Name = Role._ADMINISTRATOR;
                manager.Create(role);
            }
            catch (Exception ex) { }

            try
            {
                role = new RoleEntity();
                role.Name = Role._RUBRICATOR;
                manager.Create(role);
            }
            catch (Exception ex) { }

            try
            {
                role = new RoleEntity();
                role.Name = Role._USER;
                manager.Create(role);
            }
            catch (Exception ex) { }
        }


        private void AppUserInit(RuporDbContext context)
        {
            UserManager<UserEntity> manager =
                new UserManager<UserEntity>(new UserStore<UserEntity>(context));

            UserEntity user;
            string name = "rupor.adm@rupor.com";
            try
            {
                user = new UserEntity();
                user.UserName = user.Email = name;
                var res = manager.Create(user, "_Xz.23Az4kf-vdf-343fX+-__1");
                if (res.Succeeded)
                {
                    manager.AddToRole(user.Id, Role._ROOT);
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
