using Rupor.Domain.Entities.RssAgregate;
using System;
using System.Collections.Generic;

namespace Rupor.Services.Core.Feed
{
    public interface IFeedChannelService
    {
        FeedChannelEntity this[int id] { get; }
        FeedChannelEntity this[string url] { get; }
        IEnumerable<FeedChannelEntity> Get(string category);        
        IEnumerable<FeedChannelEntity> Get(Func<FeedChannelEntity, bool> expr = null);
        FeedChannelEntity Add(FeedChannelEntity editedInstance);
        void Remove(int id);
    }
}
