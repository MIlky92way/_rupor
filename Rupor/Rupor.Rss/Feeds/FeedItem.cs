using System.Collections.Generic;
using System.Xml.Linq;
using Rupor.Tools.Extend;

namespace Rupor.Feed.Feeds
{
    public class FeedItem
    {
        public string Title { get; private set; }
        public string Link { get; private set; }
        public ICollection<string> Categories { get; }

        public FeedItem()
        {
            Categories = new List<string>();
        }

        protected FeedItem(XElement channel):this()
        {
            Title = channel.GetValue("title");
            Link = channel.GetValue("link");
        }
    }
}
