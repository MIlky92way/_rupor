using Rupor.Domain.Entities.File;
using Rupor.Domain.Entities.Section;
using Rupor.Public.Areas.Cab.Models.Section;
using Rupor.Public.Controllers;
using Rupor.Public.Infrastructure.Attributes;
using Rupor.Public.Models.Additional;
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

        public SectionController(ISectionService sectionService)
        {
            this.sectionService = sectionService;
        }


        // GET: Cab/Section
        public ActionResult Index(SectionViewModel model)
        {
            return View(model);
        }

        public ActionResult Edit(int id = 0)
        {
            SectionEditModel model = null;
            if (id > 0)
            {
                var entry = sectionService[id];
                model = new SectionEditModel(entry);
            }
            else
                model = new SectionEditModel();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SectionEditModel model, string action = nameof(ExperienceForSubmit.Save))
        {
            SectionEntity res = null;
            if (ModelState.IsValid)
            {   
                if (model.SectionImage  !=null && model.SectionImage.ContentLength > 0)
                {
                    model.ImageId = ImageTools.SaveImage(model.SectionImage, FileArea.Section);
                }   
                res = sectionService.Edit(model.MapFrom(model));
            }
                
            if (res != null && res.Id > 0)
                if (action == nameof(ExperienceForSubmit.Apply))
                    return RedirectToAction("Edit", new { id = res.Id });

            ModelState.AddModelError("", "Во время сохранения произошли ошибки");

            return View(model);
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

    }
}