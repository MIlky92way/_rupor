using Rupor.Public.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Win32;
using Rupor.Domain.Entities.File;
using Rupor.Tools.Consts;
using Rupor.Public.Areas.Cab.Models;
using Rupor.Public.Infrastructure.FileTools.Additional;
using Rupor.Services.Core.Section;
using Rupor.Services.Core.Section.Models;
using SectionSettingArea = Rupor.Services.Core.Section.Models.SectionSettingArea;

namespace Rupor.Public.Areas.Cab.Controllers
{
    [Authorize]
    public class DashboardController : BaseController
    {
        public ISectionSettingsService SectionSettingsService;
        public DashboardController(ISectionSettingsService sectionSettingsService)
        {
            SectionSettingsService = sectionSettingsService;
        }
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = Role._ROOT)]
        public ActionResult AppSettings()
        {
            return View();
        }

        [Authorize(Roles = Role._ROOT)]
        public ActionResult ProfileSettings()
        {
            ProfileSettingsModel model = new ProfileSettingsModel();

            var result = FileService.Get(x =>
                    x.IsDefault && x.FileArea == FileArea.Profile && x.FileType == FileType.Image)
                .FirstOrDefault();

            model.IsEmptyDefaultPicture = result == null;

            return View(model);
        }

        [Authorize(Roles = Role._ROOT)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProfileSettings(ProfileSettingsModel model)
        {
            try
            {
                switch (model.FileType)
                {
                    case FileType.Document:
                        break;
                    case FileType.Image:
                        ImageTools.SaveImage(model.File, model.FileArea, isDefault: true);
                        break;
                    case FileType.Archive:
                        break;
                    default:
                        break;
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", "Во время сохранения изображения произошли ошибки");
            }

            return View(model);
        }

        [Authorize(Roles = Role._ROOT)]
        public ActionResult SectionSettings()
        {
            SectionsSettingsModel model = new SectionsSettingsModel();

            var result = FileService.Get(x =>
                x.IsDefault && x.FileArea == FileArea.Section && x.FileType == FileType.Image)
                .FirstOrDefault();

            model.IsEmptyDefaultPicture = result == null;

            var settings = SectionSettingsService.Settings;

            if (settings != null)
            {
                model.MaxAllowedSections = settings.MaxAllowedSections;
                model.DefaultPictureId = settings.DefaultPictureId.HasValue ? settings.DefaultPictureId.Value : 0;
            }

            return View(model);
        }

        [Authorize(Roles = Role._ROOT)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SectionSettings(SectionsSettingsModel model)
        {
            var serviceModel = new SectionSettingsModel();

            serviceModel.SettingsArea = SectionSettingArea.Settings;
            serviceModel.MaxAllowedSections = model.MaxAllowedSections;

            try
            {
                SectionSettingsService.RemoveDefaultSettings();

                if (model.SectionImage!=null && model.SectionImage.ContentLength > 0)
                {
                    serviceModel.DefaultPictureId = ImageTools
                        .SaveImage(model.SectionImage, model.FileArea, isDefault: true);
                }
                else
                {
                    serviceModel.DefaultPictureId = model.DefaultPictureId;
                }                
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", "Во  время сохранения изображения произошли ошибки");
            }
            try
            {
                SectionSettingsService.Edit(serviceModel);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Во  время сохранения произошли ошибки");
            }

            return View(model);
        }

        [Authorize(Roles = Role._ROOT)]
        public ActionResult DefaultSection()
        {
            DefaultSectionModel model = new DefaultSectionModel();

            return View(model);
        }

        [Authorize(Roles = Role._ROOT)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DefaultSection(DefaultSectionModel model)
        {
            var serviceModel = new SectionSettingsModel();

            serviceModel.Name = model.Name;
            serviceModel.OnTop = model.OnTop;

            try
            {
                SectionSettingsService.Edit(serviceModel);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", "Во  время сохранения произошли ошибки");
            }

            return View(model);
        }

        [Authorize(Roles = Role._ROOT)]
        public ActionResult ArticleSettings()
        {
            return View();
        }



        [Authorize(Roles = Role._ROOT)]
        public ActionResult RssSettings()
        {
            return PartialView();
        }

        [Authorize(Roles = Role._ROOT)]
        public ActionResult MaterialSettings()
        {
            return PartialView();
        }



    }
}