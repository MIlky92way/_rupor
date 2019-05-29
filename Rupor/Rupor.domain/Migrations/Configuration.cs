using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Rupor.Domain.Context;
using Rupor.Domain.Entities.User;
using System.Data.Entity.Migrations;
using Rupor.Tools.Consts;
using Rupor.Domain.Entities.Resources;
using Rupor.Domain.Entities.Article;
using System.Collections.Generic;
using Rupor.Domain.Entities.Sys;
using System.Linq;

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
            AppArticleStatusInitial(context);
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

        private void AppArticleStatusInitial(RuporDbContext context)
        {
            var initialName = "ArticleStatusInitial";

            using (var transaction = context.Database.BeginTransaction())
            {
                InitialData initialArticleStatus = context
                    .InitialData
                    .FirstOrDefault(d => d.InitialName == initialName);
                if (initialArticleStatus?.Id > 0)
                    return;
                else
                    initialArticleStatus = new InitialData();


                    var section = new AppResourceSectionEntity
                {
                    Key = SectionResource.ArticleStatus,
                    Name = nameof(SectionResource.ArticleStatus),
                };

                context
                    .AppResousrceSection
                    .Add(section);

                context.SaveChanges();

                var statusNew = new AppResourceEntity
                {
                    SectionId = section.Id,
                    Key = (int)ArticleStatus.New,
                    Value = nameof(ArticleStatus.New)
                };

                var statusDraft = new AppResourceEntity
                {
                    SectionId = section.Id,
                    Key = (int)ArticleStatus.Draft,
                    Value = nameof(ArticleStatus.Draft)
                };

                var statusCanceled = new AppResourceEntity
                {
                    SectionId = section.Id,
                    Key = (int)ArticleStatus.Canceled,
                    Value = nameof(ArticleStatus.Canceled)
                };

                var statusModeration = new AppResourceEntity
                {
                    SectionId = section.Id,
                    Key = (int)ArticleStatus.Moderation,
                    Value = nameof(ArticleStatus.Moderation)
                };

                var statusApproved = new AppResourceEntity
                {
                    SectionId = section.Id,
                    Key = (int)ArticleStatus.Approved,
                    Value = nameof(ArticleStatus.Approved)
                };

                var statusArchive = new AppResourceEntity
                {
                    SectionId = section.Id,
                    Key = (int)ArticleStatus.Archive,
                    Value = nameof(ArticleStatus.Archive)
                };

                var statusPublicated = new AppResourceEntity
                {
                    SectionId = section.Id,
                    Key = (int)ArticleStatus.Publicated,
                    Value = nameof(ArticleStatus.Publicated)
                };

                context.AppResource.AddRange(new List<AppResourceEntity>() {
                    statusNew,
                    statusDraft,
                    statusCanceled,
                    statusModeration,
                    statusApproved,
                    statusArchive,
                    statusPublicated
                });

                initialArticleStatus.DateInitial = DateTime.Now;
                initialArticleStatus.InitialName = initialName;

                context.InitialData.Add(initialArticleStatus);

                context.SaveChanges();

                transaction.Commit();
            }

            
        }
    }
}
