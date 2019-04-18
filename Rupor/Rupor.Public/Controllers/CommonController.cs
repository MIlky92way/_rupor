﻿using Rupor.Feed.Core.Readers;
using System.Web.Mvc;

namespace Rupor.Public.Controllers
{
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
                feed.Link
            };
            return Json(channelInfo, JsonRequestBehavior.AllowGet);
        }
    }
}