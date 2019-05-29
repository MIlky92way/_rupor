using Rupor.Domain.Entities.File;
using Rupor.Domain.Entities.Section;
using Rupor.Protected.UserExtend;
using Rupor.Public.Areas.Cab.Models.Section;
using Rupor.Public.Controllers;
using Rupor.Public.Infrastructure.Attributes;
using Rupor.Public.Models.Additional;
using Rupor.Services.Core.Common;
using Rupor.Services.Core.Section;
using Rupor.Tools.Consts;
using System.Linq;
using System.Web.Mvc;

namespace Rupor.Public.Areas.Cab.Controllers
{
    [CustomAuth(roles: new[] { Role._ROOT, Role._ADMINISTRATOR })]
    public class SectionController : BaseController
    {
        private readonly ISectionService sectionService;
        private readonly IServiceCore service;
        public SectionController(IServiceCore service, ISectionService sectionService)
        {
            this.service = service;
            this.sectionService = sectionService;
        }


        // GET: Cab/Section
        public ActionResult Index(SectionViewModel model)
        {
            return View(model);
        }

        public ActionResult Edit(int id = 0)
        {
            if (!CurrentUser.IdentityUser.CanEditSections())
            {
                return RedirectToAction("Index");
            }

            var model = new SectionEditModel(service, id);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SectionEditModel model, string action = nameof(ExperienceForSubmit.Save))
        {
            SectionEntity res = null;


            if (ModelState.IsValid)
            {
                if (model.SectionImage != null && model.SectionImage.ContentLength > 0)
                {
                    model.ImageId = ImageTools.SaveImage(model.SectionImage, FileArea.Section);
                }

                if (model.Id == 0 && model.OnTop && model.OverflowOnTop)
                {
                    ModelState.AddModelError("", "Кол-во разделов в шапке привысило допустимый предел.");

                    return View(model);
                }
                res = sectionService.Edit(model.MapFrom(model));
            }

            if (res != null && res.Id > 0)
                if (action == nameof(ExperienceForSubmit.Apply))
                    return RedirectToAction("Edit", new { id = res.Id });
                else
                    return RedirectToAction("Index");


            ModelState.AddModelError("", "Во время сохранения произошли ошибки");

            return View(model);
        }

        [HttpPost]
        public ActionResult ChangeState(int id, bool? isActive = null, bool? isDelete = null)
        {
            if (id > 0)
            {
                if (isActive != null)
                {
                    sectionService.ChangeActiveState(id, isActive);
                }

                if (isDelete != null)
                {
                    sectionService.ChangeDeleteState(id, isDelete);
                }
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            if (id > 0)
            {
                sectionService.Remove(id);
            }

            return RedirectToAction("Index");
        }

        public JsonResult GetSections(SectionViewModel model)
        {
            var sections = sectionService.Get(model)
               .Select(s => new
               {
                   s.Id,
                   s.Name,
                   DateCreate = s.DateCreate.ToShortDateString(),
                   s.IsActive,
                   s.IsDelete
               });

            return Json(new { data = sections, model.draw, recordsTotal = model.Total, recordsFiltered = model.Total }, JsonRequestBehavior.AllowGet);
        }



        public PartialViewResult GridSections(SectionViewModel model)
        {
            var sections = sectionService.Get(model)
                .Select(s => new SectionDto
                {
                    Id = s.Id,
                    DateCreate = s.DateCreate,
                    OnTop = s.OnTop,
                    IsActive = s.IsActive,
                    IsDelete = s.IsDelete,
                    Name = s.Name
                });
            return PartialView(sections);
        }
    }
}