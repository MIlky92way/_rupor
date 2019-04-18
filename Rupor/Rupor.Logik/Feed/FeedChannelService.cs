using Rupor.Domain.Context;
using Rupor.Domain.Entities.RssAgregate;
using Rupor.Logik.Base;
using Rupor.Services.Core.Feed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
namespace Rupor.Logik.Feed
{
    public class FeedChannelService: IFeedChannelService
    {
        
        public FeedChannelEntity this[int id]
        {
            get
            {
                FeedChannelEntity result = null;
                if (id > 0)
                {
                    using (var context = new RuporDbContext())
                    {
                        result = context.FeedChannel
                            .FirstOrDefault(x => x.Id == id);
                    }
                }
                return result;
            }
        }
        public FeedChannelEntity this[string url]
        {
            get
            {
                FeedChannelEntity result = null;
                if (!string.IsNullOrEmpty(url))
                {
                    using (var context = new RuporDbContext())
                    {
                        result = context.FeedChannel
                            .FirstOrDefault(x => x.TargetUrl.ToLower().Contains(url.ToLower()));
                    }
                }

                return result;
            }
        }


        public FeedChannelEntity Add(FeedChannelEntity editedInstance)
        {
            FeedChannelEntity result = null;
            if (editedInstance != null)
            {
                using (var context = new RuporDbContext())
                {
                    result = new FeedChannelEntity();
                    result.Name = editedInstance.Name;
                    result.TargetUrl = editedInstance.TargetUrl;
                    result.ChannelPictureId = editedInstance.ChannelPictureId;
                    result.Categories = editedInstance.Categories;
                    result.Description = editedInstance.Description;
                    result = context.FeedChannel.Add(result);
                }
            }
            return result;
        }
        
        public IEnumerable<FeedChannelEntity> Get(Func<FeedChannelEntity, bool> expr = null)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            if (id > 0)
            {
                using (var context = new RuporDbContext())
                {
                    var result = context.FeedChannel
                          .FirstOrDefault(x => x.Id == id);
                    context.FeedChannel.Remove(result);
                }
            }
        }

        public IEnumerable<FeedChannelEntity> Get(string category)
        {
            IEnumerable<FeedChannelEntity> result = null;
            using (var context = new RuporDbContext())
            {
                result = context
                    .FeedChannel
                    .Include(ch => ch.Categories)
                    .Where(ch => ch.Categories.Any(c => c.Name.ToLower().Contains(category.ToLower())))
                    .ToList();
            }

            return result;
        }
    }
}
