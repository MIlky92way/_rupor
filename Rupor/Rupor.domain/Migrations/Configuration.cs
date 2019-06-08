using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Rupor.Domain.Context;
using Rupor.Domain.Entities.Article;
using Rupor.Domain.Entities.File;
using Rupor.Domain.Entities.Resources;
using Rupor.Domain.Entities.Section;
using Rupor.Domain.Entities.Sys;
using Rupor.Domain.Entities.User;
using Rupor.Domain.Resources;
using Rupor.Tools.Consts;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Drawing;
using System.IO;
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
            AppInitialSectionSettings(context);
            AppInitialSection(context);
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
            ProfileEntity profile;
            UserEntity user;
            string name = "rupor.adm@rupor.com";
            try
            {
                profile = new ProfileEntity();
                user = new UserEntity();
                user.UserName = user.Email = name;
                var res = manager.Create(user, "_Xz.23Az4kf-vdf-343fX+-__1");

                if (res.Succeeded)
                {
                    manager.AddToRole(user.Id, Role._ROOT);

                    profile.Email = user.Email;
                    profile.OwnerId = user.Id;
                    profile.IsActive = true;
                    profile.LastAuth = DateTime.Now;
                    context.UserProfile.Add(profile);
                    context.Entry(profile).State = System.Data.Entity.EntityState.Added;
                    context.SaveChanges();

                }
            }
            catch (Exception ex)
            {

            }
        }

        private void AppArticleStatusInitial(RuporDbContext context)
        {
            var initialName = "ArticleStatusInitial";

            InitialData initialArticleStatus = context
                .InitialData
                .FirstOrDefault(d => d.InitialName == initialName);
            if (initialArticleStatus?.Id > 0)
                return;
            else
                initialArticleStatus = new InitialData();


            var section = new AppResourceSectionEntity();

            section.Key = SectionResource.ArticleStatus;
            section.Name = nameof(SectionResource.ArticleStatus);

            context
                .Entry
                (section).State = System.Data.Entity.EntityState.Added;

            var statusNew = new AppResourceEntity()
            {
                SectionId = section.Id,
                Key = (int)ArticleStatus.New,
                Value = nameof(ArticleStatus.New)
            };

            var statusDraft = new AppResourceEntity()
            {
                SectionId = section.Id,
                Key = (int)ArticleStatus.Draft,
                Value = nameof(ArticleStatus.Draft)
            };

            var statusCanceled = new AppResourceEntity()
            {
                SectionId = section.Id,
                Key = (int)ArticleStatus.Canceled,
                Value = nameof(ArticleStatus.Canceled)
            };

            var statusModeration = new AppResourceEntity()
            {
                SectionId = section.Id,
                Key = (int)ArticleStatus.Moderation,
                Value = nameof(ArticleStatus.Moderation)
            };

            var statusApproved = new AppResourceEntity()
            {
                SectionId = section.Id,
                Key = (int)ArticleStatus.Approved,
                Value = nameof(ArticleStatus.Approved)
            };

            var statusArchive = new AppResourceEntity()
            {
                SectionId = section.Id,
                Key = (int)ArticleStatus.Archive,
                Value = nameof(ArticleStatus.Archive)
            };

            var statusPublicated = new AppResourceEntity()
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

        }

        private void AppInitialSectionSettings(RuporDbContext context)
        {
            var initialName = "AppInitialSectionSettings";


            var initialData = context.InitialData.FirstOrDefault(i => i.InitialName == initialName);
            if (initialData?.Id > 0)
                return;

            var sectionSettings = new SectionSettingsEntity();

            sectionSettings.MaxAllowedSections = 25;
            sectionSettings.MaxAllowedSectionsOnTop = 10;

            context.SectionSettings.Add(sectionSettings);

            initialData = new InitialData { InitialName = initialName, DateInitial = DateTime.Now };

            context.InitialData.Add(initialData);

            context.SaveChanges();

        }

        private void AppInitialSection(RuporDbContext context)
        {
            var initialName = "AppInitialSection";


            var initialData = context.InitialData.FirstOrDefault(i => i.InitialName == initialName);

            if (initialData?.Id > 0)
                return;

            var sectionSocial = new SectionEntity
            {
                IsActive = true,
                IsDefault = true,
                Name = "Общество",
                OnAside = true,
            };

            var sectionIncident = new SectionEntity
            {
                IsActive = true,
                IsDefault = true,
                Name = "Проишествия",
                OnAside = true,
            };

            var sectionSport = new SectionEntity
            {
                IsActive = true,
                IsDefault = true,
                Name = "Спорт",
                OnAside = true,
            };

            var sectionCulture = new SectionEntity
            {
                IsActive = true,
                IsDefault = true,
                Name = "Культура",
                OnAside = true,
            };

            var sectionPolitics = new SectionEntity
            {
                IsActive = true,
                IsDefault = true,
                Name = "Политика",
                OnAside = true,
            };

            var sectionEconomic = new SectionEntity
            {
                IsActive = true,
                IsDefault = true,
                Name = "Экономика",
                OnAside = true,
            };

            context.Sections.AddRange(new List<SectionEntity>(){
                sectionSocial,
                sectionIncident,
                sectionSport,
                sectionCulture,
                sectionPolitics,
                sectionEconomic,
            });

            initialData = new InitialData { InitialName = initialName, DateInitial = DateTime.Now };

            context.InitialData.Add(initialData);

            context.SaveChanges();

        }

        private void AppInitialProfileSettings(RuporDbContext context)
        {

        }


        private int SaveFile(FileArea area)
        {
            Bitmap img = null;             
            switch (area)
            {
                case FileArea.Profile:
                    img = InitResource.super_mario;
                    break;                
                case FileArea.Section:                                    
                default:
                    img = InitResource.default_section;
                    break;
            }
            //using (img)
            //{
            //    DirectoryInfo dirInfo = new DirectoryInfo("")
            //    img.Save()
            //}
            return 0;
        }
    }
}
