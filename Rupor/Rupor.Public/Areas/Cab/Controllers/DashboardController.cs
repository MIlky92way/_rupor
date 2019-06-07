using Rupor.Domain.Entities.File;
using Rupor.Public.Areas.Cab.Models;
using Rupor.Public.Controllers;
using Rupor.Public.Infrastructure.Attributes;
using Rupor.Public.Models.Additional;
using Rupor.Services.Core.Section;
using Rupor.Services.Core.Section.Models;
using Rupor.Tools.Consts;
using System;
using System.Linq;
using System.Web.Mvc;
using SectionSettingArea = Rupor.Services.Core.Section.Models.SectionSettingArea;

namespace Rupor.Public.Areas.Cab.Controllers
{
    [CustomAuth(roles: new[] { Role._ROOT })]
    public class DashboardController : BaseController
    {
        public ISectionSettingsService SectionSettingsService;
        public DashboardController(ISectionSettingsService sectionSettingsService)
        {
            SectionSettingsService = sectionSettingsService;
        }
        [Authorize(Roles = Role._ROOT)]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = Role._ROOT)]
        public ActionResult AppSettings()
        {
            return View();
        }

        #region  profile

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

        #endregion

        #region section settings

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
                model.MaxAllowedSectionsOnTop = settings.MaxAllowedSectionsOnTop;
                model.DefaultPictureId = settings.DefaultPictureId.HasValue ? settings.DefaultPictureId.Value : 0;

                model.Sections = SectionSettingsService
                    .DefaultSections
                    .Select(s => new DefaultSectionModel()
                    {
                        Id = s.Id,
                        Name = s.Name,
                        OnTop = s.OnAside,
                        IsActive = s.IsActive
                    });
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
            serviceModel.MaxAllowedSectionsOnTop = model.MaxAllowedSectionsOnTop;

            try
            {
                SectionSettingsService.RemoveDefaultSettings();

                if (model.SectionImage != null && model.SectionImage.ContentLength > 0)
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

            return RedirectToAction("SectionSettings");
        }

        [Authorize(Roles = Role._ROOT)]
        public ActionResult DefaultSection(int id = 0)
        {
            DefaultSectionModel model = new DefaultSectionModel();
            var settings = SectionSettingsService.Settings;
            var countDefauleSections = SectionSettingsService.DefaultSections.Count();
            model.IsEmptySetting = settings == null;
            model.MaxAllowedSections = settings.MaxAllowedSections;
            model.MaxAllowedSectionsOnTop = settings.MaxAllowedSectionsOnTop;
            model.OverflowSections = countDefauleSections > model.MaxAllowedSections;

            if (id > 0)
            {
                var section = SectionSettingsService.GetDefaultSection(id);
                model.Name = section.Name;
                model.IsActive = section.IsActive;
                model.OnTop = section.OnAside;
                model.Id = section.Id;
            }

            model.Change = TempData["change"] != null ? (bool)TempData["change"] : false;

            return View(model);
        }

        [Authorize(Roles = Role._ROOT)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DefaultSection(DefaultSectionModel model, string action)
        {
            var serviceModel = new SectionSettingsModel();
            serviceModel.SettingsArea = SectionSettingArea.DefaultSections;
            serviceModel.Name = model.Name;
            serviceModel.OnTop = model.OnTop;
            serviceModel.IsActive = model.IsActive;
            serviceModel.SectionId = model.Id;
            serviceModel.MaxAllowedSections = model.MaxAllowedSections;
            TempData["change"] = true;
            try
            {
                SectionSettingsService.Edit(serviceModel);
            }
            catch (Exception e)
            {
                model.SuccessCnages = false;
            }

            if (action == nameof(ExperienceForSubmit.Save))
            {
                return RedirectToAction("SectionSettings");
            }

            return RedirectToAction("DefaultSection");

        }

        [Authorize(Roles = Role._ROOT)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RemoveDefaultSection(int id = 0)
        {
            if (id > 0)
            {
                SectionSettingsService.RemoveDefaultSection(id);
            }

            return RedirectToAction("SectionSettings");
        }

        #endregion


        #region article


        [Authorize(Roles = Role._ROOT)]
        public ActionResult ArticleSettings()
        {

            return View();
        }


        #endregion

        [Authorize(Roles = Role._ROOT)]
        public ActionResult RssSettings()
        {
            return PartialView();
        }


    }
}