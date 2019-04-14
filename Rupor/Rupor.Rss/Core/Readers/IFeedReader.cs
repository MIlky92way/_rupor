using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rupor.Feed.Feeds;

namespace Rupor.Feed.Core.Readers
{
    interface IFeedReader<TFeed>
    where TFeed:BaseFeed,new()
    {
        TFeed ReadFeed(string url);
    }
}
