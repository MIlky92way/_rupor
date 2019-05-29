using Rupor.Public.Areas.Cab.Models.ResSection;
using Rupor.Public.Controllers;
using Rupor.Public.Infrastructure.Attributes;
using Rupor.Public.Models.Additional;
using Rupor.Services.Core.Common;
using Rupor.Tools.Consts;
using System.Web.Mvc;

namespace Rupor.Public.Areas.Cab.Controllers
{
    [CustomAuth(roles: new[] { Role._ADMINISTRATOR, Role._ROOT })]
    public class ResSectionController : BaseController
    {
        private readonly IServiceCore service;
        public ResSectionController(IServiceCore service)
        {
            this.service = service;
        }
        // GET: Cab/ResSection
        public ActionResult Index(ResSectionIndexViewModel model)
        {
            return View(model);
        }

        public ActionResult Edit(int id = 0)
        {
            ResSectionEditViewModel model = null;

            if (id > 0)
            {
                var resSection = service.AppResourceSectionService.Find(id);
                if (resSection == null)
                {
                    TempData[ErrorMessage] = true;
                    return View();
                }
                model = new ResSectionEditViewModel(resSection);
            }
            else
                model = new ResSectionEditViewModel();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ResSectionEditViewModel model, string action = nameof(ExperienceForSubmit.Save))
        {
            if (ModelState.IsValid)
            {
                var result = service
                    .AppResourceSectionService
                    .Edit(model);

                if (result != null && result.Id > 0)
                {
                    return RedirectToAction("Edit", new { id = model.Id });
                }
            }

            return View(model);
        }

        public PartialViewResult GetResSections(ResSectionIndexViewModel model)
        {
            model.Init(service);

            return PartialView(model.ResSections);
        }
    }
}