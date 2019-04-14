using System.Xml.Linq;
using Rupor.Tools.Extend;

namespace Rupor.Feed.Feeds
{
    public class FeedItem
    {
        public string Title { get; private set; }
        public string Link { get; private set; }

        protected FeedItem(XElement channel)
        {
            Title = channel.GetValue("title");
            Link = channel.GetValue("link");
        }
    }
}
