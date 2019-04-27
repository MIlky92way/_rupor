using Rupor.Public.Models.Category;
using Rupor.Services.Core.Common;
using System.Web.Mvc;

namespace Rupor.Public.Controllers
{
    public class CategoryController : BaseController
    {
        private readonly IServiceCore ruporService;
        public CategoryController(IServiceCore ruporService)
        {
            this.ruporService = ruporService;
        }
        // GET: Section
        public ActionResult Index(int id = 0)
        {                       
            return View(new CategoryIndexViewModel(ruporService, id));
        }
    }
}