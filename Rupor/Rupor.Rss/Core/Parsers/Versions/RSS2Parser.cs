using Rupor.Feed.Feeds;
using System;
using System.Xml.Linq;
using Rupor.Feed.Feeds.Versions;
using Rupor.Tools.Extend;
using Rupor.Feed.Feeds.Versions.RSS2;

namespace Rupor.Feed.Core.Parsers.Versions
{
    internal class RSS2Parser : XmlParser<RSS2Feed>
    {
        
        public override RSS2Feed Parse(XDocument xDoc)
        {
            var channel = xDoc.Root.GetElement("channel");
            RSS2Feed feed = new RSS2Feed(channel);                        
            return feed;
        }
    }
}
