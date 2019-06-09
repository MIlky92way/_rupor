using Rupor.Public.Models.Component;
using Rupor.Services.Core.Common;
using System.Web.Mvc;

namespace Rupor.Public.Controllers
{
    public class ComponentController : BaseController
    {
        private readonly IServiceCore ruporService;
        public ComponentController(IServiceCore ruporService)
        {
            this.ruporService = ruporService;
        }
        // GET: Category
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult HeaderCategories()
        {
            return PartialView(new CategoryViewModel(ruporService.SectionService));
        }

        public PartialViewResult SideCategories()
        {
            return PartialView(new CategoryViewModel(ruporService.SectionService));
        }
    }
}