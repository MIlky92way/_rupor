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

namespace Rupor.Public.Areas.Cab.Controllers
{
    [Authorize]
    public class DashboardController : BaseController
    {

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
                x.IsDefault && x.FileArea == FileArea.Profile && x.FileType == FileType.Image);

            model.IsEmptyDefaultImage = result == null;

            return View(model);
        }

        [Authorize(Roles = Role._ROOT)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProfileSettings(ProfileSettingsModel model)
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
            return View(model);
        }

        [Authorize(Roles = Role._ROOT)]
        public ActionResult SectionSettings()
        {
            SectionsSettingsModel model = new SectionsSettingsModel();
            
            return View(model);
        }

        [Authorize(Roles = Role._ROOT)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SectionSettings(SectionsSettingsModel model)
        {            
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