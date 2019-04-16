using Rupor.Tools.Extend;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Rupor.Feed.Feeds.Versions.RSS2
{
    public class RSS2FeedItem : FeedItem
    {
        public string Guid { get; }
        public string Author { get; }
        private string pubDate;
        public DateTime? PublicationDate { get; }
        public string Description { get; }
        public string PictureUrl { get; }

        public RSS2FeedItem(XElement xElement) : base(xElement)
        {
            //TODO RSS2FeedItem setting props

            PictureUrl = xElement.GetValue("enclosure");
            Description = xElement.GetValue("description");
            Author = xElement.GetValue("author");
            pubDate = xElement.GetValue("pubDate");
            Guid = xElement.GetValue("guid");

            var categories = xElement.GetElements("category");
            foreach (var item in categories ?? new List<XElement>())
            {
                Categories.Add(item.Value);
            }
        }
    }
}
