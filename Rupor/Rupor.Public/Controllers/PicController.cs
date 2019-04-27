using Rupor.Domain.Entities.File;
using System.Web.Mvc;

namespace Rupor.Public.Controllers
{
    public class PicController : BaseController
    {
        // GET: Pic
        public ActionResult Index()
        {
            return View();
        }

        //Pic/GetDefaultProfileImage
        public FileStreamResult GetProfileImage(int id = 0, bool isDefault = false)
        {
            if (isDefault)
                return ImageTools.GetDefaultImage(FileArea.Profile);
            else if (id > 0)
                return ImageTools.GetDefaultImage(FileArea.Profile, id);

            return ImageTools.GetDefaultImage(FileArea.Profile);
        }

        public FileStreamResult GetSectionImage(int id = 0, bool isDefault = false)
        {
            if (isDefault)
                return ImageTools.GetDefaultImage(FileArea.Section, id);
            else if (id > 0)
                return ImageTools.GetImage(FileArea.Section, id);

            return ImageTools.GetDefaultImage(FileArea.Section);
        }

        public FileStreamResult GetFeedChannelImage(int id = 0)
        {
            return ImageTools.GetImage(FileArea.FeedChannel, id);
        }
    }
}