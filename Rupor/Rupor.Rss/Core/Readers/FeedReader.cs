using System;
using Rupor.Feed.Core.Parsers;
using Rupor.Feed.Core.Parsers.Versions;
using Rupor.Feed.Core.Types;
using Rupor.Feed.Feeds;
using Rupor.Feed.Feeds.Versions;
using Rupor.Feed.Feeds.Versions.RSS2;
using Rupor.Feed.Utility;

namespace Rupor.Feed.Core.Readers
{
    public class FeedReader:IFeedReader<RSS2Feed>
    {      
        public RSS2Feed ReadFeed(string url)
        {
            var xmlContent = UrlUtility.HttpGetString(url);
            var docInitializer = new DocInitializer();
            var xDoc = docInitializer.Initial(xmlContent);
            var parser = GetParser(docInitializer.FeedType);
            return parser.Parse(xDoc);            
        }

        private XmlParser<RSS2Feed> GetParser(FeedType feedType)
        {
            RSS2Parser parser = null;
            switch (feedType)
            {
                case FeedType.Atom:
                    //TODO instance Atom Parser
                    throw new NotImplementedException("Atom parser not implement");                    
                case FeedType.Default:
                default:
                    parser = new RSS2Parser();
                    break;
            }

            return parser;
        }
    }
}
