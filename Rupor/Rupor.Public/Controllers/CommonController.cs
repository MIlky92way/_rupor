using Rupor.Feed.Core.Readers;
using System.Linq;
using System.Web.Mvc;

namespace Rupor.Public.Controllers
{
    /// <summary>
    /// Хранит общие доп. методы (поучение информации о канале, сохранение изорбажений статьи)
    /// </summary>
    public class CommonController : BaseController
    {
        /// <summary>
        /// Получает информацию о источнике новостей
        /// </summary>
        /// <param name="url">адрес</param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetFeedChannel(string url)
        {
            var reader = new FeedReader();
            var feed = reader.ReadFeed(url);

            var channelInfo = new
            {
                feed.Description,
                feed.ImageUrl,
                feed.Title,
                feed.Link,
                Categories =feed.Items?
                .SelectMany(f=>f.Categories)
                .Distinct()
                .ToArray()
            };
            return Json(channelInfo, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveArticleImage()
        {
            return Json(null, JsonRequestBehavior.DenyGet);
        }
        
    }
}