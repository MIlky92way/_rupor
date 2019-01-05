using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Rupor.domain.Context;
using Rupor.Domain.Entities.User;
using System.Data.Entity.Migrations;


namespace Rupor.domain.Migrations
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
                role.Name = "Administrator";
                manager.Create(role);
            }
            catch (Exception ex) { }

            try
            {
                role = new RoleEntity();
                role.Name = "Editor";
                manager.Create(role);
            }
            catch (Exception ex) { }

            try
            {
                role = new RoleEntity();
                role.Name = "Moderator";
                manager.Create(role);
            }
            catch (Exception ex) { }

            try
            {
                role = new RoleEntity();
                role.Name = "User";
                manager.Create(role);
            }
            catch (Exception ex) { }
        }


        private void AppUserInit(RuporDbContext context)
        {
            UserManager<UserEntity> manager =
                new UserManager<UserEntity>(new UserStore<UserEntity>(context));

            UserEntity user;
            string name = "rupor.adm@rupor.pw";
            try
            {
                user = new UserEntity();
                user.UserName = user.Email = name;
                var res = manager.Create(user, "_qazwsxedc123!_@");
                if (res.Succeeded)
                {
                    manager.AddToRole(user.Id, "Administrator");
                }
            }
            catch (Exception ex)
            { }
        }

    }
}
