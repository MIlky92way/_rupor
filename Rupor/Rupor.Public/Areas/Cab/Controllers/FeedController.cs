using Rupor.Public.Areas.Cab.Models.Feed;
using Rupor.Public.Controllers;
using Rupor.Public.Infrastructure.Attributes;
using Rupor.Public.Models.Additional;
using Rupor.Services.Core.Feed;
using Rupor.Tools.Consts;
using System.Web.Mvc;

namespace Rupor.Public.Areas.Cab.Controllers
{
    [CustomAuth(roles: new[] { Role._ROOT, Role._ADMINISTRATOR })]
    public class FeedController : BaseController
    {
        private readonly IFeedChannelService FeedChannelService;
        public FeedController(IFeedChannelService feedChannelService)
        {
            FeedChannelService = feedChannelService;
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            FeedEditViewModel model = new FeedEditViewModel();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FeedEditViewModel model, string action = nameof(ExperienceForSubmit.Save))
        {
            var entity = model.MapFrom();

            if (ModelState.IsValid)
            {
                entity = FeedChannelService.Add(entity);
                if (entity != null && entity.Id > 0)
                {
                    TempData["SuccessSave"] = "Канал успешно добавлен";
                }
            }
            
            return RedirectToAction("Index");
        }

        public JsonResult Get()
        {
            return Json(null, JsonRequestBehavior.AllowGet);
        }

    }
}