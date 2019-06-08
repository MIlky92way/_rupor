using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Rupor.Domain.Context;
using Rupor.Domain.Entities.Article;
using Rupor.Domain.Entities.File;
using Rupor.Domain.Entities.Resources;
using Rupor.Domain.Entities.Section;
using Rupor.Domain.Entities.Sys;
using Rupor.Domain.Entities.User;
using Rupor.Tools.Consts;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
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
            AppInitialProfileDefaultAvatar(context);
            AppInitialSectionDefaultImage(context);
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

                    profile.OriginalPictureId = context
                        .Files
                        .FirstOrDefault(x => x.IsDefault && x.FileArea == FileArea.Profile)?.Id ?? null;

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

            sectionSettings.DefaultPictureId = context
                .Files
                .FirstOrDefault(x => x.IsDefault && x.FileArea == FileArea.Section)?.Id ?? null;

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

        private void AppInitialProfileDefaultAvatar(RuporDbContext context)
        {
            var initialName = "AppInitialProfileDefaultAvatar";

            var initialData = context.InitialData.FirstOrDefault(i => i.InitialName == initialName);

            if (initialData?.Id > 0)
                return;

            var fileId = SaveFile(context, FileArea.Profile, FileType.Image);

            if (fileId > 0)
            {
                initialData = new InitialData { InitialName = initialName, DateInitial = DateTime.Now };

                context.InitialData.Add(initialData);

                context.SaveChanges();
            }
        }

        private void AppInitialSectionDefaultImage(RuporDbContext context)
        {
            var initialName = "AppInitialSectionDefaultImage";

            var initialData = context.InitialData.FirstOrDefault(i => i.InitialName == initialName);

            if (initialData?.Id > 0)
                return;

            var fileId = SaveFile(context, FileArea.Section, FileType.Image);

            if (fileId > 0)
            {
                initialData = new InitialData { InitialName = initialName, DateInitial = DateTime.Now };

                context.InitialData.Add(initialData);

                context.SaveChanges();
            }
        }


        private int SaveFile(RuporDbContext context, FileArea area, FileType fileType)
        {
            string fullPath = null;
            string path = AppFilePath.DefaultPathImage;
            string ext = "";
            string fileName = "";
            string contentType = "";
            FileEntity file = new FileEntity();

            switch (area)
            {
                case FileArea.Profile:
                    fullPath = $"{AppFilePath.DefaultPathImage}{AppFilePath.DefautFileAvatar}";
                    ext = Path.GetExtension(AppFilePath.DefautFileAvatar);
                    fileName = Path.GetFileName(AppFilePath.DefautFileAvatar)?.Split('.')[0];
                    break;
                case FileArea.Section:
                default:
                    fullPath = $"{AppFilePath.DefaultPathImage}{AppFilePath.DefautFileSection}";
                    ext = Path.GetExtension(AppFilePath.DefautFileSection);
                    fileName = Path.GetFileName(AppFilePath.DefautFileSection)?.Split('.')[0];
                    break;
            }

            switch (fileType)
            {
                case FileType.Document:
                    setContentType(ext);
                    break;
                case FileType.Image:
                    setContentType(ext);
                    break;
                case FileType.Archive:
                    setContentType(ext);
                    break;
                default:
                    break;
            }

            void setContentType(string extension)
            {
                switch (extension)
                {
                    case ".zip":
                        contentType = "application/zip";
                        break;
                    case ".pdf":
                        contentType = "application/pdf";
                        break;
                    case ".png":
                        contentType = "image/png";
                        break;
                    case ".gif":
                        contentType = "image/gif";
                        break;
                    case ".jpg":
                    default:
                        contentType = "image/jpg";
                        break;
                }
            }

            file.Alt = fileName;
            file.FileArea = area;
            file.FileExtension = ext;
            file.IsDefault = true;
            file.FileName = fileName;
            file.FullPath = $"{path}/{file.FileName}{file.FileExtension}";
            file.Name = $"{file.FileName}{file.FileExtension}";
            file.FileType = fileType;
            file.ContentType = contentType;
            file = context.Files.Add(file);
            
            context.SaveChanges();

            return file.Id;
        }
    }
}
