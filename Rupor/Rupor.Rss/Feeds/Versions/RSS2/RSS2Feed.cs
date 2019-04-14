using System.Xml.Linq;

namespace Rupor.Feed.Feeds.Versions
{
    public class RSS2Feed : BaseFeed
    {
        public string Description { get; }
        public string ManagingEditor { get; set; }

        public RSS2Feed()
        {

        }
        public RSS2Feed(XElement channel) : base(channel)
        {
            //TODO RSS2Feed setting props
        }
    }
}
