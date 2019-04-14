using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Rupor.Feed.Feeds;

namespace Rupor.Feed.Core.Parsers
{
    public abstract class XmlParser<TFeed>
    where  TFeed: BaseFeed,new()
    {
        public abstract TFeed Parse(XDocument xDoc);
    }
}
