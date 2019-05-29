using Rupor.Domain.Entities.File;
using Rupor.Public.Infrastructure.FileTools;
using Rupor.Public.Infrastructure.FileTools.Additional;
using Rupor.Public.Models.UserPhoto;
using Rupor.Services.Core.Common;
using System.Web;
using System.Web.Mvc;

namespace Rupor.Public.Controllers
{
    [Authorize]
    public class UserPhotoController : BaseController
    {
        private IServiceCore service;
        public UserPhotoController(IServiceCore service)
        {
            this.service = service;
        }

        [AllowAnonymous]
        // GET: UserPhoto
        public FileStreamResult GetMinPicture(int? userId)
        {
            FileStreamResult result = null;
            string path = string.Empty;
            if (userId.HasValue)
            {
                var user = ProfileService[userId.Value];

                if (user != null && user.MinPicture != null)
                {
                    result = ImageTools.GetImage(FileArea.Profile, user.MinPicture.Id);
                }
                else
                {
                    result = ImageTools.GetDefaultImage(FileArea.Profile);
                }
            }
            else if (CurrentUser != null)
            {
                result = CurrentUser.GetMinImage();
            }

            return result;
        }
        [AllowAnonymous]
        public FileStreamResult GetOriginalPicture(int userId = 0)
        {
            FileStreamResult result = null;

            return result;
        }

        public ActionResult SavePhoto(HttpPostedFileBase file)
        {
            ImageTools.SaveImage(file, FileArea.Profile);

            return View();
        }

        public PartialViewResult CropPhoto()
        {
            var model = new CropModel(CurrentUser);
            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult CropPhoto(CropModel model)
        {
            var cropData =
                new ImageSaveData { Height = model.Height, Width = model.Width, X = model.X, Y = model.Y };

            var stream = ImageTools.GetImage(FileArea.Profile, model.OrigPicId)?.FileStream;

            var minPictureId = ImageTools.SaveImageFromStream(stream, model.OrigPicId, FileArea.Profile, crop: true, saveData: cropData);

            service.ProfileService.UpdateMiniaturePicture(CurrentUser.Id, minPictureId);

            return Json(new { minPictureId }, JsonRequestBehavior.DenyGet);
        }

        public ActionResult DeletePhoto()
        {
            return View();
        }

    }
}