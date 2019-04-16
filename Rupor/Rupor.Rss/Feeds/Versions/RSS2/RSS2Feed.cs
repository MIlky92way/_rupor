using Rupor.Tools.Extend;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Rupor.Feed.Feeds.Versions.RSS2
{
    public class RSS2Feed : BaseFeed
    {
        public string Description { get; }
        public string ManagingEditor { get; set; }
        public string ImageUrl { get; set; }
        public string Language { get; set; }
        public string Generator { get; set; }
        public ICollection<RSS2FeedItem> Items { get; set; }
        public RSS2Feed()
        {
            Items = new List<RSS2FeedItem>();
        }
        public RSS2Feed(XElement channel) : base(channel)
        {
            Items = new List<RSS2FeedItem>();

            ImageUrl = channel.GetElement("image").GetValue("url");
            Language = channel.GetValue("language");
            Generator = channel.GetValue("generator");
            var items = channel.GetElements("item");

            foreach (var item in items ?? new List<XElement>())
            {
                Items.Add(new RSS2FeedItem(item));
            }
            
        }
    }
}
