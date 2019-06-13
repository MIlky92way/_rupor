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
            FileStreamResult result = null;
            if (isDefault)
                result = ImageTools.GetDefaultImage(FileArea.Profile);
            else if (id > 0)
            {
                result = ImageTools.GetDefaultImage(FileArea.Profile, id) ??
                   ImageTools.GetDefaultImage(FileArea.Profile);
            }
            else
            {
                result = ImageTools.GetDefaultImage(FileArea.Profile);
            }
            return result;
        }

        public FileStreamResult GetSectionImage(int id = 0, bool isDefault = false)
        {
            FileStreamResult result = null;
            if (isDefault)
                result = ImageTools.GetDefaultImage(FileArea.Section);
            else if (id > 0)
            {
                result = ImageTools.GetDefaultImage(FileArea.Section, id) ??
                   ImageTools.GetDefaultImage(FileArea.Section);
            }
            else
            {
                result = ImageTools.GetDefaultImage(FileArea.Section);
            }
            return result;
        }

        public FileStreamResult GetArticleImage(int id = 0, bool isDefault = false)
        {
            FileStreamResult result = null;
            if (isDefault)
                result = ImageTools.GetDefaultImage(FileArea.Article);
            else if (id > 0)
            {
                result = ImageTools.GetDefaultImage(FileArea.Article, id) ??
                   ImageTools.GetDefaultImage(FileArea.Article);
            }
            return result;
        }

        public FileStreamResult GetFeedChannelImage(int id = 0)
        {
            return ImageTools.GetImage(FileArea.FeedChannel, id);
        }
    }
}