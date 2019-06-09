using Rupor.Domain.Entities.Article;
using Rupor.Domain.Entities.User;
using Rupor.Public.Models.Profile;
using Rupor.Services.Core.Common;
using System.Web.Mvc;

namespace Rupor.Public.Controllers
{
    [Authorize]
    public class ProfileController : BaseController
    {
        private IServiceCore service;
        public ProfileController(IServiceCore service)
        {
            this.service = service;
        }
        // GET: Profile
        public ActionResult Index(int id = 0)
        {
            var model = new ProfileIndexViewModel(service, CurrentUser, id);
            return View(model);
        }

        public ActionResult Info()
        {
            var model = new InfoViewModel(service, CurrentUser);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Info(InfoViewModel model)
        {
            if (ModelState.IsValid)
            {
                var dataModel = new Domain.Entities.User.ProfileEntity
                {
                    Id = model.Id,
                    BirthDate = model.BirthDate,
                    GivenName = model.GivenName,
                    FamilyName = model.FamilyName,
                    SocialFb = model.SocialLinkFb,
                    SocialInstagramm = model.SocialLinkInst,
                    SocialTwitter = model.SocialLinkTwitter,
                    SocialVk = model.SocialLinkVk,
                    OriginalPictureId = model.OriginalPictureId,
                    MinPictureId = model.MinPictureId,
                };

                dataModel = service.ProfileService.Edit(dataModel);

                if (dataModel != null && dataModel.Id > 0)
                {
                    return RedirectToAction("Info");
                }
            }

            return View(model);
        }


        public ActionResult FeedSettings(int? id)
        {
            return View(new FeedSettingsModel(id.GetValueOrDefault(), CurrentUser.Id));
        }

        public PartialViewResult SubscribeSection(int? settingsId)
        {
            //TODO отобразить все разделы, после выбора раздела происходит POST - раздел становится избранным
            SubscribeSectionModel model = null;

            if (settingsId.HasValue)
            {
                model = new SubscribeSectionModel(settingsId, service, CurrentUser);
            }
            else
                model = new SubscribeSectionModel(CurrentUser.Id);

            return PartialView(model);
        }

        public ActionResult Posts(int? profileId, ArticleStatus status = ArticleStatus.Approved)
        {
            ProfileEntity viewedProfile = null;

            if (profileId > 0)
            {
                viewedProfile = service.ProfileService[profileId.Value];
            }

            return View(new PostsViewModel(service.ArticleService, CurrentUser, viewedProfile, status));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePost()
        {
            var articleId = service
                .ArticleService
                .Create(CurrentUser.Id)?.Id ?? 0;

            return RedirectToAction("Post", new { id = articleId });
        }

        public ActionResult Post(int id)
        {
            var article = service.ArticleService[id];

            return View(new PostModel(article, service.ArticleService));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Post(PostModel model)
        {
            return View(model);
        }

        public ViewResult Miniature()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SubscribeSection(SubscribeSectionModel model)
        {
            if (model.Id == 0)
                model.Id = service
                    .ProfileSettings
                    .Create(model.ProfileId)?.Id ?? 0;

            if (model.Id > 0)
                service
                    .ProfileSettings
                    .SubscribeOnSections(model.ProfileId, model.SelectedSectionIds, model.Id);
            else
                TempData["error"] = "Не удалось добавить избранные разделы!";

            return RedirectToAction("FeedSettings", new { id = model.Id });
        }

        [HttpPost]
        public ActionResult UnsubscribeSection(SubscribeSectionModel model)
        {

            if (model.Id > 0)
                service
                    .ProfileSettings
                    .UnSubscribeOnSections(model.Id, model.ProfileId, model.SelectedSectionIds);
            else
                TempData["error"] = "Не удалось открепить избранные разделы!";

            return RedirectToAction("FeedSettings", new { id = model.Id });
        }

        [HttpPost]
        public ActionResult SavePicture()
        {
            var file = Request.Files[0];

            var originalPictureId = ImageTools.SaveImage(file, Domain.Entities.File.FileArea.Profile);

            service.ProfileService.UpdateOriginalPicture(CurrentUser.Id, originalPictureId);

            return Json(new { originalPictureId }, JsonRequestBehavior.DenyGet);
        }

        public PartialViewResult UserInfo(int userId = 0)
        {
            var model = new UserInfoViewModel(service, CurrentUser, userId);
            return PartialView(model);
        }

        [AllowAnonymous]
        public PartialViewResult AuthUserWidget()
        {
            var model = new AuthUserWidgetModel(User.Identity.IsAuthenticated, CurrentUser);

            return PartialView(model);
        }

    }
}