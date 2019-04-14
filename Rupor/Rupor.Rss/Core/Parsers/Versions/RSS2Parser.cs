using Rupor.Feed.Feeds;
using System;
using System.Xml.Linq;
using Rupor.Feed.Feeds.Versions;

namespace Rupor.Feed.Core.Parsers.Versions
{
    internal class RSS2Parser : XmlParser<RSS2Feed>
    {
        
        public override RSS2Feed Parse(XDocument xDoc)
        {
            RSS2Feed feed = new RSS2Feed(xDoc);
                        
            return feed;
        }
    }
}
